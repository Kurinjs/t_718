using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 namespace DBManS
{
    class Request
    {
        public void StartProcessing()
        {
            Console.WriteLine("Request type DELETE:\nDelete inf from table/DB \nFull removal: Delete <name_database> \nRemove of column: Delete <name_database> <name_table> \nRemoving of 1 element: Delete <name_database> <name_table> <name_column/number_row> \n");
            Console.WriteLine("Request type UPDATE:\nAdd info in DB: Update <name_database> <name_table> [text1 text2 text3][text4 text5 text6] etc. \n");
            Console.WriteLine("Request type SELECT:\nDisplay inf about DB/table\nlist of all tables in DB: Select <name_database>\nAll inf of table: Select <name_database> <name_table>\n");
            Console.WriteLine("Request type CREATE_TABLE\nCreate new table in DB: CreateTable <name_database> <name_for_new_table> agrument1.typesdata argument2.typesdata argument3.typesdata argument4.typesdata\n");
            Console.WriteLine("Request type CREATE_DATABASE\nCreate new DB: CreateDB <name_for_new_Database>\n");
            Console.WriteLine("Request type AVERAGE\nAverage for a column: Average <name_database> <name_table>\n");
            Console.WriteLine("Request type AMOUNT\nAmount for a column: Amount <name_database> <name_table>\n");
            Console.WriteLine("Request type MAX\nMax number in table:Max <name_database> <name_table>\n");
            Console.WriteLine("Request type MIN\nMin number in table:Min <name_database> <name_table>\n");
            Console.WriteLine("Request type SORT_INCREASE\nSort table for increase:Sort_inc <name_database> <name_table>\n");
            Console.WriteLine("Request type SORT_DECREASE\nSort table for decrease:Sort_dec <name_database> <name_table>\n");
        }
    }
    class RequestCommand : ICommand
    {
        Request req;
        
        public RequestCommand(Request _req)
        {
            req = _req;
        }
        public void Execute()
        {
            req.StartProcessing();
        }
    }

}
