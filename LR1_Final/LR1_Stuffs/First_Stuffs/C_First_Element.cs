using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1_Final.LR1_Stuffs.First_Stuffs
{
    class C_First_Element
    {
        /// <summary>
        /// Nombre del conjunto. No Terminal.
        /// </summary>
        String no_terminal;
        /// <summary>
        /// Lista de simbolos pertenecientes a este conjunto PRIMERO.
        /// </summary>
        List<string> first;

        public C_First_Element()
        {
            this.no_terminal = string.Empty;
            this.first = new List<string>();
        }


        /// <summary>
        /// Se inicializa lista de simbolos para almacenar conjunto primero.
        /// </summary>
        /// <param name="name_set">Nombre del conjunto Primero que se esta generando</param>
        public C_First_Element(string name_set)
        {
            this.no_terminal = name_set;    
            this.first = new List<string>();
        }   


        /// <summary>
        /// Obtiene y Establece el conjunto Primero, que basicamente es la lista de simbolos terminales.
        /// </summary>
        public List<string> First { get => first; set => first = value; }


        /// <summary>
        /// Obtiene o establece el No terminal del elemento 
        /// </summary>
        public string No_terminal { get => no_terminal; set => no_terminal = value; }


        /// <summary>
        /// Agrega un simbolo a la lista de simbolos de este elemento de Primero, si es que no existiera en el conjunto de primero
        /// </summary>
        /// <param name="symbol">Simbolo a insertar en el conjunto</param>
        public void add_symbol(string symbol)
        {
            if (!this.first.Contains(symbol))
                this.first.Add(symbol);
        }


        /// <summary>
        /// Determina si un determinado simbolo se encuentra en la lista de simbolos de este elemento de Primero.
        /// </summary>
        /// <param name="element">Simbolo que se esta buscando</param>
        /// <returns>True si el elemento se encuentra dentro de la lista </returns>
        public bool exist_element(string element) { return this.first.Contains(element); }


        /// <summary>
        /// Limpia la liista de simbolos de este elemento de primero.
        /// </summary>
        public void clear_set() { this.first.Clear(); }
    }
}
