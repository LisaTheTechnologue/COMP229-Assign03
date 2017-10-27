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
            // See how we can use a using statement rather than try-catch (this will close and dispose the connection similarly to a finally block
            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                string ssName = Session["studentID"].ToString();
                SqlCommand comm = new SqlCommand("Select * from Students"
                    + "where st.StudentID = @ssName ; ", thisConnection);
                comm.Parameters.Add("EmployeeID", SqlDbType.Int);
                comm.Parameters["EmployeeID"].Value = ssName;
                try
                {
                    thisConnection.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    studentData.DataSource = reader;
                    studentData.DataKeyNames = new string[] { "EmployeeID" };
                    studentData.DataBind();
                    reader.Close();
                }
                finally { thisConnection.Close(); }
            }
        }
        //protected void studentInfo_ItemCommand(object source, DataListCommandEventArgs e)
        //{
        //    if (e.CommandName == "editCommand")
        //    {
        //        int studentID = Convert.ToInt32(e.CommandArgument);

        //        TextBox fmnameTextBox = (TextBox)e.Item.FindControl("fmnameTextBox");
        //        string newFMName = fmnameTextBox.Text;

        //        TextBox lnameTextBox = (TextBox)e.Item.FindControl("lnameTextBox");
        //        string newLName = lnameTextBox.Text;

        //        TextBox enrollmentTextBox = (TextBox)e.Item.FindControl("enrdateTextBox");
        //        // Date strings are interpreted according to the current culture.
        //        // If the culture is en-US, this is interpreted as "January 8, 2008",
        //        // but if the user's computer is fr-FR, this is interpreted as "August 1, 2008"
        //        string date = enrollmentTextBox.Text;
        //        // Specify exactly how to interpret the string.
        //        IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);

        //        // Alternate choice: If the string has been input by an end user, you might 
        //        // want to format it according to the current culture:
        //        // IFormatProvider culture = System.Threading.Thread.CurrentThread.CurrentCulture;
        //        DateTime newEnrollment = DateTime.Parse(date, culture, System.Globalization.DateTimeStyles.AssumeLocal);

        //        UpdateItem(studentID, newFMName, newLName, newEnrollment);
        //        employeesList.EditItemIndex = -1;
        //        BindList();
        //    }
        //    else if (e.CommandName == "cancelCommand")
        //    {
        //        employeesList.EditItemIndex = -1;
        //        BindList();
        //    }
        //}
        //protected void UpdateItem(int studentID, string fmnameTextBox, string lnameTextBox, DateTime newEnrollment)
        //{
        //    using (SqlConnection updateConn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
        //    {
        //        string ssName = Session["studentID"].ToString();
        //        SqlCommand updateComm = new SqlCommand("Select * from Students"
        //            + "where st.StudentID = '" + ssName + "'; ", updateConn);
        //        updateConn.Open();
        //        updateComm = new SqlCommand("UpdateStudent", updateConn);
        //        updateComm.CommandType = System.Data.CommandType.StoredProcedure;
        //        updateComm.Parameters.Add("@studentID", SqlDbType.Int);
        //        updateComm.Parameters["@studentID"].Value = studentID;
        //        updateComm.Parameters.Add("@fmname", SqlDbType.NVarChar, 50);
        //        updateComm.Parameters["@fmname"].Value = fmnameTextBox;
        //        updateComm.Parameters.Add("@lname", SqlDbType.NVarChar, 50);
        //        updateComm.Parameters["@lname"].Value = lnameTextBox;
        //        updateComm.Parameters.Add("@newEnrollment", SqlDbType.DateTime, 50);
        //        updateComm.Parameters["@newEnrollment"].Value = newEnrollment;
        //        try
        //        {
        //            updateConn.Open();
        //            updateComm.ExecuteNonQuery();
        //        }
        //        finally
        //        {
        //            updateConn.Close();
        //        }
        //    }
        //}
        //protected void listSt_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    // try-finally to ensure that the connection is closed if there's an issue
        //    try
        //    {
        //        if (e.CommandName == "deleteCommand")
        //        {
        //            // You can't delete a record with references in other tables, so delete those references first
        //            SqlCommand deleteEnrollments = new SqlCommand("DELETE FROM Enrollments WHERE CourseID=@CourseID", connection);
        //            SqlCommand deleteCourse = new SqlCommand("DELETE FROM Courses WHERE CourseID=@CourseID", connection);

        //            // Parameterize everything, even if the user isn't entering the values
        //            deleteEnrollments.Parameters.AddWithValue("@CourseID", e.CommandArgument);
        //            deleteCourse.Parameters.AddWithValue("@CourseID", e.CommandArgument);

        //            connection.Open(); // open the cmd connection

        //            // delete the references FIRST
        //            deleteEnrollments.ExecuteNonQuery();
        //            deleteCourse.ExecuteNonQuery();
        //        }
        //        else if (e.CommandName == "updateCommand")
        //        {
        //            SqlCommand cmd = new SqlCommand("UPDATE Courses SET Title=@UpdatedTitle WHERE Title=@Title", connection);
        //            cmd.Parameters.AddWithValue("@Title", e.CommandArgument);
        //            cmd.Parameters.AddWithValue("@UpdatedTitle", e.CommandArgument + " - Updated");

        //            connection.Open(); // open the cmd connection

        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }

        //    // Re-bind the data with the changed database records
        //    GetClasses();
        //}
        //protected void EditCustomer(object sender, GridViewEditEventArgs e)
        //{
        //    StudentInfo.EditIndex = e.NewEditIndex;
        //    GetUpdate();
        //}
        //protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    StudentInfo.EditIndex = -1;
        //    GetUpdate();
        //}
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
            // Date strings are interpreted according to the current culture.
            // If the culture is en-US, this is interpreted as "January 8, 2008",
            // but if the user's computer is fr-FR, this is interpreted as "August 1, 2008"
            string date = newEnrollmentDateTextBox.Text;
            // Specify exactly how to interpret the string.
            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);

            // Alternate choice: If the string has been input by an end user, you might 
            // want to format it according to the current culture:
            // IFormatProvider culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            DateTime newEnrollment = DateTime.Parse(date, culture, System.Globalization.DateTimeStyles.AssumeLocal);
            string newGrade = newGradeTextBox.Text;
            
            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand("UpdateItem", thisConnection);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.Add("StudentID", SqlDbType.Int);
                comm.Parameters["StudentID"].Value = studentID;
                comm.Parameters.Add("newFMName", SqlDbType.NVarChar, 50);
                comm.Parameters["newFMName"].Value = newFMName;
                comm.Parameters.Add("newLName", SqlDbType.NVarChar, 50);
                comm.Parameters["newLName"].Value = newLName;
                comm.Parameters.Add("newEnrollment", SqlDbType.DateTime);
                comm.Parameters["newEnrollment"].Value = newEnrollment;
                comm.Parameters.Add("newGrade", SqlDbType.Int);
                comm.Parameters["newGrade"].Value = newGrade;


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
    }
}

