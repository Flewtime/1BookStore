<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Guest.Master" AutoEventWireup="true" CodeBehind="BookPage.aspx.cs" Inherits="BookStore.Views.BookPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="bookview" runat="server">
        <div>
            <h1 id="booktitle" runat="server"></h1>
        </div>
        <div>
            <asp:DropDownList ID="ddlbooktype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlbooktype_SelectedIndexChanged">
                <asp:ListItem Enabled="true" Value="-1">All Books</asp:ListItem>
                <asp:ListItem Value="All Genres"></asp:ListItem>
                <asp:ListItem Value="All Categories"></asp:ListItem>
                <asp:ListItem Value="All Authors"></asp:ListItem>
                <asp:ListItem Value="All Languages"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:TextBox ID="txtbSearch" runat="server"></asp:TextBox>
            <asp:Button ID="btnbSearch" runat="server" Text="Search" OnClick="btnbSearch_Click" />
        </div>
        <div>
            <asp:Label ID="lblnone" runat="server"></asp:Label>
            <asp:Label ID="lblbsearch" runat="server"></asp:Label>
            <asp:Repeater ID="rbook" runat="server" OnItemDataBound="rbook_ItemDataBound">
                <ItemTemplate>
                    <div class="card">
            <div class="card-header">
                <asp:Label ID="lblbrownumber" runat="server" Text="<%# Container.ItemIndex + 1 %>"></asp:Label>
            </div>
            <div class="card-body">
                <div class="book-image">
                    <asp:HyperLink ID="hldetailImage" runat="server" NavigateUrl='<%# $"~/Views/BookDetailPage.aspx?BookID={Eval("BookID")}" %>'>
                        <img src="../Assets/Books/<%# Eval("BookImage") %>" width="500" height="500" />
                    </asp:HyperLink>
                </div>
                <div class="book-details">
                    <h4>
                        <asp:HyperLink ID="hldetailTitle" runat="server" NavigateUrl='<%# $"~/Views/BookDetailPage.aspx?BookID={Eval("BookID")}" %>'>
                            <%# Eval("BookTitle") %>
                        </asp:HyperLink>
                    </h4>
                    <p><%# Eval("BookPrice") %></p>
                    <p>
                        <asp:HyperLink ID="hllanguage" runat="server" NavigateUrl='<%# $"~/Views/BookPage.aspx?LanguageID={Eval("LanguageID")}" %>'><%# Eval ("LanguageName") %></asp:HyperLink>
                        | <asp:HyperLink ID="hlgenre" runat="server" NavigateUrl='<%# $"~/Views/BookPage.aspx?GenreID={Eval("GenreID")}" %>'><%# Eval("GenreName") %></asp:HyperLink>
                        | <asp:HyperLink ID="hlcategory" runat="server" NavigateUrl='<%# $"~/Views/BookPage.aspx?CategoryID={Eval("CategoryID")}" %>'><%# Eval("CategoryName") %></asp:HyperLink>
                        | <asp:HyperLink ID="hlauthor" runat="server" NavigateUrl='<%# $"~/Views/BookPage.aspx?AuthorID={Eval("AuthorID")}" %>'><%# Eval("AuthorName") %></asp:HyperLink>
                    </p>
                    <p><%# Eval("PublisherName") %></p>
                    <div class="rating">
                        <asp:Repeater ID="rrating" runat="server">
                            <ItemTemplate>
                                <p>Average Rating :<%# Eval("AverageRating") %></p>
                            </ItemTemplate>

                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>




        </div>
    </div>
</asp:Content>
