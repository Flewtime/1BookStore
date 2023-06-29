<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Dashboard/Admin.Master" AutoEventWireup="true" CodeBehind="Book.aspx.cs" Inherits="BookStore.Views.Dashboard.Book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="bookview" runat="server">
        <div class="relative overflow-x-auto shadow-md sm:rounded-lg">
            <div class="p-5 text-lg font-semibold text-left text-gray-900 bg-white dark:text-white dark:bg-gray-800">Books</div>
            <div class="bg-white dark:bg-gray-800">
                <label for="default-search" class="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white">Search</label>
                <div class="relative">
                    <div class="absolute inset-y-0 left-0 flex items-center pl-3 pointer-events-none">
                        <svg aria-hidden="true" class="w-5 h-5 text-gray-500 dark:text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M21 21l-6-6m2-5a7 7 0 11-14 0 7 7 0 0114 0z"></path></svg>
                    </div>
                    <asp:TextBox ID="txtbSearch" runat="server" CssClass="block w-full p-4 pl-10 text-sm text-gray-900 border border-gray-300 rounded-lg bg-gray-50 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Search Books"></asp:TextBox>
                    <asp:Button ID="btnbSearch" runat="server" Text="Search" OnClick="btnbSearch_Click" CssClass="text-white absolute right-2.5 bottom-2.5 bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-4 py-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800" />
                </div>
            </div>
            <div class="flex flex-col w-full bg-white dark:bg-gray-800 py-4">
                <asp:Button ID="btnbInsert" runat="server" Text="Insert New Book" CssClass="w-full text-gray-900 bg-white border border-gray-300 focus:outline-none hover:bg-gray-100 focus:ring-4 focus:ring-gray-200 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 mb-2 dark:bg-gray-800 dark:text-white dark:border-gray-600 dark:hover:bg-gray-700 dark:hover:border-gray-600 dark:focus:ring-gray-700" OnClick="btnbInsert_Click" />
                <asp:Label ID="lblbsearch" runat="server" CssClass="w-full text-center bg-white dark:bg-gray-800 text-xs text-red-600 dark:text-red-400"></asp:Label>
            </div>
            <asp:GridView ID="gvbook" runat="server" CssClass="w-full text-sm text-left text-gray-500 dark:text-gray-400" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="gvbook_PageIndexChanging" OnRowDeleting="gvbook_RowDeleting" OnRowEditing="gvbook_RowEditing">
                <Columns>
                    <asp:TemplateField HeaderText="No." ItemStyle-Width="50" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6">
                        <ItemTemplate>
                            <asp:Label ID="lblNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="BookID" HeaderText="Book ID" SortExpression="BookID" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="BookISBN" HeaderText="Book ISBN" SortExpression="BookISBN" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="BookTitle" HeaderText="Book Title" SortExpression="BookTitle" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="BookPrice" HeaderText="Book Price" SortExpression="BookPrice" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="BookStock" HeaderText="Book Stock" SortExpression="BookStock" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="BookPage" HeaderText="Book Page" SortExpression="BookPage" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="BookWeight" HeaderText="Book Weight" SortExpression="BookWeight" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="BookDimension" HeaderText="Book Dimension" SortExpression="BookDimension" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="BookSynopsis" HeaderText="Book Synopsis" SortExpression="BookSynopsis" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="BookPublishedDate" HeaderText="Book Published Date" SortExpression="BookPublishedDate" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:ImageField DataImageUrlField="BookImage" DataImageUrlFormatString="~/Assets/Books/{0}" HeaderText="Book Image" ControlStyle-Width="200" ControlStyle-Height="200" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6">
                    </asp:ImageField>
                    <asp:BoundField DataField="GenreName" HeaderText="Genre Name" SortExpression="GenreName" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" SortExpression="CategoryName" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="AuthorName" HeaderText="Author Name" SortExpression="AuthorName" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="PublisherName" HeaderText="Publisher Name" SortExpression="PublisherName" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="LanguageName" HeaderText="Language Name" SortExpression="LanguageName" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:BoundField DataField="CoverTypeName" HeaderText="CoverType Name" SortExpression="CoverTypeName" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 text-center px-4 py-6" />
                    <asp:CommandField ButtonType="Button" HeaderText="Action" ShowDeleteButton="True" ShowEditButton="True" ShowHeader="True" HeaderStyle-CssClass="text-xs text-gray-700 uppercase bg-gray-50 dark:bg-gray-700 dark:text-gray-400 text-center px-4 py-6" ItemStyle-CssClass="border-b bg-gray-50 dark:bg-gray-800 dark:border-gray-700 font-medium text-blue-600 dark:text-blue-500 text-center px-4 py-6" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div id="bookform" runat="server" class="px-4 mx-auto max-w-2xl">
        <h1 id="bookformtitle" runat="server" class="mb-4 text-xl font-bold text-gray-900 dark:text-white"></h1>
        <div class="grid gap-4 sm:grid-cols-2 sm:gap-6">
            <div class="sm:col-span-2">
                <label for="ISBN" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">ISBN</label>
                <asp:TextBox ID="txtISBN" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Book ISBN"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <label for="title" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Title</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Book Title"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <label for="price" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Price</label>
                <asp:TextBox ID="txtPrice" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Book Price"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <label for="stock" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Stock</label>
                <asp:TextBox ID="txtStock" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Book Stock"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <label for="page" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Page</label>
                <asp:TextBox ID="txtPage" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Book Page"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <label for="weight" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Weight</label>
                <asp:TextBox ID="txtWeight" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Book Weight"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <label for="dimension" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Dimension</label>
                <asp:TextBox ID="txtDimension" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Book Dimension (length cm x width cm x height cm)"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <label for="synopsis" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Synopsis</label>
                <asp:TextBox ID="txtSynopsis" runat="server" TextMode="MultiLine" Rows="8" CssClass="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-primary-500 focus:border-primary-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Book Synopsis"></asp:TextBox>
            </div>
            <div class="sm:col-span-2">
                <label for="publisheddate" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Published Date</label>
                <div class="relative">
                    <div class="flex">
                        <asp:TextBox ID="txtPublishedDate" runat="server" Enabled="false" CssClass="basis-11/12 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-primary-500 dark:focus:border-primary-500" placeholder="Book Published Date"></asp:TextBox>
                        <asp:ImageButton ID="btniCalendar" runat="server" OnClick="btnCalendar_Click1" ImageUrl="~/Assets/Icons/Calendar.png" CssClass="basis-1/12 w-10 h-10 text-gray-800 dark:text-white" />
                    </div>
                    <div class="absolute left-0 top-0">
                        <asp:Calendar ID="calPublishedDate" runat="server" OnSelectionChanged="calPublishedDate_SelectionChanged" DayHeaderStyle-BackColor="#4B5563" DayStyle-BackColor="#9CA3AF" TitleStyle-BackColor="#374151"></asp:Calendar>
                    </div>
                </div>
            </div>
            <div>
                <label for="image" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Image</label>
                <asp:Image ID="imgBook" runat="server" Width="200" Height="200" CssClass="block w-full" />
                <asp:FileUpload ID="fuImage" runat="server" CssClass="block w-full text-sm text-gray-900 border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400" />
            </div>
            <div class="sm:col-span-2">
                <label for="genres" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select genre</label>
                <asp:DropDownList ID="ddlGenre" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"></asp:DropDownList>
            </div>
            <div class="sm:col-span-2">
                <label for="categories" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select category</label>
                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"></asp:DropDownList>
            </div>
            <div class="sm:col-span-2">
                <label for="authors" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select author</label>
                <asp:DropDownList ID="ddlAuthor" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"></asp:DropDownList>
            </div>
            <div class="sm:col-span-2">
                <label for="publishers" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select publisher</label>
                <asp:DropDownList ID="ddlPublisher" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"></asp:DropDownList>
            </div>
            <div class="sm:col-span-2">
                <label for="languages" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select language</label>
                <asp:DropDownList ID="ddlLanguage" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"></asp:DropDownList>
            </div>
            <div class="sm:col-span-2">
                <label for="covertypes" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Select cover type</label>
                <asp:DropDownList ID="ddlCoverType" runat="server" CssClass="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"></asp:DropDownList>
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
