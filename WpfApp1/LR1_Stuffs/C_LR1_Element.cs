using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Grammar_Stuffs;

namespace WpfApp1.LR1_Stuffs
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
        List<C_Closure_Element> closure;




        public C_LR1_Element() {
            this.num_state = -1;
            this.my_go_to = new C_Go_to();
            this.kernel = new List<C_Closure_Element>();
            this.closure = new List<C_Closure_Element>();
        }

        public C_LR1_Element(C_LR1_Element cl1)
        {
            this.num_state = cl1.Num_state;
            this.my_go_to = cl1.My_go_to;
            this.kernel = cl1.Kernel;
            this.closure = cl1.Closure;
        }



        public C_LR1_Element(List<C_Closure_Element>elements_closure_list, int num_s)
        {
            this.num_state = num_s;
            this.my_go_to = new C_Go_to();
            this.kernel = new List<C_Closure_Element>();
            foreach (C_Closure_Element c_el in elements_closure_list) {
                closure.Add(new C_Closure_Element(c_el));
            }
            //this.closure = elements_closure_list;
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
    }
}
