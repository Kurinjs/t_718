using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManS
{
    class OutInfo : ICommand
    {
        public void Execute()
        {
            //creating database
            Console.WriteLine("CreateDB-this command have functions for make DataBase\n" +
                "Input agruments:\nCreateDB <name_for_new_Database> - make new database with the specified name\n");
            //creating new table in DB
            Console.WriteLine("CreateTable-this command make new table\nInput arguments:\n" +
                "CreateTable <name_database> <name_for_new_table> agrument1.typesdata argument2.typesdata argument3.typesdata arument4.typesdata - make table in specified database\n");
            //Select (switch) 
            Console.WriteLine("Select-this command used for out information about DB or out table on board\n" +
                "Input arguments:\nSelect <name_database> - out list of all tables in this database\n" +
                "Select <name_database> <name_table> - out this table in console\n");
            //Update/change information in table
            Console.WriteLine("Update-this command help you add new info in table\n" +
                "Update <name_database> <name_table> [text1 text2 text3] [text4 text5 text6] etc.\n");
            //Delete info of id and etc.
            Console.WriteLine("Delete-this command can to delete column or row by id_numbers or title column\nInput arguments:\n" +
                "Delete <name_database> - delete database\n" +
                "Delete <name_database> <name_table> - delete table from this database\n" +
                "Delete <name_database> <name_table> <name_column/number_row> - delete full row or column by input name-argument\n");
            //далее будут выводиться возможные запросы, пока что только команды для работы с БД
            //Console.WriteLine("");
            ////
            //Console.WriteLine("");
            ////
            //Console.WriteLine("");
            ////
            //Console.WriteLine("");
            ////
            //Console.WriteLine("");
            ////
            //Console.WriteLine("");
            ////
            //Console.WriteLine("");
            ////
            //Console.WriteLine("");
        }
    }
    class ClearExcel
    {
        public static void Clear_all()
        {
            string nameproc = "Excel";
            System.Diagnostics.Process[] etc = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process anti in etc)
            {
                try
                {
                    if (anti.ProcessName.ToLower().Contains(nameproc.ToLower())) anti.Kill();
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
