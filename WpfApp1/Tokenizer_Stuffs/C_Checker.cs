using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace WpfApp1
{
    /// <summary>
    /// Clase que verifica que los formatos de las produccion sean los correctos.
    /// </summary>
    class C_Checker
    {
        Regex pattern_left;
        Regex pattern_right;

        public C_Checker() {
            /*Esto ahorita esta muy pedorro solo acepta casos basicos
             * \\s : Indica que acepta cualquier espacio en blanco. 
             * ~?  : Puede o no puede haber epsilon. 
             */
            string X;
            string a;
            string start = @"^";
            string end = "$";
            string side_left;
            string side_right;
            string epsilon;

            //X = "(<[a-zA-Z]+>)"; //Paso : )
            //a = "(([a-zA-Z0-9]+)|\\*|\\+)"; // Paso  : )           
            //X = "(([a-zA-Z0-9]+)|\\*|\\+)"; //Caracter Alfa numerico ademas acepta '*' '+'.
            X = "(([a-zA-Z0-9]+)|\\*|\\+|\\(|\\))"; //Caracter Alfa numerico ademas acepta '*' '+'.            
            epsilon = "~?\\s*";
            /*Construccion de Expresion Regular para patrones*/
            side_left = start + "[A-Za-z0-9]+" + end;
            //side_right = start + "(" + epsilon + X + "*" + epsilon + a + "*" + epsilon + "(" + epsilon + X + "*" + epsilon + a + "*" + epsilon + ")" + ")+" + "(\\|" + epsilon + X + "*" + epsilon + a + "*" + epsilon + "(" + epsilon + X + "*" + epsilon + a + "*" + epsilon + ")" + ")*" + end; //Expresion que no valida espacios
            //side_right = start + "((" + epsilon + X + epsilon + ")\\s*)+" +  end; // Esta expresion valida sin OR's
            side_right = start + "\\s*((" + epsilon + X + "*" + epsilon + ")\\s*)+(\\|\\s*(" + epsilon + "(" + X + "\\s*)*"+ epsilon + "))*" + end; // Esta expresion valida sin OR's
            this.pattern_left = new Regex(side_left);
            this.pattern_right = new Regex(side_right);
        }


        /// <summary>
        /// Determina si una cadena tiene el formato valido en la parte izquierda de gramatica.
        /// </summary>
        /// <param name="str">Cadena de entrada para analisar</param>
        /// <returns>True si la cadena es valida</returns>
        public bool match_string_left(string str_LEFT) {
            /*Validamos que la cantidad de simbols <> se la misma, es decir verificamos los cierres*/
            bool thus_match;

            thus_match = this.pattern_left.IsMatch(str_LEFT);
            if (thus_match)
                return true;
            return false;
        }


        /// <summary>
        ///Determina si la cadena tiene el formato valido de la parte derecha de la produccion.
        /// </summary>
        /// <param name="str_RIGHT"></param>
        /// <returns></returns>
        public bool match_string_right(string str_RIGHT)
        {
            bool thus_match = this.validate_PSEUDO_epsilon(str_RIGHT); // Validar que solo tengamos un solo epsilon en nuestra produccion.
            if (thus_match)//Todo esta chingon de puta madre
            {
                thus_match = this.validate_soft_OR_symbol(str_RIGHT); //Determinamos si el simbolo de OR no esta solo al final de la produccion.
                if (thus_match)
                {
                    thus_match = this.pattern_right.IsMatch(str_RIGHT); //Verificamos que el formato de la produccion este correcto.
                    if (thus_match)
                        return true;
                }
            }            
            return false;
        }

        /// <summary>
        /// Valida los simbolos delimitadores de los simbolos No Terminales.
        /// </summary>
        /// <param name="str">Cadena a validar</param>
        /// <returns>True si la validacion fue exitosa</returns>
        private bool less_more_symbol_validator(string str) {
            Stack<char> symbols = new Stack<char>();

            foreach(char c in str) {
                if (c == '<')
                    symbols.Push(c);                
                if (c == '>')
                    if (symbols.Count > 0)
                        symbols.Pop();
                    else
                        return false;
            }
            if(symbols.Count > 0)
                return false;
            return true;
        }

        /// <summary>
        /// Valida que el numero de epsilons sea el correcto es decir 0 o 1 xD cuack!
        /// </summary>
        /// <param name="left">Parte derecha de la produccion</param>
        /// <returns>True si la validacion de epsilon es la correcta epsilon</returns>
        private bool validate_PSEUDO_epsilon(string left) {
            int number_epsilons;
            bool exist_epsilon = left.Contains("~");

            if (exist_epsilon) {
                number_epsilons = Regex.Matches(left, "~").Count;
                if (number_epsilons > 1)
                    return false;
            }
            return true;
        }


        /// <summary>
        /// Solo verifica que el ultimo elemento de la produccion no sea un simbolo |
        /// </summary>
        /// <param name="right"></param>
        /// <returns>Si todo esta chingon, de puta madre OwO</returns>
        private bool validate_soft_OR_symbol(string right) {
            if (right[right.Length - 1] == '|')
                return false;
            return true;
        }
    }   
}




/*NOTAS
 * Modulo/Clase : C_Checker;
 *-------------------------------------------------------------------------
 * <Para la validacion de la parte derecha en donde las Gramaticas pueden ser de la forma> 
   -><No_Terminal>terminal
   ->terminal<No_Terminal>
   ->terminal terminal terminal....<No terminal > <No terminal>....
   ->terminal
   Se implemento una expresion regular simple:
   this.start + "(X*a+(X*a*))+" + this.end;
   Donde : 
          X pertence a los no Terminales 
          a = {conjunto de terminales}
 * <VALIDACION DE EPSILON>
 * Consideraciones: 
                    - No importa donde se encuentre en la parte derecha de la gramatica
                    - Solo tiene que estar una sola vez en la parte derecha(A esto se le tiene que dar especial importancia!)
 * <VALIDACION de los simbolos \< \> >
   - Esto hay que validarlo con una pila.
*/
   
/*
 *<PRUEBAS Expresiones Regulares con OR>            
 /*Prueba #1*/
//this.full_pattern = start + "th(e|is|at)" + end;
/*Prubea #2 */
//this.full_pattern = start + "th(e+|is|at)" + end;
/*Prubea #3 */
//this.full_pattern = start + "th(e|is|at)*" + end;
//<<PRUEBAS con EPSILON el epsilon del teacher xD>>
//this.full_pattern = start + "~" + end;
//pattern = new Regex(this.full_pattern);
 




