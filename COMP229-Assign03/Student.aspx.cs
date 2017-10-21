using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP229_Assign03
{
    public partial class Student : Page
    {
        // Building the connection from a string; for an example using the ConnectionStrings in web.config, go to line 29
        private SqlConnection connection = new SqlConnection("Server=localhost;Initial Catalog=Comp229Assign03;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            // Only build the list on the initial arrival, not after button presses
            if (!IsPostBack)
            {
                GetClasses();
            }
        }

        private void GetClasses()
        {
            // See how we can use a using statement rather than try-catch (this will close and dispose the connection similarly to a finally block
            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand("Select * from Students;", thisConnection);
                thisConnection.Open();
                SqlDataReader reader = comm.ExecuteReader();

                listSt.DataSource = reader;
                listSt.DataBind();

                //while (reader.Read())
                //{
                //    Label myLabel = new Label();
                //    myLabel.Text = reader["Title"].ToString();
                //    myPlaceHolder.Controls.Add(myLabel);
                //    myPlaceHolder.Controls.Add(new LiteralControl("<br/>"));
                //}

                thisConnection.Close();
            }
        }

        protected void listSt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // try-finally to ensure that the connection is closed if there's an issue
            try
            {
                if (e.CommandName == "deleteCommand")
                {
                    // You can't delete a record with references in other tables, so delete those references first
                    SqlCommand deleteEnrollments = new SqlCommand("DELETE FROM Enrollments WHERE CourseID=@CourseID", connection);
                    SqlCommand deleteCourse = new SqlCommand("DELETE FROM Courses WHERE CourseID=@CourseID", connection);

                    // Parameterize everything, even if the user isn't entering the values
                    deleteEnrollments.Parameters.AddWithValue("@CourseID", e.CommandArgument);
                    deleteCourse.Parameters.AddWithValue("@CourseID", e.CommandArgument);

                    connection.Open(); // open the cmd connection

                    // delete the references FIRST
                    deleteEnrollments.ExecuteNonQuery();
                    deleteCourse.ExecuteNonQuery();
                }
                else if (e.CommandName == "updateCommand")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Courses SET Title=@UpdatedTitle WHERE Title=@Title", connection);
                    cmd.Parameters.AddWithValue("@Title", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@UpdatedTitle", e.CommandArgument + " - Updated");

                    connection.Open(); // open the cmd connection

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }

            // Re-bind the data with the changed database records
            GetClasses();
        }
    }
}