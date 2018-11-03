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
    class Context
    {
        string[] strComand;
        public Context(string[] value)
        {
            strComand = value;
        }
        public string[] info
        {
            get { return strComand; }
        }

    }

    abstract class AbstractExpression
    {
        public abstract void Interpret(Context context);
    }
    class TerminalExpression : AbstractExpression
    {
        private string str;// { get};
        public string TerInfo
        {
            get
            {
                return str;
            }
        }
        public override void Interpret(Context context)
        {
            string[] a = context.info;
            str = a[0];
        }
    }
    class NonterminalExpression : AbstractExpression
    {
        string[] str;
        public string[] NoTInfo
        {
            get
            {
                return str;
            }
        }
        public override void Interpret(Context context)
        {
            str = new string[context.info.Length];
            for (int i = 1; i < context.info.Length; i++)
            {
                str[i - 1] = context.info[i];
            }
        }
    }
}
