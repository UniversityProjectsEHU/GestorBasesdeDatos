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
            Console.Write("Enter your user: ");
            string user = Console.ReadLine();
            Console.Write("Enter your password: ");
            string pass = Console.ReadLine();
            Console.Write("Enter the name of the database: ");
            string datab = Console.ReadLine();

            Database db = new Database(datab, user,pass);
            string res = db.getRes();

            if (res == "adminCreateDB")
            {   
                Console.WriteLine("Database created");
            }
            else if (res == "logadmin")
            {
                Console.WriteLine("Login correct as admin. Database open.");
                
            }
            else if (res == "notAdmin")
            {
                Console.WriteLine("Not enough privileges to create that database");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
            else if (res == Constants.OpenDatabaseSuccess)
            {
                Console.WriteLine("Database opened");
            }
            else
            {
                db = null;
                int i = 2;
                while (i > 0 && db==null )
                {
                    Console.WriteLine("Wrong user and password, " + i + " tries left");

                    Console.Write("Enter your user: ");
                    user = Console.ReadLine();
                    Console.Write("Enter your password: ");
                    pass = Console.ReadLine();
                    if(Database.init(datab, user, pass) == Constants.OpenDatabaseSuccess)
                    {
                       db = new Database(datab, user,pass);
                       Console.WriteLine("Database opened");
                    }
                    i--;
                }
                if (db == null)
                {
                    Console.WriteLine("No tries left");
                    Console.ReadKey(true);
                    Environment.Exit(0);
                }
            }

            Console.WriteLine("Write exit when you are ready to finish");
            string q;
            Console.Write("Enter the query: ");
            q = Console.ReadLine();

            while (q.ToLower()!= "exit")
            {            
                string infor = "";
                Stopwatch tiempo = Stopwatch.StartNew();
                infor = db.Query(q,db);
                long mitiempo = tiempo.ElapsedMilliseconds;
                Console.WriteLine(infor + " " + mitiempo + "ms");

                Console.Write("Enter the query: ");
                q = Console.ReadLine();
            }
            Environment.Exit(0);
        }
    }
}
