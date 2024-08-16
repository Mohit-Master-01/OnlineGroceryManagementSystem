using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineGroceryManagementSystem.View.Seller
{
    public partial class Billing : System.Web.UI.Page
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public static int up_id;
        public static int stock;
        public static int grd_total = 0;
        //public static int Amount = 0;
        int Seller = Login.Skey;

        public object Sel_id { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            fnConnectDB();
            fnBindGrid();

            if (!this.IsPostBack)
            {
                DataTable dt = new DataTable();
                string[] columnNames = { "ID", "Product", "Price", "Quantity", "Total" };

                foreach (string columnName in columnNames)
                {
                    dt.Columns.Add(new DataColumn(columnName));
                }

                ViewState["Bill"] = dt;
                this.fnBindbillGrid();
            }

        }

        protected void fnConnectDB()
        {
            try
            {
                string strcon = ConfigurationManager.ConnectionStrings["Myconstr"].ConnectionString;
                conn = new SqlConnection(strcon);
                if (conn.State != ConnectionState.Open)
                { 
                    conn.Open();
                    //Response.Write("<script>alert('Connected Successsfully')</script>");
                }
                else
                {
                    Response.Write("<script>alert('Connection failed')</script>");
                }
            }
            catch (Exception ex)
            {

                Response.Write(ex.ToString());
            }
        }

        protected void fnInsertBill()
        {
            try
            {
                fnConnectDB();

                // Create a SQL command with parameters
                string qry = "INSERT INTO tblBill (BIll_date, Sel_id, Amount) VALUES (@BillDate, @SelId, @GrdTotal)";
                SqlCommand cmd = new SqlCommand(qry, conn);

                // Add parameters with the correct data types
                cmd.Parameters.AddWithValue("@BillDate", DateTime.Today);
                cmd.Parameters.AddWithValue("@SelId", Seller);
                cmd.Parameters.AddWithValue("@GrdTotal", grd_total);

                // Execute the command
                int res = cmd.ExecuteNonQuery();

                if (res > 0)
                {
                    lblMsg.InnerText = "Bill Inserted!";
                }
                else
                {
                    lblMsg.InnerText = "Oops! there is an error inserting the bill";
                }
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
                string qry = "SELECT p.P_id as ID,p.Pname as Name, c.catName Category, p.Pprice as Price, p.Pqty as Quantity from tblProduct p, tblCategory c WHERE c.Cat_id = p.Cat_id; ";
                cmd = new SqlCommand(qry, conn);
                sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
                dtgProducts.DataSource = ds;
                dtgProducts.DataBind();
                conn.Close();
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
            //txtProdquan.Text = rw.Cells[5].Text;
            up_id = Convert.ToInt32(rw.Cells[1].Text);
            stock = Convert.ToInt32(rw.Cells[5].Text);

            //for (int i = 0; i < ddlProd.Items.Count; i++)
            //{
            //    // Trim whitespace and perform a case-insensitive comparison
            //    if (ddlProd.Items[i].Text.Trim().Equals(rw.Cells[3].Text.Trim(), StringComparison.OrdinalIgnoreCase))
            //    {
            //        ddlProd.SelectedIndex = i;
            //        break; // Exit the loop once a match is found
            //    }
            //}
        }

        protected void UpdateStock()
        {
            int newQty;
            newQty = stock - Convert.ToInt32(txtProdquan.Text);

            try
            {
                fnConnectDB();
                string qry = "UPDATE tblProduct SET Pqty = '" + newQty + "' WHERE P_id = '" + up_id + "'";
                cmd = new SqlCommand(qry, conn);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    lblMsg.InnerText = "Quantity Updated!";
                }
                else
                {
                    lblMsg.InnerText = "Oops! there was an error updating Quantity.";
                }
                conn.Close();
                fnBindGrid();
            }
            catch (Exception ex)
            {

                lblMsg.InnerText = ex.Message;
            }
            //string qry= "UPDATE tblProduct SET Pqty = '"+newQty+"' WHERE P_id = '"+up_id+"'";
            //cmd = new SqlCommand(qry, conn);
            //sda = new SqlDataAdapter(cmd);
            //cmd.ExecuteNonQuery();
            //conn.Close();
            //lblMsg.InnerText = "Quantity Updated!";
            //fnBindGrid();


        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // Calculate total
            int total = Convert.ToInt32(txtProdquan.Text) * Convert.ToInt32(txtProdprice.Text);

            // Retrieve DataTable from ViewState or create a new one if it doesn't exist
            DataTable dt = ViewState["Bill"] as DataTable;
            if (dt == null)
            {
                dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("Product");
                dt.Columns.Add("Price");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Total");
            }

            // Add new row to DataTable
            dt.Rows.Add(dt.Rows.Count + 1, txtProdname.Text.Trim(), txtProdprice.Text.Trim(), txtProdquan.Text.Trim(), total);

            // Save DataTable back to ViewState and bind GridView
            ViewState["Bill"] = dt;
            this.fnBindbillGrid();
            UpdateStock();
            grd_total += total;
            lblTotal.InnerText = "Rs" + grd_total;
            //fnBindGrid();

            //Amount = grd_total;
            // Clear input fields
            txtProdname.Text = string.Empty;
            txtProdprice.Text = string.Empty;
            txtProdquan.Text = string.Empty;
        }

        protected void fnBindbillGrid()
        {
            dtgBill.DataSource = ViewState["Bill"];
            dtgBill.DataBind();

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            fnInsertBill();
        }

        protected void dtgBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}