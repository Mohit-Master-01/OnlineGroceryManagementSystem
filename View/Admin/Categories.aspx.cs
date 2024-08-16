using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;

namespace OnlineGroceryManagementSystem.View.Admin
{
    public partial class Categories : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        GridViewRow rw;
        public static int up_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            fnConnectDB();
            fnBindGrid();
        }

        protected void fnConnectDB()
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Myconstr"].ConnectionString;
                conn = new SqlConnection(strcon);
                if(conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    //Response.Write("<script>alert('Connected Successfully!')</script>");
                }
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
                string qry = "INSERT INTO tblCategory(catName, catDescription) VALUES('"+txtCatname.Text+"','"+txtCatremarks.Text+"')";
                cmd = new SqlCommand(qry,conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.InnerText = "Category Added!";
                }
                else
                {
                    lblMsg.InnerText = "Oops! there was an error inserting category";
                }
                conn.Close();
                fnBindGrid();
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
                string qry = "SELECT * FROM tblCategory";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                dtgCategory.DataSource = ds;
                dtgCategory.DataBind();
                conn.Close();

            }
            catch (Exception ex)
            {
                lblMsg.InnerText = ex.Message;
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                fnConnectDB();
                string qry = "UPDATE tblCategory SET catName='"+txtCatname.Text+"', catDescription='"+txtCatremarks.Text+"' WHERE Cat_id='"+up_id+"'";
                cmd = new SqlCommand(qry,conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.InnerText = "Category Updated!";
                }
                else
                {
                    lblMsg.InnerText = "Oop! there was an error updating category";

                }
                conn.Close();
                fnBindGrid();
            }
            catch (Exception ex)
            {
                lblMsg.InnerText=ex.Message;
            }
        }

        protected void dtgCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow rw = dtgCategory.SelectedRow;
            txtCatname.Text = rw.Cells[2].Text;
            txtCatremarks.Text = rw.Cells[3].Text;

            up_id = Convert.ToInt32(rw.Cells[1].Text);

        }

        protected void dtgCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow rw = dtgCategory.Rows[e.RowIndex];
            int c_id = Convert.ToInt32(rw.Cells[1].Text);
            try
            {
                fnConnectDB();
                string qry = "DELETE FROM tblCategory WHERE Cat_id='" + c_id + "'";
                cmd = new SqlCommand(qry,conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.InnerText = "Category Deleted :(";
                }
                else
                {
                    lblMsg.InnerText = "Oops! there was an error deleting category";
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