<%@ Page Title="" Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="OrderHistory.aspx.cs" Inherits="SyaSyaDesign.App_Pages.ManageOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .heading {
            font-family: "Montserrat", Arial, sans-serif;
            font-size: 2rem;
            font-weight: 500;
            line-height: 1.5;
            text-align: center;
            padding: 3.5rem 0;
            color: #1a1a1a;
        }

        .zoom {
            transition: transform 1s;
        }

            .zoom:hover {
                transform: scale(1.02);
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h1 class="heading" style="color: black"><b><span>Order History</span></b></h1>
    <div style="width: 80%; margin: auto;">
        <form runat="server" id="form1">

            <div class="container">
                <div class="row flex-column m-auto ">

                    <asp:Repeater ID="Repeater1" runat="server">
                        <HeaderTemplate>
                            <table class="table w-300 p3 ms-auto me-auto">
                                <thead>
                                    <tr>
                                        <th style="width: 5%; text-align: left" scope="col">No. </th>
                                        <th style="width: 15%; text-align: left" scope="col">Receipient Name</th>
                                        <th style="width: 30%; text-align: left" scope="col">DeliveryAddress</th>
                                        <th style="width: 10%; text-align: left" scope="col">Order Date</th>
                                        <th style="width: 5%; text-align: left" scope="col">Total</th>
                                        <th style="width: 10%; text-align: left" scope="col">Status</th>
                                        <th style="width: 5%; text-align: right" scope="col"></th>
                                    </tr>
                                </thead>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table class="zoom table w-100 p3 ms-auto me-auto">
                                <tr>
                                    <td style="width: 5%; text-align: left" class="align-middle"><%# Container.ItemIndex + 1 %></td>
                                    <td style="width: 15%; text-align: left" class="align-middle"><%# Eval("RecipientName") %></td>
                                    <td style="width: 30%; text-align: left" class="align-middle"><%# Eval("DeliveryAddress")%></td>
                                    <td style="width: 10%; text-align: left" class="align-middle"><%# Eval("Date","{0:dd-MM-yyyy}")%></td>
                                    <td style="width: 5%; text-align: left" class="align-middle"><%# Eval("Total")%></td>
                                    <td style="width: 10%; text-align: left" class="align-middle"><%# Eval("Status")%></td>
                                    <td style="width: 5%; text-align: right" class="align-middle">
                                        <asp:LinkButton runat="server" class="btn btn-outline-primary" Style="cursor: pointer" PostBackUrl='<%#"~/App_Pages/PurchaseSummary.aspx?OrderID="+Eval("OrderID")%>'>View</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </form>
    </div>

</asp:Content>
