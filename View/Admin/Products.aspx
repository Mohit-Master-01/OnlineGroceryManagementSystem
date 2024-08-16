﻿<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="OnlineGroceryManagementSystem.View.Admin.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Mybody" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-8"><br /><img src="../../Assets/images/veg_food_icon-removebg-preview.png" height="90px" width="90px"  /><h3 class="text-success">Manage Products</h3></div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <h2 class="text-success">Products Details</h2>

                <div class="mb-3">
                    <label>Product Name:</label>
                    <asp:TextBox ID="txtProdname" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label>Product Category:</label>
                    <asp:DropDownList ID="ddlProd" runat="server" class="form-control"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label>Product Price:</label>
                    <asp:TextBox ID="txtProdprice" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label>Product Quantity:</label>
                    <asp:TextBox ID="txtProdquan" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <label>Expiry Date:</label>
                    <asp:TextBox ID="txtExpiry" runat="server" TextMode="Date"></asp:TextBox>
                </div>

                <label id="lblMsg" runat="server" class="text-success"></label><br />
                <asp:Button ID="btnEdit" runat="server" Text="   Edit   " class="btn btn-success" OnClick="btnEdit_Click" />
                <asp:Button ID="btnSave" runat="server" Text="   Save   " class="btn btn-success" OnClick="btnSave_Click" />
            </div>
            <div class="col-md-8">
                     <asp:GridView ID="dtgProducts" runat="server" class="table table-hover" AutoGenerateSelectButton="True"  AutoGenerateDeleteButton="True" OnRowDeleting="dtgProducts_RowDeleting" OnSelectedIndexChanged="dtgProducts_SelectedIndexChanged"  ></asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
