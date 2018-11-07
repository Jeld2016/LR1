using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Grammar_Stuffs;


namespace WpfApp1.LR1_Stuffs
{

    /// <summary>
    /// [S' -> .S, {$}]
    /// [C -> c.C, {c, d}]   
    /// Clase que representa cada uno de los elementos de cerradura.
    /// </summary>
    class C_Closure_Element
    {
        /// <summary>
        /// Produccion que esta dentro de Cerradura.
        /// </summary>
        C_Production production;
        /// <summary>
        /// Lista de simbolos de busqueda hacia adelante, basicamente es 'a', en la expresion A->alfa .X gama,{'a'}
        /// </summary>
        List<string> forward_search_symbols;

        public C_Closure_Element(C_Closure_Element c)
        {
            this.production = c.production;
            this.forward_search_symbols = c.forward_search_symbols;
        }

        public C_Closure_Element() {
            this.production = new C_Production();
            this.forward_search_symbols = new List<string>();
        }

        /// <summary>
        /// Inicializa una instancia de C_Closure_Element, con un C_Production que corresponde al KERNEL de esta instancia.
        /// </summary>
        /// <param name="a_production">Produccion que pertenece a este Kernel</param>
        public C_Closure_Element(C_Production a_production) {
            this.production = a_production;
            this.forward_search_symbols = new List<string>();
        }


        /// <summary>
        /// Crea una instancia de un elemento de Cerradura
        /// </summary>
        /// <param name="left_side_symbol">Parte  izquierda de la produccion que pertence a la produccion del elemento de Cerradura.</param>
        public C_Closure_Element(string left_side_symbol)
        {
            this.production = new C_Production(left_side_symbol);
            this.forward_search_symbols = new List<string>();
        }


        /// <summary>
        /// Obtiene O establece la produccion de este elemento de Cerradura.
        /// </summary>
        public C_Production Production { get => production; set => production = value; }


        /// <summary>
        /// Obtiene o establece la lista de simbolos de busqueda hacia adelante de este elemento  de Cerradura.
        /// </summary>
        public List<string> Forward_search_symbols { get => forward_search_symbols; set => forward_search_symbols = value; }
    }
}
