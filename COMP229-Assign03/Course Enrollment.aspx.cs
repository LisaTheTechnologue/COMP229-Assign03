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
                SqlCommand commAdd = new SqlCommand("Select distinct st.StudentID, FirstMidName + ' ' + LastName as FullName" +
                    " from Students st " +
                    "join Enrollments e on st.StudentID = e.StudentID " +
                    "join Courses c on e.CourseID = c.CourseID " +
                    "where not c.CourseID =  @courseID;", thisConnection);
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
                    //DataTable dt = new DataTable();
                    SqlDataAdapter ad = new SqlDataAdapter(commAdd);
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                                        studentList.DataSource = ds.Tables[0];
                        studentList.DataTextField = ds.Tables[0].Columns["FullName"].ToString(); ;
                        studentList.DataValueField = ds.Tables[0].Columns["StudentID"].ToString(); ;
                        studentList.DataBind();
                    
                }
                //catch (Exception ex)
                //{
                //    dbErrorMessage.Text = ex.Message;
                //}
                finally
                {
                    thisConnection.Close();
                }
            }
        }
        protected void Name_SelectedIndexChanged(object obj, EventArgs e)
        { }
        //    // See how we can use a using statement rather than try-catch (this will close and dispose the connection similarly to a finally block
        //    using (thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
        //    {
        //        try
        //        {
        //            thisConnection.Open();
        //            SqlCommand commLastNameAdd = new SqlCommand("Select st.LastName, st.StudentID, st.FirstMidName from Students st " +
        //               "where st.FirstMidName = @FirstMidName;", thisConnection);
        //            Session["tempFirstMidName"] = studentList.SelectedValue;
        //            commLastNameAdd.Parameters.AddWithValue("@FirstMidName", Session["tempFirstMidName"].ToString());
        //            SqlDataReader readerLastNameAdd = commLastNameAdd.ExecuteReader();
        //            while (readerLastNameAdd.Read())
        //            {
        //                if (readerLastNameAdd["LastName"].ToString() != null)
        //                {
        //                    txtLastNameAdd.Text = readerLastNameAdd["LastName"].ToString();
        //                    Session["tempStudentID"] = readerLastNameAdd["StudentID"];
        //                }
        //                else dbErrorMessage.Text = "<li>No last name added!</li>";
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //        finally
        //        {
        //            thisConnection.Close();
        //        }
        //    }
        //}
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand(" INSERT INTO[dbo].Enrollments(CourseID, StudentID, Grade)  " +
                "VALUES (@courseID,@studentID, @grade );         ",thisConnection);               
               //" SET IDENTITY_INSERT[dbo].STUDENTS OFF;                             " +
               //" SET IDENTITY_INSERT[dbo].ENROLLMENTS OFF;                          " +
                string name = studentList.SelectedItem.Text;
                string[] sep_name = name.Split();
                comm.Parameters.AddWithValue("@grade", Int32.Parse(txtGrade.Text));
                comm.Parameters.AddWithValue("@studentID", Int32.Parse(studentList.SelectedValue));
                comm.Parameters.AddWithValue("@courseID", Int32.Parse(Session["courseID"].ToString()));
                comm.Parameters.AddWithValue("@fmname", sep_name[0]);
                comm.Parameters.AddWithValue("@lname", sep_name[1]);
                comm.Parameters.AddWithValue("@newEnrollment", Convert.ToDateTime(txtStudentEnrollmentDate.Text));

                /*
                 * CREATE PROCEDURE InsertStudent(
                    @studentID Int,
                    @fmname nvarchar(50),
                    @lname nvarchar(50),
                    @newEnrollment date,
                    @courseID int,
                    @grade int)
                    AS  
                    BEGIN  
	                    SET IDENTITY_INSERT [dbo].STUDENTS ON;  			
	                    INSERT INTO [dbo].Students(StudentID, LastName,FirstMidName,EnrollmentDate)
	                    VALUES (@studentID,@fmname,@lname,@newEnrollment);
	                    SET IDENTITY_INSERT [dbo].STUDENTS OFF;	
	                    SET IDENTITY_INSERT [dbo].ENROLLMENTS OFF;  
	                    INSERT INTO [dbo].Enrollments(CourseID, StudentID, Grade)
	                    VALUES (@courseID,@studentID, @grade );
                    END  ;
                 */

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
