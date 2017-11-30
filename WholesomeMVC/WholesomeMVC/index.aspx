﻿<%@ Page Language="C#" AutoEventWireup="True" Inherits="index" CodeBehind="index.aspx.cs" %>

<!DOCTYPE html>
<html lang="en"> 
<head>
  <title>WHOLESOME | HOME</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
  <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css" rel="stylesheet" type="text/css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  <link href="/css/index.css" rel="stylesheet" type="text/css" runat="server"/>
  <style>

   ul.ui-autocomplete {
    list-style: none;
    list-style-type:none;
    padding: 0px;
    margin:0px;
    color: black;
    height: 200px; 
    overflow-y: scroll;
    overflow-x: hidden;

      }

   
 .sookyeown {
     width:50%;
     margin-top:10px;
 }

 .sook{
     width:30%;
 }

 .searchSpan{
     margin-top:50px;
 }

 #ddlCategory{
          width: auto;
          left: auto;
      }


      
  </style>
    
<script>
    $(document).ready(function (e) {
        $('.search-panel .dropdown-menu').find('a').click(function (e) {
            e.preventDefault();
            var param = $(this).attr("href").replace("#", "");
            var concept = $(this).text();
            $('.search-panel span#search_concept').text(concept);
            $('.input-group #search_param').val(param);
        });
    });
</script>  
    
<script type="text/javascript">
    $(document).ready(function () {
        $("[id*=txtSearch]").autocomplete({ source: '<%=ResolveUrl("~/AutoComplete.ashx" ) %>' });
    });     
</script>
    
</head>
    <body>
        
 <form runat="server">        

<nav class="navbar navbar-default" role="navigation">
    <div class="navbar-header">
        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
        </button>
        <a class="navbar-brand" href="index.aspx">
            <img src="https://farm5.staticflickr.com/4468/38145590072_8dd45d4da2_o.png" height="75" width="120" alt="Wholesome Logo" class="img-responsive"></a>
    </div>
    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
        <ul class="nav navbar-nav">
            <li><a style="color:#0D8843;margin-top:10px; font-size:14px;" href="manual_input.aspx">NUTRIENT CALCULATOR</a></li>
            <li><a style="color:#0D8843;margin-top:10px; font-size:14px;" href="recent.aspx">RECENT</a></li>
            <li><a style="color:#0D8843;margin-top:10px; font-size:14px;" href="saved_items.aspx">SAVED ITEMS</a></li>
        </ul>
        <!--new section-->    
          
        <div class="input-group">
            <div class="input-group-btn search-panel">
       <asp:DropDownList ID="ddlCategory" runat="server" CssClass="btn btn-default dropdown-toggle" AppendDataBoundItems="True" OnSelectedIndexChanged="Page_Load" DataSourceID="Category" DataTextField="FdGrp_Desc" DataValueField="FdGrp_Desc">
              <asp:ListItem Selected="True">--Select Category--</asp:ListItem>       
      </asp:DropDownList>
              
                </div>
      <asp:SqlDataSource ID="Category" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [FdGrp_Desc] FROM [FD_GROUP]"></asp:SqlDataSource>
            <asp:TextBox ID="txtSearch" runat="server" CssClass="sook form-control"></asp:TextBox>
           <span class="input-group-btn">
         <button type="submit" class="btn btn-default" runat="server" onserverclick="btnSearch" ><span class="glyphicon glyphicon-search"></span></button></span>
      
        
            </div>
    </div>
    <!--ADDED HTML FOR THE LOGIN DROPDOWN--> 
    
 <div class="container-fluid">
     <div class="col-sm-12">
     <div class="row">
    <div class="dropdown pull-right">  
      <button class="btn btn-default dropdown-toggle" type="button" id="userdropdown" data-toggle="dropdown" style="background-color: #0D8843; color: #fff;">Welcome  <span class="glyphicon glyphicon-user" style="color:#fff;"></span>
    <span class="caret"></span></button>
    <ul class="dropdown-menu" role="menu" aria-labelledby="userdropdown">
      <li role="presentation" id="login" runat="server"><a role="menuitem" tabindex="-1" href="login.aspx">Log-In<span class="glyphicon glyphicon-log-in"></span></a></li>
      <li role="presentation" id="acctmag" runat="server"><a role="menuitem" tabindex="-1" href="account_management.aspx">Account Management</a></li>
      <li role="presentation" id="setting" runat="server"><a role="menuitem" tabindex="-1" href="Settings.aspx">Settings</a></li>
      <li role="presentation" id="adsearch" runat="server"><a role="menuitem" tabindex="-1" href="advanced_search.aspx">Advance Search</a></li>
      <li role="presentation" class="divider"></li>
      <li role="presentation" id="logout" runat="server"><a role="menuitem" tabindex="-1" href="#">Log Out</a></li>
    </ul>      
         </div>   
         
        
