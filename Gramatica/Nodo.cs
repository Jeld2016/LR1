using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramatica
{
    class Nodo
    {
        public List<int> Relaciones = new List<int>();
        public int Name;
        public string l;
        public bool visit = true;
        public Nodo(int id,string letra)
        {
            Name = id;
            l = letra;
        }
    }
}
