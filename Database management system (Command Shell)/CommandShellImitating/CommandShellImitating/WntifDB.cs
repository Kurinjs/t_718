using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using WntifDB;
using Microsoft.Win32.SafeHandles;
using System.Xml.Serialization;
using NLog;
using System.Security.Cryptography;

namespace WntifDB
{

    class CommandShell
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            logger.Trace("Старт приложения, проверка на наличие аргументов");
            try
            {
                if (args.Length > 0)
                    throw new Exception("Error code: [1].Description: received arguments to start.");//check on exist

                Console.WriteLine("Welcome to WntifDB, now you are not logged and can continue as a limited user (you may registor now,\nsimply write 'register' for more info or 'login' if you have acount)");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                logger.Trace("Найдены непредвиденные аргументы, завершение...");
            }
            ImitatingShell();

            Console.WriteLine();
        }
        static private void ImitatingShell()
        {
            Authorization AutoUser = new Authorization(null, null);
            string InputCommand = "";
            while (InputCommand != "close")
            {
                logger.Trace("Новая интерация цикла, ждём ввода с консоли");
                Console.Write("\n@WntifDB>");//new interation application
                if (AutoUser.Name != null)
                {
                    Console.Write(AutoUser.Name + ">");
                }
                InputCommand = SecurityPassword.ReadCommandWithPassword();//request on obtain command from console
                string tempSW = "";
                try
                {
                    tempSW = InputCommand.Remove(InputCommand.IndexOf(" "));
                }
                catch (Exception e)
                {
                    tempSW = InputCommand;//if string is a one word
                }
                switch (tempSW)//first word from command line
                {
                    case "register"://case for registration
                        {
                            Regex reg = new Regex("[register] (Admin|User) [A-z]{1}[A-z||0-9]{0,10} [A-z||0-9]{1,20}", RegexOptions.IgnoreCase);//regular exspression for registration new people and switch him access

                            logger.Trace("Создание объекта класса регистрации");//logger
                            if (reg.IsMatch(InputCommand))//check
                            {
                                string[] words = InputCommand.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);//obtain array words from string
                                words[3] = hashMP5.ConvertToHash(words[3]);//obtain hash-code by input password
                                Registration NewUser = new Registration(words[1], words[2], words[3]);//access,name,hash-password
                            }
                            else
                                Console.WriteLine("Try more, uncorrect command syntax\n" +
                               "Examle: register UserOrAdmin YourName .YourPassword                 {0,50}", "//where '.' used for hidden password");//error-text
                        }
                        break;
                    case "login":
                        {
                            Regex reg = new Regex("[login] [A-z]{1}[A-z||0-9]{0,10} [A-z||0-9]{1,15}", RegexOptions.IgnoreCase);

                            logger.Trace("Создание объекта класса авторизации");
                            //будут вводиться три аргумента с командной строки 1
                            if (reg.IsMatch(InputCommand))
                            {
                                string[] words = InputCommand.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                words[2] = hashMP5.ConvertToHash(words[2]);
                                AutoUser = new Authorization(words[1], words[2]);// name, hash-code by password

                                AutoUser.LogIn();//try log in
                            }
                            else
                            {
                                string txt = "Examle: authorization YourName .YourPassword";
                                int size = 100 - txt.Length;
                                Console.WriteLine("Try more, uncorrect command syntax\n" +
                                    "Examle: login YourName .YourPassword                  {0," + size + "}", "//where '.' used for hidden password");
                            }
                        }
                        break;
                    case "StartJob":
                        {
                            if (CheckAccessUser(AutoUser))
                            {
                                interpret ob=new interpret(AutoUser);
                                while(ob.command != "back")
                                {
                                    ///
                                    ///тут будет словать или свич
                                    ///

                                    ob = new interpret(AutoUser);
                                }
                                //Regex reg = new Regex("[CREATE] (DBase|Table) '[A-z||0-9]{1,15}'", RegexOptions.IgnoreCase);
                                //if (reg.IsMatch(InputCommand) == true)
                                //{
                                //    Console.WriteLine("");
                                //}
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Denied access!");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }

                        }
                        break;

                    case "close":
                        {
                            InputCommand = "close";
                        }
                        break;
                }
            }

        }
        static public bool CheckAccessAdmin(Authorization user)
        {
            if (user.Access == "Admin")
                return true;
            else
                return false;
        }
        static public bool CheckAccessUser(Authorization user)
        {
            if (user.Access == "Admin"|| user.Access == "User")
                return true;
            else
                return false;
        }
    }
}
