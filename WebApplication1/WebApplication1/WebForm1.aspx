<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        DataSourceID="SqlDataSource1" 
        EmptyDataText="Ingen brugere..."
        DataKeyNames="UserId" Width="916px">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="UserName" HeaderText="Navn" 
                SortExpression="UserName" />
            <asp:BoundField DataField="Email" HeaderText="Email" 
                SortExpression="Email" />
            <asp:BoundField DataField="lastlogindate" ReadOnly="true" HeaderText="Sidste login" 
                SortExpression="lastlogindate" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
        ProviderName="<%$ ConnectionStrings:ApplicationServices.ProviderName %>"
        SelectCommand="SELECT m.UserId, u.UserName, m.Email, m.lastlogindate
                        FROM aspnet_Membership m
                        join aspnet_users u on u.userid = m.userid"
        UpdateCommand="UPDATE aspnet_Membership SET Email = @Email WHERE UserId = @UserId
                        UPDATE aspnet_Users set UserName = @UserName WHERE UserId = @UserId">
        <UpdateParameters>
            <asp:Parameter Name="UserId" Type="Object" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
