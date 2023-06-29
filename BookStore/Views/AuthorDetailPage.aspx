<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Guest.Master" AutoEventWireup="true" CodeBehind="AuthorDetailPage.aspx.cs" Inherits="BookStore.Views.AuthorDetailPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="authordetailview" runat="server">
        <asp:Repeater ID="rauthordetail" runat="server">
            <ItemTemplate>
                <table>
                    <tr id="authordetailrow" runat="server">
                        <td>
                            <asp:Label ID="lblarownumber" runat="server" Text="<%# Container.ItemIndex + 1 %>"></asp:Label></td>
                        <td>
                            <img src="../Assets/Authors/<%# Eval("AuthorImage") %>" /></td>
                        <td><%# Eval("AuthorName") %></td>
                        <td><%# Eval("AuthorDOB", "{0:dd/MM/yyyy}") %></td>
                        <td><%# Eval("AuthorBiography") %></td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div id="bookview" runat="server">
        <h1 id="bookviewtitle" runat="server"></h1>
        <asp:Label ID="lblnone" runat="server"></asp:Label>
        <asp:Repeater ID="rbook" runat="server" OnItemDataBound="rbook_ItemDataBound">
            <ItemTemplate>
                <table>
                    <tr id="bookrow" runat="server">
                        <td>
                            <asp:Label ID="lblbrownumber" runat="server" Text="<%# Container.ItemIndex + 1 %>"></asp:Label></td>
                        <td>
                            <img src="../Assets/Books/<%# Eval("BookImage") %>" /></td>
                        <td><%# Eval("BookTitle") %></td>
                        <td><%# String.Format("{0:C2}", Convert.ToInt32(Eval("BookPrice")), System.Globalization.CultureInfo.GetCultureInfo("id-ID")) %></td>
                        <td><asp:HyperLink ID="hllanguage" runat="server" NavigateUrl='<%# $"~/Views/BookPage.aspx?LanguageID={Eval("LanguageID")}" %>'><%# Eval ("Language.LanguageName") %></asp:HyperLink></td>
                        <td><asp:HyperLink ID="hlgenre" runat="server" NavigateUrl='<%# $"~/Views/BookPage.aspx?GenreID={Eval("GenreID")}" %>'><%# Eval("Genre.GenreName") %></asp:HyperLink></td>
                        <td><asp:HyperLink ID="hlcategory" runat="server" NavigateUrl='<%# $"~/Views/BookPage.aspx?CategoryID={Eval("CategoryID")}" %>'><%# Eval("Category.CategoryName") %></asp:HyperLink></td>
                        <td><asp:HyperLink ID="hlauthor" runat="server" NavigateUrl='<%# $"~/Views/BookPage.aspx?AuthorID={Eval("AuthorID")}" %>'><%# Eval("Author.AuthorName") %></asp:HyperLink></td>
                        <td><%# Eval("Publisher.PublisherName") %></td>
                        <td>
                            <asp:Repeater ID="rrating" runat="server">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td><%# Eval("AverageRating") %></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
