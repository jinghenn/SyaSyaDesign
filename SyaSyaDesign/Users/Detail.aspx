<%@ Page Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="SyaSyaDesign.Detail" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Product</title>
    <style>
        .img {
            object-fit: cover;
            object-position: center;
            margin: auto;
            width: 350px;
            height: 550px;
        }

        .disabled {
            opacity: 70%;
        }

        .enabled {
            opacity: 100%;
        }

        .product-name {
            font-weight: 300;
            font-size: 34px;
        }

        .label {
            font-size: 16px;
            font-weight: 800;
        }

        @media(max-width: 1000px) {
            .img {
                height: 300px;
            }
        }

        @media(max-width:800px) {
            .product-name {
                font-size: 22px;
            }
        }

        .btn-group > .badge {
            margin-right: 5px;
            margin-top: 8px;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
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
    </script>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="ProductDetailform" runat="server">
        <div class="container px-4">
            <div class="d-flex justify-content-around">
                <asp:Image ID="imgProduct" CssClass="img my-5" runat="server" />
                <div class="d-flex ms-3 my-5 flex-column w-50 justify-content-between">
                    <div>
                        <asp:Label ID="lblProductName" CssClass="product-name my-3" Text="" runat="server" />
                        <br />
                        <asp:Label ID="lblPrice" Text="" CssClass="label mt-5" runat="server" />
                        <hr />

                        <asp:Label Text="COLOR" CssClass="label" runat="server" />
                        <div style="display: flex; flex-wrap: wrap;">
                            <div class="btn-group" role="group" aria-label="Basic radio toggle button group">
                                <asp:RadioButtonList ID="rblColor" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rblColor_SelectedIndexChanged" AutoPostBack="True">
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <hr />
                    </div>
                    <div>
                        <asp:Label Text="SIZE" CssClass="label" runat="server" />
                        <div style="display: flex; flex-wrap: wrap;">
                            <div class="btn-group" role="group" aria-label="Basic radio toggle button group">
                                <asp:RadioButtonList ID="rblSize" runat="server" RepeatDirection="Horizontal"
                                    OnSelectedIndexChanged="rblSize_SelectedIndexChanged" AutoPostBack="True">
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <hr />
                    </div>
                    <div>
                        <asp:Label Text="TAGS" CssClass="label" runat="server" />
                        <div style="display: flex; flex-wrap: wrap;">
                            <asp:Repeater runat="server" ID="RptTags" DataSourceID="SqlDataSource1">
                                <ItemTemplate>
                                    <h5><span class="badge rounded-pill bg-warning" style="margin-right: 5px; margin-top: 8px;"><%# Eval("Description") %></span></h5>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:syasyadbConnection %>"
                            SelectCommand="select distinct ar.Description
from ProductAttribute pa
join attribute ar on(ar.AttributeID = pa.AttributeID)
where pa.product_id = @prod">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="5" Name="prod" Type="Int32" />
                            </SelectParameters>

                        </asp:SqlDataSource>
                        <hr style="margin-top: 5px;" />
                    </div>

                    <div class="container">
                        <div class="d-flex flex-row align-items-center flex-wrap">
                            <div class="me-3 d-flex flex-nowrap mb-3" style="width: 130px">
                                <asp:Button ID="btnMinus" ClientIDMode="Static" CssClass="btn btn-outline-secondary" runat="server" Text="-" OnClientClick="return minusQty();" />
                                <asp:TextBox ID="txtQuantity" ClientIDMode="Static" CssClass="form-control" runat="server" Text="1" />
                                <asp:Button runat="server" ID="btnPlus" ClientIDMode="Static" CssClass="btn btn-outline-secondary" Text="+" OnClientClick="return addQty();" />
                            </div>
                            <asp:Button ID="btn_add_cart" CssClass="btn-dark mx-auto flex-grow-1 mb-3 py-3 rounded-1 fw-bold"
                                Text="ADD TO CART" OnClick="btn_add_cart_Click" OnClientClick="return checkQuantity()" runat="server" />
                        </div>
                        <asp:Label ID="lblAlreadyInCart" Visible="false" runat="server" Text="Already in cart." />
                    </div>
                    <div class="w-100">
                        <asp:Label Text="Product Measurement" CssClass="label mt-5" runat="server" />
                        <table class="table table-bordered table-sm text-uppercase table-responsive-lg text-center" style="font-size: 12px">
                            <tr class="fw-bold">
                                <td>size</td>
                                <td>Shoulder</td>
                                <td>Bust</td>
                                <td>Sleeve</td>
                                <td>Length</td>
                                <td>Cuff</td>
                            </tr>
                            <tr>
                                <td class="fw-bold">0XL</td>
                                <td>44</td>
                                <td>114</td>
                                <td>23.5</td>
                                <td>66</td>
                                <td>30</td>
                            </tr>
                            <tr>
                                <td class="fw-bold">1XL</td>
                                <td>45.5</td>
                                <td>120</td>
                                <td>24</td>
                                <td>67.5 </td>
                                <td>32 </td>
                            </tr>
                            <tr>
                                <td class="fw-bold">2XL</td>
                                <td>47</td>
                                <td>126</td>
                                <td>24.5</td>
                                <td>69</td>
                                <td>34</td>
                            </tr>
                            <tr>
                                <td class="fw-bold">3XL</td>
                                <td>48.5</td>
                                <td>132</td>
                                <td>25</td>
                                <td>70.5</td>
                                <td>36</td>
                            </tr>
                            <tr>
                                <td class="fw-bold">4XL</td>
                                <td>50</td>
                                <td>138</td>
                                <td>25.5</td>
                                <td>72</td>
                                <td>38</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script>
        $(document).ready(function () {
            //$("#btnMinus").click(function () {
            //    event.preventDefault();
            //    $("input[name$='txtQuantity']").val(function (i, oldval) {
            //        return parseInt(oldval, 10) + 1;
            //    });
            //});



            //$('.btn-group input').on('click', function (event) {
            //        $('input[type=radio]').prop('checked', false);

            //        $(this).prop('checked', true);

            //        $("hiddenIndex").val(this.nextSibling.innerHTML);
            //});

            $('[id^=ContentPlaceHolder_RepeaterSize_ActiveBtn]').on('click', function (event) {
                $('[id^=ContentPlaceHolder_Repeater1_ActivecBtn]').prop('checked', false);

                $(this).prop('checked', true);

                $('#ContentPlaceHolder_hiddenID').val(this.nextSibling.innerHTML);
            });

            $('[id^=ContentPlaceHolder_Repeater1_ActivecColorBtn]').on('click', function (event) {
                $('[id^=ContentPlaceHolder_Repeater1_ActivecColorBtn]').prop('checked', false);

                $(this).prop('checked', true);

                $('#ContentPlaceHolder_hiddenColor').val(this.nextSibling.innerHTML);
            });

        });

        function checkQuantity() {
            var quantity = $("input[name$='txtQuantity']").val();
            return parseInt(quantity) > 0;
        }

        function addQty() {
            var quantity = parseInt($("#txtQuantity").val());
            quantity++;

            if (quantity > 1) {
                $("#btnMinus").prop("disabled", false);
            }
            if (quantity > 10) {
                $("#btnPlus").prop("disabled", true);
            }
            else $("#txtQuantity").val(quantity.toString());

            return false;
        }

        function minusQty() {
            var quantity = parseInt($("#txtQuantity").val());
            quantity--;

            if (quantity < 1) {
                $("#btnMinus").prop("disabled", false);
            }
            if (quantity > 0) $("#txtQuantity").val(quantity.toString());

            return false;
        }

        $(function () {
            $("[id^=ContentPlaceHolder_RepeaterSize_ActiveBtn]").prop("name", "GrpName");
            $("[id^=ContentPlaceHolder_Repeater1_ActivecColorBtn]").prop("name", "ColorGrp");
            $(".btn-group input").addClass("btn-check");
            $(".btn-group label").addClass("btn btn-outline-primary");
        });

    </script>
</asp:Content>
