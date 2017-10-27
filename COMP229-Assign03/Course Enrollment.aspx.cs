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
    public partial class Contact : Page
    {
        // Building the connection from a string; for an example using the ConnectionStrings in web.config, go to line 29
        private SqlConnection connection = new SqlConnection("Server=localhost;Initial Catalog=Comp229Assign03;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            // Only build the list on the initial arrival, not after button presses
            if (!IsPostBack)
            {
                GetStudents();
            }
        }

        private void GetStudents()
        {
            // See how we can use a using statement rather than try-catch (this will close and dispose the connection similarly to a finally block
            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand("Select  st.*, e.StudentID, e.Grade, e.CourseID, c.CourseID, c.Title from Students st" +
                    " join Enrollments e on st.StudentID = e.StudentID " +
                    "join Courses c on e.CourseID = c.CourseID " +
                    "where c.CourseID = " + Session["courseID"] + ";", thisConnection);

                SqlCommand commAdd = new SqlCommand("Select st.FirstMidName + ' ' + st.LastName as FullName, st.StudentID, e.StudentID, e.Grade, e.CourseID, c.CourseID, c.Title from Students st" +
                    " join Enrollments e on st.StudentID = e.StudentID " +
                    "join Courses c on e.CourseID = c.CourseID " +
                    "where not c.CourseID = " + Session["courseID"] + ";", thisConnection);


                //while (reader.Read())
                //{
                //    courseName.Text = reader["Title"].ToString();
                //}
                //reader.Close();
                try
                {
                    thisConnection.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    StudentInfo.DataSource = reader;
                    StudentInfo.DataBind();
                    reader.Close();
                    SqlDataReader readerAdd = commAdd.ExecuteReader();
                    studentList.DataSource = readerAdd;
                    studentList.DataTextField = "FullName";
                    studentList.DataValueField = "StudentID";
                    studentList.DataBind();
                    readerAdd.Close();
                }
                finally
                {                    
                    thisConnection.Close();
                }
            }
        }
        protected void listSt_ItemCommand(object source, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Add")
                {
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
                    {

                        SqlCommand commStringAdd = new SqlCommand("INSERT INTO Enrollments (CourseID, StudentID, Grade) " +
                            "VALUES(@CourseID, @StudentID, @Grade) ", conn);
                        commStringAdd.Parameters.Add("@CourseID", System.Data.SqlDbType.Int);
                        commStringAdd.Parameters["@CourseID"].Value = Session["CourseID"];
                        commStringAdd.Parameters.Add("@StudentID", System.Data.SqlDbType.Int);
                        //commStringAdd.Parameters["@StudentID"].Value = studentList.SelectedItem.Value;
                        commStringAdd.Parameters.Add("@Grade", System.Data.SqlDbType.Int);
                        commStringAdd.Parameters["@Grade"].Value = 0;

                        try
                        {
                            conn.Open();
                            commStringAdd.ExecuteNonQuery();
                        }
                        catch
                        {
                            dbErrorMessage.Text =
                            "Error submitting the Student request! Please " +
                            "try again later, and/or change the entered data!";
                        }
                        finally
                        {
                            conn.Close();

                        }
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE Courses SET Title=@UpdatedTitle WHERE Title=@Title", connection);
                    cmd.Parameters.AddWithValue("@Title", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@UpdatedTitle", e.CommandArgument + " - Updated");
                    connection.Open(); // open the cmd connection

                    cmd.ExecuteNonQuery();
                }

                else if (e.CommandName == "Delete")
                {
                    Button btn = (Button)(source);
                    
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
        //protected void Change(object source, EventArgs e)
        //{
        //    LinkButton btn = (LinkButton)(source);
        //    string value = btn.CommandName;
        //    try
        //    {
        //        if (value == "Delete")
        //        {
        //            // You can't delete a record with references in other tables, so delete those references first
        //            SqlCommand deleteEnrollments = new SqlCommand("DELETE FROM Enrollments WHERE StudentID=@StudentID", connection);
        //            SqlCommand deleteStudent = new SqlCommand("DELETE FROM Students WHERE StudentID=@StudentID", connection);

        //            // Parameterize everything, even if the user isn't entering the values
        //            deleteEnrollments.Parameters.AddWithValue("@StudentID", btn.CommandArgument);
        //            deleteStudent.Parameters.AddWithValue("@StudentID", btn.CommandArgument);

        //            connection.Open(); // open the cmd connection

        //            // delete the references FIRST
        //            deleteEnrollments.ExecuteNonQuery();
        //            deleteStudent.ExecuteNonQuery();
        //        }
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //}

        protected void StudentInfo_PageIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}