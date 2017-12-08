using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace COMP229_Assign03
{
    public partial class About : Page
    {
        // Building the connection from a string; for an example using the ConnectionStrings in web.config, go to line 29
        private SqlConnection connection = new SqlConnection("Server=localhost;Initial Catalog=Comp229Assign03;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            // Only build the list on the initial arrival, not after button presses
            if (!IsPostBack)
            {
                GetUpdate();
            }
        }

        private void GetUpdate()
        {
            //code from aspsnippets.com

            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                int studentID = Int32.Parse(Session["currentStudentID"].ToString());
                using (SqlCommand comm = new SqlCommand("Select * from Students s join Enrollments e on e.StudentID = s.StudentID where s.StudentID = " + studentID + " ; ", thisConnection))
                {
                    comm.Connection = thisConnection;
                    SqlDataAdapter ad = new SqlDataAdapter(comm);
                    DataSet ds = new DataSet();
                    ad.Fill(ds,"xyz");
                    studentData.DataSource = ds.Tables[0];
                    studentData.DataBind();
                }
            }
        }
        protected void DetailsViewExample_ItemCommand(object sender, DetailsViewCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {
                case "Edit":
                    studentData.ChangeMode(DetailsViewMode.Edit);
                    GetUpdate();
                    break;
                case "Cancel":
                    studentData.ChangeMode(DetailsViewMode.ReadOnly);
                    GetUpdate();
                    break;
            }
        }
        protected void studentData_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            int studentID = (int)studentData.DataKey.Value;
            TextBox newFirstMidNameTextBox =
            (TextBox)studentData.FindControl("txtFirstMidName");
            TextBox newLastNameTextBox =
            (TextBox)studentData.FindControl("txtLastName");
            TextBox newEnrollmentDateTextBox =
            (TextBox)studentData.FindControl("txtEnrollmentDate");
            TextBox newGradeTextBox =
            (TextBox)studentData.FindControl("txtGrade");
            string newFMName = newFirstMidNameTextBox.Text;
            string newLName = newLastNameTextBox.Text;
            int newGrade = Int32.Parse(newGradeTextBox.Text);
            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand("UPDATE Students " +
                    "SET LastName = @lname, FirstMidName = @fmname, EnrollmentDate = @newEnrollment" +
                    " WHERE StudentID = @studentID;" +
                    "UPDATE Enrollments SET Grade = @grade WHERE StudentID = @studentID; ",thisConnection);
                
                comm.Parameters.AddWithValue("@studentID", studentID);
                comm.Parameters.AddWithValue("@fmname", newFMName);
                comm.Parameters.AddWithValue("@lname", newLName);
                comm.Parameters.AddWithValue("@newEnrollment", Convert.ToDateTime((TextBox)studentData.FindControl("txtEnrDate")));
                comm.Parameters.AddWithValue("@grade", newGrade);
                try
                {
                    thisConnection.Open();
                    comm.ExecuteNonQuery();
                }
                finally
                {
                    thisConnection.Close();
                }
                studentData.ChangeMode(DetailsViewMode.ReadOnly);
                GetUpdate();
            }
        }

        protected void studentData_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {

        }
        protected void studentData_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            studentData.PageIndex = e.NewPageIndex;
            GetUpdate();
        }

        protected void CalendarDate_SelectionChanged(object sender, EventArgs e)
        {
            Calendar calendar = (Calendar)sender;
            TextBox tb = (TextBox)studentData.FindControl("txtEnrDate");
            DateTime dateOnly = calendar.SelectedDate;
            tb.Text = dateOnly.Date.ToString("MM/dd/yyyy");
        }
    }
}

