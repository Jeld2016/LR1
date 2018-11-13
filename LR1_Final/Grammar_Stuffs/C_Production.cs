using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1_Final.Grammar_Stuffs
{
    class C_Production
    {
        /// <summary>
        /// Parte izquierda de la gramatica.
        /// </summary>
        String producer;
        /// <summary>
        /// Lista de simbolos que pertenecen a la parte derecha de la produccion.
        /// </summary>
        List<C_Symbol> right;


        public C_Production()
        {
            this.producer = String.Empty;
            this.Right = new List<C_Symbol>();
        }


        /// <summary>
        ///Constructor por copia. 
        /// </summary>
        /// <param name=""></param>
        public C_Production(C_Production copy_production)
        {
            this.producer = copy_production.Producer;
            this.right = new List<C_Symbol>(copy_production.Right);
        }
        /// <summary>
        /// Crea una nueva instancia de C_Produccion, es decir crea una nueva produccion.
        /// </summary>
        /// <param name="name">Parte izquierda de la  produccion</param>
        public C_Production(String name)
        {
            producer = name;
            this.Right = new List<C_Symbol>();
        }

        /// <summary>
        /// Regresa y Establece el valor de la lista de Simbolos pertencientes a la produccion.
        /// </summary>
        public List<C_Symbol> Right { get => right; set => right = value; }


        /// <summary>
        /// Obtiene o establece el nombre de la Produccion, es decir su NO_Terminal
        /// </summary>
        public string Producer { get => producer; set => producer = value; }


        /// <summary>
        /// Regresa la lista de simbolos pertenecientes a esta produccion.
        /// </summary>
        /// <returns></returns>
        public List<C_Symbol> Get_Right()
        {
            return this.Right;
        }



        /// <summary>
        /// Determina si el PRIMER simbolo de la produccion es un TERMINAL.
        /// </summary>
        /// <returns>El terminal si el PRIMER simbolo es TERMINAL, de lo contrario retorna Cadena Vacia.</returns>
        public string is_first_symbol_TERMINAL()
        {
            C_Symbol first_symbol = this.right[0];

            if (first_symbol.Type_symbol == 0)
                return first_symbol.Symbol;
            return string.Empty;
        }


        /// <summary>
        /// Obtiene el primer simbolo que aparece en la produccion.
        /// </summary>
        /// <returns></returns>
        public C_Symbol get_first_symbol() { return this.right[0]; }


        /// <summary>
        /// Determina si el PRIMER simbolo de la produccion es EPSILON.
        /// </summary>
        /// <returns>TRUE si el PRIMER simbolo es EPSILON</returns>
        public bool is_first_symbol_EPSILON()
        {
            C_Symbol first_symbol = this.right[0];

            if (first_symbol.Type_symbol == 3)
                return true;
            return false;
        }



        public void swap_point()
        {

            int ap = this.index_DOT();
            List<C_Symbol> Aux = new List<C_Symbol>(right);
            List<C_Symbol> Aux2 = new List<C_Symbol>();
            Aux.Remove(right[ap]);
            for (int i = 0; i < Aux.Count; i++)
            {
                if (ap + 1 < Aux.Count)///comentario por favor 
                {
                    if (i == ap + 1)///comentario por favor 
                    {
                        Aux2.Add(right[ap]);
                        Aux2.Add(Aux[i]);
                    }
                    else///comentario por favor 
                    {
                        Aux2.Add(Aux[i]);
                    }
                }
                else
                { //Punto al final
                    Aux2.Add(Aux[i]);
                    if (i == Aux.Count - 1)
                        Aux2.Add(right[ap]);
                }
            }
            right.Clear();
            right = new List<C_Symbol>(Aux2);

        }



        /// <summary>
        /// Obtiene el simbolo que esta siguiente al punto.
        /// </summary>
        /// <returns>Simbolo que este siguiente al punto, NULL si el punto se encuentra al final de la parte derecha de la produccion </returns>
        public C_Symbol get_symbol_next_to_DOT()
        {
            int index_dot = this.index_DOT();
            if (index_dot < this.Right.Count - 1)
                return this.Right[index_dot + 1];
            else
                return null;

        }

        /// <summary>
        /// Obtiene el indice en el que se encuentra el punto dentro de la lista de simbolos que pertence a la parte 
        /// derecha de la produccion
        /// </summary>
        public int index_DOT()
        {
            int index;
            int num_symbols = this.Right.Count;
            C_Symbol tmp_symbol;

            for (index = 0; index < num_symbols; index++)
            {
                tmp_symbol = this.right[index];
                if (tmp_symbol.Type_symbol == 3)
                    break;
            }
            return index;
        }


        /// <summary>
        /// Determina si una produccion es igual a otra
        /// </summary>
        /// <param name="incoming_production">Produccion con la que se esta comparando</param>
        /// <returns>true si la produccion es igual con la que se le esta pasando como argunmento</returns>
        public bool is_equal_to_Other_C_Production(C_Production incoming_production)
        {
            bool equal = false;

            if (string.Compare(incoming_production.Producer, this.producer) == 0)
            {
                if (this.right.Count == incoming_production.Right.Count)
                {
                    int i;

                    for (i = 0; i < this.right.Count; i++)
                    {
                        if (this.right[i].simbol_equal(incoming_production.Right[i]) == false)
                            break;
                    }
                    if (i == this.right.Count)
                        equal = true;
                }
            }
            return equal;
        }
    }
}
