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
    class FuncCreateDB
    {
        public void StartProcessing(NonterminalExpression arg)
        {
            Console.WriteLine("Start creating file for DB");


            //действия для функции оздания базы данных
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();//Подклюаем Excel
            Excel.Workbook xlWorkBook;

            FileInfo fi = new FileInfo("D:\\" + arg.NoTInfo[0] + ".xlsx");//Проверяем есть ли файл с таким же названием, если да то выведит ошибку, т.к. БД уже сществует
            if (fi.Exists)
            {
                Console.WriteLine("The name is occupied by another database!Try more... Or take select function for change Table in DB");
                return;
            }
            else
            {
                xlWorkBook = xlApp.Workbooks.Add();
                Console.WriteLine("Database creation...");
            }
            xlWorkBook.SaveAs(@"D:\" + arg.NoTInfo[0] + ".xlsx");//сохраняем файл как...
            ClearExcel.Clear_all(); Console.WriteLine("Ready, you can continue self job (for list of command enter \"helpme\")");
        }
    }
    class FuncCreateDBCommand : ICommand
    {
        NonterminalExpression argOb;
        FuncCreateDB create;
        public FuncCreateDBCommand(FuncCreateDB m, NonterminalExpression arg)
        {
            create = m;
            argOb = arg;
        }
        public void Execute()
        {
            create.StartProcessing(argOb);
        }
    }
}
