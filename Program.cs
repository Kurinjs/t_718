using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PattenCommand
{
    class Delete
    {
        public void StartProcessing()
        {
            Console.WriteLine("Это работает!!!1");
        }
    }
    class DeleteCommand : ICommand
    {
        Delete delete;
        public DeleteCommand(Delete m)
        {
            delete = m;
        }
        public void Excute()
        {
            delete.StartProcessing();
        }
        public void Undo()
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
