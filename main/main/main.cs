using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using NLog;
using DBManS;

namespace DBManS
{
    class MainClass
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            log.Trace("Start of work(make objects for work, processing of request)");
        //работаем с аргументами в интерпретаторе
        var context = new Context(args);
            var list = new List<AbstractExpression>();
            TerminalExpression ob1 = new TerminalExpression();
            NonterminalExpression ob2 = new NonterminalExpression();
            list.Add(ob1);
            list.Add(ob2);
            foreach (AbstractExpression exp in list)
            {
                exp.Interpret(context);
            }//terminal exp. have key for collection with objects commands
             //Nonterminal expression have arguments for processing data in this command
            FuncCreateDB createDB = new FuncCreateDB();//make object
            CreateTable createT = new CreateTable();
            FuncSelect select = new FuncSelect();
            Update Upd = new Update();
            Delete Del = new Delete();
            RequestInfo req = new RequestInfo();
            Dictionary<string, ICommand> ListCommand = new Dictionary<string, ICommand>();//make collection
            ListCommand.Add("helpme", new OutInfo());
            ListCommand.Add("CreateDB", new FuncCreateDBCommand(createDB, ob2));//distribution him in collection and add key for him
            ListCommand.Add("CreateTable", new CreateTableCommand(createT, ob2));
            ListCommand.Add("Select", new FuncSelectCommand(select, ob2));
            ListCommand.Add("Update", new UpdateCommand(Upd, ob2));
            ListCommand.Add("Delete", new DeleteCommand(Del, ob2));
            ListCommand.Add("RequestInfo", new RequestInfoCommand(req));
            Set set = new Set();
            try
            {
                if (ListCommand.ContainsKey(ob1.TerInfo) == false) throw new Exception("This command doesn't exist!");
                log.Trace("The key found, receiving the object");
                set.SetCommand(ListCommand[ob1.TerInfo]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+"\nUse command \"helpme\"");
                log.Trace("This command doesn't exist!Send message of expression and closure of the programme");
                return;
            }
            set.Processing();
            log.Trace("Call clean-up function");
            ClearExcel.Clear_all();
            log.Trace("Good job! Closure of the application");
            Console.ReadKey();
        }
    }
}
