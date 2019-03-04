using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniSQLEngine;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Database db = Database.getDatabase("db");
            db.Query("CREATE TABLE t1(id int true,edad int false);");
            db.Query("INSERT INTO t1 VALUES (5,7);");
            db.Query("INSERT INTO t1 VALUES (8,9);");


        }
    }
}
