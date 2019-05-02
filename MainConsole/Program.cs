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
            String[] lineas = System.IO.File.ReadAllLines("..//..//..//data//testseguridad.txt");
            String[] datosBase = lineas[0].Split(',');
            Database db = new Database(datosBase[0], datosBase[1], datosBase[2]);
            int contador = 1;
            Console.WriteLine("# Test " + contador);
            Console.WriteLine(db.getRes());
            string infor = "";
            List<long> tiempos = new List<long>();
            long totaltime = 0;
            int cuantas = lineas.Length;
            int contarLineas = 0;
            foreach (string linea in lineas)
            {
                if (contarLineas == 0)
                {
                    contarLineas = contarLineas + 1;
                }
                else
                {
                    contarLineas = contarLineas + 1;
                    if (linea.Contains(";"))
                    {
                        if (linea != "" && contarLineas != cuantas)
                        {
                            Stopwatch tiempo = Stopwatch.StartNew();
                            infor = db.Query(linea,db);
                            long mitiempo = tiempo.ElapsedMilliseconds;
                            Console.WriteLine(infor + " " + mitiempo + "ms");
                            tiempos.Add(mitiempo);
                        }
                        else if (linea != "" && contarLineas == cuantas)
                        {
                            Stopwatch tiempo = Stopwatch.StartNew();
                            infor = db.Query(linea, db);
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
                    }
                    else
                    {
                        if (linea == "")
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

                            //String lineaAbrir
                            //db = new Database(dbnombre,user,pass);
                            //db.Query("CREATE DATABASE " + dbnombre + ";");
                            tiempos.Clear();
                            totaltime = 0;
                        }
                        else
                        {
                            String[] datosBase2 = linea.Split(',');
                            db = new Database(datosBase2[0], datosBase2[1], datosBase2[2]);
                            Console.WriteLine(db.getRes());
                        }
                    }
                }
            }
            Console.ReadKey(true);
        }
    }
}
