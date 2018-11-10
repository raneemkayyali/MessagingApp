<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="MessagingApp.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
<link rel="stylesheet" href="https://www.w3schools.com/lib/w3-theme-blue-grey.css">
<link rel='stylesheet' href='https://fonts.googleapis.com/css?family=Open+Sans'>
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

<style>
html,body,h1,h2,h3,h4,h5 {font-family: "Open Sans", sans-serif}
</style>

<!-- Page Container -->
<div class="w3-container w3-content" style="max-width:1400px;margin-top:80px">    
  <!-- The Grid -->
  <div class="w3-row">
    <!-- Left Column -->
    <div class="w3-col m3">
      <!-- Profile -->
      <div class="w3-card w3-round w3-white">
        <div class="w3-container">
         <h4 class="w3-center"><asp:Label ID="userName" runat="server" Text="User"></asp:Label></h4>
         <hr>
        </div>
      </div>
      <br>
      
      <!-- Accordion -->
      <div>
          <asp:Button runat="server" Text="View Activity" id="actBtn" onclick="actBtn_Click" class="w3-button w3-theme" Width="300"></asp:Button>
        </div>      
      <br>
      

    <!-- End Left Column -->
    </div>
    
    <!-- Middle Column -->
    <div class="w3-col m7">
    
      <div class="w3-row-padding">
        <div class="w3-col m12">
          <div class="w3-card w3-round w3-white">
            <div class="w3-container w3-padding">
                <asp:Label runat="server">To:</asp:Label>
                <asp:DropDownList ID="friendsList" runat="server" CssClass="messageBx"></asp:DropDownList>
                <style>.messageBx {min-width:600px;resize:both;margin-bottom:1%;}</style>
              <asp:TextBox runat="server" ID="messageTxt" TextMode="MultiLine" CssClass="messageBx" Height="300"></asp:TextBox><br />
              <style>.viewBtn {background-color:transparent; color:mediumblue; border-color:transparent;}</style>
                <asp:Button runat="server" Text="View Entire Conversation" cssclass="viewBtn" OnClick="viewBtn_Click" ID="viewBtn"/>
                <div style="float:right;">
                <asp:Button runat="server" ID="sendBtn" OnClick="sendBtn_Click" Text="Send" cssclass="w3-button w3-theme"></asp:Button> 
              </div>
            </div>
          </div>

       <!-- Alert Box -->
        <asp:PlaceHolder runat="server" ID="activity" Visible="False">
      <div class="w3-container w3-display-container w3-round w3-theme-l4 w3-border w3-theme-border w3-margin-bottom w3-hide-small" style="margin-top:2%">
        <span onclick="this.parentElement.style.display='none'" class="w3-button w3-theme-l3 w3-display-topright">
          <i class="fa fa-remove"></i>
        </span>
          <asp:GridView runat="server" Width="50%"  ID="dg1" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderColor="Black" 
    HeaderStyle-BorderWidth="2" HeaderStyle-BackColor="WhiteSmoke" HeaderStyle-ForeColor="Black" BackColor="#CCCCCC" Font-Bold="False" 
    ForeColor="Black" BorderStyle="None" CellPadding="4" 
    SelectedRowStyle-BackColor="#FFFFCC" EnableTheming="True" HorizontalAlign="Center" UseAccessibleHeader="False" GridLines="Horizontal" ShowHeader="False">
    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
    <HeaderStyle BackColor="#CCFFFF" BorderColor="#333333" ForeColor="#333333" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
    <RowStyle BorderColor="#CCCCCC" BorderStyle="Double" BorderWidth="3px" HorizontalAlign="Center" Wrap="False" />
    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F7F7F7" />
    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
    <SortedDescendingCellStyle BackColor="#E5E5E5" />
    <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
      </div>
    </asp:PlaceHolder>
      
<asp:PlaceHolder runat="server" ID="hist" Visible="False">
      <div class="w3-container w3-card w3-white w3-round w3-margin"><br>
        <asp:GridView runat="server" Width="50%" ID="dg2" 
    HeaderStyle-BorderWidth="0" HeaderStyle-BackColor="WhiteSmoke" HeaderStyle-ForeColor="Black" BackColor="White" Font-Bold="False" 
    ForeColor="Black" BorderStyle="None" CellPadding="4" 
    SelectedRowStyle-BackColor="#FFFFCC" EnableTheming="True" HorizontalAlign="Center" UseAccessibleHeader="False" GridLines="None" ShowHeader="False" BorderColor="White">
    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
    <HeaderStyle BackColor="#CCFFFF" BorderColor="#333333" ForeColor="#333333" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
    <RowStyle HorizontalAlign="left" Wrap="False" />
    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F7F7F7" />
    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
    <SortedDescendingCellStyle BackColor="#E5E5E5" />
    <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
      </div>                 
</asp:Placeholder>
              </div>
      </div>

    <!-- End Middle Column -->
    </div>
    
    <!-- Right Column -->
    <div class="w3-col m2">
      <div class="w3-card w3-round w3-white w3-center">
        <div class="w3-container">
         <p style="margin-top:1%">Friends</p>
           <asp:GridView runat="server" BorderWidth="1px" Width="100%" ID="dg" HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderColor="Black" 
    HeaderStyle-BorderWidth="2" HeaderStyle-BackColor="WhiteSmoke" HeaderStyle-ForeColor="Black" BackColor="White" Font-Bold="False" 
    ForeColor="Black" BorderColor="#CCCCCC" BorderStyle="None" CellPadding="4" 
    SelectedRowStyle-BackColor="#FFFFCC" EnableTheming="True" HorizontalAlign="Center" UseAccessibleHeader="False" GridLines="Horizontal" AutoGenerateSelectButton="True" ShowHeader="False">
    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
    <HeaderStyle BackColor="#CCFFFF" BorderColor="#333333" BorderWidth="3px" BorderStyle="Double" ForeColor="#333333" HorizontalAlign="Center" Font-Bold="True"></HeaderStyle>
    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
    <RowStyle BorderColor="#CCCCCC" BorderStyle="Double" BorderWidth="3px" HorizontalAlign="Center" Wrap="False" />
    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
    <SortedAscendingCellStyle BackColor="#F7F7F7" />
    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
    <SortedDescendingCellStyle BackColor="#E5E5E5" />
    <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
          <div class="w3-row w3-opacity">
            <div class="w3-half">
              <asp:Button runat="server" ID="blockBtn" OnClick="blockBtn_Click" cssclass="w3-button w3-block w3-red w3-section" Text="Block" Width="156"></asp:Button>
            </div>
        </div>
      </div>
      </div>
      <br>
      
    <!-- End Right Column -->
    </div>
    
  <!-- End Grid -->
  </div>
  
<!-- End Page Container -->
</div>
<br>

 
<script>
// Accordion
function myFunction() {
    //var x = document.getElementById(id);
    if (document.getElementById("active").style.display == "none") {
       document.getElementById("active").style.display = "block";
     }
    else {
       document.getElementById("active").style.display = "none";
    }
}
</script>



</asp:Content>
