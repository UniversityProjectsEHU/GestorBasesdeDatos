using MiniSQLEngine;
using System;
using System.Collections.Generic;
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
            //Create database
            string myDB1 = "usuarios1";
            string myDB2 = "usuarios2";
            string myDB3 = "usuarios3";
            Database db1 = Database.getDatabase(myDB1);
            db1.Query("CREATE DATABASE usuarios1;");
            Database db2 = Database.getDatabase(myDB2);
            db2.Query("CREATE DATABASE usuarios2;");
            Database db3 = Database.getDatabase(myDB3);
            db3.Query("CREATE DATABASE usuarios3;");

            int contador = 1;
            foreach (string linea in lineas)
            {
                if (linea!="")
                {
                    if (contador==1)
                    {
                        db1.Query(linea);
                    }
                    if (contador == 2)
                    {
                        db2.Query(linea);
                    }
                    if (contador == 3)
                    {
                        db3.Query(linea);
                    }
                }
                else
                {
                    contador = contador + 1;
                }
            }
        }
    }
}
