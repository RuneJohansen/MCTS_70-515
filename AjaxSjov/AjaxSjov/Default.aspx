<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AjaxSjov._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
	<script type="text/javascript" src="http://code.jquery.com/jquery-latest.js"></script>
	<script type="text/javascript">
		$(document).ready(function () {
			//Code here execute on page load
			$(document).on("click", function (event) {
				if ($(event.target).hasClass("btn")) {
					event.preventDefault(); // no button actions...
					$("#theTarget").load("ajaxLoadFromServer.aspx #container"); // load from this id (#container)
				}
			});
		});
/*
		$(document).ready(function () {

			$("#btnChatTextSend").bind("click", function (event) {
				msg = $("#chatText").val();
				$.post("Ajax/ChatMessages.aspx", { action: ""  }, function (data) {
					$("#targetDiv").html(data);
				});
			});
			$("#btnChatTextSend").bind("click", function (event) {
				msg = $("#chatText").val();
				$.post("Ajax/ChatMessages.aspx", { action: "" }, function (data) {
					$("#targetDiv").html(data);
				});
			});
		});
*/
	</script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
		<label>chatnavn</label><br />
		<input id="chatName" style="width: 195px" /><br />
		<button id="btnChatNameSend" class="btn" style="width: 200px" data-action="save" data-source="#chatName">Gem</button>
	<div id="#chatRows"style="height:400px; width:200px; background-color:#EEEEEE">
	</div>
		<label>besked</label><br />
		<input id="chatText" style="width:195px"/><br />
		<button id="btnChatTextSend" class="btn" style="width: 200px" data-action="prepend" data-target="#chatText">Send</button>
</asp:Content>