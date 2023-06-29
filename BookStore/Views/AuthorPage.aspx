<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Guest.Master" AutoEventWireup="true" CodeBehind="AuthorPage.aspx.cs" Inherits="BookStore.Views.AuthorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="authorview" runat="server">
        <div>
            <h1 id="authortitle" runat="server"></h1>
        </div>
        <div>
            <asp:DropDownList ID="ddlbooktype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbooktype_SelectedIndexChanged">
                <asp:ListItem Enabled="true" Value="-1">All Authors</asp:ListItem>
                <asp:ListItem Value="All Books"></asp:ListItem>
                <asp:ListItem Value="All Genres"></asp:ListItem>
                <asp:ListItem Value="All Categories"></asp:ListItem>
                <asp:ListItem Value="All Languages"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:TextBox ID="txtauSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnauSearch" runat="server" Text="Search" OnClick="btnauSearch_Click" />
        </div>
        <div>
            <asp:Label ID="lblnone" runat="server"></asp:Label>
            <asp:Label ID="lblausearch" runat="server"></asp:Label>
            <asp:Repeater ID="rauthor" runat="server" OnItemDataBound="rauthor_ItemDataBound">
                <ItemTemplate>
                    <table>
                        <tr id="authorrow" runat="server">
                            <td>
                                <asp:Label ID="lblarownumber" runat="server" Text="<%# Container.ItemIndex + 1 %>"></asp:Label></td>
                                <td><img src="../Assets/Authors/<%# Eval("AuthorImage") %>" /></td>
                            <td><%# Eval("AuthorName") %></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
