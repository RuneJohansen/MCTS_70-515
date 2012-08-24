using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

[Serializable]
class Robot
{
    public string Name;
    public int Lives;
    public int Wins;
    public int Draws;
    public int Losses;
    public List<Attack> AttackList;

    private string filePath;

    public Robot(string filePath, string name, int lives, int wins, int draws, int losses, List<Attack> attackList)
    {
        this.filePath = filePath;

        this.Name = name;
        this.Lives = lives;
        this.Wins = wins;
        this.Draws = draws;
        this.Losses = losses;
        this.AttackList = attackList;
    }
}

class Attack
{
    public int Shield;
    public int Weapon;

    public Attack(int shield, int weapon)
    {
        this.Shield = shield;
        this.Weapon = weapon;
    }

}

abstract class WarEngine
{
    public Robot robot1;
    public Robot robot2;
    
    public int NumberOfRounds;
    public int MinimumNumberOfRounds = 10;
    public int CurrentRound;

    public void NewWar(string filePath1, string filePath2)
    {
        this.robot1 = this.LoadRobotData(filePath1);
        this.robot2 = this.LoadRobotData(filePath2);
        if (this.robot1 == null || this.robot2 == null)
        {
            this.ShowMessage(9, "Robotfil kunne ikke indlæses");
            return;
        }
        this.alignNumberOfRounds();
    }

    public void StartWar()
    {
        for (int i = 0; i < this.NumberOfRounds; i++)
        {
            bool robot1Hit = false;
            bool robot2Hit = false;

            this.CurrentRound = i + 1;

            if (this.robot1.AttackList[i].Weapon != this.robot2.AttackList[i].Shield)
                robot2Hit = true;
            if (this.robot2.AttackList[i].Weapon != this.robot1.AttackList[i].Shield)
                robot1Hit = true;
            if (robot1Hit)
                this.robot1.Lives--;
            if (robot2Hit)
                this.robot2.Lives--;

            this.ShowRound(robot1Hit, robot2Hit);

            if (this.robot1.Lives < 0 || this.robot2.Lives < 0)
                break;
        }

        if (this.robot1.Lives < 0)
        {
            if (this.robot2.Lives < 0)
            {
                this.robot1.Draws++;
                this.robot2.Draws++;
            }
            else
            {
                this.robot2.Wins++;
                this.robot1.Losses++;
            }
        }

        if (this.robot2.Lives < 0)
        {
            if (this.robot1.Lives < 0) // ...i tilfælde af at begge kan skyde samtidig
            {
                this.robot1.Draws++;
                this.robot2.Draws++;
            }
            else
            {
                this.robot1.Wins++;
                this.robot2.Losses++;
            }
        }
    }


    private Robot LoadRobotData(string filePath)
    {
        XmlTextReader reader = new XmlTextReader(filePath);
        reader.WhitespaceHandling = WhitespaceHandling.None;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(reader);
        reader.Close();

        string name;
        int wins, draws, losses, lives, weapon, shield;
        List<Attack> attackList = new List<Attack>();


        name = xmlDoc.GetElementsByTagName("navn").Item(0).InnerText;
        int.TryParse(xmlDoc.GetElementsByTagName("sejre").Item(0).InnerText, out wins);
        int.TryParse(xmlDoc.GetElementsByTagName("uafgjort").Item(0).InnerText, out draws);
        int.TryParse(xmlDoc.GetElementsByTagName("tab").Item(0).InnerText, out losses);
        int.TryParse(xmlDoc.GetElementsByTagName("liv").Item(0).InnerText, out lives);

        XmlNode node = xmlDoc.GetElementsByTagName("runder").Item(0);
        if (node.HasChildNodes)
            foreach (XmlElement elem in node.ChildNodes)
            {
                if (elem.Name == "runde")
                {
                    int.TryParse(elem.Attributes["skjold"].Value, out shield);
                    int.TryParse(elem.Attributes["vaaben"].Value, out weapon);
                    attackList.Add(new Attack(shield, weapon));
                }
            }

        if (attackList.Count == 0)
            return null;
        else
            return new Robot(filePath, name, lives, wins, draws, losses, attackList);
    }
    private void SaveRobotData(string filePath)
    {

        XmlTextWriter writer = new XmlTextWriter(filePath, System.Text.Encoding.UTF8);
    }


    private void alignNumberOfRounds()
    {
        this.NumberOfRounds = this.robot1.AttackList.Count > this.robot2.AttackList.Count ? this.robot1.AttackList.Count : this.robot2.AttackList.Count;
        if (this.NumberOfRounds < this.MinimumNumberOfRounds)
            this.NumberOfRounds = this.MinimumNumberOfRounds;

        this.robot1.AttackList = this.populateRobotAttackList(this.NumberOfRounds, robot1);
        this.robot2.AttackList = this.populateRobotAttackList(this.NumberOfRounds, robot2);
    }

    private List<Attack> populateRobotAttackList(int maxRounds, Robot robot)
    {
        List<Attack> newList = new List<Attack>();
        newList.AddRange(robot.AttackList);

        if (robot.AttackList.Count < maxRounds)
        {
            while (newList.Count < maxRounds)
            {
                newList.AddRange(robot.AttackList);
                if (newList.Count > maxRounds)
                    newList.RemoveRange(maxRounds - 1, newList.Count - maxRounds);
            }
        }
        return newList;
    }

    public abstract void ShowRound(bool robot1Hit, bool robot2Hit);
    public abstract void ShowMessage(int typeId, string message);
}