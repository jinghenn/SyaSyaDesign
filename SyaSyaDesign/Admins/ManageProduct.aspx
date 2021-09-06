<%@ Page Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="ManageProduct.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="SyaSyaDesign.Admins.ManageProduct" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>Manage</title>

    <link href="https://cdn.rawgit.com/harvesthq/chosen/gh-pages/chosen.min.css" rel="stylesheet" />


    <style>
        .product-table {
            /*overflow-y: scroll;*/
            /*word-break: break-all;*/
            overflow-x:scroll;
        }

        html {
            overflow: scroll;
            overflow-x: hidden;
        }

        ::-webkit-scrollbar {
            width: 0; /* Remove scrollbar space */
            background: transparent; /* Optional: just make scrollbar invisible */
        }

        .modal-dialog {
            width: auto !important;
        }
    </style>

    <script>
        function ShowStatus() {
            $(document).ready(function () {
                $("#myModal").modal("show");
            })
        }
    </script>

</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="form1" runat="server">
        <div id="myModal" class="modal fade" tabindex="-1" aria-labelledby="modalLabel">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h2>Quantity Management</h2>
                        <button type="button" class="btn btn-danger close" data-bs-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <div class="row col-md-12">
                            <asp:Label ID="ProdID" EnableViewState="true" runat="server" ClientIDMode="Static" Visible="false"></asp:Label>
                            <asp:Label ID="ProdDesc" EnableViewState="true" runat="server" ClientIDMode="Static"></asp:Label>
                        </div>
                        <br />
                        <div class="row col-md-12">
                            <asp:GridView runat="server" ID="TableQuantity" AutoGenerateColumns="false" CssClass="table table-sm table-striped w-auto" EmptyDataText="No Records.">
                                <Columns>
                                    <asp:TemplateField ControlStyle-CssClass="col-md-8">
                                        <HeaderTemplate>Size</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtSize" Text='<%# Eval("size") %>' Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-CssClass="col-md-8">
                                        <HeaderTemplate>Color</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtColor" Text='<%# Eval("color") %>' Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-CssClass="col-md-8">
                                        <HeaderTemplate>Quantity</HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("quantity") %>' Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <br />
                        <br />
                        <div class="row card">
                            <h4>Manage quantity</h4>
                            <asp:Table runat="server" Height="40">
                                <asp:TableRow>
                                    <asp:TableCell>Size :</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlSizeAdd" runat="server"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>Color :</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="DropDownListColor" runat="server"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>Quantity :</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtQtyAdd" runat="server"></asp:TextBox>
                                        <asp:RangeValidator CssClass="text-danger" ID="RangeValidator1" runat="server" Type="Integer" MinimumValue="-99" MaximumValue="999999999" ControlToValidate="txtQtyAdd" ErrorMessage="Invalid range" Display="Dynamic" ValidationGroup="product" />
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
                            <div class="row mb-3 mx-auto">
                                <asp:Button runat="server" ID="BtnAddQty" CssClass="btn btn-success w-25 " Text="Update" Style="float: right;" OnClick="BtnAddQty_Click" />
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row card">
                            <h4>Add Product Details</h4>
                            <asp:Table runat="server" Height="40">
                                <asp:TableRow>
                                    <asp:TableCell>Size :</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlSizeList" runat="server"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>Color :</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:DropDownList ID="ddlColorList" runat="server"></asp:DropDownList>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>Quantity :</asp:TableCell>
                                    <asp:TableCell>
                                        <asp:TextBox ID="txtnewQty" runat="server"></asp:TextBox>
                                        <asp:RangeValidator CssClass="text-danger" ID="RangeValidator2" runat="server" Type="Integer" MinimumValue="-99" MaximumValue="999999999" ControlToValidate="txtnewQty" ErrorMessage="Invalid range" Display="Dynamic" ValidationGroup="product" />
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
                            <div class="row mb-3 mx-auto">
                                <asp:Button runat="server" ID="Button1" CssClass="btn btn-success w-25 " Text="Add" Style="float: right;" OnClick="ButtonNewProdDetails_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <p>Control your quantity</p>
                    </div>
                </div>
            </div>
        </div>

        <div id="newModal" class="modal fade" tabindex="-1" aria-labelledby="modalLabel">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <h2>Insertion</h2>
                        <button type="button" class="btn btn-danger close" data-bs-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">
                        <br />
                        <div class="d-flex w-50 flex-column mx-auto">
                            <div class="mb-3">
                                <label class="form-label">New Product Name</label>
                                <asp:TextBox ID="txtNewProductName" CssClass="form-control form-control-sm" runat="server" />
                                <%--<asp:RequiredFieldValidator CssClass="text-danger" Display="dynamic" runat="server"
                        ErrorMessage="*Required" ControlToValidate="txtNewProductName" ValidationGroup="newProduct" />--%>
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
                            </div>
                            <div class="d-flex justify-content-between">
                                <%--<div class="mb-3 w-50 me-2">
                        <label class="form-label">Size</label>
                        <asp:DropDownList ID="ddlSizeList" runat="server" CssClass="form-control-sm">
                        </asp:DropDownList>
                    </div>
                    <div class="mb-3 w-50 me-2">
                        <label class="form-label">Color</label>
                        <asp:DropDownList ID="ddlColorList" runat="server" CssClass="form-control-sm">
                        </asp:DropDownList>
                    </div>--%>
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
                    </div>
                    <div class="modal-footer">
                        <p>Add new peoduct</p>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="Hidden" runat="server"></asp:HiddenField>


        <div class="container">
            
                <div class="row" style="margin-top: 25px">
                        <div class="col-12">
                            <div class="card input-group" style="margin-bottom: 10px;">
                <div class="card-body" style="border: solid; border-radius: 5px; border: brown 0.5px; margin-right: 5px; width:inherit">
                    <%--<div class="product-table">--%>
                        <h3>Manage Product </h3>
                        <div class="container" style="margin-right: 5px; margin-top: 25px;">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Button ID="AddProductButton" runat="server" ClientIDMode="Static" CssClass="btn btn-primary float-end" OnClientClick="return false;" Text="Create New" data-bs-toggle="modal" data-bs-target="#newModal" />
                                </div>
                            </div>
                            <br />
                        </div>
                    <div class="row">
                        <div class="table-responsive">
                        <asp:GridView ID="gvProduct" CssClass="table product-table  table-striped" runat="server" AutoGenerateColumns="false" OnRowUpdating="gvProduct_RowUpdating"
                            OnRowEditing="gvProduct_RowEditing" OnRowCancelingEdit="gvProduct_RowCancelingEdit" OnRowDataBound="gvProduct_RowDataBound" OnRowDeleting="gvProduct_RowDeleting"
                            OnSelectedIndexChanged="gvProduct_SelectedIndexChanged">
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
                                        <asp:Label ID="lblProductName" runat="server" ClientIDMode="Static" Text='<%# Eval("product_name") %>' />
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

                                <%--<asp:TemplateField>
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
                        </asp:TemplateField>--%>

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
                                    <HeaderTemplate>
                                        Quantity Management
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button runat="server" CommandName="Select" CausesValidation="False" CommandArgument='<%#Eval("product_id") + "," + Eval("product_name") %>' ID="ModalLink" class="ModalClick" UseSubmitBehavior="false" Text="Manage" ClientIDMode="Static" CssClass="btn btn-primary"></asp:Button>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <p class="fw-bold">Attribute</p>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="container">
                                            <asp:TextBox ID="attributeRecord" runat="server" Enabled="false">
                                            </asp:TextBox>

                                            <%--<asp:Label ID="lblAttributeId" CssClass="text-center mx-auto" runat="server" Text='<%# Eval("attribute_id") %>' />--%>
                                        </div>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:ListBox ID="AttributeList" runat="server" multiple CssClass="chosen-select" Enabled="true" SelectionMode="multiple">
                                            <asp:ListItem>--Select--</asp:ListItem>
                                        </asp:ListBox>
                                        <%--<asp:TextBox ID="txtAttributeId" CssClass="form-control form-control-sm" runat="server" Text='<%# Eval("attribute_id") %>' />--%>
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
                    <%--</div>--%>
                    </div>
                    </div></div>
                    </div>
                </div>
            </div>

            <asp:SqlDataSource ID="dsCategory" runat="server" ConnectionString="Data Source=syasyadesign.database.windows.net;Initial Catalog=syasyadb;User ID=syasya;Password=Ssdesign12@34;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [ProductCategory]"></asp:SqlDataSource>
        </div>
    </form>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdn.rawgit.com/harvesthq/chosen/gh-pages/chosen.jquery.min.js"></script>
    <script>
        $(".chosen-select").chosen({
            no_results_text: "Oops, nothing found!"
        });

        <%--function changeID(id) {
            $("#ProdID").val(id);
            $('#<%=ProdID.ClientID%>').text = id;
            $("#ProdID").empty();
            $("#ProdID").append(id);
        }

        function changeName(desc) {
            $('#<%=ProdDesc.ClientID%>').text = desc;
            $("#ProdDesc").empty();
            $("#ProdDesc").append(desc);
        }--%>



    </script>
</asp:Content>

