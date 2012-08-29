<%@ page title="Home Page" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" codebehind="Default.aspx.cs" inherits="RobotWars.Default" %>
<%@ register src="~/Controls/RobotInfo.ascx" tagprefix="uc" tagname="RobotInfo" %>

<asp:content id="HeaderContent" runat="server" contentplaceholderid="HeadContent">
</asp:content>

<asp:content id="BodyContent" runat="server" contentplaceholderid="MainContent">
	<div class="row-fluid">
		<div class="span5">
			<label>XML-fil</label>
			<asp:fileupload id="robotFile1" width="100%" class="btn" runat="server" />&nbsp;
		</div>
		<div class="span2" style="text-align: left;">
			<label>&nbsp;</label>
			<asp:button id="save" class="btn btn-primary" width="100%" runat="server" text="Upload robot" onclick="save_Click" />
		</div>
		<div class="span5">
		</div>
	</div>
	<div class="row-fluid">
		<div class="span12">
			<hr />
		</div>
	</div>
	<!--
	<div class="row-fluid">
		<div class="span12">
			<asp:fileupload id="robotFile2" class="btn" runat="server" /><br />
		</div>
	</div>
-->

	<div class="row-fluid">
		<div class="span5">
			<strong>Contender 1</strong><br />
			<asp:dropdownlist id="ddContender1" width="100%" runat="server" autopostback="True" onselectedindexchanged="ddContender1_SelectedIndexChanged" />
		</div>
		<div class="span2" style="text-align:center;">
			<strong>vs</strong><br />
			<asp:button class="btn btn-danger" width="100%" id="Start" runat="server" text="Start" onclick="Start_Click" />
		</div>
		<div class="span5">
			<strong>Contender 2</strong><br />
			<asp:dropdownlist id="ddContender2" width="100%" runat="server" autopostback="True" onselectedindexchanged="ddContender2_SelectedIndexChanged" />
		</div>
	</div>
	<div class="row-fluid">
		<div class="span12">
			<hr />
		</div>
	</div>
	<div class="row-fluid">
		<div class="span5">
			<uc:robotinfo id="ucRobot1Info" robotnumber="1" imagefilepath="~/Styles/robot1.png" runat="server" />
		</div>
		<div class="span2" style="text-align: center;">
		</div>
		<div class="span5">
			<uc:robotinfo id="ucRobot2Info" robotnumber="2" imagefilepath="~/Styles/robot2.png" runat="server" />
		</div>
	</div>
	<img id="imgWeapon" src="" height="200px" width="130px" hidden="hidden" alt="" />
	<img id="imgShield" src="" height="200px" width="130px" hidden="hidden" alt="" />
</asp:content>
