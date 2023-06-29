<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Dashboard/Admin.Master" AutoEventWireup="true" CodeBehind="Author.aspx.cs" Inherits="BookStore.Views.Dashboard.Author" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="authorview" runat="server">
        <div class="relative overflow-x-auto shadow-md sm:rounded-lg">
            <div class="p-5 text-lg font-semibold text-left text-gray-900 bg-white dark:text-white dark:bg-gray-800">Authors</div>
            <div class="bg-white dark:bg-gray-800">
                <label for="default-search" class="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white">Search</label>
                <div class="relative">
                    <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                        <svg aria-hidden="true" class="w-5 h-5 text-gray-500 dark:text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path></svg>
                    </div>
                    <asp:TextBox ID="txtauSearch" runat="server" CssClass="block w-full p-4 pl-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Search Authors"></asp:TextBox>
                    <asp:Button ID="btnauSearch" runat="server" Text="Search" OnClick="btnauSearch_Click" CssClass="text-white absolute right-2.5 bottom-2.5 bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" />
                </div>
            </div>
            <div class="flex flex-col w-full bg-white dark:bg-gray-800 py-4">
                <asp:Button ID="btnauInsert" runat="server" Text="Insert New Author" CssClass="w-full text-gray-900 bg-white border border-gray-300 focus:outline-none hover:bg-gray-100 focus:ring-4 focus:ring-gray-200 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-600 dark:focus:ring-gray-700" OnClick="btnauInsert_Click" />
                <asp:Label ID="lblausearch" runat="server" CssClass="w-full text-center text-xs text-red-600 dark:text-red-400"></asp:Label>
            </div>
            <asp:GridView ID="gvauthor" runat="server" CssClass="w-full text-sm text-left text-gray-500 dark:text-gray-400" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="gvauthor_PageIndexChanging" OnRowDeleting="gvauthor_RowDeleting" OnRowEditing="gvauthor_RowEditing">
                <Columns>
                    <asp:TemplateField HeaderText="No." ItemStyle-Width="50" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AuthorID" HeaderText="Author ID" SortExpression="AuthorID" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="AuthorName" HeaderText="Author Name" SortExpression="AuthorName" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="AuthorDOB" HeaderText="Author Date of Birth" SortExpression="AuthorDOB" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="AuthorBiography" HeaderText="Author Biography" SortExpression="AuthorBiography" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:ImageField DataImageUrlField="AuthorImage" DataImageUrlFormatString="~/Assets/Authors/{0}" HeaderText="Author Image" ControlStyle-Width="200" ControlStyle-Height="200" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6">
                    </asp:ImageField>
                    <asp:CommandField ButtonType="Button" HeaderText="Action" ShowDeleteButton="True" ShowEditButton="True" ShowHeader="True" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 font-medium text-blue-600 dark:text-blue-500 text-center px-4 py-6" />
                </Columns>
                <PagerSettings Mode="NumericFirstLast" />
            </asp:GridView>
        </div>
    </div>
    <div id="authorform" runat="server" class="px-4 mx-auto max-w-2xl">
        <h1 id="authorformtitle" runat="server" class="mb-4 text-xl font-bold text-gray-900 dark:text-white"></h1>
        <div class="grid gap-4 sm:grid-cols-2 sm:gap-6">
            <div class="sm:col-span-2">
                <label for="name" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Author Name</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Author Name"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <label for="dob" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Date of Birth</label>
                <div class="relative">
                    <div class="flex">
                        <asp:TextBox ID="txtDOB" runat="server" Enabled="false" CssClass="basis-11/12 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Author Date of Birth"></asp:TextBox>
                        <asp:ImageButton ID="btniCalendar" runat="server" OnClick="btniCalendar_Click" ImageUrl="~/Assets/Icons/Calendar.png" CssClass="basis-1/12 w-10 h-10 text-gray-800 dark:text-white" />
                    </div>
                    <div class="absolute left-0 top-0">
                        <asp:Calendar ID="calDOB" runat="server" OnSelectionChanged="calDOB_SelectionChanged" DayHeaderStyle-BackColor="#4B5563" DayStyle-BackColor="#9CA3AF" TitleStyle-BackColor="#374151"></asp:Calendar>
                    </div>
                </div>
            </div>
            <div class="sm:col-span-2">
                <label for="biography" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Biography</label>
                <asp:TextBox ID="txtBiography" runat="server" TextMode="MultiLine" Rows="8" CssClass="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-primary-500 focus:border-primary-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Author Biography"></asp:TextBox>
            </div>
            <div>
                <label for="image" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Image</label>
                <asp:Image ID="imgAuthor" runat="server" Width="200" Height="200" CssClass="block w-full" />
                <asp:FileUpload ID="fuImage" runat="server" CssClass="block w-full text-sm text-gray-900 border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400" />
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
