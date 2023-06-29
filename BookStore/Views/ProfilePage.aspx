<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Master/Customer.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="BookStore.Views.ProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Add custom styles for the profile page */
        #profileview {
            display: flex;
            flex-direction: column;
            align-items: center;
            padding: 20px;
            background-color: #f8f8f8;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
        }

        #profiletitle {
            font-size: 24px;
            margin-bottom: 20px;
        }

        .profile-section {
        margin-top: 20px;
        text-align: center;
    }

    .profile-section h2 {
        font-size: 18px;
        margin-bottom: 10px;
    }

    .button {
        display: inline-block;
        padding: 8px 16px;
        font-size: 14px;
        font-weight: bold;
        text-align: center;
        text-decoration: none;
        border-radius: 4px;
        background-color: #007bff;
        color: #ffffff;
        transition: background-color 0.3s ease;
    }

    .button:hover {
        background-color: #0056b3;
    }
        #userrow {
            display: flex;
            flex-direction: column;
            align-items: center;
            margin-bottom: 20px;
            padding: 10px;
            background-color: #ffffff;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        #userrow img {
            width: 100px;
            height: 100px;
            border-radius: 50%;
            object-fit: cover;
            margin-bottom: 10px;
        }

        #userrow h2 {
            font-size: 16px;
            font-weight: bold;
            margin-bottom: 5px;
        }

        #userrow h3 {
            font-size: 14px;
            margin-bottom: 10px;
        }

        #passwordtext {
            display: inline-block;
            margin-right: 10px;
        }

        #updateprofile,
        #userdelete,
        #logout {
            margin-top: 20px;
        }

        .button {
            display: inline-block;
            padding: 10px 20px;
            font-size: 16px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
            border-radius: 5px;
            background-color: #007bff;
            color: #ffffff;
            transition: background-color 0.3s ease;
        }

        .button:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="profileview" runat="server">
        <h1 id="profiletitle" runat="server"></h1>
        <asp:Repeater ID="rprofile" runat="server" OnItemDataBound="rprofile_ItemDataBound">
            <ItemTemplate>
                <div id="userrow">
                    <img src="../Assets/Users/<%# Eval("UserImage") %>" />
                    <h2>Name:</h2>
                    <h3><%# Eval("UserName") %></h3>
                    <h2>Email:</h2>
                    <h3><%# Eval("UserEmail") %></h3>
                    <h2>Password:</h2>
                    <div>
                        <h3 id="passwordtext" runat="server"><%# Eval("UserPassword") %></h3>
                        <asp:Button ID="btntogglepassword" runat="server" Text="Show" OnClick="btntogglepassword_Click" CssClass="button" />
                    </div>
                    <h2>Phone Number:</h2>
                    <h3><%# Eval("UserPhoneNumber") %></h3>
                    <h2>Date of Birth:</h2>
                    <h3><%# Eval("UserDOB", "{0:dd MMMM yyyy}") %></h3>
                    <h2>Gender:</h2>
                    <h3><%# Eval("UserGender") %></h3>
                    <h2>Address:</h2>
                    <h3><%# Eval("UserAddress") %></h3>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div id="userdelete" runat="server" class="profile-section">
        <h2>Delete Account</h2>
        <asp:Button ID="btncdelete" runat="server" Text="Delete" OnClick="btncdelete_Click" CssClass="button" />
    </div>
    <div id="updateprofile" runat="server" class="profile-section">
        <h2>Update Profile</h2>
        <asp:Button ID="btncupdate" runat="server" Text="Update" OnClick="btncupdate_Click" CssClass="button" />
    </div>
    <div id="logout" runat="server" class="profile-section">
        <h2>Logout</h2>
        <asp:Button ID="btnlogout" runat="server" Text="Logout" OnClick="btnlogout_Click" CssClass="button" />
    </div>
</asp:Content>
