using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gramatica
{
    class Thomson
    {

        public List<List<int>> C = new List<List<int>>();
        public List<Nodo> Nodos = new List<Nodo>();
        public Thomson()
        {
        }

        public void CreaLista(List<int>  t)
        {
            List<int> NewLista = new List<int>();
            for (int i = 0; i < t.Count; i++)
                NewLista.Add(t[i]);
            C.Add(NewLista);
        }

        public void newNodos(int or,int des,string let)
        {
            Nodos.Add(new Nodo(or,let));
            Nodos[Nodos.Count - 1].Relaciones.Add(des);
        }

        public bool SetLista(int k, List<int> T)
        {
            bool Diferente = true;

            if (T.Count == C[k].Count)
                for (int i = 0; i < C[k].Count; i++)
                {
                    if (T[i] != C[k][i])
                        Diferente = false;
                }
            else
                Diferente = false;

            return Diferente;
        }
        public List<int> GetLista(int k)
        {
            return C[k];
        }

    }
}
