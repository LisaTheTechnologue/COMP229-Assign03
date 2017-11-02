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
            studentList.Items.Add(new ListItem("--- Select Student's First Name---", ""));
            studentList.AppendDataBoundItems = true;
            // See how we can use a using statement rather than try-catch (this will close and dispose the connection similarly to a finally block
            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand("Select  st.*, e.StudentID, e.Grade, e.CourseID, c.CourseID, c.Title from Students st" +
                    " join Enrollments e on st.StudentID = e.StudentID " +
                    "join Courses c on e.CourseID = c.CourseID " +
                    "where c.CourseID = @courseID;", thisConnection);
                comm.Parameters.AddWithValue("@courseID", Int32.Parse(Session["courseID"].ToString()));
                SqlCommand commFirstMidNameAdd = new SqlCommand("Select st.FirstMidName, st.StudentID, e.StudentID, e.Grade, e.CourseID, c.CourseID, c.Title from Students st" +
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
                    while (reader.Read())
                    {
                        Session["tempEnrollmentID"] = reader["EnrollmentID"];

                    }
                    reader.Close();
                    SqlDataReader readerAdd = commFirstMidNameAdd.ExecuteReader();
                    studentList.DataSource = readerAdd;
                    studentList.DataTextField = "st.FirstMidName";
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
        protected void Name__SelectedIndexChanged(object obj, EventArgs e)
        {
            // See how we can use a using statement rather than try-catch (this will close and dispose the connection similarly to a finally block
            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                try
                {
                    thisConnection.Open();
                    SqlCommand commLastNameAdd = new SqlCommand("Select st.LastName , st.StudentID from Students st " +
                        "where not st.StudentID = @stIDAdd;", thisConnection);
                    commLastNameAdd.Parameters.AddWithValue("@stIDAdd", studentList.SelectedValue);
                    SqlDataReader readerLastNameAdd = commLastNameAdd.ExecuteReader();
                    while (readerLastNameAdd.Read())
                    {
                        txtLastNameAdd.Text = readerLastNameAdd[0].ToString();
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
        protected void listSt_ItemCommand(object source, GridViewCommandEventArgs e)
        {
            //Casting Date variable
            string date = txtEnrDate.Text;
            // Specify exactly how to interpret the string.
            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);

            // Alternate choice: If the string has been input by an end user, you might 
            // want to format it according to the current culture:
            // IFormatProvider culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            DateTime newEnrollment = DateTime.Parse(date, culture, System.Globalization.DateTimeStyles.AssumeLocal);

            try
            {
                if (e.CommandName == "Delete")
                {
                    using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
                    {
                        //SqlCommand comm2 = new SqlCommand("INSERT INTO Students (LastName, FirstMidName, StudentID) values (@stID, @lname,@fmname, @enrDate);", thisConnection);
                        //SqlCommand comm1 = new SqlCommand("INSERT INTO Enrollments (EnrollmentID, StudentID, CourseID) values (@enrID,@stID,@crID) ;", thisConnection);
                        SqlCommand comm = new SqlCommand("MasterInsertUpdateDelete", thisConnection);
                        comm.CommandType = System.Data.CommandType.StoredProcedure;
                        comm.Parameters.AddWithValue("@StatementType", "delete");
                        comm.Parameters.AddWithValue("@enrollmentID", Int32.Parse(Session["tempEnrollmentID"].ToString()));
                        comm.Parameters.AddWithValue("@studentID", Int32.Parse(Session["tempStudentID"].ToString()));
                        comm.Parameters.AddWithValue("@courseID", Int32.Parse(Session["courseID"].ToString()));
                        comm.Parameters.AddWithValue("@fmname", studentList.SelectedValue);
                        comm.Parameters.AddWithValue("@lname", txtLastNameAdd.Text);
                        comm.Parameters.AddWithValue("@newEnrollment", newEnrollment);
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
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }
        protected void StudentInfo_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            
            try
            {
                //Casting Date variable
                string date = txtEnrDate.Text;
                // Specify exactly how to interpret the string.
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);

                // Alternate choice: If the string has been input by an end user, you might 
                // want to format it according to the current culture:
                // IFormatProvider culture = System.Threading.Thread.CurrentThread.CurrentCulture;
                DateTime newEnrollment = DateTime.Parse(date, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
                {
                    //SqlCommand comm2 = new SqlCommand("INSERT INTO Students (LastName, FirstMidName, StudentID) values (@stID, @lname,@fmname, @enrDate);", thisConnection);
                    //SqlCommand comm1 = new SqlCommand("INSERT INTO Enrollments (EnrollmentID, StudentID, CourseID) values (@enrID,@stID,@crID) ;", thisConnection);
                    SqlCommand comm = new SqlCommand("MasterInsertUpdateDelete", thisConnection);
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.AddWithValue("@StatementType", "insert");
                    comm.Parameters.AddWithValue("@enrollmentID", Int32.Parse(Session["tempEnrollmentID"].ToString()));
                    comm.Parameters.AddWithValue("@studentID", Int32.Parse(Session["tempStudentID"].ToString()));
                    comm.Parameters.AddWithValue("@courseID", Int32.Parse(Session["courseID"].ToString()));
                    comm.Parameters.AddWithValue("@fmname", studentList.SelectedValue);
                    comm.Parameters.AddWithValue("@lname", txtLastNameAdd.Text);
                    comm.Parameters.AddWithValue("@newEnrollment", newEnrollment);
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
                        dbErrorMessage.Text = "Please reload the page to see the changes!";
                    }
                }
            }
            catch ( Exception ex)
            {
                dbErrorMessage.Text += ex.Message;
                
            }
            
        }
    }
}
