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
            String[] lineas = System.IO.File.ReadAllLines("..//..//..//data//TesterInput.txt");
            Database db = new Database("usuarios1");
            db.Query("CREATE DATABASE usuarios1;");
            int contador = 1;
            Console.WriteLine("# Test " + contador);
            string infor = "";
            List<long> tiempos = new List<long>();
            long totaltime = 0;
            int cuantas = lineas.Length;
            int contar = 0;
            foreach (string linea in lineas)
            {
                contar = contar + 1;
                if (linea != "" && contar != cuantas)
                {
                    Stopwatch tiempo = Stopwatch.StartNew();
                    infor = db.Query(linea);
                    long mitiempo = tiempo.ElapsedMilliseconds;
                    Console.WriteLine(infor + " " + mitiempo + "ms");
                    tiempos.Add(mitiempo);
                }
                else if (linea != "" && contar == cuantas)
                {
                    Stopwatch tiempo = Stopwatch.StartNew();
                    infor = db.Query(linea);
                    long mitiempo = tiempo.ElapsedMilliseconds;
                    Console.WriteLine(infor + " " + mitiempo + "ms");
                    tiempos.Add(mitiempo);
                    for (int i = 0; i < tiempos.Count; i++)
                    {
                        long eltime = tiempos.ElementAt(i);
                        totaltime = totaltime + eltime;
                    }
                    Console.WriteLine("TOTAL TIME: " + totaltime);
                    tiempos.Clear();
                    totaltime = 0;
                }
                else
                {
                    for (int i = 0; i < tiempos.Count; i++)
                    {
                        long eltime = tiempos.ElementAt(i);
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
