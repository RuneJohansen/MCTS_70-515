using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;

abstract class WarEngine
{
    public Robot Robot1;
    public Robot Robot2;

    public int NumberOfRounds;
    public int MinimumNumberOfRounds = 10;
    public int CurrentRound;
    public List<List<RoundInformation>> BattleRounds;

    private string connString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

    public void NewWar(string filePath1, string filePath2)
    {
        this.Robot1 = this.LoadRobotData(filePath1);
        this.Robot2 = this.LoadRobotData(filePath2);
        if (this.Robot1 == null || this.Robot2 == null)
        {
            this.ShowMessage(9, "Robotfil kunne ikke indlæses");
            return;
        }
    }

    public void StartWar(long robot1ID, long robot2ID)
    {
        this.Robot1 = new Robot();
        this.Robot1.LoadFromDB(robot1ID);
        this.Robot2 = new Robot();
        this.Robot2.LoadFromDB(robot2ID);
        this.StartWar();
    }

    public void StartWar()
    {
        this.NumberOfRounds = this.Robot1.Rounds.Count > this.Robot2.Rounds.Count ? this.Robot1.Rounds.Count : this.Robot2.Rounds.Count;
        if (this.NumberOfRounds < this.MinimumNumberOfRounds)
            this.NumberOfRounds = this.MinimumNumberOfRounds;

        this.BattleRounds = new List<List<RoundInformation>>();

        int idx1, idx2;

        for (int i = 0; i < this.NumberOfRounds; i++)
        {
            bool robot1Hit = false;
            bool robot2Hit = false;

            this.CurrentRound = i;

            if (this.CurrentRound >= this.Robot1.Rounds.Count && this.CurrentRound != 0)
                idx1 = this.CurrentRound % this.Robot1.Rounds.Count;
            else
                idx1 = this.CurrentRound;
            if (this.CurrentRound >= this.Robot2.Rounds.Count && this.CurrentRound != 0)
                idx2 = this.CurrentRound % this.Robot2.Rounds.Count;
            else
                idx2 = this.CurrentRound;

            if (this.Robot1.Rounds[idx1].Weapon != this.Robot2.Rounds[idx2].Shield)
                robot2Hit = true;
            if (this.Robot2.Rounds[idx2].Weapon != this.Robot1.Rounds[idx1].Shield)
                robot1Hit = true;

            List<RoundInformation> roundInfo = new List<RoundInformation>();
            roundInfo.Add(new RoundInformation(this.CurrentRound, this.Robot1.Rounds[idx1].Weapon, this.Robot1.Rounds[idx1].Shield, robot1Hit));
            roundInfo.Add(new RoundInformation(this.CurrentRound, this.Robot2.Rounds[idx2].Weapon, this.Robot2.Rounds[idx2].Shield, robot2Hit));
            this.BattleRounds.Add(roundInfo);

            if (robot1Hit)
                this.Robot1.Lives--;
            if (robot2Hit)
                this.Robot2.Lives--;

            this.ShowRound(roundInfo[0], roundInfo[1]);

            if (this.Robot1.Lives < 0 || this.Robot2.Lives < 0)
                break;
        }

        if (this.Robot1.Lives < 0)
        {
            if (this.Robot2.Lives < 0)
            {
                this.Robot1.Draws++;
                this.Robot2.Draws++;
            }
            else
            {
                this.Robot2.Wins++;
                this.Robot1.Losses++;
            }
        }

        if (this.Robot2.Lives < 0)
        {
            if (this.Robot1.Lives < 0)
            {
                this.Robot1.Draws++;
                this.Robot2.Draws++;
            }
            else
            {
                this.Robot1.Wins++;
                this.Robot2.Losses++;
            }
        }
    }


    private Robot LoadRobotData(string filePath)
    {
        List<Round> rounds = new List<Round>();
        Robot robot = null;
        XElement xmlFile = XElement.Load(filePath);

        var r = from elem in xmlFile.DescendantsAndSelf("robot")
                select new
                {
                    Name = (string)elem.Element("navn"),
                    Lives = (int)elem.Element("liv"),
                    Wins = (int)elem.Element("sejre"),
                    Draws = (int)elem.Element("uafgjort"),
                    Losses = (int)elem.Element("tab")
                };

        var rnds = from elem in xmlFile.Descendants("runde")
                   select new
                   {
                       Shield = Int32.Parse(elem.Attribute("skjold").Value.ToString()),
                       Weapon = Int32.Parse(elem.Attribute("vaaben").Value.ToString())
                   };

        foreach (var round in rnds)
            rounds.Add(new Round(round.Shield, round.Weapon));

        foreach (var rob in r)
            robot = new Robot(filePath, rob.Name, rob.Lives, rob.Wins, rob.Draws, rob.Losses, rounds);

        if (robot.Rounds.Count == 0)
            return null;
        else
            return robot;

    }

    private void SaveRobotData(string filePath)
    {

        XmlTextWriter writer = new XmlTextWriter(filePath, System.Text.Encoding.UTF8);
    }

    public DataSet GetContendersNameAndID()
    {
        DataSet ds = new DataSet();
        using (SqlConnection conn = new SqlConnection(this.connString))
        {
            SqlCommand cmd = new SqlCommand("SELECT RobotName, RobotID FROM Robot", conn);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            conn.Open();
            da.Fill(ds);
            conn.Close();
        }
        return ds;
    }
    public abstract void ShowRound(RoundInformation robot1RoundInfo, RoundInformation robot2RoundInfo);
    public abstract void ShowMessage(int typeId, string message);
}