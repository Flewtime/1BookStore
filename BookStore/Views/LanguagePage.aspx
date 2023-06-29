<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Guest.Master" AutoEventWireup="true" CodeBehind="LanguagePage.aspx.cs" Inherits="BookStore.Views.LanguagePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="languageview" runat="server">
        <div>
            <h1 id="languagetitle" runat="server"></h1>
        </div>
        <div>
            <asp:DropDownList ID="ddlbooktype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbooktype_SelectedIndexChanged">
                <asp:ListItem Enabled="true" Value="-1">All Languages</asp:ListItem>
                <asp:ListItem Value="All Books"></asp:ListItem>
                <asp:ListItem Value="All Genres"></asp:ListItem>
                <asp:ListItem Value="All Categories"></asp:ListItem>
                <asp:ListItem Value="All Authors"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:TextBox ID="txtlSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnlSearch" runat="server" Text="Search" OnClick="btnlSearch_Click" />
        </div>
        <div>
            <asp:Label ID="lblnone" runat="server"></asp:Label>
            <asp:Label ID="lbllsearch" runat="server"></asp:Label>
            <asp:Repeater ID="rlanguage" runat="server" OnItemDataBound="rlanguage_ItemDataBound">
                <ItemTemplate>
                    <table>
                        <tr id="languagerow" runat="server">
                            <td><asp:Label ID="lbllrownumber" runat="server" Text="<%# Container.ItemIndex + 1 %>"></asp:Label></td>
                            <td><img src="https://source.unsplash.com/400x400/?<%# Eval("LanguageName") %>-flag" /></td>
                            <td><%# Eval("LanguageName") %></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
