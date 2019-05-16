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
        private string[] columns;
        private string table;
        private string condition;
        private string result;
        private string Query;

        public ClassSelect(string[] columns, string table, string condition, string pQuery)
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
            if (condition.Contains("'"))
            {
                String[] i = condition.Split('=');
                i[1] = i[1].Trim('\'');
                condition = i[0] + "=" + i[1];
            }

            List<string> rm = new List<string>();
            string allFile = System.IO.File.ReadAllText("..//..//..//data//" + dbname + "//" + table + ".def");
            string[] splittedFile = allFile.Split(',');
            string op;
            string[] elements = { };
            int poscond = 0;
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
                return;
            }
            else
            {


                //I read all the file
                allFile = System.IO.File.ReadAllText("..//..//..//data//" + dbname + "//" + table + ".def");
                splittedFile = allFile.Split(',');
                List<objectDef> list = new List<objectDef>();
                string comptype;
                if (condition == "")
                {
                    comptype = "nolo";
                }
                else
                {
                    comptype = "";
                }
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
                if (showall)
                {
                    atindb.Add("*");

                }
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



                    if (comptype.ToLower() == "double")
                    {
                        comptype = "int";
                    }
                    string[] allFile2 = System.IO.File.ReadAllLines("..//..//..//data//" + dbname + "//" + table + ".data");
                    if (!showall)
                    {
                        result = "The result for the Query '" + Query + "' is:";



                        foreach (string linea in allFile2)
                        {
                            string[] splittedline = linea.Split(',');

                            if (condition == "")
                            {
                                foreach (objectDef obj in list)
                                {
                                    result = result + " " + obj.GetColumnName() + " " + splittedline[obj.GetPos()];
                                }
                                result = result + ";";
                            }
                            else
                            {



                                if (op == "=")
                                {
                                    if (splittedline[poscond] == elements[1])
                                    {
                                        foreach (objectDef obj in list)
                                        {
                                            result = result + " " + obj.GetColumnName() + " " + splittedline[obj.GetPos()];
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
                                                result = result + " " + obj.GetColumnName() + " " + splittedline[obj.GetPos()];

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
                                                result = result + " " + obj.GetColumnName() + " " + splittedline[obj.GetPos()];

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
                                                result = result + " " + obj.GetColumnName() + " " + splittedline[obj.GetPos()];

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
                                                result = result + " " + obj.GetColumnName() + " " + splittedline[obj.GetPos()];

                                            }
                                            result = result + ";";

                                        }
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
                            string[] splittedline = linea.Split(',');
                            if (condition == "")
                            {
                                result = result + " " + atindb[1];
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


                            else
                            {


                                if (op == "=")
                                {
                                    if (splittedline[poscond] == elements[1])
                                    {
                                        result = result + " " + atindb[1];
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
                                else if (op == "<")
                                {
                                    if (comptype.ToLower() == "int")
                                    {
                                        if (Int32.Parse(splittedline[poscond]) < Int32.Parse(elements[1]))
                                        {
                                            result = result + " " + atindb[1];

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
                                            result = result + " " + atindb[1];

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
                                            result = result + " " + atindb[1];

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
                                            result = result + " " + atindb[1];

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
                foreach (string at in columns)
                {
                    rm.Add(at);
                }

                if (result != Constants.ColumnDoesNotExist)
                {

                    string[] separate = result.Split(':');
                    string values = separate[1];
                    string[] separate2 = values.Split(' ');
                    List<string> atribs = new List<string>();
                    List<string> val = new List<string>();

                    for (int v = 1; v < separate2.Length; v = v + 2)
                    {
                        if (!atribs.Contains(separate2[v]))
                        {
                            atribs.Add(separate2[v]);
                        }
                        val.Add(separate2[v + 1]);
                    }
                    result = "{";
                    int tam = atribs.Count;
                    int cont = 1;
                    if (atribs.Count != 1)
                    {
                        for (int i = 0; i < atribs.Count; i++)
                        {
                            if (i == atribs.Count - 1)
                            {
                                result = result + atribs[i] + "}{";

                            }
                            else
                            {
                                result = result + atribs[i] + ",";
                            }
                        }
                        for (int i = 0; i < val.Count; i++)
                        {
                            if (cont != tam)
                            {
                                result = result + val[i] + ",";
                                cont++;
                            }
                            else
                            {
                                string[] a = val[i].Split(';');
                                result = result + a[0] + "}";
                                cont = 1;
                                if (i != val.Count - 1)
                                {
                                    result = result + "{";

                                }
                            }



                        }

                    }
                    else
                    {
                        for (int i = 0; i < atribs.Count; i++)
                        {
                            if (i == atribs.Count - 1)
                            {
                                result = result + atribs[i] + "}{";

                            }
                            else
                            {
                                result = result + atribs[i] + ",";
                            }
                        }
                        for (int i = 0; i < val.Count; i++)
                        {
                            if (cont != tam)
                            {
                                result = result + val[i] + ",";
                                cont++;
                            }
                            else
                            {
                                string[] a = val[i].Split(';');
                                result = result + a[0] + "}";
                                cont = 1;
                                if (i != val.Count - 1)
                                {
                                    result = result + "{";

                                }
                            }



                        }
                    }
                    if (separate2.Length == 1 && separate2[0] == "" && showall==true)
                    {
                        string[] list1 = new string[splittedFile.Length];
                        for (int i = 0; i < splittedFile.Length; i++)
                        {
                            string[] line = splittedFile[i].Split(' ');
                            list1[i] = line[0];
                        }
                        int cont1 = 0;
                        int tam1 = list1.Length;
                        for (int i = 0; i < list1.Length; i++)
                        {
                            if (cont1 < tam1 - 1)
                            {
                                result = result + list1[i] + ",";
                                cont1++;
                            }
                            else
                            {
                                result = result + list1[i] + "}{}";
                            }
                        }

                    }
                    if (separate2.Length == 1 && separate2[0] == "" && showall == false)
                    {
                        if (atribs.Count != 1)
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (i == list.Count - 1)
                                {
                                    string tmp = list[i].GetColumnName();
                                    result = result + tmp + "}{}";

                                }
                                else
                                {
                                    string tmpElse = list[i].GetColumnName();
                                    result = result + tmpElse + ",";
                                }
                            }
                            

                        }
                        else
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (i == list.Count - 1)
                                {
                                    result = result + list[i] + "}{}";

                                }
                                else
                                {
                                    result = result + list[i] + ",";
                                }
                            }
                        }
                    }
                }
            }



        }



    }
    public class objectDef
    {
        private string columnname;
        private int pos;
        private string type;

        public objectDef(string pname, int ppos, string ptype)
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
}

