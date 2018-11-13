using LR1_Final.Grammar_Stuffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1_Final.LR1_Stuffs
{
    /// <summary>
    ///| GO TO      | Kernel                               | State | Closure                                                                |
    ///|------------+--------------------------------------+-------+------------------------------------------------------------------------|
    ///| goto(0, E) | {[E' -> E., $]; [E -> E.S T, $/+/-]} |     1 | {[E' -> E., $]; [E -> E.S T, $/+/-]; [S -> .+, (/ n]; [S -> .-, (/ n]} |
    /// </summary>
    class C_LR1_Element
    {
        /// <summary>
        /// Numero de estado de este elemento LR1.
        /// </summary>
        int num_state;
        /// <summary>
        /// Es el IR_A de este elemento LR1(estado).
        /// </summary>
        C_Go_to my_go_to;
        /// <summary>
        /// Produccion con la cual se crea estem elemento LR1 o estado.
        /// </summary>
        List<C_Closure_Element> kernel;
        /// <summary>
        /// Esta es la cerradura del elemento LR1.
        /// </summary>
        List<C_Closure_Element> closure = new List<C_Closure_Element>();//checar




        public C_LR1_Element()
        {
            this.num_state = -1;
            this.my_go_to = new C_Go_to();
            this.kernel = new List<C_Closure_Element>();
            this.closure = new List<C_Closure_Element>();
        }


        /// <summary>
        /// Constructor por copia 
        /// </summary>
        /// <param name="copy_lr1_element"></param>
        public C_LR1_Element(C_LR1_Element copy_lr1_element)
        {
            this.num_state = copy_lr1_element.Num_state;
            this.my_go_to = copy_lr1_element.My_go_to;
            this.kernel = copy_lr1_element.Kernel;
            this.closure = copy_lr1_element.Closure;
        }




        public C_LR1_Element(List<C_Closure_Element> elements_closure_list, int num_s)
        {
            this.num_state = num_s;
            this.my_go_to = new C_Go_to();
            this.kernel = new List<C_Closure_Element>();
            foreach (C_Closure_Element c_el in elements_closure_list)
            {
                closure.Add(new C_Closure_Element(c_el));
            }
            //this.closure = elements_closure_list;
        }

        public C_LR1_Element(List<C_Closure_Element> elements_closure_list, int num_s, List<C_Closure_Element> ker, C_Go_to a_go_to)
        {
            this.num_state = num_s;
            this.my_go_to = new C_Go_to();
            this.kernel = ker;
            foreach (C_Closure_Element c_el in elements_closure_list)
            {
                closure.Add(new C_Closure_Element(c_el));
            }
            this.my_go_to = a_go_to;
        }


        /// <summary>
        /// Obtiene o establece el numero de estado de este elemento LR1.
        /// </summary>
        public int Num_state { get => num_state; set => num_state = value; }



        /// <summary>
        ///Obtiene o establece el IR_A de este elemento LR1. 
        /// </summary>
        internal C_Go_to My_go_to { get => my_go_to; set => my_go_to = value; }


        /// <summary>
        /// Obtiene o establece la lista de elementos de Cerradura de este elemento de LR1(Estado).
        /// </summary>
        internal List<C_Closure_Element> Closure { get => closure; set => closure = value; }


        /// <summary>
        /// Obtiene o establece la lista de elementos de Cerradura de donde se origino este estado o Elemento de LR1.
        /// </summary>
        internal List<C_Closure_Element> Kernel { get => kernel; set => kernel = value; }


        public List<C_Closure_Element> generates_new_Kernel(C_Symbol symbol)
        {
            List<C_Closure_Element> nw_list_closure_element;
            C_Symbol tmp_symbol;

            nw_list_closure_element = new List<C_Closure_Element>();
            foreach (C_Closure_Element cl_element in this.closure)
            {
                tmp_symbol = cl_element.Production.get_symbol_next_to_DOT();
                if (tmp_symbol != null)
                {
                    if (string.Compare(tmp_symbol.Symbol, symbol.Symbol) == 0)
                        nw_list_closure_element.Add(new C_Closure_Element(cl_element));
                }
            }
            return nw_list_closure_element;
        }


        /// <summary>
        /// Checa si un Kernel ya existe en este estado o Elemento LR1
        /// </summary>
        /// <param name="a_kernel">Kernel con el que se esta comparando</param>
        /// <returns>True si ya existe</returns>
        public bool kernel_Exist(List<C_Closure_Element> a_kernel)
        {
            bool exist = false;
            int length_kernel = this.kernel.Count;
            int index_closure_element = 0;

            if (a_kernel.Count == length_kernel)
            {
                for (index_closure_element = 0; index_closure_element < length_kernel; index_closure_element++)
                {
                    C_Closure_Element cl_Element0;
                    C_Closure_Element cl_Element1;

                    cl_Element0 = this.kernel[index_closure_element];
                    cl_Element1 = a_kernel[index_closure_element];
                    if (cl_Element0.Closure_Element_is_Equal_to_Another_Closure(cl_Element1.Production, cl_Element1.Forward_search_symbols) == true)
                        break;
                }
                if (index_closure_element < length_kernel) //En algun momento se encontro la  <
                    exist = true;
            }          
            return exist;
        }
    }
}
