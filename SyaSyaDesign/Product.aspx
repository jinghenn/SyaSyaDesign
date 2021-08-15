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

        .sort-by-selection {
            width: 150px !important;
        }

        .card {
            min-width: 120px !important;
            transition: transform .15s;
        }

            .card:hover {
                transform: scale(1.03);
            }

        .side-bar-item {
            transition-property: background-color;
            transition-duration: .3s;
        }

            .side-bar-item:hover {
                background-color: #e2e2e2;
            }
    </style>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="product_form" runat="server" class="container-fluid">
        <div class="container-sm px-2">
            <div id="main-content" class="d-flex w-100 flex-column">
                <!--title and search row-->
                <div id="search-row" class="d-flex justify-content-between">
                    <h1 id="categoryTitle" class="fw-bold ms-2" runat="server">Product</h1>
                    <div class="d-flex align-items-end">
                        <span class="text-uppercase fw-bold me-1 pb-2 text-black-50" style="font-size: 10px">Sort By</span>
                        <div class="input-group-sm me-1">
                            <asp:DropDownList ID="ddlSort" CssClass="form-select me-1 sort-by-selection" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged">
                                <asp:ListItem Value="0">Latest</asp:ListItem>
                                <asp:ListItem Value="1">Popular</asp:ListItem>
                                <asp:ListItem Value="2">Price (Low to High)</asp:ListItem>
                                <asp:ListItem Value="3">Price (High to Low)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="input-group-sm d-flex" style="width: 250px; align-items: baseline">
                            <input class="form-control" type="text" placeholder="Search" />
                            <button class="input-group-text">search</button>
                        </div>
                    </div>
                </div>
                <!-- side bar and product list-->
                <div id="product-row" class="mt-3">
                    <div class="d-flex justify-content-around">
                        <div id="side-bar" class="flex-fill px-3" style="width: 200px">
                            <hr class="mt-0" />
                            <asp:Repeater ID="rpt_product_category" runat="server">
                                <ItemTemplate>
                                    <a class="link-dark d-block plain-link fw-bold ps-2 py-2 side-bar-item"
                                        href="Product.aspx?category=<%# Eval("category_id") %>"><%# Eval("category_name")%></a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div id="product-list" class="d-flex flex-fill flex-row w-75">
                            <asp:Repeater ID="rpt_product_item" runat="server">
                                <ItemTemplate>
                                    <div class="card me-2 mb-2" style="width: 13rem">
                                        <a href="Detail.aspx?product_id=<%# Eval("product_id")%>">
                                            <img src="https://via.placeholder.com/150" class="card-img-top" alt="..."></a>
                                        <div class="card-body">
                                            <a href="Detail.aspx?product_id=<%# Eval("product_id") %>" class="link-dark plain-link">
                                                <p class="card-text text-truncate"><%# Eval("product_name") %></p>
                                            </a>
                                            <p class="mt-1 text-truncate fw-bold">RM <%# Eval("price") %></p>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <p id="lblNoProduct" runat="server" class="text-black-50 text-uppercase">No Product Yet</p>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
