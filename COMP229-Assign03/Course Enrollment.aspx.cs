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
            studentList.Items.Add(new ListItem("--- Select Student's First Name---", ""));
            studentList.AppendDataBoundItems = true;
            // See how we can use a using statement rather than try-catch (this will close and dispose the connection similarly to a finally block
            using (thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand("Select  st.*, e.StudentID, e.Grade, e.CourseID, c.CourseID, " +
                    "c.Title from Students st" +
                    " join Enrollments e on st.StudentID = e.StudentID " +
                    "join Courses c on e.CourseID = c.CourseID " +
                    "where c.CourseID = @courseID;", thisConnection);
                comm.Parameters.AddWithValue("@courseID", Int32.Parse(Session["courseID"].ToString()));
                SqlCommand commFirstMidNameAdd = new SqlCommand("Select st.FirstMidName, st.StudentID, e.StudentID, " +
                    "e.Grade, e.CourseID, e.EnrollmentID, c.CourseID, c.Title from Students st" +
                    " inner join Enrollments e on st.StudentID = e.StudentID " +
                    "inner join Courses c on e.CourseID = c.CourseID " +
                    "where not c.CourseID = @courseID;", thisConnection);
                commFirstMidNameAdd.Parameters.AddWithValue("@courseID", Int32.Parse(Session["courseID"].ToString()));
                try
                {
                    thisConnection.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    StudentInfo.DataSource = reader;
                    StudentInfo.DataBind();
                    reader.Close();
                    SqlDataReader readerAdd = commFirstMidNameAdd.ExecuteReader();
                    studentList.DataSource = readerAdd;
                    studentList.DataTextField = "FirstMidName";
                    studentList.DataValueField = "StudentID";
                    studentList.DataBind();

                    readerAdd.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    thisConnection.Close();
                }
            }
        }
        protected void Name_SelectedIndexChanged(object obj, EventArgs e)
        {
            // See how we can use a using statement rather than try-catch (this will close and dispose the connection similarly to a finally block
            using (thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                try
                {
                    thisConnection.Open();
                    SqlCommand commLastNameAdd = new SqlCommand("Select st.LastName, st.StudentID, st.FirstMidName from Students st " +
                       "where st.FirstMidName = @FirstMidName;", thisConnection);
                    Session["tempFirstMidName"] = studentList.SelectedValue;
                    commLastNameAdd.Parameters.AddWithValue("@FirstMidName", Session["tempFirstMidName"].ToString());
                    SqlDataReader readerLastNameAdd = commLastNameAdd.ExecuteReader();
                    while (readerLastNameAdd.Read())
                    {
                        txtLastNameAdd.Text = readerLastNameAdd["LastName"].ToString();
                        Session["tempStudentID"] = readerLastNameAdd["StudentID"];
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    thisConnection.Close();
                }
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                //SqlCommand comm2 = new SqlCommand("INSERT INTO Students (LastName, FirstMidName, StudentID) values (@stID, @lname,@fmname, @enrDate);", thisConnection);
                //SqlCommand comm1 = new SqlCommand("INSERT INTO Enrollments (EnrollmentID, StudentID, CourseID) values (@enrID,@stID,@crID) ;", thisConnection);
                SqlCommand comm = new SqlCommand();
                comm.Connection = thisConnection;
                comm.CommandTimeout = 0;
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "InsertStudent";

                comm.Parameters.AddWithValue("@grade", Int32.Parse(txtGrade.Text));
                comm.Parameters.AddWithValue("@studentID", Int32.Parse(Session["tempStudentID"].ToString()));
                comm.Parameters.AddWithValue("@courseID", Int32.Parse(Session["courseID"].ToString()));
                comm.Parameters.AddWithValue("@fmname", Session["tempFirstMidName"].ToString());
                comm.Parameters.AddWithValue("@lname", txtLastNameAdd.Text);
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
                }
            }
        }

        protected void StudentInfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = thisConnection;
                comm.CommandTimeout = 0;
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "DeleteStudent";
                               
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
