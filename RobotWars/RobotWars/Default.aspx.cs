using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace RobotWars
{

    public partial class Default : System.Web.UI.Page
    {
        class RobotWar : WarEngine
        {
            public System.Text.StringBuilder sbRound = new System.Text.StringBuilder();
            public System.Text.StringBuilder rounds2 = new System.Text.StringBuilder();

            public override void ShowRound(RoundInformation robot1RoundInfo, RoundInformation robot2RoundInfo)
            {
                if (robot2RoundInfo.WasHit)
                    sbRound.Append(String.Format("ShowHit({0},{1});", 1, robot1RoundInfo.Weapon));
//                else
//                    sbRound.Append(String.Format("ShowMiss({0},{1},{2});", 1, robot1RoundInfo.Weapon, robot2RoundInfo.Shield));
                if (robot1RoundInfo.WasHit)
                    sbRound.Append(String.Format("ShowHit({0},{1});", 2, robot2RoundInfo.Weapon));
//                else
//                    sbRound.Append(String.Format("ShowMiss({0},{1},{2});", 2, robot2RoundInfo.Weapon, robot1RoundInfo.Shield));

                //sbRound.Append(String.Format("<span class='{0}'>W[{1}] S[{2}]</span><br />", robot1RoundInfo.WasHit ? "robotHit" : "robotMissed", robot1RoundInfo.Weapon, robot1RoundInfo.Shield));
                //rounds2.Append(String.Format("<span class='{0}'>W[{1}] S[{2}]</span><br />", robot2RoundInfo.WasHit ? "robotHit" : "robotMissed", robot2RoundInfo.Weapon, robot2RoundInfo.Shield));
            }
            public override void ShowMessage(int typeId, string message)
            {
            }
        }

        Robot robot1 = new Robot();
        Robot robot2 = new Robot();
        RobotWar warEngine = new RobotWar();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddContender1.DataSource = this.warEngine.GetContendersNameAndID();
                this.ddContender1.DataTextField = "RobotName";
                this.ddContender1.DataValueField = "RobotID";
                this.ddContender1.DataBind();
                if (ddContender1.Items.Count > 0)
                {
                    ddContender1.SelectedValue = ddContender1.Items[0].Value;
                    this.robot1.LoadFromDB(long.Parse(ddContender1.Items[0].Value));
                    this.showRobot1();
                }
                this.ddContender2.DataSource = this.warEngine.GetContendersNameAndID();
                this.ddContender2.DataTextField = "RobotName";
                this.ddContender2.DataValueField = "RobotID";
                this.ddContender2.DataBind();
                if (ddContender2.Items.Count > 0)
                {
                    ddContender2.SelectedValue = ddContender2.Items[0].Value;
                    this.robot2.LoadFromDB(long.Parse(ddContender2.Items[0].Value));
                    this.showRobot2();
                }
            }
        }

        protected void ddContender1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.robot1.LoadFromDB(long.Parse(ddContender1.SelectedValue));
            this.showRobot1();
        }
        protected void ddContender2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.robot2.LoadFromDB(long.Parse(ddContender2.SelectedValue));
            this.showRobot2();
        }
        private void showRobot1()
        {
            this.ucRobot1Info.Name = this.robot1.Name;
            this.ucRobot1Info.Lives = this.robot1.Lives;
            this.ucRobot1Info.Wins = this.robot1.Wins;
            this.ucRobot1Info.Draws = this.robot1.Draws;
            this.ucRobot1Info.Losses = this.robot1.Losses;
        }
        private void showRobot2()
        {
            this.ucRobot2Info.Name = this.robot2.Name;
            this.ucRobot2Info.Lives = this.robot2.Lives;
            this.ucRobot2Info.Wins = this.robot2.Wins;
            this.ucRobot2Info.Draws = this.robot2.Draws;
            this.ucRobot2Info.Losses = this.robot2.Losses;

        }

        protected void save_Click(object sender, EventArgs e)
        {
            if (robotFile1.HasFile)
            {
                string file1 = Server.MapPath("~/Files/") + robotFile1.FileName;
                if (System.IO.File.Exists(file1))
                    System.IO.File.Delete(file1);
                robotFile1.SaveAs(file1);
                Robot robot = new Robot();
                robot.LoadFromXml(file1);
                if (robot.Rounds != null && robot.Rounds.Count > 0)
                {
                    if (robot.VerifyUniqueRobot())
                        robot.SaveInDB();
                }
                System.IO.File.Delete(file1);
            }
        }

        protected void Start_Click(object sender, EventArgs e)
        {
            warEngine.StartWar(long.Parse(ddContender1.SelectedValue), long.Parse(ddContender2.SelectedValue));
            ClientScriptManager csm = Page.ClientScript;
            csm.RegisterStartupScript(this.GetType(), "animScript", this.warEngine.sbRound.ToString(), true); 

            #region oldcrap
            /*
            if (robotFile1.HasFile && robotFile2.HasFile)
            {
                string file1 = Server.MapPath("~/Files/") + robotFile1.FileName;
                string file2 = Server.MapPath("~/Files/") + robotFile2.FileName;
                if (System.IO.File.Exists(file1))
                    System.IO.File.Delete(file1);
                if (System.IO.File.Exists(file2))
                    System.IO.File.Delete(file2);
                robotFile1.SaveAs(file1);
                robotFile2.SaveAs(file2);

                RobotWar war = new RobotWar();
                war.NewWar(file1, file2);
                Literal lit1 = new Literal();
                lit1.Text = String.Format(@"<strong>{0}</strong><br />Lives:  {1}<br /><br />Wins:   {2}<br />Draws:  {3}<br />Losses: {4}<br />", war.Robot1.Name, war.Robot1.Lives, war.Robot1.Wins, war.Robot1.Draws, war.Robot1.Losses);
                this.pnlContender1.Controls.Add(lit1);

                Literal lit2 = new Literal();
                lit2.Text = String.Format(@"<strong>{0}</strong><br />Lives:  {1}<br /><br />Wins:   {2}<br />Draws:  {3}<br />Losses: {4}<br />", war.Robot2.Name, war.Robot2.Lives, war.Robot2.Wins, war.Robot2.Draws, war.Robot2.Losses);
                this.pnlContender2.Controls.Add(lit2);

                war.StartWar();
             
                Literal litRounds1 = new Literal();
                litRounds1.Text = war.rounds1.ToString();
                pnlContender1.Controls.Add(litRounds1);

                Literal litRounds2 = new Literal();
                litRounds2.Text = war.rounds1.ToString();
                pnlContender2.Controls.Add(litRounds2);

                Literal lit3 = new Literal();
                lit3.Text = String.Format(@"<strong>{0}</strong><br />Lives:  {1}<br /><br />Wins:   {2}<br />Draws:  {3}<br />Losses: {4}<br />", war.Robot1.Name, war.Robot1.Lives, war.Robot1.Wins, war.Robot1.Draws, war.Robot1.Losses);
                this.pnlContender1After.Controls.Add(lit3);

                Literal lit4 = new Literal();
                lit4.Text = String.Format(@"<strong>{0}</strong><br />Lives:  {1}<br /><br />Wins:   {2}<br />Draws:  {3}<br />Losses: {4}<br />", war.Robot2.Name, war.Robot2.Lives, war.Robot2.Wins, war.Robot2.Draws, war.Robot2.Losses);
                this.pnlContender2After.Controls.Add(lit4);
            }
*/
            #endregion oldcrap
        }

    }
}