using MiniSQLEngine;
using System;
using System.Collections.Generic;
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
            Console.WriteLine("# Test "+ contador); 
            foreach (string linea in lineas)
            {
                if (linea != "")
                {            
                        
                        db.Query(linea);                      
                }
                else
                {
                    contador = contador + 1;
                    Console.WriteLine("# Test " + contador);
                    string dbnombre = "usuarios" + contador;
                    db = new Database(dbnombre);
                    db.Query("CREATE DATABASE "+dbnombre+";");
                }
            }
            Console.ReadKey(true);
        }
    }
}
