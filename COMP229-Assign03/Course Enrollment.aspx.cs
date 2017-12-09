using System;
using System.Collections.Generic;
using System.Data;
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
        private SqlConnection thisConnection;

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
            using (thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand("Select  st.*, e.StudentID, e.Grade, e.CourseID, c.CourseID, " +
                    "c.Title from Students st" +
                    " join Enrollments e on st.StudentID = e.StudentID " +
                    "join Courses c on e.CourseID = c.CourseID " +
                    "where c.CourseID = @courseID;", thisConnection);
                comm.Parameters.AddWithValue("@courseID", Int32.Parse(Session["courseID"].ToString()));
                SqlCommand commAdd = new SqlCommand("Select distinct st.StudentID, FirstMidName + ' ' + LastName as FullName" +
                    " from Students st " +
                    "WHERE st.StudentID NOT IN ( SELECT e.StudentID FROM Enrollments e " +
                    "where e.CourseID =  @courseID);", thisConnection);
                commAdd.Parameters.AddWithValue("@courseID", Int32.Parse(Session["courseID"].ToString()));
                try
                {
                    thisConnection.Open();

                    //display students in the course
                    SqlDataReader reader = comm.ExecuteReader();
                    StudentInfo.DataSource = reader;
                    StudentInfo.DataBind();
                    reader.Close();

                    //display students not in the course
                    studentList.DataSource = commAdd.ExecuteReader();
                    studentList.DataTextField = "FullName";
                    studentList.DataValueField = "StudentID";
                    studentList.DataBind();

                    studentList.Items.Insert(0, new ListItem("----- Select Student's Name -----", ""));
                }
                catch (Exception ex)
                {
                    dbErrorMessage.Text = ex.Message;
                }
                finally
                {
                    thisConnection.Close();
                }
            }
        }
        protected void Name_SelectedIndexChanged(object obj, EventArgs e)
        { }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand(" INSERT INTO [dbo].Enrollments(CourseID, StudentID, Grade)  " +
                "VALUES (@courseID,@studentID, @grade );         ", thisConnection);
                string name = studentList.SelectedItem.Text;
                string[] sep_name = name.Split();
                comm.Parameters.AddWithValue("@grade", Int32.Parse(txtGrade.Text));
                comm.Parameters.AddWithValue("@studentID", Int32.Parse(studentList.SelectedValue));
                comm.Parameters.AddWithValue("@courseID", Int32.Parse(Session["courseID"].ToString()));
                comm.Parameters.AddWithValue("@fmname", sep_name[0]);
                comm.Parameters.AddWithValue("@lname", sep_name[1]);
                comm.Parameters.AddWithValue("@newEnrollment", Convert.ToDateTime(txtStudentEnrollmentDate.Text));
                try
                {
                    thisConnection.Open();
                    comm.ExecuteNonQuery();
                    dbErrorMessage.Text = "Inserted new student!";
                }
                catch (SqlException error)
                {
                    dbErrorMessage.Text += error.Message;
                }
                finally
                {
                    thisConnection.Close();
                    GetStudents();
                }
            }
        }

        protected void StudentInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand("DELETE FROM Enrollments WHERE StudentID = @studentID and CourseID = @courseID;" , thisConnection);
                comm.Parameters.AddWithValue("@courseID", Int32.Parse(Session["courseID"].ToString()));
                comm.Parameters.AddWithValue("@studentID", Int32.Parse(StudentInfo.DataKeys[e.RowIndex].Values[0].ToString()));
                try
                {
                    thisConnection.Open();
                    comm.ExecuteNonQuery();
                }
                catch (SqlException error)
                {
                    dbErrorMessage.Text = error.Message;
                }
                finally
                {
                    thisConnection.Close();
                    GetStudents();
                }
            }
        }
        protected void timePicker_changed(object sender, EventArgs e)
        {
            DateTime dateOnly = CalendarDate.SelectedDate;
            txtStudentEnrollmentDate.Text = dateOnly.Date.ToString("MM/dd/yyyy");
        }
    }
}
