using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using NLog;
namespace WntifDB
{
    static class Create
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public class Database : ICreate
        {
            string Name;
            public Database(string Name)
            {
                
                this.Name = Name.Substring(Name.LastIndexOf(" "));
                logger.Trace("Имя базы данных " + this.Name);
                Create();
            }
            public void Create()
            {
                XDocument xdoc = new XDocument();
                xdoc.Save(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\" + Name + ".xml");
                Console.WriteLine("database creating is completed!");
            }

        }
       public class Table : ICreate
        {
            string InputStr; Regex reg;
            string Name;
            string NameDB;
            string[] arg;
            public Table(string text,string NameDatabase)
            {
                this.NameDB = NameDatabase;
                try
                {
                    reg = new Regex("CREATE TABLE [A-z||0-9]{1,20}([A-z||0-9]{0,400})");
                    if (!reg.IsMatch(text))
                    {
                        throw new Exception("Incorrect syntax,try more");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
                this.InputStr = text;
                TableInterpret();
            }
            private void TableInterpret()
            {
                InputStr = InputStr.Remove(0, 13);
                logger.Trace("Попытка обрезать 'CREATE TABLE ' в связи с взятием имени таблицы\nполучилось: " + InputStr);
                string temp;
                Name = InputStr.Remove( InputStr.IndexOf("("));
                temp = InputStr.Substring(InputStr.IndexOf("(")+1);
                temp = temp.Remove(temp.Length - 1); 
                logger.Trace("Попытка обрезатm имя: " + Name);
                logger.Trace("Попытка обрезать аргументы: " + temp+"\nАргументы:");

                arg = temp.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string k in arg)
                {
                    logger.Trace(k);
                }
                try
                {

                    reg = new Regex("[A-z]{0,10} (INT|CHAR|TIME)",RegexOptions.IgnoreCase);
                    
                    foreach (string k in arg)
                    {
                        if (!reg.IsMatch(k))
                        {
                            Console.WriteLine(k);
                            throw new Exception("Arguments from command have incorrect format");
                        }
                    }
                    Console.WriteLine("All good");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                //Такс, я что то устал слишком, мы тут обработали запросик...
                //разбили его на слова вернее, теперь вот чему что равно
                //arg[]   -   это все аргументы, 1 строка это аргументы для одного столбца
                //Name    -   это имя будующей таблицы
                //NameDB  -   это нужная нам база данных
            }
            public void Create()
            {
               // XDocument xdoc = XDocument.Load(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\" + Name + ".xml");

            }
        }
    }
}
