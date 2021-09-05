﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SyasyaDesign.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SyaSyaDesign.App_Pages.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
		<link rel="stylesheet" type="text/css" href="../css/userModule.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
	
     <div class="whiteboxPosition">
			<div class="registerWhiteboxSize">
				<form id="signInForm" runat="server" class="form">
					<span class="text1" style="margin-bottom:-20px;">
						Register
					</span>
					<asp:Label ID="lblRegisterOk" class="text2" style="color: red;" runat="server"></asp:Label><br />							
                  
                    <span class="text2">
						Username
					<asp:RequiredFieldValidator ID="rsfRUsername" runat="server" ControlToValidate="TxtRUsername" Display="Dynamic" ErrorMessage="Username is required." ForeColor="Red" font-size = "8px">*Username is required.</asp:RequiredFieldValidator>
					</span>
					<div class="wrapInput1">
                        <asp:TextBox ID="TxtRUsername" runat="server" class="input"></asp:TextBox>
                     </div>

                    <span class="text2">
						Password
						<asp:RequiredFieldValidator ID="rsfRPassword" runat="server" ControlToValidate="TxtRPass" ErrorMessage="Password is required." ForeColor="Red" font-size = "8px">*Password is required.</asp:RequiredFieldValidator>
					</span>
					<div class="wrapInput1">
                        <asp:TextBox ID="TxtRPass" runat="server" class="input"></asp:TextBox>
                     </div>
					
					<span class="text2">
						Confirm Password<asp:RequiredFieldValidator ID="rsvRConfirmPass" runat="server" ControlToValidate="TxtRConfirmPass" Display="Dynamic" ErrorMessage="Confirm password is required." ForeColor="Red" font-size = "8px">*Confirm password is required.</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvRConfirmPass" runat="server" ControlToValidate="TxtRConfirmPass" Display="Dynamic" ErrorMessage="Confirm password not match." Font-Size="8px" ForeColor="Red" OnServerValidate="cvRConfirmPass_ServerValidate">*Confirm password not match.</asp:CustomValidator>
&nbsp;</span><div class="wrapInput1">
						<asp:TextBox ID="TxtRConfirmPass" runat="server" class="input" ></asp:TextBox>
						
					</div>
					
					<span class="text2">
						Email
					<asp:RequiredFieldValidator ID="rsfREmail" runat="server" ControlToValidate="TxtREmail" Display="Dynamic" ErrorMessage="Email is required." ForeColor="Red" font-size = "8px">*Email is required.</asp:RequiredFieldValidator>
					<asp:RegularExpressionValidator ID="revREmail" runat="server" Display="Dynamic" ErrorMessage="Invalid email address." Font-Size="8px" ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="TxtREmail">*Invalid email address</asp:RegularExpressionValidator>
					</span>
					<div class="wrapInput1">
						<asp:TextBox ID="TxtREmail" runat="server" class="input" TextMode="Email" ></asp:TextBox>
						
					</div>
					

					<asp:Button ID="registerFormBtn" runat="server" Text="Register" class="formBtn" OnClick="registerFormBtn_Click"  />  
						

						
						

						<div>
                            <asp:HyperLink id="signUpText" NavigateUrl="~/App_Pages/Login.aspx" Text="Already have an account? Login" runat="server" class="signUpText"/> 

						</div>
					

				</form>
			</div>
		</div>
</asp:Content>
