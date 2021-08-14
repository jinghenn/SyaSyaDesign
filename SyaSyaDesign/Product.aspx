<%@ Page Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="SyaSyaDesign.App_Pages.Product" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Product</title>
    <style>
        .plain-link {
            text-decoration: none;
        }

        .card-img-top {
            object-fit: contain;
            object-position: center;
        }

        .card {
            min-width: 150px !important;
            transition: transform .2s;
        }

            .card:hover {
                transform: scale(1.05);
            }
    </style>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="product_form" runat="server" class="container-fluid">
        <div id="product_grid" class="row">
            <!--side bar section-->
            <div id="product_sidebar_column" class="col-2 bg-info" style="padding: 10px 30px">
                <span class="mb-3"><b>Category</b></span>
                <asp:Repeater ID="rpt_product_category" runat="server">
                    <ItemTemplate>
                        <a class="link-dark d-block py-1 mx-auto plain-link"
                            href="Product.aspx?category=<%# Eval("category_id") %>"><%# Eval("category_name")%></a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <!--end of side bar section-->

            <div id="product_list_column" class="col-10">
                <div class="row">

                    <div class="col">
                        <h1 id="category_title" runat="server">PRODUCT</h1>
                    </div>

                    <div class="col">
                        <asp:TextBox Style="width: 200px" runat="server"></asp:TextBox>
                    </div>
                </div>
                <!--product item-->
                <div id="product_list" class="d-flex flex-wrap mt-5">
                    <asp:Repeater ID="rpt_product_item" runat="server">
                        <ItemTemplate>
                            <div class="card me-2 mb-2" style="width: 15rem">
                                <a href="Detail.aspx?product_id=<%# Eval("product_id")%>">
                                    <img src="https://via.placeholder.com/150" class="card-img-top" alt="..."></a>
                                <div class="card-body">
                                    <a href="Detail.aspx?product_id=<%# Eval("product_id") %>" class="link-dark plain-link">
                                        <p class="card-text text-truncate"><%# Eval("product_name") %></p>
                                    </a>
                                    <p class="mt-1 text-truncate text-black-50">RM <%# Eval("price") %></p>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </div>
    </form>
</asp:Content>
