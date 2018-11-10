<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="MessagingApp.About" %>

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
        <h2>Create Account</h2>
        <hr />
        <span>Username</span><br />
        <asp:TextBox runat="server" ID="userTxt" Width="300"></asp:TextBox><br />
        <span>Password</span><br />
        <asp:TextBox runat="server" ID="pwdTxt" TextMode="Password" Width="300"></asp:TextBox><br />
        <span>Confirm Password</span><br />
        <asp:TextBox runat="server" ID="confirmTxt" TextMode="Password" Width="300"></asp:TextBox><br />
        <br />
        <asp:Button runat="server" ID="loginBtn" onclick="createBtn_Click" Text="Create" class="btn btn-primary btn-lg" Width="300"></asp:Button><br />
       <br />
    </div>
</asp:Content>
