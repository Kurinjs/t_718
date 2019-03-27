using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace WntifDB
{
    class SecurityPassword
    {
        public static string ReadCommandWithPassword()
        {
            string text = "";
            int temp = -1;
            bool HiddenPassword = false;
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                temp++;
                // text += Console.ReadKey().KeyChar;
                if (info.Key != ConsoleKey.Backspace)
                {
                    
                    Console.Write(info.KeyChar);
                    if (info.KeyChar == '.')
                    {
                        HiddenPassword = true;
                        break;
                    }
                        
                    text += info.KeyChar;
                }
                if (info.Key == ConsoleKey.Backspace)
                {
                    temp--;
                    if (!string.IsNullOrEmpty(text))
                    {
                        text = text.Substring(0, text.Length - 1);
                        int position = Console.CursorLeft;
                        Console.SetCursorPosition(position - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(position - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            if (HiddenPassword == true)
                text += ReadPassword();
            else
                Console.WriteLine();
            return text;
        }
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int position = Console.CursorLeft;
                        Console.SetCursorPosition(position - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(position - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            return password;
        }
    }
    class hashMP5
    {
        static public string ConvertToHash(string arg)
        {
            string source = arg;
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, source);
                return hash;

            }

        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
