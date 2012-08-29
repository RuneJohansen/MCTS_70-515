<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AjaxSjov._Default" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<script type="text/javascript" src="http://code.jquery.com/jquery-latest.js"></script>
	<script>
		$(document).ready(function () {
			$("#btnSend").bind("click", function (event) {
				msg = $("#chatText").val();
				$.post("Ajax/ChatMessage.aspx", { action: ""  }, function (data) {
					$("#targetDiv").html(data);
				});
			});
		});
	</script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

	<div id="#chatRows"style="height:400px; width:200px; background-color:#EEEEEE">
	</div>

	<br />

	<input id="chatText" style="width:195px" />
	<button id="btnSend" style="width:200px" />

</asp:Content>
