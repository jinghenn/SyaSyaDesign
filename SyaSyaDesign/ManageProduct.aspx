<%@ Page Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="ManageProduct.aspx.cs" Inherits="SyaSyaDesign.ManageProduct" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Manage</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <style>
        .product-table {
            height: 500px;
            overflow-y: scroll;
        }

        html {
            overflow: scroll;
            overflow-x: hidden;
        }

        ::-webkit-scrollbar {
            width: 0; /* Remove scrollbar space */
            background: transparent; /* Optional: just make scrollbar invisible */
        }
    </style>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="form1" runat="server">
        <div class="container">
            <div class="product-table">

                <asp:GridView ID="gvProduct" CssClass="table table-sm table-striped" runat="server" AutoGenerateColumns="false" OnRowUpdating="gvProduct_RowUpdating"
                    OnRowEditing="gvProduct_RowEditing" OnRowCancelingEdit="gvProduct_RowCancelingEdit" OnRowDataBound="gvProduct_RowDataBound" OnRowDeleting="gvProduct_RowDeleting">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <p class="fw-bold">Product ID</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProductID" CssClass="text-center td-center" runat="server" Text='<%# Eval("product_id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <p class="fw-bold">Product Name</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("product_name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtProductName" CssClass="form-control form-control-sm" runat="server" Text='<%# Eval("product_name") %>' />
                                <asp:RequiredFieldValidator CssClass="text-danger" Display="dynamic" runat="server"
                                    ErrorMessage="*Required" ControlToValidate="txtProductName" ValidationGroup="product" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <p class="fw-bold">Price</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPrice" CssClass="text-center td-center" runat="server" Text='<%# Eval("price") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPrice" CssClass="form-control form-control-sm" runat="server" Text='<%# Eval("price") %>' />
                                <asp:CompareValidator CssClass="text-danger" runat="server" Operator="DataTypeCheck" Type="Double" ControlToValidate="txtPrice" ErrorMessage="Invalid Price" Display="Dynamic" ValidationGroup="product" />
                                <asp:RangeValidator CssClass="text-danger" runat="server" Type="Double" MinimumValue="0" MaximumValue="999999999" ControlToValidate="txtPrice" ErrorMessage="Invalid price range" Display="Dynamic" ValidationGroup="product" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <p class="fw-bold">Quantity</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" CssClass="text-center td-center" runat="server" Text='<%# Eval("quantity") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQuantity" CssClass="form-control form-control-sm" runat="server" Text='<%# Eval("quantity") %>' />
                                <asp:CompareValidator CssClass="text-danger" ID="vldQuantityType" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtQuantity" ErrorMessage="Invalid quantity" Display="Dynamic" ValidationGroup="product" />
                                <asp:RangeValidator CssClass="text-danger" ID="vldQuantityRange" runat="server" Type="Integer" MinimumValue="0" MaximumValue="999999999" ControlToValidate="txtQuantity" ErrorMessage="Invalid range" Display="Dynamic" ValidationGroup="product" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <p class="fw-bold">Attribute</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="container">
                                    <asp:Label ID="lblAttributeId" CssClass="text-center mx-auto" runat="server" Text='<%# Eval("attribute_id") %>' />
                                </div>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAttributeId" CssClass="form-control form-control-sm" runat="server" Text='<%# Eval("attribute_id") %>' />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <p class="fw-bold">Product Category</p>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("category_name") %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:HiddenField ID="categoryID" runat="server" Value='<%#Eval("category_id") %>' />
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control form-control-sm"
                                    DataSourceID="dsCategory" DataTextField="category_name" DataValueField="category_id">
                                </asp:DropDownList>

                            </EditItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="d-flex flex-nowrap">
                                    <asp:LinkButton ID="btnEdit" CssClass="btn btn-primary btn-sm" runat="server" CommandName="Edit">
                                        <i class="bi bi-pencil"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" CssClass="btn btn-danger btn-sm" runat="server" CommandName="Delete">
                                        <i class="bi bi-trash"></i>
                                    </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <div class="d-flex flex-nowrap">
                                    <asp:LinkButton ID="btnUpdate" CssClass="btn btn-success btn-sm" runat="server" CommandName="Update" ValidationGroup="product">
                                        <i class="bi bi-check"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-danger btn-sm" Text="Cancel" runat="server" CommandName="Cancel">
                                        <i class="bi bi-x"></i>
                                    </asp:LinkButton>
                                </div>
                            </EditItemTemplate>

                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>
            </div>
            <div class="d-flex w-50 flex-column mx-auto">
                <div class="mb-3">
                    <label class="form-label">New Product Name</label>
                    <asp:TextBox ID="txtNewProductName" CssClass="form-control form-control-sm" runat="server" />
                    <asp:RequiredFieldValidator CssClass="text-danger" Display="dynamic" runat="server"
                        ErrorMessage="*Required" ControlToValidate="txtNewProductName" ValidationGroup="newProduct" />
                </div>
                <div class="d-flex justify-content-between">
                    <div class="mb-3 w-50 me-2">
                        <label class="form-label">Price</label>
                        <asp:TextBox ID="txtNewPrice" CssClass="form-control form-control-sm" runat="server" />
                        <asp:CompareValidator CssClass="text-danger" runat="server" Operator="DataTypeCheck" Type="Double" ControlToValidate="txtNewPrice"
                            ErrorMessage="*Invalid Price" Display="Dynamic" ValidationGroup="newProduct" />
                        <asp:RangeValidator CssClass="text-danger" runat="server" Type="Double" MinimumValue="0" MaximumValue="999999999"
                            ControlToValidate="txtNewPrice" ErrorMessage="*Invalid price range" Display="Dynamic" ValidationGroup="newProduct" />
                        <asp:RequiredFieldValidator CssClass="text-danger" Display="dynamic" runat="server"
                            ErrorMessage="*Required" ControlToValidate="txtNewPrice" ValidationGroup="newProduct" />
                    </div>

                    <div class="mb-3 w-50 me-2">
                        <label class="form-label">Quantity</label>
                        <asp:TextBox ID="txtNewQuantity" CssClass="form-control form-control-sm" runat="server" />
                        <asp:CompareValidator CssClass="text-danger" ID="vldQuantityType" runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="txtNewQuantity"
                            ErrorMessage="*Invalid quantity" Display="Dynamic" ValidationGroup="newProduct" />
                        <asp:RangeValidator CssClass="text-danger" ID="vldQuantityRange" runat="server" Type="Integer" MinimumValue="0" MaximumValue="999999999"
                            ControlToValidate="txtNewQuantity" ErrorMessage="*Invalid range" Display="Dynamic" ValidationGroup="newProduct" />
                        <asp:RequiredFieldValidator CssClass="text-danger" Display="dynamic" runat="server"
                            ErrorMessage="*Required" ControlToValidate="txtNewQuantity" ValidationGroup="newProduct" />
                    </div>
                </div>
                <div class="d-flex justify-content-between">
                    <div class="mb-3 w-50 me-2">
                        <label class="form-label">Attribute</label>
                        <asp:TextBox ID="txtNewAttributeId" CssClass="form-control form-control-sm" runat="server" />
                    </div>
                    <div class="mb-3 w-50">
                        <label class="form-label">Category</label>
                        <asp:DropDownList ID="ddlNewCategory" runat="server" CssClass="form-control form-control-sm w-100"
                            DataSourceID="dsCategory" DataTextField="category_name" DataValueField="category_id">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="mb-3 mx-auto">
                    <asp:LinkButton ID="btnInsert" CssClass="btn btn-success btn-sm" runat="server" OnClick="btnInsert_Click" ValidationGroup="newProduct">
                    <i class="bi bi-plus"></i>Insert
                    </asp:LinkButton>
                </div>
            </div>
            <asp:SqlDataSource ID="dsCategory" runat="server" ConnectionString="Data Source=syasyadesign.database.windows.net;Initial Catalog=syasyadb;User ID=syasya;Password=Ssdesign12@34;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [ProductCategory]"></asp:SqlDataSource>
        </div>
    </form>
    <script>

</script>

</asp:Content>

