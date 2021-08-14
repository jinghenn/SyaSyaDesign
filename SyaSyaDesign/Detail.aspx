<%@ Page Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="SyaSyaDesign.Detail" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Product</title>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="ProductDetailform" runat="server">
        <div class="container">
            <div class="d-flex">
                <div class="w-50 m-5" style="height: 500px">
                    <img src="https://via.placeholder.com/300" />
                </div>

                <div class="d-flex w-50 m-5">
                    <div>
                        <asp:Label ID="lblProductName" CssClass="" Text ="" runat="server" />
                        <asp:Label ID="lblPrice" Text="" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
