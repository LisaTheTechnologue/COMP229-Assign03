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
                string studentID = Session["currentStudentID"].ToString();
                SqlCommand comm = new SqlCommand("Select * from Students"
                    + " where StudentID = @ssName ; ", thisConnection);
                comm.Parameters.Add("@ssName", SqlDbType.Int);
                comm.Parameters["@ssName"].Value = Int32.Parse(studentID);
                try
                {
                    thisConnection.Open();
                    SqlDataReader reader = comm.ExecuteReader();
                    studentData.DataSource = reader;
                    studentData.DataKeyNames = new string[] { "studentID" };
                    studentData.DataBind();
                    reader.Close();
                }
                finally
                {
                    thisConnection.Close();
                }
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
            // Change current mode to the selected one
            studentData.ChangeMode(e.NewMode);
            // Rebind the grid
            GetUpdate();
        }
    }
}

