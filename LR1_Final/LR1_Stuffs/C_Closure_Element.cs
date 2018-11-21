using LR1_Final.Grammar_Stuffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1_Final.LR1_Stuffs
{
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
            this.production = new C_Production(c.production);
            this.forward_search_symbols = new List<string>(c.forward_search_symbols);
        }

        public C_Closure_Element()
        {
            this.production = new C_Production();
            this.forward_search_symbols = new List<string>();
        }

        /// <summary>
        /// Inicializa una instancia de C_Closure_Element, con un C_Production que corresponde al KERNEL de esta instancia.
        /// </summary>
        /// <param name="a_production">Produccion que pertenece a este Kernel</param>
        public C_Closure_Element(C_Production a_production)
        {
            this.production = a_production;
            this.forward_search_symbols = new List<string>();
        }


        public void append_look_up_symbols(List<string>lookup_symbs) {
            foreach (string simple_string in lookup_symbs)
                if (this.forward_search_symbols.Contains(simple_string) == false)
                    this.forward_search_symbols.Add(simple_string);
        }

        public void add_look_up_symbols(List<string> lookup_symbs) {
            this.forward_search_symbols = new List<string>(lookup_symbs);
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


        /// <summary>
        /// Checa si este Closure_Element es igual a otro Closure_Element
        /// </summary>
        /// <param name="incoming_pr">Produccion del Closure_Element que se esta comparando</param>
        /// <param name="incoming_srch_symb">Lista de simbolos de busqueda hacia adelante del Closure_Element que se esta comparando</param>
        /// <returns></returns>
        public bool Closure_Element_is_Equal_to_Another_Closure(C_Production incoming_pr, List<string> incoming_srch_symb)
        {
            bool equal = false;

            if (this.production.is_equal_to_Other_C_Production(incoming_pr) == true)
            {
                int length_list = this.forward_search_symbols.Count;
                if (length_list == incoming_srch_symb.Count) {
                    int index;

                    for (index = 0; index < length_list; index++) {
                        if (this.forward_search_symbols.Contains(incoming_srch_symb[index]) == false)
                            break;
                    }
                    if (index == length_list)
                        equal = true;
                }
            }
            return equal;
        }
    }
}
