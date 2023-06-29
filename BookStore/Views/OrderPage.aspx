<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Customer.Master" AutoEventWireup="true" CodeBehind="OrderPage.aspx.cs" Inherits="BookStore.Views.OrderPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Add your custom CSS styles here */
        #orderview {
            font-family: Arial, sans-serif;
            padding: 20px;
            background-color: #f5f5f5;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            margin-bottom: 20px;
        }

        #ordertitle {
            font-size: 24px;
            margin-bottom: 10px;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 15px;
        }

        th {
            background-color: #f9f9f9;
            text-align: left;
            padding: 8px;
        }

        td {
            padding: 8px;
            border-bottom: 1px solid #ddd;
        }

        img {
            max-width: 100px;
            height: auto;
        }

        .empty-order {
            font-style: italic;
            color: #999;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="orderview" runat="server">
        <h1 id="ordertitle" runat="server">Order History</h1>
        <asp:Label ID="lblonone" runat="server" CssClass="empty-order" Text="User Does Not Have Any Order Left!"></asp:Label>
        <asp:Repeater ID="rorder" runat="server" OnItemDataBound="rorder_ItemDataBound">
            <ItemTemplate>
                <table>
                    <tr id="orderrow" runat="server">
                        <th>#</th>
                        <th>Username</th>
                        <th>Order Date</th>
                        <th>Order Time</th>
                        <th>Total Book Price</th>
                        <th>Shipment Price</th>
                        <th>Payment Fee</th>
                        <th>Order Grand Total</th>
                        <th>Shipment Tracking ID</th>
                        <th>Payment Status</th>
                        <th>Shipment Status</th>
                    </tr>
                    <tr id="Tr1" runat="server">
                        <td><asp:Label ID="lblorownumber" runat="server" Text="<%# Container.ItemIndex + 1 %>"></asp:Label></td>
                        <td><%# Eval("UserName") %></td>
                        <td><%# Eval("OrderDate") %></td>
                        <td><%# Eval("OrderTime") %></td>
                        <td><%# Eval("TotalBookPrice") %></td>
                        <td><%# Eval("ShipmentPrice") %></td>
                        <td><%# Eval("PaymentFee") %></td>
                        <td><%# Eval("OrderGrandTotal") %></td>
                        <td><%# Eval("ShipmentTrackingID") %></td>
                        <td><%# Eval("PaymentStatus") %></td>
                        <td><%# Eval("ShipmentStatus") %></td>
                    </tr>
                    <asp:Repeater ID="rorderdetail" runat="server">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <th>#</th>
                                    <th>Book Image</th>
                                    <th>Book Title</th>
                                    <th>Book Price</th>
                                    <th>Quantity</th>
                                    <th>Total Price</th>
                                </tr>
                                <tr>
                                    <td><asp:Label ID="lblodrownumber" runat="server" Text="<%# Container.ItemIndex + 1 %>"></asp:Label></td>
                                    <td><img src="../Assets/Books/<%# Eval("Book.BookImage") %>" /></td>
                                    <td><%# Eval("Book.BookTitle") %></td>
                                    <td><%# String.Format("{0:C2}", Convert.ToInt32(Eval("Book.BookPrice")), System.Globalization.CultureInfo.GetCultureInfo("id-ID")) %></td>
                                    <td><%# Eval("Qty") %></td>
                                    <td><%# String.Format("{0:C2}", Convert.ToInt32(Eval("Qty")) * Convert.ToInt32(Eval("Book.BookPrice")), System.Globalization.CultureInfo.GetCultureInfo("id-ID")) %></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
