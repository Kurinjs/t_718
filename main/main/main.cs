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
using DBManS;

namespace DBManS
{
    class MainClass
    {
        static void Main(string[] args)
        {
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
            Request req = new Request();
            Dictionary<string, ICommand> ListCommand = new Dictionary<string, ICommand>();//make collection
            ListCommand.Add("helpme", new OutInfo());
            ListCommand.Add("CreateDB", new FuncCreateDBCommand(createDB, ob2));//distribution him in collection and add key for him
            ListCommand.Add("CreateTable", new CreateTableCommand(createT, ob2));
            ListCommand.Add("Select", new FuncSelectCommand(select, ob2));
            ListCommand.Add("Update", new UpdateCommand(Upd, ob2));
            ListCommand.Add("Delete", new DeleteCommand(Del, ob2));
            ListCommand.Add("RequestInfo", new RequestCommand(req));
            Set set = new Set();
            set.SetCommand(ListCommand[ob1.TerInfo]);
            set.Processing();
            ClearExcel.Clear_all();
            Console.ReadKey();
        }
    }
}
