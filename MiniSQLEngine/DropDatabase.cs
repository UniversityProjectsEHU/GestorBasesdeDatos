using MiniSQLEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DropDatabase:Query
{
    private string name;
    
    public DropDatabase(string pName)
    {
        name = pName;
    }

    public override void Run()
    {
        throw new NotImplementedException();
    }
}
