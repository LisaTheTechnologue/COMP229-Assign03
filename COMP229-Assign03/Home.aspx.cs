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
    public partial class _Default : Page
    {
        // Building the connection from a string; for an example using the ConnectionStrings in web.config, go to line 29
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
                SqlCommand comm = new SqlCommand("Select LastName, FirstMidName, StudentID from Students;", thisConnection);
                thisConnection.Open();
                SqlDataReader reader = comm.ExecuteReader();

                listSt.DataSource = reader;
                listSt.DataBind();

                reader.Close();
                thisConnection.Close();
            }
        }
        protected void listSt_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "MoreDetail")
            {
                Session["currentStudentID"] = e.CommandArgument.ToString();
                Response.Redirect("Student.aspx");
            }
        }

        protected void addStudent_Click(object sender, EventArgs e)
        {
            try
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
                    
                    //comm.Parameters.AddWithValue("@enrollmentID", Int32.Parse(insertEnrollmentID.Text));
                    comm.Parameters.AddWithValue("@grade", 0);
                    comm.Parameters.AddWithValue("@studentID", Int32.Parse(insertStudentID.Text));
                    comm.Parameters.AddWithValue("@courseID", Int32.Parse(insertCourseID.Text));
                    comm.Parameters.AddWithValue("@fmname", insertStudentFirstMidName.Text);
                    comm.Parameters.AddWithValue("@lname", insertStudentLastName.Text);
                    comm.Parameters.AddWithValue("@newEnrollment", Convert.ToDateTime(insertStudentEnrollmentDate.Text));
                    try
                    {
                        thisConnection.Open();
                        comm.ExecuteNonQuery();
                        errorMsg.Text = "Inserted new student!";
                    }
                    catch (SqlException error)
                    {
                        errorMsg.Text += error.Message;
                    }
                    finally
                    {
                        thisConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg.Text += ex.Message;
            }
        }

        protected void timePicker_changed(object sender, EventArgs e)
        {
            DateTime dateOnly = CalendarDate.SelectedDate;
            insertStudentEnrollmentDate.Text = dateOnly.Date.ToString("MM/dd/yyyy");
        }
    }
}