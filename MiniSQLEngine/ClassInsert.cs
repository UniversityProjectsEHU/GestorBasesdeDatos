
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassInsert : Query
    {
        private string aTable;
        private string[] values;
        private string result = "";
        private string[] atributes;
        public ClassInsert(String table, String[] myArray, String[] myArray2)
        {
            aTable = table;
            values = myArray;
            atributes = myArray2;
        }
        public string GetTable()
        {
            return aTable;
        }

        public string[] GetValues()
        {
            return values;
        }


        public override string getClass()
        {
            return "insert";
        }

        public override string getResult()
        {
            return result;
        }

        public override void Run(string dbname)
        {
            int contando = 0;
            foreach (String i in values)
            {
                
                if (i.Contains("'"))
                {
                    values[contando]=i.Trim('\'');
                }
                contando++;
            }
            string pathfileDATA = @"..//..//..//data//" + dbname + "//" + aTable + ".data";
            bool continuar = true;

            if (!File.Exists(pathfileDATA))
            {
                result = Constants.TableDoesNotExist;
                continuar = false;
            }

            //Error column not exits
            if (continuar == true && atributes != null)
            {
                String[] lineadef = System.IO.File.ReadAllLines("..//..//..//data//" + dbname + "//" + aTable + ".def");
                foreach (String valor in atributes)
                {
                    if (lineadef[0].Contains(valor) == false)
                    {
                        result = Constants.ColumnDoesNotExist;
                        continuar = false;
                    }
                }
            }

            //Error data type incorrect
            if (continuar == true && atributes!=null)
            {
                String[] lineadef = System.IO.File.ReadAllLines("..//..//..//data//" + dbname + "//" + aTable + ".def");
                foreach (String parte in lineadef)
                {
                    String[] lineadef2 = parte.Split(',');
                    foreach (String parte2 in lineadef2)
                    {
                        String[] parte3 = parte2.Split(' ');
                        int indice = 0;
                        foreach (String atributoigual in atributes)
                        {
                            if (parte3[0] == atributoigual)
                            {
                                String tipo = parte3[1].ToUpper();
                                //INT
                                if (tipo == "INT")
                                {
                                    try
                                    {
                                        int.Parse(values[indice]);
                                    }
                                    catch
                                    {
                                        result = Constants.IncorrectDataType;
                                        continuar = false;
                                    }
                                }
                                //DOUBLE
                                if (tipo == "DOUBLE")
                                {
                                    try
                                    {
                                        double.Parse(values[indice]);
                                    }
                                    catch
                                    {
                                        result = Constants.IncorrectDataType;
                                        continuar = false;
                                    }
                                }
                            }
                            indice++;
                        }
                    }
                }
            }


            if (continuar==true)
            {
                if (atributes==null)
                {
                string pathfileDEF = @"..//..//..//data//" + dbname + "//" + aTable + ".def";
                string line1;
                List<string> columns = new List<string>();

                using (StreamReader sr = new StreamReader(pathfileDEF))
                {
                    while ((line1 = sr.ReadLine()) != null)
                    {
                        string[] parts = line1.Split(',');
                        foreach (string element in parts)
                        {
                            string[] atributes = element.Split(' ');
                            string type = atributes[1];
                            columns.Add(type);
                        }
                    }
                }
                    if (columns.Count() != values.Length)
                    {
                        result = Constants.WrongSyntax;
                    }

                    else
                    {
                        int index = 0;
                        foreach (string c in columns)
                        {

                            if (c.ToLower().Equals("int"))
                            {
                                try
                                {
                                    int.Parse(values[index]);
                                }
                                catch
                                {
                                    result = Constants.IncorrectDataType;
                                }
                            }
                            index++;
                        }

                        if (result == "")
                        {
                            string texto = "";
                            for (int i = 0; i < values.Length; i++)
                            {
                                if (i == values.Length - 1)
                                {
                                    texto = texto + values[i];
                                }
                                else
                                {
                                    texto = texto + values[i] + ",";

                                }
                            }

                            using (StreamWriter file = File.AppendText(pathfileDATA))
                            {
                                //Data added to the document
                                file.WriteLine(texto);
                                file.Close();
                                result = Constants.InsertSuccess;
                            }
                        }
                    }
                }
                else
                {
                    String[] lineadef = System.IO.File.ReadAllLines("..//..//..//data//" + dbname + "//" + aTable + ".def");
                    String[] porComas = lineadef[0].Split(',');
                    String[] atributosDEF = new String[porComas.Length];
                    int contador = 0;
                    foreach(String actual in porComas)
                    {
                        String[] espacio=actual.Split(' ');
                        atributosDEF[contador] = espacio[0];
                        contador++;
                    }
                    String[] linea = new String[porComas.Length];
                    int indice = 0;
                    foreach (String atriDEFActual in atributosDEF)
                    {
                        int pos = 0;
                        Boolean esta = false;
                        int posVerdad = 0;
                        foreach (String atributoActual in atributes)
                        {
                            if (atributoActual == atriDEFActual)
                            {
                                posVerdad = pos;
                                esta = true;
                            }
                            else
                            {
                                pos++;
                            }
                        }
                        if (esta==true)
                        {
                            linea[indice] = values[posVerdad];
                        }
                        else
                        {
                            linea[indice] = null;
                        }
                        indice++;
                    }
                    String linea2 = "";
                    int contar = 0;
                    foreach (String ahora in linea)
                    {
                        if (contar==0)
                        {
                            if (ahora==null)
                            {
                                string ahora2 = "null";
                                linea2 = ahora2;
                                contar++;
                            }
                            else
                            {
                                linea2 = ahora;
                                contar++;
                            }
                        }
                        else
                        {
                            if (ahora == null)
                            {
                                string ahora2 = "null";
                                linea2 = linea2 + "," + ahora2;
                                contar++;
                            }
                            else
                            {
                                linea2 = linea2 + "," + ahora;
                            }
                        }
                    }

                    using (StreamWriter file = File.AppendText(pathfileDATA))
                    {
                        //Data added to the document
                        file.WriteLine(linea2);
                        file.Close();
                        result = Constants.InsertSuccess;
                    }
                }
            }
        }
    }
}
