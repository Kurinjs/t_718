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
    class Update
    {
        public void StartProcessing(NonterminalExpression arg)
        {
            string[] arguments = arg.NoTInfo;
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();//Подклюаем Excel
            Excel.Workbook xlWorkBook;
            //FileInfo fi = new FileInfo("D:\\" + arg.NoTInfo[0] + ".xlsx");//Проверяем есть ли файл с таким же названием, если да то выведит ошибку, т.к. БД уже сществует
            //if (fi.Exists)
            //{
            //    xlWorkBook = xlApp.Workbooks.Open(@"D:\" + arg.NoTInfo[0] + ".xlsx");
            //}
            //else
            //{
            //    Console.WriteLine("File is not!");
            //    return;
            //}

            try
            {
                xlWorkBook = xlApp.Workbooks.Open(@"D:\" + arg.NoTInfo[0] + ".xlsx");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return;
            }
            var xlSheets = xlWorkBook.Sheets as Excel.Sheets;
            Excel.Worksheet ObjWorkSheet = (Excel.Worksheet)xlWorkBook.Sheets[Convert.ToString(arguments[1])];
            var lastCell = ObjWorkSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell);//1 ячейку
            string[,] list = new string[lastCell.Column, lastCell.Row]; // массив значений с листа равен по размеру листу
            for (int i = 0; i < lastCell.Column; i++) //по всем колонкам
                for (int j = 0; j < lastCell.Row; j++) // по всем строкам
                    list[i, j] = ObjWorkSheet.Cells[i + 1, j + 1].Text.ToString();//считываем текст в строку

            string tempstr = ""; string TypeText = "";
            int ii = lastCell.Row + 1, jj = 1;
            int check = 0;
            foreach (string k in arguments)
            {
                if (k == arguments[0] || k == arguments[1]) continue;
                try
                {

                    if (jj > lastCell.Column) { Console.WriteLine("Error syntax"); return; }
                    if (k.IndexOf("[") != -1)
                    {
                        TypeText = taketype(ObjWorkSheet.Cells[1, jj].Text.ToString());
                        Regex ob1 = new Regex(TypeText);
                        tempstr = k.Remove(k.IndexOf("["), 1);
                        if (ob1.IsMatch(tempstr))
                            ObjWorkSheet.Cells[ii, jj] = tempstr;
                        else
                        {
                            Console.WriteLine("according't types..."); return;
                        }
                        jj++;
                    }
                    else if (k.IndexOf("]") != -1)
                    {
                        TypeText = taketype(ObjWorkSheet.Cells[1, jj].Text.ToString());

                        tempstr = k.Remove(k.IndexOf("]"), 1);
                        Regex ob2 = new Regex(TypeText);
                        if (ob2.IsMatch(tempstr))
                            ObjWorkSheet.Cells[ii, jj] = tempstr;
                        else
                        {
                            Console.WriteLine("according't types..."); return;
                        }
                        ii++; jj = 1;
                    }
                    else
                    {
                        TypeText = taketype(ObjWorkSheet.Cells[1, jj].Text.ToString());

                        Regex ob3 = new Regex(TypeText);
                        if (ob3.IsMatch(k))
                            ObjWorkSheet.Cells[ii, jj] = k;
                        else
                        {
                            Console.WriteLine("according't types..."); return;
                        }

                        jj++;

                    }
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
            xlWorkBook.Save();
        }
        public string taketype(string a)
        {
            a = a.Remove(0, a.IndexOf('.'));
            if (a == ".int") a = @"[0-9]";
            if (a == ".txt") a = @"[A-z]";
            if (a == ".date") a = @"[0-9]{1,2}.[0-9]{1,2}.[0-9]{4}";
            return a;
        }
        public string INT(string inputData)
        {
            Regex ob1 = new Regex(@"[0-9]{0,10}");
            if (ob1.IsMatch(inputData)) ;
            else
            {
                Console.WriteLine("Incorrect type data in {0}...", inputData);
                return "-666";
            }
            return inputData;
        }
    }

    class UpdateCommand : ICommand
    {
        Update Upd;
        NonterminalExpression argm;
        public UpdateCommand(Update m, NonterminalExpression argmore)
        {
            Upd = m;
            argm = argmore;
        }
        public void Execute()
        {
            Upd.StartProcessing(argm);
        }
    }
}
