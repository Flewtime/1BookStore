<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Customer.Master" AutoEventWireup="true" CodeBehind="PaymentPage.aspx.cs" Inherits="BookStore.Views.PaymentPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="payment" runat="server">
        <div>
            <h1>Payment Page</h1>
        </div>
        <div>
            <div>
                <asp:Label ID="lbltotalprice" runat="server" Text="Total Price: "></asp:Label>
                <asp:TextBox ID="txttotalprice" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblpmethod" runat="server" Text="Payment Method: "></asp:Label>
                <asp:DropDownList ID="ddlpmethod" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpmethod_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div>
                <asp:Label ID="lblpmethodfee" runat="server" Text="Payment Method Fee: "></asp:Label>
                <asp:TextBox ID="txtpmethodfee" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblptotal" runat="server" Text="Payment Total: "></asp:Label>
                <asp:TextBox ID="txtptotal" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnpfinish" runat="server" Text="Finish Payment" OnClick="btnpfinish_Click" OnClientClick="paymentAlert()" />
            </div>
        </div>
    </div>
</asp:Content>
