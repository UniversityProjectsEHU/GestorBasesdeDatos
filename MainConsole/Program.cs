using MiniSQLEngine;
using System;
using System.Collections;
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
                ArrayList tiempos = new ArrayList();
                int totaltime = 0;
                foreach (string linea in lineas)
                {
                    if (linea != "")
                    {
                        Stopwatch tiempo = Stopwatch.StartNew();
                        infor = db.Query(linea);
                        Console.WriteLine(infor + " " + tiempo.ElapsedMilliseconds.ToString() + "ms");
                        tiempos.Add(tiempo.ElapsedMilliseconds);
                    }
                    else
                    {
                        for (int i = 0; i < tiempos.Count; i++)
                        {
                            int eltime = Convert.ToInt16(tiempos[i]);
                            totaltime = totaltime + eltime;
                        }
                        Console.WriteLine("TOTAL TIME: " + totaltime);
                        contador = contador + 1;
                        Console.WriteLine("");
                        Console.WriteLine("# Test " + contador);
                        string dbnombre = "usuarios" + contador;
                        db = new Database(dbnombre);
                        db.Query("CREATE DATABASE " + dbnombre + ";");
                        tiempos.Clear();
                        totaltime = 0;
                    }
            }
                Console.ReadKey(true);
            }
    }
}
