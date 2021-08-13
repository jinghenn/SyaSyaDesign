<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelloWorld.aspx.cs" Inherits="SyaSyaDesign.App_Pages.HelloWorld" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Hello Azure</h1>
            <p>Group Member:</p>
            <p>Zi Yan</p>
            <p>Ai Yan</p>
            <p>Jia Min</p>

        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=syasyadesign.database.windows.net;Initial Catalog=syasyadb;User ID=syasya;Password=Ssdesign12@34;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM [User]"></asp:SqlDataSource>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="user_id" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="user_id" HeaderText="user_id" InsertVisible="False" ReadOnly="True" SortExpression="user_id" />
                <asp:BoundField DataField="username" HeaderText="username" SortExpression="username" />
                <asp:BoundField DataField="password" HeaderText="password" SortExpression="password" />
                <asp:CheckBoxField DataField="isAdmin" HeaderText="isAdmin" SortExpression="isAdmin" />
            </Columns>
        </asp:GridView>
        <asp:TextBox ID="txtNewUsername" runat="server"></asp:TextBox>
        <asp:Button ID="btn_add_user" runat="server" OnClick="btn_add_user_Click" Text="Button" />
        <br />
    </form>
</body>
</html>
