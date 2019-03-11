using MiniSQLEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] lineas = System.IO.File.ReadAllLines("..//..//..//data//input.txt");
            Database db = new Database("usuarios1");
            db.Query("CREATE DATABASE usuarios1;");
            int contador = 1;
            Console.WriteLine("# Test " + contador);
            string infor = "";
            foreach (string linea in lineas)
            {
                if (linea != "")
                {
                    if (linea.Contains("CREATE TABLE"))
                    {
                        infor = "Table created";
                    }
                    else if (linea.Contains("INSERT INTO"))
                    {
                        infor = "Tuple added";
                    }
                    else if (linea.Contains("SELECT"))
                    {
                        infor = "Select information";
                    }
                    else if (linea.Contains("DELETE FROM"))
                    {
                        infor = "Tuple delete";
                    }
                    else if (linea.Contains("UPDATE"))
                    {
                        infor = "Tuple update";
                    }

                    Stopwatch tiempo = Stopwatch.StartNew();

                    if (linea.Contains("SELECT"))
                    {
                        Console.WriteLine(db.Query(linea));
                    }
                    else
                    {
                        db.Query(linea);
                    }
                    Console.WriteLine(infor + " " + tiempo.ElapsedMilliseconds.ToString() + "ms");
                }
                else
                {
                    contador = contador + 1;
                    Console.WriteLine("");
                    Console.WriteLine("# Test " + contador);
                    string dbnombre = "usuarios" + contador;
                    db = new Database(dbnombre);
                    db.Query("CREATE DATABASE " + dbnombre + ";");
                }
            }
            Console.ReadKey(true);

            Console.WriteLine(db.Query("SELECT Name,Age FROM MyTable WHERE Age=18;"));
            Console.ReadKey(true);
        }
    }
}
