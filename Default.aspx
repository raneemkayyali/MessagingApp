<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MessagingApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
           <div class="navbar navbar-inverse navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                </div>
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <li><a runat="server" href="~/Contact"></a></li>
                       <li><a runat="server" href="~/"></a></li>
                    </ul>
                </div>
            </div>
        </div>
    <div class="jumbotron" style="width:400px">
        <h1>Welcome</h1>
        <hr />
        <span>Username</span><br />
        <asp:TextBox runat="server" ID="userTxt" Width="300"></asp:TextBox><br />
        <span>Password</span><br />
        <asp:TextBox runat="server" ID="pwdTxt" TextMode="Password" Width="300"></asp:TextBox><br /><br />
        <asp:Button runat="server" ID="loginBtn" onclick="loginBtn_Click" Text="Login" class="btn btn-primary btn-lg" Width="300"></asp:Button><br />
       <br />
        <style> .links {background-color:transparent; border-color:transparent; color:mediumblue;} </style>
        <asp:Button runat="server" CssClass="links" Text="Create Account" ID="createBtn" OnClick="createBtn_Click"/>
    </div>
</asp:Content>
