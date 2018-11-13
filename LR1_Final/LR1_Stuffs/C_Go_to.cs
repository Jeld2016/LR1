using LR1_Final.Grammar_Stuffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1_Final.LR1_Stuffs
{
    /// <summary>
    /// Clase IR_A.
    /// Basicamente cumple con la representacion:
    /// | IR_A |
    /// |------+
    /// |(0,S) |
    /// </summary>
    class C_Go_to
    {
        /// <summary>
        /// Numero de Estado.
        /// </summary>
        int state;



        /// <summary>
        /// Solo un Simbolo
        /// </summary>
        C_Symbol symbol_state;


        /// <summary>
        /// Instancia nueva de C_Go_to.
        /// </summary>
        public C_Go_to()
        {
            this.state = -1;
            this.symbol_state = new C_Symbol();
        }


        /// <summary>
        /// Genera una nueva instancia de IR_A(GOTO)
        /// </summary>
        /// <param name="from_state"></param>
        /// <param name="symbol_transition"></param>
        public C_Go_to(int from_state, C_Symbol symbol_transition)
        {
            this.state = from_state;
            this.symbol_state = symbol_transition;
        }


        /// <summary>
        /// Obtiene o establece el estado, de este IR_A
        /// </summary>
        public int State { get => state; set => state = value; }


        /// <summary>
        /// Obtiene o establece el simbolo de este IR_A 
        /// </summary>
        public C_Symbol Symbol_state { get => symbol_state; set => symbol_state = value; }
    }
}
