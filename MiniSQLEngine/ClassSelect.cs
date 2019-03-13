using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassSelect : Query
    {
        private string []columns;
        private string table;
        private string condition;
        private string result;
        private string Query;

        public ClassSelect(string [] columns,string table,string condition,string pQuery)
        {
            this.columns = columns;
            this.table = table;
            this.condition = condition;
            Query = pQuery;
        }
        public string GetTable()
        {
            return table;
        }

        public string[] GetColumns()
        {
            return columns;
        }

        public string GetCondition()
        {
            return condition;
        }
        public override string getResult()
        {
            return result;

        }
        public string getQuery()
        {
            return Query;
        }

        public override string getClass()
        {
            return "select";
        }

        public override void Run(string dbname)
        {
            string op;
            string[] elements;
            int poscond=0;
            if (condition.Contains("="))
            {
                elements = condition.Split('=');
                op = "=";
            }
            else if (condition.Contains("<"))
            {
                elements = condition.Split('<');
                op = "<";
            }
            else
            {
                elements = condition.Split('>');
                op = ">";
            }
            if (!(File.Exists("..//..//..//data//" + dbname + "//" + table + ".def")))
            {
                result = Constants.TableDoesNotExist;
            }
            else
            {

            
            //I read all the file
            string allFile = System.IO.File.ReadAllText("..//..//..//data//" + dbname + "//" + table + ".def");
            string []splittedFile = allFile.Split(',');
            List<objectDef> list= new List<objectDef>();
            string comptype="";
            Boolean showall = false;
            if (columns[0] == "*")
            {
                for (int i = 0; i < splittedFile.Length; i++)
                {
                    for (int j = 0; j < columns.Length; j++)
                    {
                        list.Add(new objectDef(columns[j], i, columns[j]));
 
                    }
                    string[] temp = splittedFile[i].Split(' ');
                    if (temp[0] == elements[0])
                    {
                        poscond = i;
                        comptype = temp[1];

                    }
                }
                showall = true;
            }
            else
            {


                for (int i = 0; i < splittedFile.Length; i++)
                {
                    //Miramos que atributos debemos mostrar
                    for (int j = 0; j < columns.Length; j++)
                    {
                        if (splittedFile[i].Contains(columns[j]))
                        {
                                list.Add(new objectDef(columns[j], i, columns[j]));



                            }
                        }
                    //Miramos la posicion y el tipo de la condicion
                    string[] temp = splittedFile[i].Split(' ');
                    if (temp[0] == elements[0])
                    {
                        poscond = i;
                        comptype = temp[1];

                    }

                }

            }
                List<string> atindb = new List<string>();
                atindb.Add("*");
                Boolean valid = true;
                foreach (string line in splittedFile)
                {
                    string[] compr = line.Split(' ');

                    atindb.Add(compr[0]);
                }
                foreach (string col in columns)
                {
                    if (!(atindb.Contains(col)))
                    {
                        valid = false;

                    }
                }
            if ((poscond == 0 && comptype == ""))
            {
                result = Constants.ColumnDoesNotExist;
            }
            else if (!valid)
                {
                    result = Constants.ColumnDoesNotExist;

                }


                else
            {




                string[] allFile2 = System.IO.File.ReadAllLines("..//..//..//data//" + dbname + "//" + table + ".data");
                if (!showall)
                {
                    result = "The result for the Query '" + Query + "' is: ";



                    foreach (string linea in allFile2)
                    {
                        string[] splittedline = linea.Split(',');



                        if (op == "=")
                        {
                            if (splittedline[poscond] == elements[1])
                            {
                                foreach (objectDef obj in list)
                                {
                                    result = result + obj.GetColumnName() + " " + splittedline[obj.GetPos()] + " ";
                                }
                                result = result + ";";

                            }


                        }
                        else if (op == "<")
                        {
                            if (comptype.ToLower() == "int")
                            {
                                if (Int32.Parse(splittedline[poscond]) < Int32.Parse(elements[1]))
                                {
                                    foreach (objectDef obj in list)
                                    {
                                        result = result + obj.GetColumnName() + " " + splittedline[obj.GetPos()] + " ";

                                    }
                                    result = result + ";";



                                }
                            }
                            else
                            {
                                if ((splittedline[poscond].Length) < (elements[1]).Length)
                                {
                                    foreach (objectDef obj in list)
                                    {
                                        result = result + obj.GetColumnName() + " " + splittedline[obj.GetPos()] + " ";

                                    }
                                    result = result + ";";

                                }
                            }
                        }
                        else
                        {
                            if (comptype.ToLower() == "int")
                            {
                                if (Int32.Parse(splittedline[poscond]) > Int32.Parse(elements[1]))
                                {
                                    foreach (objectDef obj in list)
                                    {
                                        result = result + obj.GetColumnName() + " " + splittedline[obj.GetPos()] + " ";

                                    }
                                    result = result + ";";

                                }
                            }
                            else
                            {
                                if ((splittedline[poscond].Length) > (elements[1]).Length)
                                {
                                    foreach (objectDef obj in list)
                                    {
                                        result = result + obj.GetColumnName() + " " + splittedline[obj.GetPos()] + " ";

                                    }
                                    result = result + ";";

                                }
                            }
                        }

                    }
                }
                //show all
                else
                {
                    result = "The result for the Query '" + Query + "' is:";
                    foreach (string linea in allFile2)
                    {
                            result = result + " " + atindb[1];
                        string[] splittedline = linea.Split(',');



                        if (op == "=")
                        {
                            if (splittedline[poscond] == elements[1])
                            {
                                int cont = 2;
                                foreach (string line2 in splittedline)
                                {
                                    if((cont) < atindb.Count)
                                        {
                                            result = result + " " + line2 + " " + atindb[cont];
                                            cont++;
                                        }
                                    else if((cont) == atindb.Count)
                                        {
                                            result = result + " " + line2;
                                        }
                                }
                                result = result + ";";


                            }


                        }
                        else if (op == "<")
                        {
                            if (comptype.ToLower() == "int")
                            {
                                if (Int32.Parse(splittedline[poscond]) < Int32.Parse(elements[1]))
                                {
                                        int cont = 2;
                                        foreach (string line2 in splittedline)
                                        {
                                            if ((cont) < atindb.Count)
                                            {
                                                result = result + " " + line2 + " " + atindb[cont];
                                                cont++;
                                            }
                                            else if ((cont) == atindb.Count)
                                            {
                                                result = result + " " + line2;
                                            }
                                        }
                                        result = result + ";";




                                    }
                                }
                            else
                            {
                                if ((splittedline[poscond].Length) < (elements[1]).Length)
                                {
                                        int cont = 2;
                                        foreach (string line2 in splittedline)
                                        {
                                            if ((cont) < atindb.Count)
                                            {
                                                result = result + " " + line2 + " " + atindb[cont];
                                                cont++;
                                            }
                                            else if ((cont) == atindb.Count)
                                            {
                                                result = result + " " + line2;
                                            }
                                        }
                                        result = result + ";";



                                    }
                                }
                        }
                        else
                        {
                            if (comptype.ToLower() == "int")
                            {
                                if (Int32.Parse(splittedline[poscond]) > Int32.Parse(elements[1]))
                                {
                                        int cont = 2;
                                        foreach (string line2 in splittedline)
                                        {
                                            if ((cont) < atindb.Count)
                                            {
                                                result = result + " " + line2 + " " + atindb[cont];
                                                cont++;
                                            }
                                            else if ((cont) == atindb.Count)
                                            {
                                                result = result + " " + line2;
                                            }
                                        }
                                        result = result + ";";



                                    }
                                }
                            else
                            {
                                if ((splittedline[poscond].Length) < (elements[1]).Length)
                                {
                                        int cont = 2;
                                        foreach (string line2 in splittedline)
                                        {
                                            if ((cont) < atindb.Count)
                                            {
                                                result = result + " " + line2 + " " + atindb[cont];
                                                cont++;
                                            }
                                            else if ((cont) == atindb.Count)
                                            {
                                                result = result + " " + line2;
                                            }
                                        }
                                        result = result + ";";



                                    }
                                }
                        }

                    }
                }
            }
            }
            if (result== "The result for the Query '" + Query + "' is: ")
            {
                result = "Empty Query";
            }
            }


            
        }
       
    }
    public class objectDef
    {
        private string columnname;
        private int pos;
        private string type;

        public objectDef(string pname,int ppos,string ptype)
        {
            columnname = pname;
            pos = ppos;
            type = ptype;
        }
        public string GetColumnName()
        {
            return columnname;
        }
        public string GetTypeColumn()
        {
            return type;
        }
        public int GetPos()
        {
            return pos;
        }
    }

