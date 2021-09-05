<%@ Page Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="SyaSyaDesign.Payments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="formPayment" runat="server">
        <link href="../lib/css/Payment.css" rel="stylesheet"/>
        
        <script type="text/javascript">
            function successalert(stitle, stext) {
                swal({
                    title: stitle,
                    text: stext,
                    type: 'success'
                });
            }
            function failalert(ftitle, ftext) {
                swal({
                    title: ftitle,
                    text: ftext,
                    type: 'error'
                });
            }
            function openTab(tabName, elmnt) {
                var i, tabcontent, tablinks;

                tabcontent = document.getElementsByClassName("tab-content");
                for (i = 0; i < tabcontent.length; i++) {
                    tabcontent[i].style.display = "none";
                }

                tablinks = document.getElementsByClassName("tablink");
                for (i = 0; i < tablinks.length; i++) {
                    tablinks[i].style.backgroundColor = "";
                }

                document.getElementById(tabName).style.display = "block";
                document.getElementById('payMethod').value = tabName;
                elmnt.style.backgroundColor = "#d4a59a";
            }

            $(function () {
                $(window).ready(function () {
                    document.getElementById("defaultOpen").click();
                });
            });
        </script>
        <div class="container">
            <%--<div class="row justify-content-center">
                <ul class="list-group list-group-horizontal tab">
                    <li class="list-group-item">Cart</li>
                    <li class="list-group-item">Shipping</li>
                    <li class="list-group-item active" style="z-index: 1; padding-bottom: 0px;">Payment</li>
                </ul>
            </div>--%>
            <h2>Payment</h2>
            <div class="row container">
                <%--Shipping details--%>
                <div class="col-md-8 col-lg-6 float-sm-start cart">
                    <div class="payment-details">
                        <div class="cart-body">
                            <div class="container">
                                <div class="row">
                                    <%--<page:PageTitle runat="server" ID="title" PageHeader="Payment Details"></page:PageTitle>--%>
                                </div>
                                <div class="row">
                                    <%--<div class="col-6 col-sm-4">--%>
                                    <asp:Button runat="server" CssClass="tablink col" OnClientClick="openTab('tab-card', this);return false;" ID="defaultOpen" ClientIDMode="Static" Text="Card" UseSubmitBehavior="false"/>
                                    <%--</div>--%>
                                    <%--<div class="col-6 col-sm-4">--%>
                                        <asp:Button runat="server" CssClass="tablink col" OnClientClick="openTab('tab-online', this);return false;" Text="Online banking" UseSubmitBehavior="false" />
                                        <%--</div>--%>
                                </div>
                                <div id="tab-card" class="tab-content" style="display: block;">
                                    <asp:Table ID="TableCard" runat="server" CssClass="tblCard">
                                        <asp:TableRow Height="60px">
                                            <asp:TableCell HorizontalAlign="Center">
                                            <i class="fa  fa-credit-card-alt"></i>
                                            </asp:TableCell>
                                            <asp:TableCell Width="200px">
                                            Name on card
                                            </asp:TableCell>
                                            <asp:TableCell HorizontalAlign="Center">
                                                <asp:TextBox ID="txtcardHolder" runat="server" placeholder="Cardholder" ClientIDMode="Static" CssClass="line-input"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow Height="60px">
                                            <asp:TableCell HorizontalAlign="Center">
                                            <i class="fa fa-info"></i>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                            Credit card number
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <div style="display: flex">
                                                    <asp:TextBox ID="txtCardNo1" runat="server" placeholder="xxxx" CssClass="input-credit"></asp:TextBox>
                                                    -
                                                <asp:TextBox ID="txtCardNo2" runat="server" placeholder="xxxx" CssClass="input-credit"></asp:TextBox>
                                                    -
                                                <asp:TextBox ID="txtCardNo3" runat="server" placeholder="xxxx" CssClass="input-credit"></asp:TextBox>
                                                    -
                                                <asp:TextBox ID="txtCardNo4" runat="server" placeholder="xxxx" CssClass="input-credit"></asp:TextBox>
                                                </div>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow Height="60px">
                                            <asp:TableCell HorizontalAlign="Center">
                                            <i class="fa fa-calendar-times-o"></i>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                            Validity of the card
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtMonth" runat="server" placeholder="Month" TextMode="Month" CssClass="line-input input-validity"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow Height="60px">
                                            <asp:TableCell HorizontalAlign="Center">
                                            <i class="fa fa-user-secret"></i>
                                            </asp:TableCell>
                                            <asp:TableCell>
                                            CVV Number
                                            </asp:TableCell>
                                            <asp:TableCell>
                                                <asp:TextBox ID="txtCvv" runat="server" placeholder="CVV" TextMode="Number" CssClass="line-input" Width="60px"></asp:TextBox>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>
                                <div class="tab-content" id="tab-online">
                                    Please select the bank :
                                    <asp:RadioButtonList ID="radioBank" runat="server" ClientIDMode="Static">
                                        <asp:ListItem Text="TAYY Bank" Value="TAYY Bank" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="YAY Bank" Value="YAY Bank"></asp:ListItem>
                                        <asp:ListItem Text="MUU Bank" Value="MUU Bank"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="row">
                                    <asp:Button CssClass="btn btn-success mx-auto" runat="server" Text="Make Payment" Width="300" ID="payButton" ClientIDMode="Static" OnClick="PayButton_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--End shipping details--%>

                <%-- Order Summary --%>
                <div class="col-md-4 mx-auto order-summary">
                    <div class="card">
                        <div class="card-header">
                            Order Summary
                        </div>
                        <div class="border-top card-body bottom-card">
                            <div class="row">
                                <div class="col-6">
                                    Subtotal
                                </div>
                                <div class="col-6">
                                    : RM
                                    <asp:Label ID="lblSubtotal" runat="server" CssClass="float-end" Text="0.00"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6">
                                    Shipping
                                </div>
                                <div class="col-6">
                                    : RM
                                    <asp:Label ID="lblShipping" runat="server" CssClass="float-end" Text="0.00"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6">
                                    Final Total 
                                </div>
                                <div class="col-6">
                                    : RM
                                    <asp:Label ID="lblFinalTotal" runat="server" CssClass="float-end" Text="0.00"></asp:Label><br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="div-button">
                        <asp:Button ID="BtnBack" runat="server" Text="Cancel" Width="125" CssClass="btn btn-danger btnBack" OnClick="BtnBack_Click"/>
                    </div>
                    <div style="word-wrap: break-word">
                        If you faced any question, kindly read
                        <asp:HyperLink ID="linkFAQ" runat="server" Text="FAQ"></asp:HyperLink>
                        or contact us with 018-888 8300.
                    </div>
                </div>
                <%-- End Order Summary --%>
            </div>
        </div>
        <asp:HiddenField ID="payMethod" ClientIDMode="Static" runat="server" />
    </form>

</asp:Content>
