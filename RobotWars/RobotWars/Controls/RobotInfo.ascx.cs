using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RobotWars.Controls
{
    public partial class RobotInfo : System.Web.UI.UserControl
    {
        private int robotnumber = 0;
        public int RobotNumber
        {
            get { return this.robotnumber; }
            set
            {
                this.robotnumber = value;
                this.img.CssClass = String.Format("imgR{0}", this.robotnumber);
            }
        }

        public string Name
        {
            get { return this.tbName.Text;  }
            set { this.tbName.Text = value;  }
        }
        public int Lives
        {
            get { return int.Parse(this.tbLives.Text); }
            set { this.tbLives.Text = value.ToString(); }
        }
        public int Wins
        {
            get { return int.Parse(this.tbWins.Text); }
            set { this.tbWins.Text = value.ToString(); }
        }
        public int Draws
        {
            get { return int.Parse(this.tbDraws.Text); }
            set { this.tbDraws.Text = value.ToString(); }
        }
        public int Losses
        {
            get { return int.Parse(this.tbLosses.Text); }
            set { this.tbLosses.Text = value.ToString(); }
        }
        public string ImageFilePath
        {
            get { return this.img.ImageUrl; }
            set { this.img.ImageUrl = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}