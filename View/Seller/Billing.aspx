<%@ Page Title="" Language="C#" MasterPageFile="~/View/Seller/SellerMaster.Master" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="OnlineGroceryManagementSystem.View.Seller.Billing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function PrintPanel() {
            var Pgrid = document.getElementById('<%= dtgBill.ClientID %>');
            Pgrid.border = 0;
            var Pwin = window.open('', 'PrintGrid', 'left=100, top=100, width=1024, height=768, tollbar=0, scrollbars=1, status=0, resizable=1');
            Pwin.document.write(Pgrid.outerHTML);
            Pwin.document.close();
            Pwin.focus();
            Pwin.print();
            Pwin.close();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Mybody" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-mid-5">
                <div class="row">
                    <div class="col">
                        <br />
                        <div class="mb-3">
                            <label>Product Name:</label>
                            <asp:TextBox ID="txtProdname" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label>Product Price:</label>
                            <asp:TextBox ID="txtProdprice" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label>Product Quantity:</label>
                            <asp:TextBox ID="txtProdquan" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col">
                        <img src="../../Assets/images/check%20dollar%20icon.jpg" height="150px" width="150px" style="bottom: 5%" /><br />
                        <br />
                        <label id="lblMsg" runat="server" class="text-success"></label>
                        <br />
                        <asp:Button ID="btnAdd" runat="server" Text="   Add   " class="btn btn-primary" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="   Reset   " class="btn btn-success" />

                    </div>
                </div>

                <div class="row">
                    <div class="col">
                        <asp:GridView ID="dtgProducts" runat="server" class="table table-hover" AutoGenerateSelectButton="True" OnSelectedIndexChanged="dtgProducts_SelectedIndexChanged"></asp:GridView>

                    </div>
                </div>
            </div>


            <div class="col-mid-7">
                <div class="row">
                    <div class="col-3"></div>
                    <div class="col">
                        <h1 class="text-success pl-2">Client Billing</h1>
                    </div>
                </div>
                <div class="row">
                    <asp:GridView ID="dtgBill" runat="server" class="table" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" GridLines="Horizontal" OnSelectedIndexChanged="dtgBill_SelectedIndexChanged">
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                </div>
                <div class="row">
                    <div class="col"></div>
                    <div class="col">
                        <h3>
                            <label id="lblTotal" class="text-success" runat="server"></label>
                        </h3>
                         </div>
                    <div class="col d-grid">
                        <asp:Button ID="btnPrint" runat="server" Text="Print Bill" class="btn btn-success btn-block" OnClientClick="PrintPanel()" OnClick="btnPrint_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
