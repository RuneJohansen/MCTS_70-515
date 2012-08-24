<%@ page title="Home Page" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" codebehind="Default.aspx.cs" inherits="RobotWars.Default" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>
<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">
	<asp:fileupload id="robotFile1" runat="server" /><br />
	<asp:fileupload id="robotFile2" runat="server" /><br /><br />
	<asp:button id="Start" runat="server" text="Start" onclick="Start_Click" />
	<br /><br /><br />
	<asp:panel id="pnlContenders" cssclass="pnlContenders" runat="server">
		<asp:panel id="pnlContender1" cssclass="divContender1" runat="server"><strong>Contender 1</strong><br /><br /></asp:panel>
		<asp:panel id="pnlContender2" cssclass="divContender2" runat="server"><strong>Contender 2</strong><br /><br /></asp:panel>
	</asp:panel>
	<br /><br />
	<br />
	<asp:panel id="pnlContenders2" cssclass="pnlContenders" runat="server">
		<asp:panel id="pnlContender1After" cssclass="divContender1" runat="server">
			<strong>Contender 1</strong><br />
			<br />
		</asp:panel>
		<asp:panel id="pnlContender2After" cssclass="divContender2" runat="server">
			<strong>Contender 2</strong><br />
			<br />
		</asp:panel>
	</asp:panel>
</asp:content>
