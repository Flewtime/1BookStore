<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Dashboard/Admin.Master" AutoEventWireup="true" CodeBehind="Shipment.aspx.cs" Inherits="BookStore.Views.Dashboard.Shipment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="shipmentview" runat="server">
        <div class="relative overflow-x-auto shadow-md sm:rounded-lg">
            <div class="p-5 text-lg font-semibold text-left text-gray-900 bg-white dark:text-white dark:bg-gray-800">Shipments</div>
            <div class="bg-white dark:bg-gray-800">
                <label for="default-search" class="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white">Search</label>
                <div class="relative">
                    <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                        <svg aria-hidden="true" class="w-5 h-5 text-gray-500 dark:text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path></svg>
                    </div>
                    <asp:TextBox ID="txtshSearch" runat="server" CssClass="block w-full p-4 pl-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Search Shipments"></asp:TextBox>
                    <asp:Button ID="btnshSearch" runat="server" Text="Search" OnClick="btnshSearch_Click" CssClass="text-white absolute right-2.5 bottom-2.5 bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" />
                </div>
            </div>
            <div class="flex flex-col w-full bg-white dark:bg-gray-800 py-4">
                <asp:Label ID="lblshsearch" runat="server" CssClass="w-full text-center text-xs text-red-600 dark:text-red-400"></asp:Label>
            </div>
            <asp:GridView ID="gvshipment" runat="server" CssClass="w-full text-sm text-left text-gray-500 dark:text-gray-400" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvshipment_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="No." ItemStyle-Width="50" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ShipmentID" HeaderText="Shipment ID" SortExpression="ShipmentID" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="ShipmentWeight" HeaderText="Shipment Weight" SortExpression="ShipmentWeight" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="ShipmentAddress" HeaderText="Shipment Address" SortExpression="ShipmentAddress" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="ShipmentDate" HeaderText="Shipment Date" SortExpression="ShipmentDate" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="ShipmentTime" HeaderText="Shipment Time" SortExpression="ShipmentTime" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="ShipmentPrice" HeaderText="Shipment Price" SortExpression="ShipmentPrice" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="ShipmentStatus" HeaderText="Shipment Status" SortExpression="ShipmentStatus" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="ShipmentTrackingID" HeaderText="Shipment Tracking ID" SortExpression="ShipmentTrackingID" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="ShipmentTypeName" HeaderText="Shipment Type Name" SortExpression="ShipmentTypeName" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
