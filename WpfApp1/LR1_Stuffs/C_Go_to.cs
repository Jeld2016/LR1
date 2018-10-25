using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Grammar_Stuffs;

namespace WpfApp1.LR1_Stuffs
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
        public C_Go_to() {
            this.state = -1;
            this.symbol_state = new C_Symbol();
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
