<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Guest.Master" AutoEventWireup="true" CodeBehind="BookDetailPage.aspx.cs" Inherits="BookStore.Views.BookDetailPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="bookdetailview" runat="server">
        <div>
            <h1>Book Detail</h1>
        </div>
        <div class="row">
            <asp:Repeater ID="rbookdetail" runat="server" OnItemDataBound="rbookdetail_ItemDataBound">
                <ItemTemplate>
                    <div class="col-lg-6">
                        <div class="card mb-3">
                            <img src="../Assets/Books/<%# Eval("BookImage") %>" class="card-img-top" alt="Book Image">
                            <div class="card-body">
                                <h5 class="card-title"><%# Eval("BookTitle") %></h5>
                                <p class="card-text">ISBN: <%# Eval("BookISBN") %></p>
                                <p class="card-text">Price: <%# String.Format("{0:C2}", Convert.ToInt32(Eval("BookPrice")), System.Globalization.CultureInfo.GetCultureInfo("id-ID")) %></p>
                                <p class="card-text">Stock: <%# Eval("BookStock") %></p>
                                <p class="card-text">Page: <%# Eval("BookPage") %></p>
                                <p class="card-text">Weight: <%# Eval("BookWeight") %></p>
                                <p class="card-text">Dimension: <%# Eval("BookDimension") %></p>
                                <p class="card-text">Synopsis: <%# Eval("BookSynopsis") %></p>
                                <p class="card-text">Published Date: <%# Eval("BookPublishedDate", "{0:dd MMMM yyyy}") %></p>
                                <p class="card-text">Genre: <%# Eval("Genre.GenreName") %></p>
                                <p class="card-text">Category: <%# Eval("Category.CategoryName") %></p>
                                <p class="card-text">Author: <%# Eval("Author.AuthorName") %></p>
                                <p class="card-text">Publisher: <%# Eval("Publisher.PublisherName") %></p>
                                <p class="card-text">Language: <%# Eval("Language.LanguageName") %></p>
                                <p class="card-text">Cover Type: <%# Eval("CoverType.CoverTypeName") %></p>
                                <p class="card-text">Cover Material: <%# Eval("CoverType.CoverTypeMaterial") %></p>
                                <asp:Repeater ID="rrating" runat="server">
                                    <ItemTemplate>
                                        <p class="card-text">Average Rating: <%# Eval("AverageRating") %></p>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div id="addcart" runat="server">
        <div>
            <div>
                <h1>Add to Cart</h1>
            </div>
            <div>
                <div>
                    <asp:Label ID="lblquantity" runat="server" Text="Quantity: "></asp:Label>
                </div>
                <div>
                    <asp:Button ID="btnmin" runat="server" Text="-" OnClientClick="return decrement(event)" />
                    <input type="number" id="txtquantity" name="txtquantity" value="1" min="1" />
                    <asp:Button ID="btnplus" runat="server" Text="+" OnClientClick="return increment(event)" />
                </div>
                <div>
                    <asp:Label ID="lblcvalidation" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <asp:Button ID="btnaddtocart" runat="server" Text="Add to Cart" OnClick="btnaddtocart_Click" />
                </div>
            </div>
        </div>
    </div>
    <div id="review" runat="server">
        <div>
            <h1>All Reviews</h1>
        </div>
        <div id="reviewinsert" runat="server">
            <asp:Label ID="lblreviewinsert" runat="server" Text="Review: "></asp:Label>
            <div id="ratingstarinsert">
                <span id="starinsert1" class="starinsert" runat="server" onclick="rateinsert(1.0)">&#9733;</span>
                <span id="starinsert2" class="starinsert" runat="server" onclick="rateinsert(2.0)">&#9733;</span>
                <span id="starinsert3" class="starinsert" runat="server" onclick="rateinsert(3.0)">&#9733;</span>
                <span id="starinsert4" class="starinsert" runat="server" onclick="rateinsert(4.0)">&#9733;</span>
                <span id="starinsert5" class="starinsert" runat="server" onclick="rateinsert(5.0)">&#9733;</span>
            </div>
            <input type="hidden" id="ratinginsert" name="ratinginsert" value="0" />
            <asp:TextBox ID="txtreviewinsert" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnreviewinsert" runat="server" Text="Submit" OnClick="btnreviewinsert_Click" />
        </div>
        <div id="reviewedit" runat="server">
            <asp:Label ID="lblreviewedit" runat="server" Text="Review: "></asp:Label>
            <div id="ratingstaredit">
                <span id="staredit1" class="staredit" runat="server" onclick="rateedit(1.0)">&#9733;</span>
                <span id="staredit2" class="staredit" runat="server" onclick="rateedit(2.0)">&#9733;</span>
                <span id="staredit3" class="staredit" runat="server" onclick="rateedit(3.0)">&#9733;</span>
                <span id="staredit4" class="staredit" runat="server" onclick="rateedit(4.0)">&#9733;</span>
                <span id="staredit5" class="staredit" runat="server" onclick="rateedit(5.0)">&#9733;</span>
            </div>
            <input type="hidden" id="ratingedit" name="ratingedit" value="0" />
            <asp:TextBox ID="txtreviewedit" runat="server" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnreviewedit" runat="server" Text="Submit" OnClick="btnreviewedit_Click" />
        </div>
        <div>
            <asp:Label ID="lblrnone" runat="server" Text="Book Doesn't Have Any Review!"></asp:Label>
            <asp:Repeater ID="rreview" runat="server" OnItemDataBound="rreview_ItemDataBound">
                <ItemTemplate>
                    <table>
                        <tr id="reviewrow" runat="server">
                            <td><%# Eval("User.UserName") %></td>
                            <td><%# Eval("ReviewRating") %></td>
                            <td><%# Eval("ReviewComment") %></td>
                            <td><%# Eval("ReviewDateTime", "{0:dd/MM/yyyy HH:mm}") %></td>
                            <td><asp:Button ID="btnrdelete" runat="server" Text="Delete" CommandArgument='<%# Eval("ReviewID") %>' OnCommand="btnrdelete_Command" /></td>
                            <td><asp:Button ID="btnredit" runat="server" Text="Edit" CommandArgument='<%# Eval("ReviewID") %>' OnCommand="btnredit_Command" /></td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
