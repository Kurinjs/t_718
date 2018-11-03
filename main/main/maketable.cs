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
    class CreateTable
    {
        public void StartProcessing(NonterminalExpression arg)//Arguments: 1)name database 2)name new table 3)-more is data for table
        {
            Regex ob1 = new Regex(@"[A-Z||a-z]{1}[a-z||0-9]{0,12}.[int||txt||date]");
            string[] args = arg.NoTInfo;
            for (int i = 2; i < args.Length - 1; i++)
            {

                if (ob1.IsMatch(args[i])) ;
                else
                {
                    Console.WriteLine("Incorrect syntax, try again or call the helpme command");
                    return;
                }

            }


            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();//Подклюаем Excel
            Excel.Workbook xlWorkBook;
            FileInfo fi = new FileInfo("D:\\" + arg.NoTInfo[0] + ".xlsx");//Проверяем есть ли файл с таким же названием, если да то выведит ошибку, т.к. БД уже сществует
            if (fi.Exists)
            {
                Console.WriteLine("File is open, processing...");
                xlWorkBook = xlApp.Workbooks.Open(@"D:\" + arg.NoTInfo[0] + ".xlsx");

            }
            else
            {
                Console.WriteLine("DataBase {0} is not, try more...", arg.NoTInfo[0]);
                ClearExcel.Clear_all();
                return;
            }
            var xlSheets = xlWorkBook.Sheets as Excel.Sheets;

            var xlNewSheet = (Excel.Worksheet)xlSheets.Add(xlSheets[xlSheets.Count], Type.Missing, Type.Missing, Type.Missing);//Создаем новый лист
                                                                                                                               // xlSheets[xlSheets.Count - 1].Visible = false;                                                                                                   // string ValueForCells = "";

            for (int i = 0; i < args.Length - 2; i++)
            {
                xlNewSheet.Cells[1, i + 1] = args[i + 2];
                // xlNewSheet.Cells[1,i+1]=TableInfo[i];
            }
            xlNewSheet.Name = Convert.ToString(arg.NoTInfo[1]);//указываем имя книги
            //xlNewSheet.Visible = true;
            //xlNewSheet.Range["A0"].TextToColumns("jhk");
            xlWorkBook.ReadOnlyRecommended = false;//выключаем защиту документа
            xlWorkBook.Save();
            xlWorkBook.Close();
            xlApp.Quit();
            ClearExcel.Clear_all();
            Console.WriteLine("Making table is end");
        }

    }

    class CreateTableCommand : ICommand
    {
        CreateTable create;
        NonterminalExpression argm;
        public CreateTableCommand(CreateTable m, NonterminalExpression argmore)
        {
            create = m;
            argm = argmore;
        }
        public void Execute()
        {
            create.StartProcessing(argm);
        }
    }
    //Select - command for out table at console
}
