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
    interface ICommand
    {
        void Execute();
        // void Undo();
    }

    // Invoker
    class Set
    {
        ICommand command;

        public Set() { }

        public void SetCommand(ICommand com)
        {
            command = com;
        }

        public void Processing()
        {
            command.Execute();
        }
        /*public void EndProcessing()
        {
            command.Undo();
        }*/
    }
}
