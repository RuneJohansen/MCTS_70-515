using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Data.SqlClient;

class Robot
{
    public long RobotID;
    public string Name;
    public int Lives;
    public int Wins;
    public int Draws;
    public int Losses;
    public List<Round> Rounds;

    private string filePath;

    private string connString = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

    public Robot()
    {
    }

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
    public void LoadFromDB(long robotID)
    {
        using (SqlConnection conn = new SqlConnection(this.connString))
        {
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 * FROM Robot WHERE RobotID = @RobotID", conn);
            cmd.Parameters.AddWithValue("@RobotID", robotID);
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    long.TryParse(reader["RobotID"].ToString(), out this.RobotID);
                    this.Name = reader["RobotName"].ToString();
                    int.TryParse(reader["Lives"].ToString(), out this.Lives);
                    int.TryParse(reader["Wins"].ToString(), out this.Wins);
                    int.TryParse(reader["Draws"].ToString(), out this.Draws);
                    int.TryParse(reader["Losses"].ToString(), out this.Losses);
                }
            }
            cmd = new SqlCommand("SELECT Weapon, Shield FROM RobotRounds WHERE RobotID = @RobotID ORDER BY RoundNumber", conn);
            cmd.Parameters.AddWithValue("@RobotID", robotID);
            this.Rounds = new List<Round>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    this.Rounds.Add(new Round(int.Parse(reader["Shield"].ToString()), int.Parse(reader["Weapon"].ToString())));
            }
        }
    }
    
    public bool VerifyUniqueRobot()
    {
        object o = null;
        using (SqlConnection conn = new SqlConnection(this.connString))
        {
            SqlCommand cmd = new SqlCommand("SELECT TOP 1 RobotID FROM Robot WHERE RobotName = @RobotName", conn);
            cmd.Parameters.AddWithValue("@RobotName", this.Name);
            conn.Open();
            o = cmd.ExecuteScalar();
        }
        if (o == null || o == System.DBNull.Value)
            return true;
        else
            return false;
    }

    public string SaveInDB()
    {
        string message = "";
        using (SqlConnection conn = new SqlConnection(this.connString))
        {
            SqlCommand cmd = new SqlCommand("INSERT Robot (RobotName, Lives, Wins, Draws, Losses)  OUTPUT INSERTED.RobotID VALUES (@RobotName, @Lives, @Wins, @Draws, @Losses)", conn);
            cmd.Parameters.AddWithValue("@RobotName", this.Name);
            cmd.Parameters.AddWithValue("@Lives", this.Lives);
            cmd.Parameters.AddWithValue("@Wins", this.Wins);
            cmd.Parameters.AddWithValue("@Draws", this.Draws);
            cmd.Parameters.AddWithValue("@Losses", this.Losses);
            conn.Open();

            this.RobotID = (long)cmd.ExecuteScalar();

            if (this.RobotID != 0)
            {
                cmd = new SqlCommand("INSERT RobotRounds (RobotID, RoundNumber, Weapon, Shield) VALUES (@RobotID, @RoundNumber, @Weapon, @Shield)", conn);
                int rndNo = 1;
                foreach (Round r in this.Rounds)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@RobotID", this.RobotID);
                    cmd.Parameters.AddWithValue("@RoundNumber", rndNo);
                    cmd.Parameters.AddWithValue("@Weapon", r.Weapon);
                    cmd.Parameters.AddWithValue("@Shield", r.Shield);
                    cmd.ExecuteNonQuery();
                    rndNo++;
                }
            }
        }
        if (this.RobotID == 0)
            message = String.Format("Kunne ikke gemme robotten '{0}' - navn findes allerede i databasen", this.Name);
        return message;
    }

    public void LoadFromXml(string filePath)
    {
        List<Round> rounds = new List<Round>();
        XElement xmlFile = XElement.Load(filePath);

        var xmlRobot = from elem in xmlFile.DescendantsAndSelf("robot")
                select new
                {
                    Name = (string)elem.Element("navn"),
                    Lives = (int)elem.Element("liv"),
                    Wins = (int)elem.Element("sejre"),
                    Draws = (int)elem.Element("uafgjort"),
                    Losses = (int)elem.Element("tab")
                };

        var xmlRounds = from elem in xmlFile.Descendants("runde")
                   select new
                   {
                       Shield = Int32.Parse(elem.Attribute("skjold").Value.ToString()),
                       Weapon = Int32.Parse(elem.Attribute("vaaben").Value.ToString())
                   };

        this.Rounds = new List<Round>();
        foreach (var round in xmlRounds)
            this.Rounds.Add(new Round(round.Shield, round.Weapon));

        foreach (var rob in xmlRobot)
        {
            this.filePath = filePath;
            this.Name = rob.Name;
            this.Lives = rob.Lives;
            this.Wins = rob.Wins;
            this.Draws = rob.Draws;
            this.Losses = rob.Losses;
            break;
        }
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