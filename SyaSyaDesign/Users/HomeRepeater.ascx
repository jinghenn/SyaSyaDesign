<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeRepeater.ascx.cs" Inherits="SyaSyaDesign.Users.HomeRepeater" %>
<asp:Label ID="heading" CssClass="h1 mb-4" runat="server" Text=""/> <br /><br />
<div class="card-group">
    <asp:Repeater ID="rpt" runat="server">
        <ItemTemplate>
            <div class="card">
                <asp:ImageButton ID="ibtn" runat="server" class="card-img-top" src='<%# GetImage("" + Eval("URL")) %>' CommandArgument='<%# Eval("product_id") %>' OnClick="SlideImg_Click" />
                <div class="card-body">
                    <h5 class="card-title" runat="server"><%# Eval("product_name") %></h5>
                    <p class="card-text" runat="server">RM <%# Eval("price") %></p>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>