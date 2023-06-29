<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Customer.Master" AutoEventWireup="true" CodeBehind="CartPage.aspx.cs" Inherits="BookStore.Views.CartPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="cartview" runat="server">
        <div>
            <h1 id="carttitle" runat="server"></h1>
        </div>
        <div>
            <asp:Label ID="lblcnone" runat="server" Text="Customer's Cart is Empty!" CssClass="fw-bold"></asp:Label>
            <asp:Repeater ID="rcart" runat="server">
                <ItemTemplate>
                    <div class="card mb-3">
                        <div class="row g-0">
                            <div class="col-md-2">
                                <img src="../Assets/Books/<%# Eval("Book.BookImage") %>" class="img-fluid" alt="Book Image">
                            </div>
                            <div class="col-md-10">
                                <div class="card-body">
                                    <h5 class="card-title"><%# Eval("Book.BookTitle") %></h5>
                                    <p class="card-text">Qty: <%# Eval("Qty") %></p>
                                    <p class="card-text">
                                        <strong>Price:</strong> <%# String.Format("{0:C2}", Convert.ToInt32(Eval("Book.BookPrice")), System.Globalization.CultureInfo.GetCultureInfo("id-ID")) %>
                                    </p>
                                    <p class="card-text">
                                        <strong>Total:</strong> <%# String.Format("{0:C2}", Convert.ToInt32(Eval("Book.BookPrice")) * Convert.ToInt32(Eval("Qty")), System.Globalization.CultureInfo.GetCultureInfo("id-ID")) %>
                                    </p>
                                    <asp:Button ID="btncdelete" runat="server" Text="Delete" CommandArgument='<%# Eval("BookID") %>' OnCommand="btncdelete_Command" CssClass="btn btn-danger" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div id="btncheckout" runat="server" class="text-end">
            <asp:Button ID="btnccheckout" runat="server" Text="Checkout" OnClick="btnccheckout_Click" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>
