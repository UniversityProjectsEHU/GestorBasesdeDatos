using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //string input = "SELECT * FROM table1";
            //string regEx = @"SELECT\s+(\*)\s+FROM\s+(\w+)";


            ////Solo una coincidencia
            //Match match1 = Regex.Match(input, regEx);
            //if (match1.Success)
            //{
            //    Console.WriteLine("String entera: " + match1.Groups[0].Value);
            //    Console.WriteLine("Se selecciona el atributo: " + match1.Groups[1].Value);
            //    Console.WriteLine("De la tabla: " + match1.Groups[2].Value);

            //}
            ////Mas coincidencias
            //Console.WriteLine("Más coincidencias:");
            //foreach (Match m in Regex.Matches(input, regEx))
            //{
            //    Console.WriteLine("String entera: " + match1.Groups[0].Value);
            //    Console.WriteLine("Se selecciona el atributo: " + match1.Groups[1].Value);
            //    Console.WriteLine("De la tabla: " + match1.Groups[2].Value);
           // }
            using (StreamWriter writer = File.CreateText("test.txt"))
            {
                writer.WriteLine("Name String");
                writer.WriteLine("Age int");
                writer.WriteLine("Address String");
                writer.WriteLine("somthing String");

            }
            string allFile = File.ReadAllText("test.txt");
            string[] lines = allFile.Split('\n');
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
