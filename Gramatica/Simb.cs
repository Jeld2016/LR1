using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramatica
{
    public class Simbolos
    {
        public string cadena;
        public List<string> list;

        public Simbolos(string c)
        {
            cadena = c;
            list = new List<string>();
        }
    }
}
