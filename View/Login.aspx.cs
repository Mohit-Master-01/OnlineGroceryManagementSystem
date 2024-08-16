using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineGroceryManagementSystem.View
{   

    public partial class Login : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public static string Sname;
        public static int Skey;

        protected void Page_Load(object sender, EventArgs e)
        {
            fnConnectDB();

        }

        protected void fnConnectDB()
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Myconstr"].ConnectionString;
                conn = new SqlConnection(constr);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    //Response.Write("<script>alert('Connected successfully')</script>");
                }
            }
            catch(Exception ex) 
            {
                Response.Write(ex.ToString());
            }
          
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            if(rblUser.SelectedValue == "Admin")
            {
                if(txtUsername.Text == "Admin" && txtPassword.Text == "123")
                {
                    Session["s_admin"] = txtUsername.Text.Trim();
                    Response.Redirect("../View/Admin/Sellers.aspx");
                }
                else
                {
                    lblMsg.InnerText = "Invalid Admin!";
                }

            }
            if(rblUser.SelectedValue == "Seller")
            {
                string qry = "SELECT Sel_id,Sname, Spassword from tblSeller where Sname='" + txtUsername.Text.Trim() + "' and Spassword='" + txtPassword.Text.Trim() + "' ";
                cmd = new SqlCommand(qry,conn);

                DataTable dt = new DataTable();
                sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    Session["s_seller"] = txtUsername.Text.Trim();
                    Skey = Convert.ToInt32(dt.Rows[0][0].ToString());
                    Response.Redirect("../View/Seller/Billing.aspx");
                    // Check if the Sel_id column exists and then convert it to an integer

                }
                else
                {
                    lblMsg.InnerText = "Invalid User!";
                }
            }
        }
    }
}

//if (txtUsername.Text.Trim() == "Admin" && txtPassword.Text.Trim() == "123")
//{
//    // Session["s_admin"] = txtUsername.Text;
//    Session["s_admin"] = rblUser.SelectedValue;

//    // Check if the selected value is "Admin"
//    if (rblUser.SelectedValue == "Admin")
//    {
//        // Redirect to the dashboard page if "Admin" is selected
//        Response.Redirect("../View/Admin/Dashboard.aspx");
//    }
//    else
//    {
//        // Show an error message or do not redirect
//        Response.Write("<script>alert('Only Admins can access the dashboard!')</script>");
//    }
//}
//else
//{
//    Response.Write("<script>alert('error')</script>");
//}