<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Guest.Master" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="BookStore.Views.RegisterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="userform" runat="server">
        <div>
            <h1 id="usertitleform" runat="server"></h1>
        </div>
        <div>
            <div>
                <asp:Label ID="lbluname" runat="server" Text="Name: "></asp:Label>
                <asp:TextBox ID="txtuname" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lbluemail" runat="server" Text="Email: "></asp:Label>
                <asp:TextBox ID="txtuemail" runat="server" TextMode="Email"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblupassword" runat="server" Text="Password: "></asp:Label>
                <asp:TextBox ID="txtupassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lbluphonenumber" runat="server" Text="Phone Number: +62"></asp:Label>
                <asp:TextBox ID="txtuphonenumber" runat="server" TextMode="Phone"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lbludob" runat="server" Text="Date of Birth: "></asp:Label>
                <asp:Button ID="btncalendar" runat="server" Text="Calendar" OnClick="btncalendar_Click" />
                <asp:Calendar ID="cdob" runat="server" OnSelectionChanged="cdob_SelectionChanged"></asp:Calendar>
                <asp:TextBox ID="txtudob" runat="server" Enabled="false"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblugender" runat="server" Text="Gender: "></asp:Label>
                <asp:RadioButtonList ID="rblugender" runat="server"></asp:RadioButtonList>
            </div>
            <div>
                <asp:Label ID="lbluaddress" runat="server" Text="Address: "></asp:Label>
                <asp:TextBox ID="txtuaddress" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lbluimage" runat="server" Text="Profile Picture: "></asp:Label>
            </div>
            <div>
                <asp:Image ID="imgu" runat="server" Height="200px" Width="200px" />
            </div>
            <div>
                <asp:FileUpload ID="fuuimage" runat="server"></asp:FileUpload>
            </div>
            <div>
                <asp:Label ID="lbluerror" runat="server"></asp:Label>
            </div>
            <div>
                <asp:Button ID="btnuregister" runat="server" Text="Register" OnClick="btnuregister_Click" />
                <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click" />
            </div>
        </div>
    </div>
</asp:Content>
