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
                SqlCommand comm = new SqlCommand("Select st.*, e.StudentID, e.Grade, e.CourseID, c.CourseID, c.Title from Students st" +
                    " join Enrollments e on st.StudentID = e.StudentID " +
                    "join Courses c on e.CourseID = c.CourseID " +
                    "where st.StudentID = '" + ssName + "';", thisConnection);
                thisConnection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                stCourse.Text = "";
                while (reader.Read())
                {
                    stName.Text = reader["FirstMidName"].ToString() + " " + reader["LastName"].ToString();
                    stID.Text = reader["StudentID"].ToString();
                    stDate.Text = reader["EnrollmentDate"].ToString();
                    stGrade.Text = reader["Grade"].ToString();
                    stCourse.Text += "<asp:LinkButton ID='Course' runat='server'" + count + " CommandName='MoreDetail' " +
                        "OnClick='Change' CommandArgument='" + reader["CourseID"] + "' Text='" + reader["Title"].ToString() + "'/><br/> ";

                    count++;
                }

                reader.Close();
                thisConnection.Close();
            }
        }
        protected void Change(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "MoreDetail")
                {
                    Session["courseID"] = e.CommandArgument.ToString();
                    Response.Redirect("Course Enrollment.aspx");
                }
                else if (e.CommandName == "Update")
                {
                    Response.Redirect("Update.aspx");
                }
                else if (e.CommandName == "Delete")
                {
                    // You can't delete a record with references in other tables, so delete those references first
                    SqlCommand deleteEnrollments = new SqlCommand("DELETE FROM Enrollments WHERE StudentID=@StudentID", connection);
                    SqlCommand deleteCourse = new SqlCommand("DELETE FROM Student WHERE StudentID=@StudentID", connection);

                    // Parameterize everything, even if the user isn't entering the values
                    deleteEnrollments.Parameters.AddWithValue("@StudentID", e.CommandArgument);
                    deleteCourse.Parameters.AddWithValue("@StudentID", e.CommandArgument);

                    connection.Open(); // open the cmd connection

                    // delete the references FIRST
                    deleteEnrollments.ExecuteNonQuery();
                    deleteCourse.ExecuteNonQuery();
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}