<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Guest.Master" AutoEventWireup="true" CodeBehind="GenrePage.aspx.cs" Inherits="BookStore.Views.GenrePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="genreview" runat="server">
        <div>
            <h1 id="genretitle" runat="server"></h1>
        </div>
        <div>
            <asp:DropDownList ID="ddlbooktype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbooktype_SelectedIndexChanged">
                <asp:ListItem Enabled="true" Value="-1">All Genres</asp:ListItem>
                <asp:ListItem Value="All Books"></asp:ListItem>
                <asp:ListItem Value="All Categories"></asp:ListItem>
                <asp:ListItem Value="All Authors"></asp:ListItem>
                <asp:ListItem Value="All Languages"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:TextBox ID="txtgSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btngSearch" runat="server" Text="Search" OnClick="btngSearch_Click" />
        </div>
        <div>
            <asp:Label ID="lblnone" runat="server"></asp:Label>
            <asp:Label ID="lblgsearch" runat="server"></asp:Label>
            <asp:Repeater ID="rgenre" runat="server" OnItemDataBound="rgenre_ItemDataBound">
                <ItemTemplate>
                    <table>
                        <tr id="genrerow" runat="server">
                            <td><asp:Label ID="lblgrownumber" runat="server" Text="<%# Container.ItemIndex + 1 %>"></asp:Label></td>
                            <td><img src="https://source.unsplash.com/400x400/?<%# Eval("GenreName") %>" /></td>
                            <td><%# Eval("GenreName") %></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
