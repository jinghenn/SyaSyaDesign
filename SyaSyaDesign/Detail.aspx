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
    </style>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="ProductDetailform" runat="server">
        <div class="container px-4">
            <div class="d-flex justify-content-around">
                <img class="img my-5" src="https://via.placeholder.com/300x600" />
                <div class="d-flex ms-3 my-5 flex-column w-50 justify-content-between">
                    <div>
                        <asp:Label ID="lblProductName" CssClass="product-name my-3" Text="" runat="server" />
                        <br />
                        <asp:Label ID="lblPrice" Text="" CssClass="label mt-5" runat="server" />
                        <hr />
                    </div>
                    
                    <div>
                        <asp:Label Text="SIZE" CssClass="label" runat="server" />
                        <hr />
                    </div>
                    
                    <div class="container">
                        <asp:Button ID="btn_add_cart" CssClass="btn-dark w-100 mx-auto py-3 rounded-1 fw-bold" Text="ADD TO CART" runat="server" />
                    </div>
                    <div class="w-100">
                        <asp:Label Text="Product Measurement" CssClass="label mt-5" runat="server" />
                        <table class="table table-bordered table-sm text-uppercase table-responsive-lg text-center" style="font-size:12px">
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
</asp:Content>
