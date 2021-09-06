<%@ Page Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="Attributes.aspx.cs" Inherits="SyaSyaDesign.Attributes"%>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <style>
        .btn:hover {
            color: white;
        }

        /*.modal {
            max-width: 600px;
        }

        .center {
            position: absolute;
            left: 50%;
            top: 50%;
            transform: translate(-50%, -50%);
        }*/

        .width-btn{
            width: 45px;
        }

        table.table tr th{background-color:black !important; color:white !important;}
    </style>

    <script>
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()
        })

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

    <form runat="server">
        <div id="myModal" class="modal fade" tabindex="-1" aria-labelledby="modalLabel">
            <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h2>Create Attribute</h2>
                    <button type="button" class="btn btn-danger close" data-bs-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <asp:Table runat="server" Height="40">
                        <asp:TableRow>
                            <asp:TableCell>Description :</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="txtDescription" runat="server" placeholder="Description">
                                </asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Status :</asp:TableCell>
                            <asp:TableCell>
                                <asp:RadioButton ID="RadioButton1" runat="server" Text="Active" GroupName="status" Checked="true" />
                                <asp:RadioButton ID="RadioButton2" runat="server" Text="Deactive" GroupName="status" CssClass="p-xxl-5" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <asp:Button runat="server" ID="BtnAdd" CssClass="btn btn-success" Text="Add" Style="float: right;" OnClick="BtnAdd_Click" />
                </div>
                <div class="modal-footer">
                    <p>To add your attribute</p>
                </div>
            </div>
                </div>
        </div>
        <div class="container">
            <div class="card input-group" style="margin-bottom: 10px;">
                <div class="card-body" style="border: solid; border-radius: 5px; border: brown 0.5px; margin-right: 5px;">
                    <h3>Attribute </h3>
                    <div class="container" style="margin-right: 5px; margin-top: 25px;">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Button ID="AddAttributeButton" runat="server" ClientIDMode="Static" CssClass="btn btn-primary float-end" Text="Create New" data-bs-toggle="modal" data-bs-target="#myModal" />
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 25px">
                        <div class="table-responsive table-hover">
                            <asp:GridView ID="TableAttribute" AutoGenerateColumns="false" GridLines="Both" HorizontalAlign="Center" OnRowCommand="TableAttribute_RowCommand" OnRowUpdating="TableAttribute_RowUpdating" ShowHeaderWhenEmpty="true"
                                OnRowCancelingEdit="TableAttribute_RowCancelingEdit" OnRowEditing="TableAttribute_RowEditing"
                                Font-Names="Verdana" CellPadding="15" CellSpacing="0" runat="server" class="table table-striped" OnRowDataBound="OnRowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" runat="server" CssClass="align-middle" />
                                            <asp:HiddenField ID="AttributeID" runat="server" Value='<%# Eval("AttributeID").ToString()%>'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" CssClass="align-middle" Text='<%# Eval("Description").ToString() %>' />
                                            </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="w-100" Text='<%#Eval("Description") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LabelStatus" CssClass="align-middle" Text='<%# (Boolean.Parse(Eval("IsActive").ToString())) ? "Active" : "Inactive"%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="col-md-4">
                                        <ItemTemplate>
                                            <asp:Button ID="EditButton" CssClass="btn-lg btn-info align-middle fa width-btn" Text="&#xf040;" runat="server" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit" CommandName="Edit"/>
                                            <asp:Button ID="ActivateButton" Text="&#xf007;" CssClass="btn-lg btn-success align-middle fa width-btn" CommandName="Activate"  OnClientClick='return confirm("Are you confirm to activate?");' CommandArgument='<%# Eval("AttributeID").ToString()%>' runat="server" Visible='<%# !Boolean.Parse(Eval("IsActive").ToString())%>' data-bs-toggle="tooltip" data-bs-placement="top" title="Activate" />
                                            <asp:Button ID="DectivateButton" Text="&#xf235;" CssClass="btn-lg btn-danger align-middle fa width-btn" CommandName="Deactivate"  OnClientClick='return confirm("Are you confirm to deactivate?");' CommandArgument='<%# Eval("AttributeID").ToString()%>'  runat="server" Visible='<%# Boolean.Parse(Eval("IsActive").ToString())%>' data-bs-toggle="tooltip" data-bs-placement="top" title="Deactivate" />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Button runat="server" ID="btnupdate" Text="&#xf00c;" CssClass="fa btn-lg btn-primary" ToolTip="Update" CommandName="update" OnClientClick='return confirm("Are you confirm to save it?");' CommandArgument='<%# Eval("AttributeID").ToString()%>'/>
                                            <asp:Button runat="server" ID="btncancel" Text="&#xf00d;" CommandName="cancel" CssClass="btn-lg btn-danger align-middle fa" ToolTip="Cancel" CommandArgument='<%# Eval("AttributeID").ToString()%>'/>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Record Available</EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Button runat="server" ID="BtnBackList" Text="Back To List" OnClick="BtnBackList_Click" CssClass="btn btn-outline-primary w-25"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script src="../lib/js/tools.js" type="text/javascript"></script>

</asp:Content>
