using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WntifDB;
using System.Xml.XPath;
namespace WntifDB
{

    class Authorization
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        string Access;
        string Password;
        string Name;
        public Authorization(string login, string password)
        {
            Access = "User";
            Name = login;
            Password = password;
            
        }
        public void LogIn()
        {
            try
            {

                bool EcxistUser = false;
                XDocument xdoc = XDocument.Load(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\InfoAboutUsers.xml");
                XElement users = xdoc.Element("Users");
                foreach (XElement k in users.Elements("User"))
                {
                if (k.Element("password").Value == Password && k.Attribute("name").Value == Name)
                {
                    EcxistUser = true;
                    Access = k.Element("access").Value;
                }
                }

                // var FindNode = users.Descendants().SingleOrDefault(el => el.Attribute("name").Value == Name);
                if (EcxistUser==true)
                {
                    Console.WriteLine("Welcome " + Name + "! (Access:{0})",Access);
                  
                }
                else
                {
                    Console.WriteLine("This account not is ecxist!");
                    Access = "User";
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
            }
        }

    }
}
