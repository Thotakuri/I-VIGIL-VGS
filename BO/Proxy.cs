/* 
 * Provigil Surveillance Limited 
 */ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace I_vigil.BO
{
    public class Proxy
    {
        string host = "";

        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        int port = 80;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        string userName = "";

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        string password = "";

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
