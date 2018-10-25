using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Grammar_Stuffs;


namespace WpfApp1.LR1_Stuffs
{
    /// <summary>
    /// Clase que representa cada uno de los elementos de cerradura.
    /// </summary>
    class C_Closure_Element
    {
        /// <summary>
        /// Produccion que esta dentro de Cerradura.
        /// </summary>
        C_Production a_production;
        /// <summary>
        /// Lista de simbolos de busqueda hacia adelante, basicamente es 'a', en la expresion A->alfa .X gama,{'a'}
        /// </summary>
        List<string> forward_search_symbols;

        /// <summary>
        /// Obtiene O establece la produccion de este elemento de Cerradura.
        /// </summary>
        public C_Production A_production { get => a_production; set => a_production = value; }


        /// <summary>
        /// Obtiene o establece la lista de simbolos de busqueda hacia adelante de este elemento  de Cerradura.
        /// </summary>
        public List<string> Forward_search_symbols { get => forward_search_symbols; set => forward_search_symbols = value; }
    }
}
