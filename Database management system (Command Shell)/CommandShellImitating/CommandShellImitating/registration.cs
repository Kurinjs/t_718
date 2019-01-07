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
namespace WntifDB
{
    class Registration
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        string access;
        string name;
        string password;
        public Registration(string access, string name, string password)
        {
            logger.Trace("Запуск конструктора, инициализация данных о новом пользователе");
            this.access = access;
            this.name = name;
            this.password = password;
            RecordNewUser();
        }

        private void RecordNewUser()
        {
            logger.Trace("Попытка записи новых данных");
            if (!File.Exists(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)+"\\InfoAboutUsers.xml"))
            {
                XDocument xdoc = new XDocument();
                XElement user = new XElement("User");
                XAttribute NameUser = new XAttribute("name", name);
                XElement AccessUser = new XElement("access", access);
                XElement PasswordUser = new XElement("password", password);
                user.Add(NameUser);
                user.Add(AccessUser);
                user.Add(PasswordUser);
                XElement users = new XElement("Users");
                users.Add(user);
                xdoc.Add(users);
                xdoc.Save(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)+"\\InfoAboutUsers.xml");
                string FileName = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\InfoAboutUsers.xml";
                FileAttributes attribute = File.GetAttributes(FileName);//make hidden file xml
                attribute |= FileAttributes.Hidden;
                File.SetAttributes(FileName, attribute);
                Console.WriteLine("Record data about you is completed, welcome to WntifDB, {0}", name);
                logger.Trace("Запись пользователя {0} завершена, уровень доступа {1}", name, access);
            }
            else
            {
                try
                {


                    XDocument xdoc = XDocument.Load(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)+"\\InfoAboutUsers.xml");
                    XElement users = xdoc.Element("Users");

                    IEnumerable<XElement> usersCheck =
            (from el in xdoc.Elements("Users").Elements("User")
             where (string)el.Attribute("name") == name
             select el);
                    if (usersCheck.Count() > 0)
                        throw new Exception("Name already in use");
                    XElement user = new XElement("User");

                    IEnumerable<XElement> AdminCheck =
            (from el in xdoc.Elements("Users").Elements("User").Elements("access")
             where (string)el.Value == "Admin"
             select el);
                    if (access == "Admin" && AdminCheck.Count() > 0)
                    {
                        Console.Write("Input name and password other admin\nName:");
                        string TempName = Console.ReadLine();
                        Console.Write("Password:");
                        string TempPassword = SecurityPassword.ReadPassword();
                        TempPassword = hashMP5.ConvertToHash(TempPassword);
                        IEnumerable<XElement> PasswordCheck =
           (from el in xdoc.Elements("Users").Elements("User").Elements("password")
            where (string)el.Value == TempPassword
            select el);
                        if (PasswordCheck.Count() == 0)
                            throw new Exception("false input password or name administration");
                    }

                    //FileAttributes attribute = File.GetAttributes(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\InfoAboutUsers.xml");//make hidden file xml
                    //attribute |= FileAttributes.Hidden;
                    //File.SetAttributes(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\InfoAboutUsers.xml", attribute);

                    XAttribute NameUser = new XAttribute("name", name);
                    XElement AccessUser = new XElement("access", access);
                    XElement PasswordUser = new XElement("password", password);
                    user.Add(NameUser);
                    user.Add(AccessUser);
                    user.Add(PasswordUser);
                    users.Add(user);
                    xdoc.Save(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)+"\\InfoAboutUsers.xml");
                    Console.WriteLine("Record data about you is completed\n" +
                        "Name {0} Access:{1}", name, access);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message+"\n"+e.StackTrace);
                    access = "User";
                    name = "Visitor";
                    password = "";
                }
            }
        }
    }
}
