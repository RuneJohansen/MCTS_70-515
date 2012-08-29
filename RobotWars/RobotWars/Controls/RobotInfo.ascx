<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RobotInfo.ascx.cs" Inherits="RobotWars.Controls.RobotInfo" %>
<div class="row-fluid">
	<div class="span6">
		<label class="form-inline">Navn</label>
		<asp:textbox cssclass="input-large uneditable-input bold" readonly="true" id="tbName" runat="server"></asp:textbox>
	</div>
	<div class="offset2 span2">
		<label class="form-inline">Liv</label>
		<asp:textbox cssclass="input-mini uneditable-input bold" readonly="true" id="tbLives" runat="server"></asp:textbox>
	</div>
</div>
<div class="row-fluid">
	<div class="span2">
		<label class="form-inline">Sejre</label>
		<asp:textbox cssclass="input-mini uneditable-input" readonly="true" id="tbWins" runat="server"></asp:textbox>
	</div>
	<div class="offset2 span2">
		<label class="form-inline">Uafjorte</label>
		<asp:textbox cssclass="input-mini uneditable-input" readonly="true" id="tbDraws" runat="server"></asp:textbox>
	</div>
	<div class="offset2 span2">
		<label class="form-inline">Nederlag</label>
		<asp:textbox cssclass="input-mini uneditable-input" readonly="true" id="tbLosses" runat="server"></asp:textbox>
	</div>
</div>
<div class="row-fluid">
	<div class="span12">
		<hr />
	</div>
</div>
<div class="row-fluid">
	<div class="span12" style="text-align:center;">
		<asp:image imageurl="~/Styles/robot1.png" height="200px" width="130px" id="img" runat="server" />
	</div>
</div>
<div class="row-fluid">
	<div class="span12">
		<hr />
	</div>
</div>
<div class="row-fluid">
	<div class="span12">
		<asp:textbox id="tbRounds" cssclass="input-large" width="100%" runat="server" height="100">
			
		</asp:textbox>
	</div>
</div>
