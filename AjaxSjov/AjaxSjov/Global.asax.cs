using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace AjaxSjov
{

    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            Application["Sessions"] = new Dictionary<string, UserSession>();
        }

        void Application_End(object sender, EventArgs e)
        {
        }

        void Application_Error(object sender, EventArgs e)
        {
        }

        void Session_Start(object sender, EventArgs e)
        {
            ((Dictionary<string, UserSession>)Application["Sessions"]).Add(this.Session.SessionID, new UserSession(this.Session, ""));
        }

        void Session_End(object sender, EventArgs e)
        {
            ((Dictionary<string, UserSession>)Application["Sessions"]).Remove(this.Session.SessionID);
        }

    }
}
