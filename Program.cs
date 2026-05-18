using System;
using System.IO;
using System.Text;
using VM_Stack;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Crap_VM :)");


            string code;
            using (StreamReader reader = new StreamReader("test.txt", Encoding.UTF8))
            {
                code = reader.ReadToEnd();
            }

            Interpreter interpreter = new Interpreter(code);
            interpreter.Run();
        }
    }
}