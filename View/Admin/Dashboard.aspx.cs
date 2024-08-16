using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineGroceryManagementSystem.View.Admin
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public static int cnt;

        protected void Page_Load(object sender, EventArgs e)
        {
            fnBindSeller();
            if (!IsPostBack)
            {
                fnConnectDB();
                if (Session["s_admin"] == null && Session["s_seller"] == null)
                {
                    //Response.Redirect("../Login.aspx");
                }
                Session["Products"] = FNproduct(cnt);
                Session["Category"] = FNcategory(cnt);
                Session["Finance"] = FNfinance(cnt);
                Session["Sellers"] = FNsellers(cnt);
                Session["Total_sells"] = FNtotal_sells(cnt);


            }
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
            catch (Exception ex)
            {
                Response.Write("<script>alert('Connection faied')</script>");
            }
        }

        protected void fnBindSeller()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                string qry = "SELECT * FROM tblSeller";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                ddlSeller.DataSource = ds;
                ddlSeller.DataTextField = "Sname";
                ddlSeller.DataValueField = "Sel_id";
                ddlSeller.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }

        protected int FNproduct(int cnt)
        {

            fnConnectDB();
            {
                // Create a new command to execute the first query
                cmd = new SqlCommand("SELECT count(*) from tblProduct", conn);
                // Execute the command and store the results in a DataTable
                SqlDataReader sda = cmd.ExecuteReader();
                while (sda.Read())
                {
                    if (sda[0] == DBNull.Value)
                    {
                        cnt = 0;
                    }
                    else
                    {
                        cnt = Convert.ToInt32(sda[0]);
                    }
                }
                return cnt;
            }
        }

        protected int FNcategory(int cnt)
        {

            fnConnectDB();
            {
                // Create a new command to execute the first query
                cmd = new SqlCommand("SELECT count(*) from tblCategory", conn);
                // Execute the command and store the results in a DataTable
                SqlDataReader sda = cmd.ExecuteReader();
                while (sda.Read())
                {
                    if (sda[0] == DBNull.Value)
                    {
                        cnt = 0;
                    }
                    else
                    {
                        cnt = Convert.ToInt32(sda[0]);
                    }
                }
                return cnt;
            }
        }

        protected int FNfinance(int cnt)
        {
            fnConnectDB();
            {
                cmd = new SqlCommand("SELECT Sum(Amount) FROM tblBill", conn);
                SqlDataReader sda = cmd.ExecuteReader();
                while (sda.Read())
                {
                    if (sda[0] == DBNull.Value)
                    {
                        cnt = 0;
                    }
                    else
                    {
                        cnt = Convert.ToInt32(sda[0]);
                    }
                }
                return cnt;
            }
        }

        protected int FNsellers(int cnt)
        {
            fnConnectDB();
            {
                cmd = new SqlCommand("SELECT Count(*) FROM tblSeller", conn);
                SqlDataReader sda = cmd.ExecuteReader();
                while (sda.Read())
                {
                    if (sda[0] == DBNull.Value)
                    {
                        cnt = 0;
                    }
                    else
                    {
                        cnt = Convert.ToInt32(sda[0]);
                    }
                }
                return cnt;
            }
        }

        protected int FNtotal_sells(int cnt)
        {
            fnConnectDB();
            {
                string qry = "SELECT Sum(Amount) FROM  tblBill WHERE Sel_id =@s_id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("s_id",ddlSeller.SelectedValue);
                SqlDataReader sda = cmd.ExecuteReader();
                while (sda.Read())
                {
                    if (sda[0] == DBNull.Value)
                    {  
                        cnt = 0;
                    }
                    else
                    {
                        cnt = Convert.ToInt32(sda[0]);
                    }
                }
            }
            return cnt;
        }

        protected void ddlSeller_SelectedIndexChanged(object sender, EventArgs e)
        {
            FNtotal_sells(cnt);
        }
    }
}