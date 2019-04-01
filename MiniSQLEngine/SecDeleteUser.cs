﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    class SecDeleteUser : Query
    {
        private string user;
        private string result;

        public SecDeleteUser(string user)
        {
            this.user = user;
        }
        public override string getClass()
        {
            return "SecDeleteUser";
        }

        public override string getResult()
        {
            return result;
        }

        public override void Run(string dbname)
        {
            Boolean keepatit= true;
            if (user == "admin")
            {
                result = "Tu arbol genealógico es una pelota";
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(@"..//..//..//data//" + dbname + "//profiles");
                FileInfo[] files = di.GetFiles();

                for (int i = 0; i < files.Length; i++)
                {
                    var fi=files.ElementAt(i);
                    string tempfile=fi.Name;
                    if (tempfile == "admin")
                    {
                        //DO NOTHING,CONTINUE
                    }
                    else if(keepatit)
                    {
                        string temppath = "..//..//..//data//" + dbname + "//profiles//" + tempfile + ".pf";
                        string[] lines =File.ReadAllLines(temppath);

                        try
                        {

                            
                            StreamWriter sw = new StreamWriter("..//..//..//data//" + dbname + "//profiles//" + tempfile + ".pf");




                            foreach (string line in lines)
                            {
                                //The user is saved in the first index and the password in the second
                                string[] userANDpw = line.Split(',');
                                if (userANDpw[0] == user)
                                {
                                    keepatit = false;

                                }
                                else
                                {
                                    // Write a line of text
                                    sw.WriteLine(userANDpw[0]+","+userANDpw[1]);
                                }
                            }
                        }
                        catch(Exception e)
                        {
                            result = e.StackTrace;
                        }
                    }
                }
               
            }




        }
    }
}
