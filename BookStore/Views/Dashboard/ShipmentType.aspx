<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Dashboard/Admin.Master" AutoEventWireup="true" CodeBehind="ShipmentType.aspx.cs" Inherits="BookStore.Views.ShipmentType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="shipmenttypeview" runat="server">
        <div class="relative overflow-x-auto shadow-md sm:rounded-lg">
            <div class="p-5 text-lg font-semibold text-left text-gray-900 bg-white dark:text-white dark:bg-gray-800">Shipment Types</div>
            <div class="bg-white dark:bg-gray-800">
                <label for="default-search" class="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white">Search</label>
                <div class="relative">
                    <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                        <svg aria-hidden="true" class="w-5 h-5 text-gray-500 dark:text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path></svg>
                    </div>
                    <asp:TextBox ID="txtstSearch" runat="server" CssClass="block w-full p-4 pl-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Search Shipment Types"></asp:TextBox>
                    <asp:Button ID="btnstSearch" runat="server" Text="Search" OnClick="btnstSearch_Click" CssClass="text-white absolute right-2.5 bottom-2.5 bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" />
                </div>
            </div>
            <div class="flex flex-col w-full bg-white dark:bg-gray-800 py-4">
                <asp:Button ID="btnstInsert" runat="server" Text="Insert New Shipment Type" CssClass="w-full text-gray-900 bg-white border border-gray-300 focus:outline-none hover:bg-gray-100 focus:ring-4 focus:ring-gray-200 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-600 dark:focus:ring-gray-700" OnClick="btnstInsert_Click" />
                <asp:Label ID="lblstsearch" runat="server" CssClass="w-full text-center text-xs text-red-600 dark:text-red-400"></asp:Label>
            </div>
            <asp:GridView ID="gvshipmenttype" runat="server" CssClass="w-full text-sm text-left text-gray-500 dark:text-gray-400" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvshipmenttype_PageIndexChanging" OnRowDeleting="gvshipmenttype_RowDeleting" OnRowEditing="gvshipmenttype_RowEditing">
                <Columns>
                    <asp:TemplateField HeaderText="No." ItemStyle-Width="50" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ShipmentTypeID" HeaderText="Shipment Type ID" SortExpression="ShipmentTypeID" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="ShipmentTypeName" HeaderText="Shipment Type Name" SortExpression="ShipmentTypeName" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="ShipmentTypeDeliveryTime" HeaderText="Shipment Type Delivery Time" SortExpression="ShipmentTypeDeliveryTime" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="ShipmentTypePrice" HeaderText="Shipment Type Price" SortExpression="ShipmentTypePrice" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:CommandField ButtonType="Button" HeaderText="Action" ShowDeleteButton="True" ShowEditButton="True" ShowHeader="True" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 font-medium text-blue-600 dark:text-blue-500 text-center px-4 py-6" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div id="shipmenttypeform" runat="server" class="px-4 mx-auto max-w-2xl">
        <h1 id="shipmenttypeformtitle" runat="server" class="mb-4 text-xl font-bold text-gray-900 dark:text-white"></h1>
        <div class="grid gap-4 sm:grid-cols-2 sm:gap-6">
            <div class="sm:col-span-2">
                <label for="name" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Shipment Type Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Shipment Type Name"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <label for="deliverytime" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Delivery Time</label>
                <asp:TextBox ID="txtDeliveryTime" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Shipment Type Delivery Time"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <label for="price" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Price</label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Shipment Type Price"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <asp:Label ID="lblError" runat="server" CssClass="text-xs text-red-600 dark:text-red-400"></asp:Label>
            </div>
            <div>
                <asp:Button ID="btnPost" runat="server" OnClick="btnPost_Click" CssClass="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800" />
            </div>
        </div>
    </div>
</asp:Content>
