using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

abstract class SticksGame
{
    private int numberOfPlayers;
    private string[] playerName = new string[2] { "", "" };

    private int sticksCount;
    private int currentPlayer;
    private void test()
    {
        string name;
        string filePatch = "";
        int wins, draws, losses, lives, weapon, shield;
        List<Round> rounds = new List<Round>();

        XElement xmlFile = XElement.Load(filePath);

        IEnumerable<Xelement> robot = from elem in xmlFile.Descendants() select elem;

        name = (from elem in robot.Elements("navn") select elem.ToString()).First();

        int.TryParse((from elem in robot.Elements("sejre") select elem.ToString()).First(), out wins);
        int.TryParse((from elem in robot.Elements("uafgjort") select elem.ToString()).First(), out draws);
        int.TryParse((from elem in robot.Elements("tab") select elem.ToString()).First(), out losses);
        int.TryParse((from elem in robot.Elements("liv") select elem.ToString()).First(), out lives);


    }
    public bool ComputerOpponent
    {
        get { return this.Player2.ToLower() == "computer"; }

    }
    public int SticksCount
    {
        get { return this.sticksCount; }
    }
    public string Player1
    {
        get { return (this.playerName[0] != "") ? this.playerName[0] : "Ukendt"; }
        set { this.playerName[0] = value; }
    }
    public string Player2
    {
        get { return (this.playerName[1] != "") ? this.playerName[1] : "Computer"; }
        set { this.playerName[1] = value; }
    }
    public string CurrentPlayer
    {
        get { return this.playerName[this.currentPlayer]; }
    }
    public void NewGame()
    {
        this.initNewGame(0,"","");
    }
    public void NewGame(int numberOfPlayers)
    {
        this.initNewGame(numberOfPlayers,"","");
    }
    public void NewGame(string playerName)
    {
        this.initNewGame(1,playerName,"");
    }
    public void NewGame(string player1Name, string player2Name)
    {
        this.initNewGame(2, player1Name, player2Name);
    }
    private void initNewGame(int numberOfPlayers, string player1Name, string player2Name)
    {
        this.numberOfPlayers = numberOfPlayers;
        this.playerName[0] = player1Name;
        this.playerName[1] = player2Name;
    }

    public void StartGame()
    {
        if (playerName[0] == "")
        {
            this.ShowMessage("Kan ikke starte spil uden mindst én spiller");
            return;
        }
        this.sticksCount = 15;
        this.ShowSticks();
    }

    
    public void ComputerTurn()
    {
        // difficulty..
        this.RemoveSticks(1);
        
    }
    public void RemoveSticks(int count)
    {
        if (this.sticksCount >= count)
        {
            this.sticksCount -= count;
            this.ShowSticks();
            if (this.sticksCount == 0)
            {
                this.ShowWinner();
                this.EndGame();
            }
            this.currentPlayer = 1 - this.currentPlayer;
        }
        else
            this.ShowMessage(String.Format("Kan ikke fjerne {0} {1}", count, count > 1 ? "pinde" : "pind"));
    }

    public void EndGame()
    {
        this.ShowMenu();
    }
    
    /// <summary>
    /// Display sticks on screen
    /// </summary>
    public abstract void ShowSticks();
    /// <summary>
    /// Input number of sticks
    /// </summary>
    public abstract void PlayerPick();
    /// <summary>
    /// Display player name(s)
    /// </summary>
    public abstract void ShowMessage(string message);
    /// <summary>
    /// Display game menu
    /// </summary>
    public abstract void ShowMenu();
    /// <summary>
    /// Display name of the winner
    /// </summary>
    public abstract void ShowWinner();


}
