using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml.Linq;

namespace OnlineGroceryManagementSystem.View.Admin
{
    public partial class Product : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        GridViewRow rw;
        public static int up_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fnConnectDB();
                fnBindCategory();
                fnBindGrid();
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
                lblMsg.InnerText = ex.Message;
            }


        }

        protected void fnBindCategory()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                string qry = "SELECT * FROM tblCategory";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                ddlProd.DataSource = ds;
                ddlProd.DataTextField = "catName";
                ddlProd.DataValueField = "Cat_id";
                ddlProd.DataBind();
                ddlProd.Items.Insert(0, new ListItem("---Select Category---"));
                conn.Close();
            }
            catch (Exception ex)
            {

                lblMsg.InnerText = ex.Message;
            }
        }

        protected void fnBindGrid()
        {
            DataSet ds = new DataSet();
            try
            {
                fnConnectDB();
                string qry = "SELECT p.P_id,p.Pname, c.catName, p.Pprice, p.Pqty, p.Pexpdate from tblProduct p, tblCategory c WHERE c.Cat_id = p.Cat_id; ";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                dtgProducts.DataSource = ds;
                dtgProducts.DataBind();
                conn.Close();
            }
            catch (Exception ex)
            {

                lblMsg.InnerText = ex.Message;
            }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                fnConnectDB();
                string qry = "INSERT INTO tblProduct(Pname,Cat_id,Pprice,Pqty,Pexpdate) VALUES('" + txtProdname.Text + "','" + ddlProd.SelectedValue + "','" + txtProdprice.Text + "','" + txtProdquan.Text + "','" + txtExpiry.Text + "')";
                cmd = new SqlCommand(qry, conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.InnerText = "Product Added!";
                }
                else
                {
                    lblMsg.InnerText = "Oops! there was an error inserting product.";
                }
                conn.Close();
                fnBindGrid();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        protected void dtgProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rw = dtgProducts.SelectedRow;
            txtProdname.Text = rw.Cells[2].Text;
            txtProdprice.Text = rw.Cells[4].Text;
            txtProdquan.Text = rw.Cells[5].Text;
            up_id = Convert.ToInt32(rw.Cells[1].Text);

            for (int i = 0; i < ddlProd.Items.Count; i++)
            {
                // Trim whitespace and perform a case-insensitive comparison
                if (ddlProd.Items[i].Text.Trim().Equals(rw.Cells[3].Text.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    ddlProd.SelectedIndex = i;
                    break; // Exit the loop once a match is found
                }
            }

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                fnConnectDB();
                String qry = "UPDATE tblProduct SET Pname='" + txtProdname.Text + "',Cat_id='" + ddlProd.SelectedValue + "',Pprice='" + txtProdprice.Text + "',Pqty='" + txtProdquan.Text + "',Pexpdate='" + txtExpiry.Text + "' WHERE P_id=@up_id";
                cmd = new SqlCommand(qry, conn);
                cmd.Parameters.AddWithValue("up_id", up_id);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.InnerText = "Product Updated!";
                }
                else
                {
                    lblMsg.InnerText = "Oops! there was an error updating product.";
                }
                conn.Close();
                fnBindGrid();
            }
            catch (Exception ex)
            {
                lblMsg.InnerText = ex.Message;
            }
        }



        protected void dtgProducts_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow rw = dtgProducts.Rows[e.RowIndex];
            int p_id = Convert.ToInt32(rw.Cells[1].Text);
            try
            {
                fnConnectDB();
                string qry = "DELETE FROM tblProduct WHERE P_id = '"+p_id+"'";
                cmd = new SqlCommand(qry, conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.InnerText = "Product Deleted!";
                }
                else
                {
                    lblMsg.InnerText = "Oops! there was an error deleting product.";
                }
                conn.Close();
                fnBindGrid();

            }
            catch (Exception ex)
            {

                lblMsg.InnerText = ex.Message;
            }
        }


    }
}