using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSQLEngine
{
    public class ClassUpdate : Query
    {
        private string Table,Condition;
        //atrb=date ; atrib=date
        private string[] Column;
        private string result;
        
        public ClassUpdate(string pTable,string[] pColumn,string pCondition)
        {
            Table = pTable;
            Column = pColumn;
            Condition = pCondition;
        }
        public override string getResult()
        {
            return result;

        }
        public string GetTable()
        {
            return Table;
        }

        public string[] GetColumns()
        {
            return Column;
        }
        public string GetCondition()
        {
            return Condition;
        }

        public override string getClass()
        {
            return "update";
        }

        public override void Run(string dbname)
        {
            int contando = 0;
            foreach (String i in Column)
            {
                String[] o = i.Split('=');
                String dato1=o[1];
                if (dato1.Contains("'"))
                {
                    o[1] = dato1.Trim('\'');
                }
                String nuevacolum = o[0] + "=" + o[1];
                Column[contando] = nuevacolum;
                contando++;
            }

            if (Condition.Contains("'"))
            {
                String[] i = Condition.Split('=');
                i[1] = i[1].Trim('\'');
                Condition = i[0] + "=" + i[1];
            }

            Boolean hayerror = false;
            //Error table not exits
            if (!File.Exists("..//..//..//data//" + dbname + "//" + Table + ".data"))
            {
                result = Constants.TableDoesNotExist;
                hayerror = true;
            }

            //Error column not exits
            if (hayerror == false)
            {
                String[] lineadef = System.IO.File.ReadAllLines("..//..//..//data//" + dbname + "//" + Table + ".def");
                foreach (String lacol in Column)
                {
                    String[] yasplit = lacol.Split('=');
                    String buscar = yasplit[0];
                    if (lineadef[0].Contains(buscar) == false)
                    {
                        result = Constants.ColumnDoesNotExist;
                        hayerror = true;
                    }
                }
            }

            //Error data type incorrect
            if (hayerror == false)
            {
                String[] lineadef = System.IO.File.ReadAllLines("..//..//..//data//" + dbname + "//" + Table + ".def");
                foreach (String parte in lineadef)
                {
                    String[] lineadef2 = parte.Split(',');
                    foreach (String parte2 in lineadef2)
                    {
                        String[] parte3 = parte2.Split(' ');
                        foreach (String atributoigual in Column)
                        {
                            String[] atributo = atributoigual.Split('=');
                            if (parte3[0] == atributo[0])
                            {
                                String tipo = parte3[1].ToUpper();
                                //INT
                                if (tipo == "INT")
                                {
                                    try
                                    {
                                        int.Parse(atributo[1]);
                                    }
                                    catch
                                    {
                                        result = Constants.IncorrectDataType;
                                        hayerror = true;
                                    }
                                }
                                //DOUBLE
                                if (tipo == "DOUBLE")
                                {
                                    try
                                    {
                                        double.Parse(atributo[1]);
                                    }
                                    catch
                                    {
                                        result = Constants.IncorrectDataType;
                                        hayerror = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Error primary key already exists
            if (hayerror == false)
            {
                String[] lineadef = System.IO.File.ReadAllLines("..//..//..//data//" + dbname + "//" + Table + ".def");
                String[] atributos = lineadef[0].Split(',');
                int posPK = 0;
                String atribPK = "";
                for (int i = 0; i < atributos.Length; i++)
                {
                    if (atributos[i].Contains("true"))
                    {
                        posPK = i;
                        String[] atrib = atributos[i].Split(' ');
                        atribPK = atrib[0];
                    }
                }
                String cambio = "";
                Boolean semodifica = false;
                foreach (String upda in Column)
                {
                    String[] updaactual = upda.Split('=');
                    String esultimo = updaactual[0] + ";";
                    if (updaactual[0] == atribPK)
                    {
                        cambio = updaactual[1];
                        semodifica = true;
                    }
                    else if (esultimo == atribPK)
                    {
                        cambio = updaactual[1];
                        semodifica = true;
                    }
                }
                if (semodifica == true)
                {
                    String[] lineas = System.IO.File.ReadAllLines("..//..//..//data//" + dbname + "//" + Table + ".data");
                    foreach (String lineactual in lineas)
                    {
                        String[] lineactualsplit = lineactual.Split(',');
                        if (lineactualsplit[posPK] == cambio)
                        {
                            result = Constants.Error + "primary key already exists";
                            hayerror = true;
                        }
                    }
                }
            }

            //NO Error
            if (hayerror == false)
            {
                String[] elements = new String[2];
                String operador = "";
                int posicion = 0;

                //I need to know the operator of the condition
                if (Condition.Contains("="))
                {
                    elements = Condition.Split('=');
                    operador = "=";
                }
                else if (Condition.Contains("<"))
                {
                    elements = Condition.Split('<');
                    operador = "<";
                }
                else if (Condition.Contains(">"))
                {
                    elements = Condition.Split('>');
                    operador = ">";
                }




                //I need a new line with the new dates of the row
                //String newRow = "";
                //int longitud = Column.Length;
                //int cuenta = 1;
                //foreach (String colum in Column)
                //{
                //    if (cuenta!=longitud)
                //    {
                //        String[] actual = colum.Split('=');
                //        newRow = newRow + actual[1] + ",";
                //    }
                //    else
                //    {
                //        String[] actual = colum.Split('=');
                //        newRow = newRow + actual[1];
                //    }
                //    cuenta++;
                //}

                //Open te file .def
                String allFile = System.IO.File.ReadAllText("..//..//..//data//" + dbname + "//" + Table + ".def");
                String[] atrib = allFile.Split(',');

                //Search the postion of the atribute that appears in the condition
                String buscar = elements[0];
                Boolean parar = false;
                foreach (String atributo in atrib)
                {
                    if (!parar)
                    {
                        if (!atributo.Contains(buscar))
                        {
                            posicion = posicion + 1;
                        }
                        else
                        {
                            parar = true;
                        }
                    }
                }

                //Open te file .data
                String[] lineas = System.IO.File.ReadAllLines("..//..//..//data//" + dbname + "//" + Table + ".data");

                //[atr1/atr2]
                String[] lineadef = System.IO.File.ReadAllLines("..//..//..//data//" + dbname + "//" + Table + ".def");
                String[] lineacoma = lineadef[0].Split(',');
                String[] atributos = new String[lineacoma.Length];
                int cuantas = 0;
                foreach (string linea in lineacoma)
                {
                    String[] lineaespacio = linea.Split(' ');
                    atributos[cuantas] = lineaespacio[0];
                    cuantas = cuantas + 1;
                }
                //Make the update
                int inde = 0;
                foreach (String linea in lineas)
                {
                    String[] datos = linea.Split(',');
                    if (operador == "<")
                    {
                        int numero = Int32.Parse(datos[posicion]);
                        if (numero < Int32.Parse(elements[1]))
                        {
                            foreach (String columna in Column)
                            {
                                String[] columnaSep = columna.Split('=');
                                for (int i = 0; i < atributos.Length; i++)
                                {
                                    if (columnaSep[0] == atributos[i])
                                    {
                                        datos[i] = columnaSep[1];
                                    }
                                }
                            }
                            String newRow = "";
                            for (int i = 0; i < datos.Length; i++)
                            {
                                if (i != (datos.Length - 1))
                                {
                                    newRow = newRow + datos[i] + ",";
                                }
                                else
                                {
                                    newRow = newRow + datos[i];
                                }
                            }
                            lineas.SetValue(newRow, inde);
                        }
                        inde++;
                    }
                    else if (operador == ">")
                    {
                        int numero = Int32.Parse(datos[posicion]);
                        if (numero > Int32.Parse(elements[1]))
                        {
                            foreach (String columna in Column)
                            {
                                String[] columnaSep = columna.Split('=');
                                for (int i = 0; i < atributos.Length; i++)
                                {
                                    if (columnaSep[0] == atributos[i])
                                    {
                                        datos[i] = columnaSep[1];
                                    }
                                }
                            }
                            String newRow = "";
                            for (int i = 0; i < datos.Length; i++)
                            {
                                if (i != (datos.Length - 1))
                                {
                                    newRow = newRow + datos[i] + ",";
                                }
                                else
                                {
                                    newRow = newRow + datos[i];
                                }
                            }
                            lineas.SetValue(newRow, inde);
                        }
                        inde++;

                    }
                    else if (operador == "=")
                    {
                        if (datos[posicion] == elements[1])
                        {
                            foreach (String columna in Column)
                            {
                                String[] columnaSep = columna.Split('=');
                                for (int i = 0; i < atributos.Length; i++)
                                {
                                    if (columnaSep[0] == atributos[i])
                                    {
                                        datos[i] = columnaSep[1];
                                    }
                                }
                            }
                            String newRow = "";
                            for (int i = 0; i < datos.Length; i++)
                            {
                                if (i != (datos.Length - 1))
                                {
                                    newRow = newRow + datos[i] + ",";
                                }
                                else
                                {
                                    newRow = newRow + datos[i];
                                }
                            }
                            lineas.SetValue(newRow, inde);
                        }
                        inde++;
                    }
                }

                //Make changes on the file
                using (StreamWriter sw = System.IO.File.CreateText("..//..//..//data//" + dbname + "//" + Table + ".data"))
                    foreach (String linea in lineas)
                    {
                        sw.WriteLine(linea);
                    }
                result = Constants.TupleUpdateSuccess;
            }
        }
    }
}

