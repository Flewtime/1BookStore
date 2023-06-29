<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Customer.Master" AutoEventWireup="true" CodeBehind="ShipmentPage.aspx.cs" Inherits="BookStore.Views.ShipmentPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="shipmentform" runat="server">
        <div>
            <h1>Shipment Page</h1>
        </div>
        <div>
            <div>
                <asp:Label ID="lblsweight" runat="server" Text="Total Weight: "></asp:Label>
                <asp:TextBox ID="txtsweight" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblsaddress" runat="server" Text="Shipment Address: "></asp:Label>
                <asp:TextBox ID="txtsaddress" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblstype" runat="server" Text="Shipment Type: "></asp:Label>
                <asp:DropDownList ID="ddlstype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlstype_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div>
                <asp:Label ID="lblstypeprice" runat="server" Text="Shipment Price: "></asp:Label>
                <asp:TextBox ID="txtstypeprice" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblpricebefore" runat="server" Text="Total Before Shipment Price: "></asp:Label>
                <asp:TextBox ID="txtpricebefore" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblsprice" runat="server" Text="Total After Shipment Price: "></asp:Label>
                <asp:TextBox ID="txtsprice" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div>
                <h4>Note: Shipment will be process in 12 hours - 24 hours after payment completed!</h4>
            </div>
            <div>
                <asp:Button ID="btnsnext" runat="server" Text="Next" OnClick="btnsnext_Click" />
            </div>
        </div>
    </div>
</asp:Content>
