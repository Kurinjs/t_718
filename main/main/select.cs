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
    class FuncSelect//last change
    {
        public void StartProcessing(NonterminalExpression arg)
        {
            string[] arguments = arg.NoTInfo;

            Regex ob1 = new Regex(@"[A-Z||a-z]{1}[a-z||0-9]{0,12}");
            string[] args = arg.NoTInfo;
            for (int i = 1; i < args.Length - 1; i++)
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
                xlWorkBook = xlApp.Workbooks.Open(@"D:\" + arg.NoTInfo[0] + ".xlsx");
            }
            else
            {
                Console.WriteLine("File is not!");
                return;
            }
            var xlSheets = xlWorkBook.Sheets as Excel.Sheets;
            if (arguments.Length == 2)
            {
                string[] result;

                result = new string[xlWorkBook.Sheets.Count];
                try
                {
                    var sheet = (Excel.Worksheet)xlWorkBook.Sheets["Лист1"];//delete default sheet
                    sheet.Delete();
                }
                catch (Exception e) { }
                try
                {
                    for (int i = 0; i < xlWorkBook.Sheets.Count; i++)
                    {

                        Console.WriteLine((i + 1) + ". " + ((Excel.Worksheet)xlWorkBook.Sheets[i + 1]).Name);//out list of names tables
                        xlWorkBook.Save();
                        // ClearExcel.Clear_all();
                    }
                }
                catch (Exception e) { }

            }
            else
            {
                if (arguments.Length == 3)
                {
                    Console.WriteLine(Convert.ToString(arguments[1]));
                    Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[Convert.ToString(arguments[1])];
                    var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);//1 ячейку
                    string[,] list = new string[lastCell.Column, lastCell.Row]; // массив значений с листа равен по размеру листу
                    for (int i = 0; i < lastCell.Column; i++) //по всем колонкам
                        for (int j = 0; j < lastCell.Row; j++) // по всем строкам
                            list[i, j] = ObjWorkSheet.Cells[j + 1, i + 1].Text.ToString();//считываем текст в строку
                    for (int i = 0; i < lastCell.Row; i++)
                    {
                        for (int j = 0; j < lastCell.Column; j++)
                        {
                            if (list[j, i].IndexOf(".") != -1) list[j, i] = list[j, i].Remove(list[j, i].IndexOf("."));
                            Console.Write("{0,-10}|", list[j, i]);
                        }
                        Console.WriteLine();
                    }
                }
            }
            xlWorkBook.Close(false, Type.Missing, Type.Missing); //закрыть не сохраняя
            ClearExcel.Clear_all();
        }
    }
    class FuncSelectCommand : ICommand
    {
        NonterminalExpression argOb;
        FuncSelect select;

        public FuncSelectCommand(FuncSelect m, NonterminalExpression arg)
        {
            argOb = arg;
            select = m;
        }
        public void Execute()
        {
            select.StartProcessing(argOb);
        }
    }
}
