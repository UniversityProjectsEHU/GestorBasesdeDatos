using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassDelete : Query
    {
        private string table, condition,result;

        public ClassDelete(string pTable,string pCondition)
        {
            table = pTable;
            condition = pCondition;
        }

        public override string getClass()
        {
            return "delete";
        }

        public override void Run(string dbname)
        {
            string pathfileDEF = @"..//..//..//data//" + dbname + "//" + table + ".def";
            if (File.Exists(pathfileDEF) == false)
            {
                result = Constants.TableDoesNotExist;
            }
            else
            {
                string line1;
                List<string> columns = new List<string>();
                string attr = null;
                string symbol = null;
                string cond = null;


                Match at = Regex.Match(condition, Constants.regExConditionAttribute);
                if (at.Success)
                {
                    attr = at.Groups[1].Value;
                    if (attr == "")
                    {
                        attr = at.Groups[2].Value;
                        if (attr == "")
                        {
                            attr = at.Groups[3].Value;
                        }
                    }
                }
                Match val = Regex.Match(condition, Constants.regExConditionValue);
                if (val.Success)
                {
                    string value = val.Groups[1].Value;
                    if (value != "")
                    {
                        symbol = value.Substring(0, 1);
                        cond = value.Substring(1);
                    }

                    value = val.Groups[2].Value;
                    if (value != "")
                    {
                        symbol = value.Substring(0, 1);
                        cond = value.Substring(1);
                    }

                    value = val.Groups[3].Value;
                    if (value != "")
                    {
                        symbol = value.Substring(0, 1);
                        cond = value.Substring(1);
                    }
                }

                using (StreamReader sr = new StreamReader(pathfileDEF))
                {
                    while ((line1 = sr.ReadLine()) != null)
                    {
                        string[] parts = line1.Split(',');
                        foreach (string element in parts)
                        {
                            string[] atributes = element.Split(' ');
                            string type = atributes[0];
                            columns.Add(type);
                           
                        }

                    }
                }
                if (columns.Contains("atrr") == false)
                {
                    result = Constants.ColumnDoesNotExist;
                }
                else
                {

                    int index = columns.IndexOf(attr);
                    string pathfileDATA = @"..//..//..//data//" + dbname + "//" + table + ".data";
                    string pathfileTMP = @"..//..//..//data//" + dbname + "//" + table + "2.data";
                    string line2 = "";

                    using (StreamWriter fileWrite = new StreamWriter(pathfileTMP))
                    {
                        using (StreamReader sr2 = new StreamReader(pathfileDATA))
                        {
                            while ((line2 = sr2.ReadLine()) != null)
                            {
                                string[] data = line2.Split(',');
                                if (symbol.Equals("="))
                                {
                                    if (!data[index].Equals(cond))
                                    {
                                        fileWrite.WriteLine(line2);
                                    }
                                }
                                if (symbol.Equals(">"))
                                {
                                    int queryCon = int.Parse(data[index]);
                                    int searchCon = int.Parse(cond);
                                    if (queryCon <= searchCon)
                                    {
                                        fileWrite.WriteLine(line2);
                                    }
                                }
                                if (symbol.Equals("<"))
                                {
                                    int queryCon = int.Parse(data[index]);
                                    int searchCon = int.Parse(cond);
                                    if (queryCon >= searchCon)
                                    {
                                        fileWrite.WriteLine(line2);
                                    }
                                }
                            }

                        }
                    }
                    File.Delete(pathfileDATA);
                    File.Move(pathfileTMP, pathfileDATA);
                    result = Constants.TupleDeleteSuccess;
                }
            }
        }
        public string GetTableName()
        {
            return table;
        }
        public string getCondition()
        {
            return condition;
        }
        public string getResult()
        {
            return result;
        }
    }
}