</div>
         </div>
</div>     
         
<!--END ADDED HTML FOR THE LOGIN DROPDOWN--> 
</nav>
  <!--end nav bar-->    
     

<div class="bgimg-1"> 
    
    <div class="container icons">
<div class="row text-center">
  <div class="col-sm-3">
<span style="color: #fff;" class="glyphicon glyphicon-heart logo"></span>
    </div>  
  <div class="col-sm-3">
    <span style="color: #fff;" class="glyphicon glyphicon-grain logo"></span>
    </div>
  <div class="col-sm-3">
    <span style="color: #fff;" class="glyphicon glyphicon-usd logo"></span>
    </div>
<div class="col-sm-3">
    <span style="color: #fff;" class="glyphicon glyphicon-globe logo"></span>
    </div>       
</div>    
 </div> 
    
  <div class="caption">      
    <span class="border" style="background-color:transparent;font-size:25px;color: #f7f7f7;"><a href="login.aspx"><button type="button" class="btn btn-outline-success btn-lg">LOG-IN</button> </a></span>
          <span class="border" style="background-color:transparent;font-size:25px;color: #f7f7f7;">
              <a href="/Account/Register"><button type="button" class="btn btn-outline btn-lg" style="background-color:#fff; color: #0D8843;" >SIGN-UP</button> </span>
  </div>
</div>
     
<div style="background-color:white;text-align:center;padding:50px 80px;text-align: justify;">
     
<h2 style="text-align:center;">We want the <b>best</b> for you.</h2><br>
 <h2 style="text-align:center;">But we want <b>you</b> to have a choice.</h2><br> 
     <center><h2 style="font-family:'Nunito', sans-serif; font-size: 30px;">Using Wholesome, you can quickly find out the different 
     nutritional values of your <br>food options and decide for yourself what works for you and your needs.</h2></center>
 </div> 

<%--<div style="position:relative;">
  <div style="background-color:#0D8843;text-align:center;padding:50px 80px;text-align: justify;">
     <center><h3 style="color:#fff; font-family:'Nunito', sans-serif; font-size: 30px;">Using Wholesome, you can quickly find out the different 
     nutritional values of your <br>food options and decide for yourself what works for you and your needs.</h3></center>
  </div>
</div>--%>

<%--<div class="bgimg-3">
  <div class="caption2">
   <img src="https://farm5.staticflickr.com/4483/38121282646_39cba86aa4_o.png">
  </div>
</div>--%>


<!--start footer-->
     
<footer style="padding-top: 20px; position:relative; bottom:0; border-top:1px solid #fff; background-color: #0D8843;">
  <div class="container">
    <div class="row" >
    
            <div class="col-md-2">
                <div class="hidden-md hidden-lg">
              <h1 style="margin-top:0px; color: #fff; font-size: 20px; text-transform:uppercase; letter-spacing:2px;">Site Navigation</h1>   
                </div>
    <ul class="nav navbar-nav">
            <li><a style="color:#fff; font-size:14px;" href="manual_input.aspx">NUTRIENT CALCULATOR</a></li>
            <li><a style="color:#fff; font-size:14px;" href="recent.aspx">RECENT</a></li>
            <li><a style="color:#fff; font-size:14px;" href="saved_items.aspx">SAVED ITEMS</a></li>
        </ul>           
        </div> <!--end col 1-->     
        
      </div>
      
      
    <div class="row">   
  <div class="col-md-12 text-right">    
     <p style="color:#fff;">Copyright 2017 Wholesome Inc. All rights reserved.</p>   

      </div>
      </div> 
    </div>  
    
    </footer>
    
<!--end footer-->
         
        </form>
</body>
</html>