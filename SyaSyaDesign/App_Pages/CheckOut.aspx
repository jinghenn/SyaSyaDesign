<%@ Page Title="Check Out" Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="SyaSyaDesign.App_Pages.CheckOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="form1" runat="server">

        <h1 style="text-align: center">Checkout</h1>

        <div class="row" style="margin-left=16dp;">

            <div class="row" id="section1">
                <h3 class="mt-2 mb-4">Delivery Info</h3>

                <div class="form-group row">
                    <asp:Label ID="lblRecipientName" class="col-4 col-form-label" runat="server">Recipient Name</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtName" name="txtName" placeholder="Alice" type="text" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-text">
                                <i class="bi bi-file-person"></i>
                            </div>
                        </div>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Recipent Name is required" ForeColor="Red"></asp:RequiredFieldValidator>
                            &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtName" ErrorMessage="Must be in characters" ValidationExpression="^[a-zA-Z ]{1,50}$" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                        </p>

                    </div>
                </div>

                <div class="form-group row">
                    <asp:Label ID="lblEmail" class="col-4 col-form-label" runat="server">Email Address</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <asp:TextBox runat="server" ID="txtEmail" name="text" placeholder="abc123@gmail.com" type="text" class="form-control" TextMode="Email"></asp:TextBox>
                            <div class="input-group-text">
                                <i class="bi bi-envelope"></i>
                            </div>
                        </div>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Email is required" ForeColor="Red"></asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator ID="emailRegex" runat="server" ControlToValidate="txtEmail"
                                ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                ErrorMessage="Invalid email address" Display="Dynamic" />
                        </p>

                    </div>
                </div>

                <div class="form-group row">
                    <asp:Label runat="server" ID="lblContactNumber" class="col-4 col-form-label">Contact Number</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <div class="input-group-text">+60</div>
                            </div>
                            <asp:TextBox ID="txtContactNumber" name="txtContactNumber" placeholder="123456789" type="text" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-text">
                                <i class="bi bi-telephone"></i>
                            </div>
                        </div>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtContactNumber" Display="Dynamic" ErrorMessage="Contact Number is required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtContactNumber" Display="Dynamic" ErrorMessage="Invalid Format (eg. +60 123456789)" ValidationExpression="^[0-9]{9,10}$" ForeColor="Red"></asp:RegularExpressionValidator>
                        </p>


                    </div>
                </div>
                <div class="form-group row">
                    <asp:Label for="lblDeliveryAddress" class="col-4 col-form-label" runat="server">Delivery Address</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtDeliveryAddress" name="txtDeliveryAddress" placeholder="88, Jalan Malinja 1, Setapak" type="text" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            <div class="input-group-text">
                                <i class="bi bi-house-door"></i>
                            </div>
                        </div>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtDeliveryAddress" Display="Dynamic" ErrorMessage="Deliver Address is required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </p>

                    </div>
                </div>

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
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlState" Display="Dynamic" ErrorMessage="State is required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </p>


                    </div>
                </div>

                <div class="form-group row">
                    <asp:Label ID="lblCity" class="col-4 col-form-label" runat="server">City</asp:Label>
                    <div class="col-8">
                        <div class="input-group">
                            <asp:TextBox ID="txtCity" name="txtCity" placeholder="Kuala Lumpur" type="text" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-text">
                                <i class="bi bi-building"></i>
                            </div>
                        </div>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtCity" Display="Dynamic" ErrorMessage="City is required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </p>


                    </div>
                </div>
                <div class="form-group row">
                    <asp:Label ID="lblZipCode" class="col-4 col-form-label" runat="server">Zip Code</asp:Label>
                    <div class="col-4">
                        <div class="input-group">
                            <asp:TextBox ID="txtZipCode" name="txtZipCode" placeholder="52100" type="text" class="form-control" runat="server"></asp:TextBox>
                            <div class="input-group-text">
                                <i class="bi bi-building"></i>
                            </div>
                        </div>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtZipCode" Display="Dynamic" ErrorMessage="Zip Code is required" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtZipCode" ErrorMessage="Zip Code should be 5 digits" ValidationExpression="^[0-9]{5}$" ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                        </p>

                    </div>
                </div>
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
