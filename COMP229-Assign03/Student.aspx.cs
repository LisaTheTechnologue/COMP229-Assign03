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
                string studentID = Session["currentStudentID"] as string;
                int sID;
                int.TryParse(studentID, out sID);

                //SqlCommand sqlcom = new SqlCommand("select st.* from Students st where st.StudentID = '" + ssName + "'", thisConnection);
                SqlCommand comm = new SqlCommand("Select * from Students " +
                    //"INNER JOIN Enrollments ON Students.StudentID = Enrollments.StudentID " +
                    //"INNER JOIN Courses ON Courses.CourseID = Enrollments.CourseID " +
                    "where Students.StudentID = @sID;", thisConnection);
                comm.Parameters.Add("@sID", System.Data.SqlDbType.Int);
                comm.Parameters["@sID"].Value = sID;
                try
                {
                    thisConnection.Open();
                    SqlDataReader reader = comm.ExecuteReader();

                    if (reader.Read())
                    {
                        //setName
                        stName.Text = reader["FirstMidName"] + " " + reader["LastName"];
                        //setID
                        stID.Text = reader["StudentID"] + "";
                        //setDate
                        stDate.Text = reader["EnrollmentDate"] + "";
                    }

                    reader.Close();
                    //setCourse
                    SqlCommand comm2 = new SqlCommand("SELECT * FROM dbo.Courses " +
                        "INNER JOIN dbo.Enrollments ON dbo.Courses.CourseID = dbo.Enrollments.CourseID " +
                        "WHERE dbo.Enrollments.StudentID = @sID;", thisConnection);
                    comm2.Parameters.Add("@sID", System.Data.SqlDbType.Int);
                    comm2.Parameters["@sID"].Value = sID;

                    reader = comm2.ExecuteReader();
                    listCr.DataSource = reader;
                    listCr.DataBind();

                    reader.Close();
                }
                catch
                {
                    Response.Write("Error retrieving user data");
                }
                finally
                {
                    thisConnection.Close();
                }
            }
        }
        protected void Change(object source, EventArgs e)
        {
            LinkButton btn = (LinkButton)(source);
            string value = btn.CommandName;
            try
            {
                if (value == "Update")
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

        protected void listCr_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "MoreDetail")
            {
                Session["courseID"] = e.CommandArgument.ToString();
                Response.Redirect("Course Enrollment.aspx");
            }
        }
        //protected void listCr_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    if (e.CommandName == "MoreDetail")
        //    {
        //        Session["courseID"] = e.CommandArgument.ToString();
        //        Response.Redirect("Course Enrollment.aspx");
        //    }
        //}
    }
}