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
                SqlCommand comm = new SqlCommand("Select LastName, FirstMidName, StudentID from Students;", thisConnection);
                thisConnection.Open();
                SqlDataReader reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    courseName.Text = reader["Title"].ToString();
                    courseSt.Text = reader["FirstMidName"].ToString() + " " + reader["LastName"].ToString();
                }

                reader.Close();
                thisConnection.Close();
            }
        }
    }
}