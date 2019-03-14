using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniSQLEngine;

namespace MiniSQLDBConsole
{
    class MiniSQLDBConsole
    {
        static void Main(string[] args)
        {
            Database db = new Database("userdb");
            db.Query("CREATE DATABASE userdb;");
            Console.WriteLine("Write exit when you are ready to finish");
            string q;
            Console.Write("Enter the query: ");
            q = Console.ReadLine();

            while (q.ToLower()!= "exit")
            {            
                string infor = "";
                Stopwatch tiempo = Stopwatch.StartNew();
                infor = db.Query(q);
                long mitiempo = tiempo.ElapsedMilliseconds;
                Console.WriteLine(infor + " " + mitiempo + "ms");

                Console.Write("Enter the query: ");
                q = Console.ReadLine();
            }
            Environment.Exit(0);
        }
    }
}
