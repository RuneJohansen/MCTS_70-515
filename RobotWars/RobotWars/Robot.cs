using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

class Robot
{
    public string Name;
    public int Lives;
    public int Wins;
    public int Draws;
    public int Losses;
    public List<Round> Rounds;

    private string filePath;
    private XElement xml;

    public Robot(string filePath, string name, int lives, int wins, int draws, int losses, List<Round> rounds)
    {
        this.filePath = filePath;

        this.Name = name;
        this.Lives = lives;
        this.Wins = wins;
        this.Draws = draws;
        this.Losses = losses;
        this.Rounds = rounds;
    }

    public XElement Xml
    {
        get { return this.xml; }
        set { this.xml = value; }
    }

}

class Round
{
    public int Shield;
    public int Weapon;

    public Round(int shield, int weapon)
    {
        this.Shield = shield;
        this.Weapon = weapon;
    }

}