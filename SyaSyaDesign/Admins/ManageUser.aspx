<%@ Page Title="" Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="SyaSyaDesign.Admins.ManageUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
      
        .button1 {
            background-color: white;
            text-align: left;
            border-radius: 0px;
            transition-duration: 0.4s;
            width:calc(100% - 15px); 
            height:70px; 
            border-width:0px; 
            padding-left:20px;  
            margin-left:15px;
        }

            .button1:hover {
                background-color: #D6E4F1; /* light blue */
                color: #0275d8;
            } 
        

        .button2 {
            background-color: #f5f3f0;
            text-align: left;
            border-radius: 0px;
            transition-duration: 0.4s;
            width:calc(100% - 15px); 
            height:70px; 
            border-width:0px; 
            padding-left:40px;  
            margin-left:15px;
        }

            .button2:hover {
                background-color: #D6E4F1; /* light blue */
                color: #0275d8;
            }
        
           

         .label1{
            width: 100%;            
            height: 70px; 
            padding:25px 0 0 30px;
         }
         .rightButton{
              float: right;
              margin-right:30px;
              
              
         }
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form runat="server">

     <div class="modal fade bd-example-modal-lg" id="modalForm">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Add Admin</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body">

                        <div class="row mb-3">
                            <label for="formUsername" class="col-sm-3 col-form-label">Username</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="formUsername" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="Add Form" ForeColor="Red" ID="rfvFormUsername" ControlToValidate="formUsername" runat="server" Display="Dynamic" ErrorMessage="Username Cannot Be Blank"></asp:RequiredFieldValidator>
                            </div>
                        </div>  

                        <div class="row mb-3">
                            <label for="formUserPassword" class="col-sm-3 col-form-label">User Password</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="formUserPassword" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="Add Form" ForeColor="Red" ID="rfvFormUserPassword" ControlToValidate="formUserPassword" runat="server" Display="Dynamic" ErrorMessage="User Password Cannot Be Blank"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        
                        <div class="row mb-3">
                            <label for="formddlUserType" class="col-sm-3 col-form-label">Art Category</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="formddlUserType" CssClass="form-select" runat="server">
                                    <asp:ListItem Value="Manager"> Manager </asp:ListItem>
                                    <asp:ListItem Value="Admin"> Admin </asp:ListItem>
                                 </asp:DropDownList> 

                            </div>
                        </div>
                    
                        
                        <div class="row mb-3">
                            <label for="formEmail" class="col-sm-3 col-form-label">Eamil</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="formEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="Add Form" ForeColor="Red" ID="rfvFormEmail" ControlToValidate="formEmail" runat="server" Display="Dynamic" ErrorMessage="Phone No Cannot Be Blank"></asp:RequiredFieldValidator>
                            </div>
                        </div>  

                        <div class="row mb-3">
                            <asp:Button CssClass="btn btn-success" runat="server" OnClick="addAdminFormSubmitClicked" ValidationGroup="Add Form" Text="Save" />
                        </div>

                    </div>

                </div>
            </div>
        </div>

    <div class="main main-raised" >
             <div class="label1">     
                <h3>Manage Admin </h3><div class="container" style="margin-right: 5px; margin-top: 25px;">             

             </div>
             <div class="label1" >
                <div class="input-group rounded" style="width:30%;">
                   <asp:TextBox ID="txtSearch" runat="server" class="form-control rounded" placeholder="Search" aria-label="Search" aria-describedby="search-addon" ></asp:TextBox>
      
                
                    <asp:LinkButton ID="Button1" runat="server" class="input-group-text border-0" OnClick="btnSearch_Click"><i class="fa fa-search" ></i></asp:LinkButton>
                </div>                            
                <asp:Button ID="btnAdd"  CssClass="btn btn-outline-success rightButton" runat="server" Text="Add" data-toggle="modal" data-target="#modalForm" OnClientClick="return false;"/>           
             </div>                            
       
             <asp:Repeater ID="rptUserList" runat="server"  OnItemDataBound="rptUserList_ItemDataBound" OnItemCommand="rptUserList_ItemCommand">
                <HeaderTemplate>
                    <table class="table">
                      <thead>
                        <tr>
                          <th style="width: 4%"></th>
                          <th style="width: 6%">#</th>
                          <th style="width: 20%">Username</th>
                          <th style="width: 20%">User Type</th>
                          <th style="width: 30%">Email</th>
                          <th style="width: 20%"></th>
                        </tr>
                      </thead>         
                    </table>
                </HeaderTemplate>
                <ItemTemplate>
                    <table class="table table-borderless table-hover table-responsive">
                       <tr>
                         <td style="width: 10%; text-align: center" class="align-middle">
                             <asp:Label ID="lblNo" runat="server"  />
                         </td>
                         <td style="width: 20%">
                             <asp:TextBox ID="txtUsername" Text='<%#Eval("username") %>' runat="server" Enabled="false" BorderStyle="None" BackColor="Transparent" ></asp:TextBox>
                             <asp:RequiredFieldValidator ValidationGroup="Edit" ID="RequiredFieldValidator2" ForeColor="Red" Display="Dynamic" ControlToValidate="txtUsername" runat="server" ErrorMessage="Username Cannot Be Blank"></asp:RequiredFieldValidator>

                         </td>
                         <td style="width: 20%" class="align-middle">
                             <asp:Label ID="lblUserType" runat="server" Text='<%# Eval("userType") %>'></asp:Label>
                             <asp:DropDownList ID="ddlCat" CssClass="form-select" runat="server" Visible="false">
                              <asp:ListItem Value="Manager"> Manager </asp:ListItem>
                              <asp:ListItem Value="Admin"> Admin </asp:ListItem>
                             </asp:DropDownList>
                         </td>
                     
                        <td style="width: 30%">
                             <asp:TextBox ID="txtEmail" Text='<%#Eval("Email") %>' runat="server" Enabled="false" BorderStyle="None" BackColor="Transparent" ></asp:TextBox>
                             <asp:RequiredFieldValidator ValidationGroup="Edit" ID="RequiredFieldValidator3" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmail" runat="server" ErrorMessage="Phone No Cannot Be Blank"></asp:RequiredFieldValidator>                        
                         </td>
                        <td class="align-middle align-content-center" style="width: 20%;">
                         <div class="btn-group-horizontal" role="group">
                            <asp:LinkButton ID="btnEdit" CssClass="btn btn-outline-info" runat="server" CommandName="edit" CommandArgument='<%# Eval("username")%>' CausesValidation="false">Edit</asp:LinkButton>
                            <asp:LinkButton ID="btnSave" CssClass="btn btn-outline-success" runat="server" CommandName="save" CommandArgument='<%# Eval("username")%>' Visible="False" ValidationGroup="Edit">Save</asp:LinkButton>
                            <asp:LinkButton ID="btnCancel" CssClass="btn btn-outline-danger" runat="server" CommandName="cancel" Visible="False" CausesValidation="false">Cancel</asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" CssClass="btn btn-danger" runat="server" CommandName="delete" CommandArgument='<%# Eval("username")%>' Visible="False" CausesValidation="false" OnClientClick='return confirm("Are you sure you want to delete this admin?");'>Delete</asp:LinkButton>
                          </div>
                        </td>                                                       
                    </table>
                </ItemTemplate>
                <FooterTemplate>
                <%-- Label used for showing Error Message --%>
                    <asp:Label ID="lblErrorMsg" runat="server" Text="Sorry, there are no user." Visible="false">
                    </asp:Label>
                </FooterTemplate>
            </asp:Repeater>

        </div>  
       </div>  
    </form>
</asp:Content>
