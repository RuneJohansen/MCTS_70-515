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
            public System.Text.StringBuilder rounds1 = new System.Text.StringBuilder();
            public System.Text.StringBuilder rounds2 = new System.Text.StringBuilder();

            public override void ShowRound(bool robot1Hit, bool robot2Hit)
            {

                rounds1.Append(String.Format("<span class='{0}'>W[{1}] S[{2}]</span><br />", robot1Hit ? "robotHit" : "robotMissed", robot1.AttackList[CurrentRound].Weapon, robot1.AttackList[CurrentRound].Shield));
                rounds2.Append(String.Format("<span class='{0}'>W[{1}] S[{2}]</span><br />", robot2Hit ? "robotHit" : "robotMissed", robot2.AttackList[CurrentRound].Weapon, robot2.AttackList[CurrentRound].Shield));

                /*
                    robot1.Name, robot1.AttackList[CurrentRound].Weapon, robot1.AttackList[CurrentRound].Shield,
                    robot2.Name, robot2.AttackList[CurrentRound].Weapon, robot2.AttackList[CurrentRound].Shield,
                    robot1.Name, robot1Hit ? "was hit" : "was missed",
                    robot2.Name, robot2Hit ? "was hit" : "was missed"));
                 * */
            }
            public override void ShowMessage(int typeId, string message)
            {
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Start_Click(object sender, EventArgs e)
        {
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
                lit1.Text = String.Format(@"<strong>{0}</strong><br />Lives:  {1}<br /><br />Wins:   {2}<br />Draws:  {3}<br />Losses: {4}<br />", war.robot1.Name, war.robot1.Lives, war.robot1.Wins, war.robot1.Draws, war.robot1.Losses);
                this.pnlContender1.Controls.Add(lit1);

                Literal lit2 = new Literal();
                lit2.Text = String.Format(@"<strong>{0}</strong><br />Lives:  {1}<br /><br />Wins:   {2}<br />Draws:  {3}<br />Losses: {4}<br />", war.robot2.Name, war.robot2.Lives, war.robot2.Wins, war.robot2.Draws, war.robot2.Losses);
                this.pnlContender2.Controls.Add(lit2);

                war.StartWar();

                Literal litRounds1 = new Literal();
                litRounds1.Text = war.rounds1.ToString();
                pnlContender1.Controls.Add(litRounds1);

                Literal litRounds2 = new Literal();
                litRounds2.Text = war.rounds1.ToString();
                pnlContender2.Controls.Add(litRounds2);

                Literal lit3 = new Literal();
                lit3.Text = String.Format(@"<strong>{0}</strong><br />Lives:  {1}<br /><br />Wins:   {2}<br />Draws:  {3}<br />Losses: {4}<br />", war.robot1.Name, war.robot1.Lives, war.robot1.Wins, war.robot1.Draws, war.robot1.Losses);
                this.pnlContender1After.Controls.Add(lit3);

                Literal lit4 = new Literal();
                lit4.Text = String.Format(@"<strong>{0}</strong><br />Lives:  {1}<br /><br />Wins:   {2}<br />Draws:  {3}<br />Losses: {4}<br />", war.robot2.Name, war.robot2.Lives, war.robot2.Wins, war.robot2.Draws, war.robot2.Losses);
                this.pnlContender2After.Controls.Add(lit4);

            }
        }

    }
}