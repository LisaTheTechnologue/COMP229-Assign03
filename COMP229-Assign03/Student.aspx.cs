using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP229_Assign03
{
    public partial class Student : System.Web.UI.Page
    {
        // Building the connection from a string; for an example using the ConnectionStrings in web.config, go to line 29
        private SqlConnection connection = new SqlConnection("Server=localhost;Initial Catalog=Comp229Assign03;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetStInfo();
            }
        }
        private void GetStInfo()
        {
            // See how we can use a using statement rather than try-catch (this will close and dispose the connection similarly to a finally block
            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                int count = 1;
                string ssName = Session["studentID"].ToString();
                //SqlCommand sqlcom = new SqlCommand("select st.* from Students st where st.StudentID = '" + ssName + "'", thisConnection);
                SqlCommand comm = new SqlCommand("Select st.*, e.StudentID, e.Grade, e.CourseID,  from Students st" +
                    " join Enrollments e on st.StudentID = e.StudentID " +
                    "join Courses c on e.CourseID = c.CourseID " +
                    "where st.StudentID = '" + ssName + "';", thisConnection);
                thisConnection.Open();
                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    stName.Text = reader["FirstMidName"].ToString() + " " + reader["LastName"].ToString();
                    stID.Text = reader["StudentID"].ToString();
                    stDate.Text = reader["EnrollmentDate"].ToString();
                    
                }

                listCr.DataSource = reader;
                listCr.DataBind();

                reader.Close();
                thisConnection.Close();
            }
        }
        protected void Change(object source, EventArgs e)
        {
            LinkButton btn = (LinkButton)(source);
            string value = btn.CommandName;
            try
            {
                if (value == "MoreDetail")
                {
                    Session["courseID"] = btn.CommandArgument.ToString();
                    Response.Redirect("Course Enrollment.aspx");
                }
                else if (value == "Update")
                {
                    Response.Redirect("Update.aspx");
                }
                else if (value == "Delete")
                {
                    // You can't delete a record with references in other tables, so delete those references first
                    SqlCommand deleteEnrollments = new SqlCommand("DELETE FROM Enrollments WHERE StudentID=@StudentID", connection);
                    SqlCommand deleteStudent = new SqlCommand("DELETE FROM Students WHERE StudentID=@StudentID", connection);

                    // Parameterize everything, even if the user isn't entering the values
                    deleteEnrollments.Parameters.AddWithValue("@StudentID", btn.CommandArgument);
                    deleteStudent.Parameters.AddWithValue("@StudentID", btn.CommandArgument);

                    connection.Open(); // open the cmd connection

                    // delete the references FIRST
                    deleteEnrollments.ExecuteNonQuery();
                    deleteStudent.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}