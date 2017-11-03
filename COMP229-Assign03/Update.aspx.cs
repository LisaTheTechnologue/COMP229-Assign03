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
                catch (Exception e)
                {
                    errorMsg.Text = e.Message;
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

            using (SqlConnection thisConnection = new SqlConnection(WebConfigurationManager.ConnectionStrings["Comp229Assign03"].ConnectionString))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = thisConnection;
                comm.CommandTimeout = 0;
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.CommandText = "UpdateDelete";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@StatementType", "update");
                comm.Parameters.AddWithValue("@studentID", studentID);
                comm.Parameters.AddWithValue("@fmname", newFMName);
                comm.Parameters.AddWithValue("@lname", newLName);
                comm.Parameters.AddWithValue("@newEnrollment", Convert.ToDateTime(newEnrollmentDateTextBox.Text));
               
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

