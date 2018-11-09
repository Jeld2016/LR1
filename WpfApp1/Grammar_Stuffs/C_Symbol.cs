using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Grammar_Stuffs
{
    public class C_Symbol
    {     

        /// <summary>
        /// type:  <0 = terminal> / <1 = no_terminal> / <2 = epsilon>/ <3 = punto> / -1 = me vale madres que sea.
        /// </summary>
        int type_symbol;
        /// <summary>
        /// Simbolo que basicamente es una simple cadena
        /// </summary>
        String symbol;


        public C_Symbol()
        {
            this.type_symbol = 0;
            this.symbol = string.Empty;
        }



        /// <summary>
        /// Crea un nuevo simbolo con la cadena especificada.
        /// </summary>
        /// <param name="name_symbol">Cadena con la que se creara este simbolo</param>
        public C_Symbol(string name_symbol) {
            this.symbol = name_symbol;
            this.type_symbol = 0;
        }


        /// <summary>
        /// Crea un nuevo simbolo
        /// </summary>
        /// <param name="value">Cadena que representa al simbolo</param>
        /// <param name="types">Tipo del simbolo 0 = Terminal, 1 = No Termnal, 3 = Epsilon, -1 = ALV</param>
        public C_Symbol(string value, int types)
        {
            this.type_symbol = types;
            this.symbol = value;
        }


        /// <summary>
        /// Obtiene y establece el tipo de simbolo.
        /// </summary>
        public int Type_symbol { get => type_symbol; set => type_symbol = value; }


        /// <summary>
        /// Obtiene y establece el valor del simbolo que basicamente es una cadena.
        /// </summary>
        public string Symbol { get => symbol; set => symbol = value; }


        /// <summary>
        /// Checa si un simbolo es igual a este
        /// </summary>
        /// <param name="incoming_symbol">Simbolo con el que se esta comparando</param>
        /// <returns></returns>
        public bool simbol_equal(C_Symbol incoming_symbol) {
            if (type_symbol == incoming_symbol.Type_symbol)
                if (string.Compare(this.symbol, incoming_symbol.Symbol) == 0)
                    return true;
            return false;
        }
    }
}
