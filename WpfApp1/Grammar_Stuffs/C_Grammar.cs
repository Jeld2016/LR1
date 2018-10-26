using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.LR1_Stuffs.First_Stuffs;
using System.Text.RegularExpressions;

namespace WpfApp1.Grammar_Stuffs
{
    public class C_Grammar
    {
        /// <summary>
        /// Lista de produccion que representan la gramatica.
        /// </summary>
        List<C_Production> grammar;
        
        /// <summary>
        /// Lista de no terminales existentes en esta gramatica.
        /// </summary>
        List<string> No_terminals;


        /// <summary>
        /// Obtiene o establece la lista de No terminales Existentes en esta Gramatica.
        /// </summary>
        public List<string> No_terminals1 { get => No_terminals; set => No_terminals = value; }

        public C_Grammar() {
            this.grammar = new List<C_Production>();
            this.No_terminals1 = new List<string>();
        }


        /// <summary>
        /// Establece el conjunto de producciones indicada para la gramatica a usar.
        /// </summary>
        /// <param name="a_list">Lista de producciones que conforman la Gramatica</param>
        public void Set_Grammar(List<C_Production> a_list) {
            foreach (C_Production pr in a_list) {
                this.grammar.Add(pr);
            }
        }


        /// <summary>
        /// Regresa la lista de producciones, es decir la gramatica.
        /// </summary>
        /// <returns></returns>
        public List<C_Production> Get_Grammar() { return this.grammar; }


        /// <summary>
        /// Inserta una produccion desde su estado mas simple(una cadena)
        /// </summary>
        /// <param name="left">Parte izquierda de la produccion</param>
        /// <param name="right">Parte derecha de la produccion</param>
        public void add_element(string left, string right) {
            C_Production new_production;
            C_Symbol symbol;
            string no_term = string.Empty;
            bool can_insert_no_term = false;
            string[] productions = right.Split('|'); //Para obtener generar las produccion a partir del simbolo OR, si es que lo tuviese.

            //this.first_set.add_seed_first_set(left); /*Inicializamos un conjunto de Primero en base al Nombre obtenido del lado izquierdo de la produccion*/
            this.No_terminals1.Add(left);
            foreach (String a_produc in productions)
            {
                new_production = new C_Production(left);
                foreach (char c in a_produc)
                {
                    if (c == '<')//Si se encuentra un simbolo como este, se entiende que los siguientes caracteres componen un  TERMINAL.
                        can_insert_no_term = true;
                    if (c == '>')
                    { // Se Genera el simbolo no TERMINAL y se agrega al elemento de la produccion.
                        can_insert_no_term = false;
                        symbol = new C_Symbol(no_term, 1);
                        new_production.Right.Add(symbol);
                        no_term = string.Empty;
                    }
                    if (can_insert_no_term)//Se esta generando un NO TERMINAL.
                    {
                        if (c != '<')
                            no_term += c;
                    }
                    else //Si no se esta generando puede ser un terminal o bien un Epsilon.
                    {
                        if (c != '>')
                        {
                            symbol = new C_Symbol();
                            if (c == '~')
                                symbol.Type_symbol = 2;                            
                            else//Es un simbolo  terminal.                                
                                symbol.Type_symbol = 0;
                            symbol.Symbol = c.ToString();                            
                            new_production.Right.Add(symbol);
                        }
                    }
                }
                this.grammar.Add(new_production);
            }
        }



        /// <summary>
        /// Agrega una produccion a la gramatica a partir de texto plano.
        /// </summary>
        /// <param name="left">No terminal de la produccion, PARTE izquierda</param>
        /// <param name="right">Parte derecha  de la produccion</param>
        public void add_prodution_from_source(string left, string right) {
            C_Production new_production;      // Nueva produccion a insertar
            C_Symbol symbol;                 // simbolo auxiliar, se esta generando cada vez que se encuentra un simbolo del lenguaje.
            string no_term = string.Empty;  //Cadena que conforma el NO TERMINAL.
            bool is_no_term; //Bandera para saber si el simbolo es NO TERMINAL.
            string[] productions = right.Split('|'); //Para poder generar las producciones a partir del simbolo OR, si es que lo tuviese.
            string[] sub_tokens; //Obtiene cada uno de los simbolos de la produccion.


            foreach (string sub_production in productions) {
                new_production = new C_Production(left);    
                sub_tokens = Regex.Split(sub_production, "\\s"); // Obtenemos cada simbolo del lenguaje 
                foreach (string a_symbol in sub_tokens) { //Se recorre cada una los simbolos obtenidos para esta produccion.
                    if (string.Compare(a_symbol, string.Empty) != 0) { //Solo se agregan simbolos obviamente, si no son cadenas vacias.
                        symbol = new C_Symbol(a_symbol);
                        //Verificamos si es un simbolo no terminal o un epsilon.
                        is_no_term = this.is_a_NON_TERMINAL(a_symbol); //Primero se averigua si el simbolo Es un NO TERMINAL.
                        if (is_no_term)
                            symbol.Type_symbol = 1; //El simbolo es un No_Terminal
                        else
                        {
                            if (string.Compare(a_symbol, "~") == 0) //El simbolo es un epsilon.
                                symbol.Type_symbol = 2; //El simbolo es epsilon
                            else
                                symbol.Type_symbol = 0;//El simbolo es un Terminal
                        }
                        new_production.Right.Add(symbol);
                    }
                }
                this.grammar.Add(new_production);
            }
        }


        /// <summary>
        /// Determina si una cadena es un NO TERMINAL
        /// </summary>
        /// <param name="simple_symbol">Cadena que se esta Evaluando</param>
        /// <returns>TRUE si es un No Terminal</returns>
        private bool is_a_NON_TERMINAL(string simple_symbol) {
            return this.No_terminals.Contains(simple_symbol);
        }


        /// <summary>
        /// Obtiene las producciones correspondientes a un NO TERMINAL.
        /// </summary>
        /// <param name="no_term">NO terminal del cual se estan buscando las producciones. PARTE IZQUIERDA</param>
        /// <returns>Las lista de producciones ue inician con un determinado NO TERMINAL</returns>
        public List<C_Production> get_Productions(String no_term) {
            List<C_Production> list_productions = new List<C_Production>();
            string name_production = string.Empty;

            foreach (C_Production production in this.grammar) {
                name_production = production.Producer;
                if (string.Compare(name_production, no_term) == 0)
                    list_productions.Add(production);                
            }
            return list_productions;
        }


        /// <summary>
        /// Extiende la gramatica para tener un punto comun de inicio.
        /// Hace <S' -> S
        /// </summary>
        public string extend_grammar() {
            string start_symbol; 
            C_Production nw_production;

            start_symbol = this.grammar[0].Producer;//Obtenemos el simbolo de inicio
            nw_production = new C_Production(start_symbol + "'");
            nw_production.Right.Add(new C_Symbol(start_symbol, 1));
            this.grammar.Insert(0, nw_production);

            return start_symbol = "." + start_symbol;
        }
    }
}