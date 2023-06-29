<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Customer.Master" AutoEventWireup="true" CodeBehind="TransactionHistoryPage.aspx.cs" Inherits="BookStore.Views.TransactionHistoryPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Add modern and stylish styles here */
        #transactionhistoryview {
            font-family: Arial, sans-serif;
            background-color: #f5f5f5;
            padding: 20px;
            border-radius: 4px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        #transactionhistorytitle {
            font-size: 24px;
            color: #333;
            margin: 0;
            padding: 10px 0;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        th, td {
            padding: 8px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }

        th {
            background-color: #f2f2f2;
        }

        tr:hover {
            background-color: #f9f9f9;
        }

        img {
            max-width: 100px;
            height: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="transactionhistoryview">
        <div>
            <h1 id="transactionhistorytitle" runat="server"></h1>
        </div>
        <div>
            <asp:Label ID="lblthnone" runat="server" Text="User Does Not Have Any Transaction History!"></asp:Label>
            <asp:Repeater ID="rtransactionheader" runat="server" OnItemDataBound="rtransactionheader_ItemDataBound">
                <ItemTemplate>
                    <table>
                        <tr id="transactionhistoryrow" runat="server">
                            <th>#</th>
                            <th>User Name</th>
                            <th>Transaction Date</th>
                            <th>Transaction Time</th>
                            <th>Total Book Price</th>
                            <th>Shipment Price</th>
                            <th>Payment Fee</th>
                            <th>Grand Total</th>
                            <th>Payment Status</th>
                            <th>Shipment Status</th>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblthrownumber" runat="server" Text="<%# Container.ItemIndex + 1 %>"></asp:Label></td>
                            <td><%# Eval("UserName") %></td>
                            <td><%# Eval("TransactionDate") %></td>
                            <td><%# Eval("TransactionTime") %></td>
                            <td><%# Eval("TotalBookPrice") %></td>
                            <td><%# Eval("ShipmentPrice") %></td>
                            <td><%# Eval("PaymentFee") %></td>
                            <td><%# Eval("TransactionGrandTotal") %></td>
                            <td><%# Eval("PaymentStatus") %></td>
                            <td><%# Eval("ShipmentStatus") %></td>
                        </tr>
                        <asp:Repeater ID="rtransactiondetail" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><asp:Label ID="lbltdrownumber" runat="server" Text="<%# Container.ItemIndex + 1 %>"></asp:Label></td>
                                    <td><img src="../Assets/Books/<%# Eval("Book.BookImage") %>" /></td>
                                    <td><%# Eval("Book.BookTitle") %></td>
                                    <td><%# String.Format("{0:C2}", Convert.ToInt32(Eval("Book.BookPrice")), System.Globalization.CultureInfo.GetCultureInfo("id-ID")) %></td>
                                    <td><%# Eval("Qty") %></td>
                                    <td><%# String.Format("{0:C2}", Convert.ToInt32(Eval("Qty")) * Convert.ToInt32(Eval("Book.BookPrice")), System.Globalization.CultureInfo.GetCultureInfo("id-ID")) %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
