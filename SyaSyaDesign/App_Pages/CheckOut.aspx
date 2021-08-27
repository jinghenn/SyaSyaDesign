﻿<%@ Page Title="Check Out" Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="SyaSyaDesign.App_Pages.CheckOut" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function CustomValidator1_ClientValidate(source, args) {
            var num = args.Value;
            var pattern = /^[4|5]/;
            if (!pattern.test(num)) {
                args.IsValid = false;
            }
        }
        function CustomValidator2_ClientValidate(source, args) {
            var num = args.Value;
            var pattern1 = /^[4|5]/;
            var pattern2 = /^[4|5]\d{15}$/;
            if (!pattern1.test(num)) {

            }
            else {
                if (!pattern2.test(num)) {
                    args.IsValid = false;
                }
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="form1" runat="server">

        <h1 style="text-align:center">Checkout</h1>

        <div class="row" style="margin-left=16dp;">

            <div class="row" id="section1">
                <h3 class="mt-2 mb-4">Delivery Info</h3>

                <div class="form-group row">
                    <asp:Label ID="lblRecipientName" class="col-4 col-form-label" runat="server">Recipient Name</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtName" name="txtName" placeholder="Alice" type="text" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-append">
                                <div class="input-group-text">
                                    <i class="fa fa-address-book-o"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Recipent Name is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtName" ErrorMessage="Must be in characters" ValidationExpression="^[a-zA-Z ]{1,50}$" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                </p>

                <div class="form-group row">
                    <asp:Label ID="lblEmail" class="col-4 col-form-label" runat="server">Email Address</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <asp:TextBox runat="server" ID="txtEmail" name="text" placeholder="abc123@gmail.com" type="text" class="form-control" TextMode="Email"></asp:TextBox>
                            <div class="input-group-append">
                                <div class="input-group-text">
                                    <i class="fa fa-envelope-o"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Email is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="emailRegex" runat="server" ControlToValidate="txtEmail"
                        ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                        ErrorMessage="Invalid email address" Display="Dynamic" />
                </p>

                <div class="form-group row">
                    <asp:Label runat="server" ID="lblContactNumber" class="col-4 col-form-label">Contact Number</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <div class="input-group-text">+60</div>
                            </div>
                            <asp:TextBox ID="txtContactNumber" name="txtContactNumber" placeholder="123456789" type="text" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-append">
                                <div class="input-group-text">
                                    <i class="fa fa-mobile-phone"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtContactNumber" Display="Dynamic" ErrorMessage="Contact Number is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtContactNumber" Display="Dynamic" ErrorMessage="Invalid Format (eg. +60 123456789)" ValidationExpression="^[0-9]{9,10}$" ForeColor="Red"></asp:RegularExpressionValidator>
                </p>

                <div class="form-group row">
                    <asp:Label for="lblDeliveryAddress" class="col-4 col-form-label" runat="server">Delivery Address</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtDeliveryAddress" name="txtDeliveryAddress" placeholder="88, Jalan Malinja 1, Setapak" type="text" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <div class="input-group-append">
                                <div class="input-group-text">
                                    <i class="fa fa-home"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDeliveryAddress" Display="Dynamic" ErrorMessage="Deliver Address is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </p>

                <div class="form-group row">
                    <asp:Label ID="lblState" class="col-4 col-form-label" runat="server">State</asp:Label>
                    <div class="col-8">
                        <asp:DropDownList ID="ddlState" name="ddlState" class="form-select" runat="server">
                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                            <asp:ListItem>Johor</asp:ListItem>
                            <asp:ListItem>Kedah</asp:ListItem>
                            <asp:ListItem>Kelantan</asp:ListItem>
                            <asp:ListItem>Malacca</asp:ListItem>
                            <asp:ListItem>Negeri Sembilan</asp:ListItem>
                            <asp:ListItem>Pahang</asp:ListItem>
                            <asp:ListItem>Penang</asp:ListItem>
                            <asp:ListItem>Perak</asp:ListItem>
                            <asp:ListItem>Perlis</asp:ListItem>
                            <asp:ListItem>Sabah</asp:ListItem>
                            <asp:ListItem>Sarawak</asp:ListItem>
                            <asp:ListItem>Selangor</asp:ListItem>
                            <asp:ListItem>Terengganu</asp:ListItem>
                            <asp:ListItem>Kuala Lumpur</asp:ListItem>
                            <asp:ListItem>Labuan</asp:ListItem>
                            <asp:ListItem>Putrajaya</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlState" Display="Dynamic" ErrorMessage="State is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </p>

                <div class="form-group row">
                    <asp:Label ID="lblCity" class="col-4 col-form-label" runat="server">City</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtCity" name="txtCity" placeholder="Kuala Lumpur" type="text" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-append">
                                <div class="input-group-text">
                                    <i class="fa "></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtCity" Display="Dynamic" ErrorMessage="City is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </p>

                <div class="form-group row">
                    <asp:Label ID="lblZipCode" class="col-4 col-form-label" runat="server">Zip Code</asp:Label>
                    <div class="col-4">
                        <div class="input-group">
                            <asp:TextBox ID="txtZipCode" name="txtZipCode" placeholder="52100" type="text" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-append">
                                <div class="input-group-text">
                                    <i class="fa "></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtZipCode" Display="Dynamic" ErrorMessage="Zip Code is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtZipCode" ErrorMessage="Zip Code should be 5 digits" ValidationExpression="^[0-9]{5}$" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                </p>
            </div>


            <div class="row" id="section2">
                <h3 class="mt-2 mb-4">Payment Info</h3>
                <div class="form-group row">
                    <asp:Label ID="lblPaymentMethod" class="col-4 col-form-label" runat="server">Payment Method</asp:Label>

                    <div class="col-8" style="display: inline-flex">
                        <asp:RadioButtonList ValidateRequestMode="Enabled" class="rbl" ID="rblPayment" runat="server" RepeatColumns="2">
                            <asp:ListItem Value="Visa"><img style="width:50px; height:40px;" src="https://blackbullmarkets.com/wp-content/uploads/2020/09/Visa-logo.png"/></asp:ListItem>
                            <asp:ListItem Value="Master"><img style="width:50px; height:45px; " src="https://brand.mastercard.com/content/dam/mccom/brandcenter/thumbnails/mastercard_vrt_pos_92px_2x.png"/></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>

                </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rblPayment" Display="Dynamic" ErrorMessage="Payment Mothod is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </p>

                <br />
                <div class="form-group row">
                    <asp:Label ID="lblCardholderName" class="col-4 col-form-label" runat="server">Cardholder Name</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtCardholderName" name="Cardholder Name" type="text" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-append">
                                <div class="input-group-text">
                                    <i class="fa fa-address-card-o"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCardholderName" Display="Dynamic" ErrorMessage="Name On Card is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCardholderName" ErrorMessage="Must be in characters" ValidationExpression="^[a-zA-Z ]{1,50}$" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                </p>

                <div class="form-group row">
                    <asp:Label ID="lblCardNumber" class="col-4 col-form-label" runat="server">Card Number</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtCardNumber" name="txtCardNumber" type="text" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-append">
                                <div class="input-group-text">
                                    <i class="fa fa-credit-card-alt"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCardNumber" Display="Dynamic" ErrorMessage="Card Number is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtCardNumber" Display="Dynamic" OnServerValidate="CustomValidator1_ServerValidate" ForeColor="Red" ErrorMessage="Card Number should start with 4 for Visa or 5 for Master" ClientValidationFunction="CustomValidator1_ClientValidate"></asp:CustomValidator>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txtCardNumber" Display="Dynamic" ForeColor="Red" ErrorMessage="Card Number should be 16 digits" ClientValidationFunction="CustomValidator2_ClientValidate"></asp:CustomValidator>
                </p>

                <div class="form-group row">
                    <asp:Label ID="lblExpirationDate" class="col-4 col-form-label" runat="server">Expiration Date</asp:Label>
                    <div class="col-3">
                        <asp:DropDownList class="form-select" ID="ddlMonth" runat="server">
                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>11</asp:ListItem>
                            <asp:ListItem>12</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-3">
                        <asp:DropDownList class="form-select" ID="ddlYear" runat="server" >
                            <asp:ListItem Text="" Selected="True"></asp:ListItem>
                            <asp:ListItem>2021</asp:ListItem>
                            <asp:ListItem>2022</asp:ListItem>
                            <asp:ListItem>2023</asp:ListItem>
                            <asp:ListItem>2024</asp:ListItem>
                            <asp:ListItem>2025</asp:ListItem>
                            <asp:ListItem>2026</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlMonth" Display="Dynamic" ErrorMessage="Expiration Date of Month is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="ddlMonth" Display="Dynamic" OnServerValidate="CustomValidator3_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                </p>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlYear" Display="Dynamic" ErrorMessage="Expiration Date of Year is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </p>

                <br />
                <div class="form-group row">
                    <asp:Label ID="lblCVV" class="col-4 col-form-label" runat="server">CVV</asp:Label>
                    <div class="col-4">
                        <div class="input-group">
                            <asp:TextBox ID="txtCVV" name="txtCVV" type="text" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-append">
                                <div class="input-group-text">
                                    <i class="fa "></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCVV" Display="Dynamic" ErrorMessage="CVV is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCVV" Display="Dynamic" ErrorMessage="CVV should be 3 digits" ValidationExpression="^[0-9]{3}$" ForeColor="Red"></asp:RegularExpressionValidator>
                </p>
            </div>


            <div class="row" style="font-size: 16px">
                <h3 class="mt-2 mb-4">Purchase Info</h3>
                <div class="tab-pane" id="checkout">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <HeaderTemplate>
                            <table class="table w-100 p3 ms-auto me-auto">
                                <thead>
                                    <tr>
                                        <th style="width: 40%; text-align: center" scope="col">Product</th>
                                        <th style="width: 5%; text-align: center" scope="col">Quantity</th>
                                        <th style="width: 30%; text-align: center" scope="col">Unit Price</th>
                                        <th style="width: 25%; text-align: right" scope="col">Price</th>
                                    </tr>
                                </thead>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table class="table w-100 p3 ms-auto me-auto">
                                <tr>
                                    <td style="width: 40%; text-align: center" class="align-middle"><%# Eval("ProductName")%></td>
                                    <td style="width: 5%; text-align: center" class="align-middle"><%# Eval("Quantity")%></td>
                                    <td style="width: 30%; text-align: center" class="align-middle"><%# Eval("Price")%></td>
                                    <td style="width: 25%; text-align: right" class="align-middle"><%# Eval("TotalPrice", "{0:0.00}") %></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                    <br />

                    <table style="border: 0px solid transparent;" class="table w-100 p3 ms-auto me-auto">
                        <tr>
                            <td style="width: 60%">
                                <asp:Label ID="lblSubtotalDis" runat="server" Text="Subtotal"></asp:Label></td>

                            <td style="width: 10%" class="align-middle"></td>

                            <td style="width: 30%">
                                <asp:Label Style="display: block; text-align: right" ID="lblSubtotal" runat="server" Text="RM???"></asp:Label></td>
                        </tr>

                        <tr>
                            <td style="width: 60%">
                                <asp:Label ID="lblTaxDis" runat="server" Text="Tax"></asp:Label>&nbsp;(6%)</td>

                            <td style="width: 10%" class="align-middle"></td>

                            <td style="width: 30%">
                                <asp:Label Style="display: block; text-align: right" ID="lblTax" runat="server" Text="RM???"></asp:Label></td>
                        </tr>

                        <tr>
                            <td style="width: 60%">
                                <asp:Label ID="lblTotalDis" runat="server" Text="Total"></asp:Label></td>

                            <td style="width: 10%" class="align-middle"></td>

                            <td class="align-middle" style="width: 30%">
                                <asp:Label Style="display: block; text-align: right" ID="lblTotal" runat="server" Text="RM???"></asp:Label></td>
                        </tr>

                    </table>
                </div>
                <asp:Button class="btn btn-primary btn-lg btn-brand" ID="btnCheckout" runat="server" Text="Complete Payment" OnClick="btnCheckout_Click" />
                <asp:Button class="btn btn-danger btn-lg btn-brand" ID="btnContinue" runat="server" Text="Continue Shopping" OnClick="btnContinue_Click" CausesValidation="False" ValidationGroup="First" />

            </div>
        </div>
    </form>
</asp:Content>
