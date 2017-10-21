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
                SqlCommand comm = new SqlCommand("Select LastName, FirstMidName from Students;", thisConnection);
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
            if (e.CommandName == "MoreDetailsPlease")
            { }
        }

        protected void listSt_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "MoreDetailsPlease")
            {
                
            }
        }
    }
}