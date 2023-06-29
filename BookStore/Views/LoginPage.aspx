<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Guest.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="BookStore.Views.LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="loginform" class="container">
        <div class="row">
            <div class="col-md-6 col-md-offset-4">
                <h1 class="text-center">Login</h1>
                <div class="form-group">
                    <asp:Label ID="lbluemail" runat="server" Text="Email: " CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtuemail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblupassword" runat="server" Text="Password: " CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="txtupassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="checkbox">
                    <asp:CheckBox ID="cbxcremember" runat="server" Text="Remember Me" CssClass="control-label" />
                </div>
                <div>
                    <asp:Label ID="lbluerror" runat="server"></asp:Label>
                </div>
                <div class="text-center">
                    <asp:Button ID="btnulogin" runat="server" Text="Login" OnClick="btnulogin_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
