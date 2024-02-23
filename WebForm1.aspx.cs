using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//#add this using for sql purpose
using System.Data.SqlClient;

//nag lagay ako comment dito kase trip ko
using System.Data;

namespace firsttryconnectingtodb
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        string usernames;
        protected void Page_Load(object sender, EventArgs e)
        {

            BindGridView();

            if (Page.IsPostBack == true)
            {
                Label1.Text = ("Data inserted");
            }

        }


        //Code to connect db to gridview
        private void BindGridView()
        {

            try 
            {
                SqlConnection storename = new SqlConnection("Server=tcp:bagongserver.database.windows.net,1433;Initial Catalog=bagongdb;Persist Security Info=False;User ID=Frankdb;Password=Frank12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                storename.Open();
                SqlCommand select = new SqlCommand("SELECT * FROM bagongtable", storename);
                SqlDataAdapter adap = new SqlDataAdapter(select);
                DataSet ds = new DataSet();
                adap.Fill(ds);
                if(ds.Tables[0].Rows.Count>0) 
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();


                }
            }
            catch(Exception ex) 
            {
                throw;
            
            }

        }

        //Code to insert value to db
        protected void Button1_Click(object sender, EventArgs e)
        {

            //sql connection
            SqlConnection storename = new SqlConnection("Server=tcp:bagongserver.database.windows.net,1433;Initial Catalog=bagongdb;Persist Security Info=False;User ID=Frankdb;Password=Frank12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            {
                SqlCommand insert = new SqlCommand("EXEC dbo.insertName @Name", storename);

                insert.Parameters.AddWithValue("@Name", TextBox1.Text);

                storename.Open();
                insert.ExecuteNonQuery();
                storename.Close();

                if (IsPostBack) 
                {
                    TextBox1.Text = "";
                
                
                };


            }
        }


        //Code to verify login
        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlConnection storename = new SqlConnection("Server=tcp:bagongserver.database.windows.net,1433;Initial Catalog=bagongdb;Persist Security Info=False;User ID=Frankdb;Password=Frank12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            {

                SqlCommand select = new SqlCommand("EXEC dbo.selectUsers", storename);

                storename.Open();

                SqlDataReader dr = select.ExecuteReader();

                

                if (dr.Read()) 
                
                {
                    usernames = dr["Name"].ToString();
                    Label2.Text = usernames;
                
                }

                storename.Close();

            }

        }

        public void Button3_Click(object sender, EventArgs e)
        {
            SqlConnection storename = new SqlConnection("Server=tcp:bagongserver.database.windows.net,1433;Initial Catalog=bagongdb;Persist Security Info=False;User ID=Frankdb;Password=Frank12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            String username, password;

            username = TextBox2.Text;
            password = TextBox3.Text;

            try
            {
                String querry = "SELECT * FROM userLogin WHERE username= '"+ TextBox2.Text+"' AND password = '"+ TextBox3.Text+"'";

                SqlDataAdapter sda = new SqlDataAdapter(querry, storename);

                DataTable dtable = new DataTable();

                sda.Fill(dtable);

                if(dtable.Rows.Count > 0)
                {
                    username = TextBox2.Text;
                    password = TextBox3.Text;

                    Response.Redirect("WebForm2.aspx");


                }
                else
                {

                    Label5.Text = ("invalid login");

                }
                

            }
            catch
            {

                Label5.Text = ("ERROR!");


            }


        }
    }
}