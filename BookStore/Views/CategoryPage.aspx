<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Guest.Master" AutoEventWireup="true" CodeBehind="CategoryPage.aspx.cs" Inherits="BookStore.Views.CategoryPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="categoryview" runat="server">
        <div>
            <h1 id="categorytitle" runat="server"></h1>
        </div>
        <div>
            <asp:DropDownList ID="ddlbooktype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbooktype_SelectedIndexChanged">
                <asp:ListItem Enabled="true" Value="-1">All Categories</asp:ListItem>
                <asp:ListItem Value="All Books"></asp:ListItem>
                <asp:ListItem Value="All Genres"></asp:ListItem>
                <asp:ListItem Value="All Authors"></asp:ListItem>
                <asp:ListItem Value="All Languages"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:TextBox ID="txtcSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btncSearch" runat="server" Text="Search" OnClick="btncSearch_Click" />
        </div>
        <div>
            <asp:Label ID="lblnone" runat="server"></asp:Label>
            <asp:Label ID="lblcsearch" runat="server"></asp:Label>
            <asp:Repeater ID="rcategory" runat="server" OnItemDataBound="rcategory_ItemDataBound">
                <ItemTemplate>
                    <table>
                        <tr id="categoryrow" runat="server">
                            <td><asp:Label ID="lblcatrownumber" runat="server" Text="<%# Container.ItemIndex + 1 %>"></asp:Label></td>
                            <td><img src="https://source.unsplash.com/400x400/?<%# Eval("CategoryName") %>" /></td>
                            <td><%# Eval("CategoryName") %></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
