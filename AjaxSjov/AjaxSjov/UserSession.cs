using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace AjaxSjov
{
    class UserSession
    {
        public string Name;
        public HttpSessionState Session;

        public UserSession(HttpSessionState session, string name)
        {
            this.Name = name;
            this.Session = session;
        }
    }


}