﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.LR1_Stuffs.First_Stuffs;
using WpfApp1.Grammar_Stuffs;
namespace WpfApp1.LR1_Stuffs
{
    public class C_LR1
    {
        /// <summary>
        /// Conjunto primero de la gramatica que se esta analizando.
        /// </summary>
        private C_First_Set first_set;
        /// <summary>
        /// Gramatica con la que se esta generando el automata.
        /// </summary+>
        private C_Grammar grammar;

        /// <summary>
        /// Cola de IR_A, cada vez que se genera un nuevo elemento en cerradura se encola.
        /// </summary>
        Queue<C_Go_to> go_tos;
        /// <summary>
        /// Lista de elementos LR1 que contiene todos los estados del automata.
        /// </summary>
        List<C_LR1_Element> list_states;
        int num_state; 

        private List<C_Closure_Element> closure_elements_tmp;

        public C_LR1(C_Grammar g)
        {
            this.first_set = new C_First_Set();
            this.go_tos = new Queue<C_Go_to>();
            this.list_states = new List<C_LR1_Element>();
            this.grammar = g;
            this.num_state = 0;
        }        

        /// <summary>
        /// Obtiene o Establece el conjunto Primero relativo a la gramatica que se esta analizando.
        /// </summary>
        internal C_First_Set First_set { get => first_set; set => first_set = value; }


        /// <summary>
        /// Inicializa los conjuntos primero con sus respectivos nombres.
        /// </summary>
        /// <param name="seed"></param>
        public void add_seeds_Sets(List<string> seed)
        {
            foreach (string name_set in seed) { this.first_set.add_seed_first_set(name_set); }
        }

        
        /// <summary>
        /// Empieza la creacion del automata del LR1
        /// </summary>
        /// <param name="gram">Gramatica con la que se genera el automata.</param>
        public void generates_LR1_Automate() {
            this.grammar.extend_grammar(); //Se hace la gramatica extendida.}
            //Inicia el analisis del estado 0.
            creates_zero_state();

            while (this.go_tos.Count > 0) {
                C_Go_to tmp_go_to = this.go_tos.Dequeue();
                C_LR1_Element a_lr1_element = search_state(tmp_go_to.State);
                List<C_Closure_Element> list = a_lr1_element.generates_new_Kernel(tmp_go_to.Symbol_state);

                for(int i = 0; i < list.Count; i++){
                    list[i].Production.swap_point();
                }
                list_states.Add(this.generate_new_state(list, tmp_go_to.State));
            }               
        }



        /// <summary>
        /// Genera el conjunto Primero, a partir de la gramatica con la que se esta trabajando.
        /// </summary>
        /// <param name="gram">Gramatica con la que se genera primeros</param>
        public void generate_first_set_to_LR()
        {
            C_First_Element first_set;
            bool complete_first_set = false;
            string first_symbol = string.Empty;
            List<C_Production> productions_to_Analize;
            List<int> length_of_sets = new List<int>();
            /*Indice del elemento de conjunto Primero*/
            int index_OF_current_set_of_first;


            while (!complete_first_set)
            {
                /*  Recorrido sobre la lista de los simbolos no Terminales contenidos en esta Gramatica
                 *  Los simbolos Terminales han sido preseleccionados en una lista contenida en esta clase. 
                 *  Esto porque puede suceder que exista mas de una produccion que contenga el mismo no terminal.
                 */
                foreach (string antecedent in this.grammar.No_terminals1)
                {
                    /*Se obtiene las producciones que que corresponden al no terminal que estamos analizando*/
                    productions_to_Analize = this.grammar.get_Productions(antecedent);
                    /*Obtenemos el indice del conjunto PRIMERO del No Terminal al que se le tienen que agregar simbolos.*/
                    index_OF_current_set_of_first = this.first_set.get_index_of_first_element(antecedent);
                    foreach (C_Production production_to_analize in productions_to_Analize) {
                        first_set = this.get_first_simple_set(production_to_analize.Right);
                        /*Se  insertan los simbolos en la tabla de primeros y en el */
                        foreach (string simple_simbol in first_set.First) {
                            this.first_set.First_set[index_OF_current_set_of_first].add_symbol(simple_simbol);
                        }
                    }
                }
                if (this.first_set.match_length_of_Sets(length_of_sets) == true)
                    complete_first_set = true;
                else {
                    length_of_sets.Clear();
                    foreach (C_First_Element an_element in this.first_set.First_set) {
                        length_of_sets.Add(an_element.First.Count);
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="simbols_to_get_first">Es una lista de simbolos con la que se determinara el conjunto primero</param>
        /// <returns></returns>
        public C_First_Element get_first_simple_set(List<C_Symbol> simbols_to_get_first)
        {
            C_First_Element new_first_Set = new C_First_Element();

            bool have_epsilon = false;
            int num_simbols = simbols_to_get_first.Count;
            bool we_can_brake_travel = false;
            /*Recorrido sobre cada uno de los simbolos*/
            for (int index_of_symbol = 0; index_of_symbol < num_simbols && !we_can_brake_travel; index_of_symbol++)
            {
                C_Symbol consequent = simbols_to_get_first[index_of_symbol];
                /*Inicia analisis para el calculo de Primero*/
                switch (consequent.Type_symbol)
                {
                    /*el simbolo es un terminal, o es un Epsilon*. Es una agregacion directa.*/
                    case 0:
                    case 2:                       
                        new_first_Set.add_symbol(consequent.Symbol);
                        we_can_brake_travel = true;
                        break;
                    default:
                        int index_first_element_to_search;

                        /*INDICE del conjunto Primero del no terminal encontrado en el analisis de los simbolos*/
                        index_first_element_to_search = first_set.get_index_of_first_element(consequent.Symbol);
                        have_epsilon = false;
                        /*Recorrido de todos los simbolos de Primero  de este conjunto.*/
                        foreach (string simple_symbol in first_set.First_set[index_first_element_to_search].First) {
                            /*Si el simbolo simplemente no es un terminal se agrega.*/
                            if (string.Compare(simple_symbol, "~") != 0)                            
                                new_first_Set.add_symbol(simple_symbol);                            
                            else
                                have_epsilon = true;                            
                        }
                        /*Se determina si el simbolo es anulable o no.*/
                        we_can_brake_travel = (have_epsilon) ? false : true;
                        break;
                }
                /*Se averigua si se esta en el ultimo simbolo y si este contiene epsilon, de ser asi se agrega epsilon al conjunto de PRIMERO*/
                if (index_of_symbol == (num_simbols - 1) && have_epsilon)                 
                    new_first_Set.add_symbol("~");
            }
            return new_first_Set;
        }


        /// <summary>
        /// Le llegan chingaderas como esta:
        /// {[S' -> .S, $]}
        /// {[S -> C.C, $]}
        /// {[F -> (.E ), $/+/-/*]}
        /// Que basicamente es un elemento de Cerradura.
        /// </summary>
        /// <param name="kernel">Cerradura de donde se podrian derivar mas estados </param>
        private void generate_Closure(C_Symbol symbol, List<string> forward_search_symbols) {
            List<C_Production> productions = this.grammar.get_Productions(symbol.Symbol);
            C_Production tmp_production;
            C_Closure_Element new_closure_Element;            

            for (int i = 0; i < productions.Count; i++) {

                C_Go_to new_go_to;

                tmp_production = productions[i]; //Aqui solamente se obtiene la produccion totalmente Virgen, es decir que no tiene punto
                new_closure_Element = this.creates_NUCLEAR_LR0_element(tmp_production, forward_search_symbols);//Generamos el nuevo elemento de Cerradura LR0 a apartir de la produccion correspondiente.                              

                C_Symbol tmp_symbol = new_closure_Element.Production.get_symbol_next_to_DOT();

                if (can_insert_closure_element(new_closure_Element) == true)
                {
                    closure_elements_tmp.Add(new_closure_Element);
                    new_go_to = new C_Go_to(this.num_state, tmp_symbol); //Generacion de un nuevo IR_A
                    this.go_tos.Enqueue(new_go_to);
                    if (tmp_symbol != null) { //Si se encontro algun simbolo
                        if (tmp_symbol.Type_symbol == 1) { //Si el simbolo es NO TERMINAL entonces genera cerradura.                 
                            generate_Closure(tmp_symbol, get_first_simple_set(cadenaalfa(new_closure_Element.Production, forward_search_symbols)).First);
                        }
                    }
                }                                                               
            }
        }

        
        /// <summary>
        /// AGREGA COMENTARIOS A ESTO WEEEEEEE!!!
        /// </summary>
        /// <param name="cerradura"></param>
        /// <param name="forward_search_symbols"></param>
        /// <returns></returns>
        private List<C_Symbol> cadenaalfa(C_Production cerradura, List<string> forward_search_symbols)
        {
            List<C_Symbol> aux2 = new List<C_Symbol>();
            List<C_Symbol> aux = new List<C_Symbol>();
            int index = cerradura.index_DOT();
            if (cerradura.Get_Right().Count > index)
            {
                for(int i=index;i< cerradura.Get_Right().Count;i++)
                {
                    if (i != index + 1 && i!=index)
                    {
                        aux.Add(cerradura.Get_Right()[i]);
                    }
                }
            }
            for(int j = 0; j < forward_search_symbols.Count; j++)
            {
                C_Symbol a = new C_Symbol(forward_search_symbols[j], 0);
                aux2.Add(a);
            }
            for(int k = 0; k < aux.Count; k++)
            {
                aux2.Add(aux[k]);
            }
            return aux2;
        }

        /// <summary>
        /// Genera un nuevo estado del AFD
        /// </summary>
        /// <param name="list_kernels"></param>
        /// <param name="current_state"></param>
        /// <returns></returns>
        private C_LR1_Element generate_new_state(List<C_Closure_Element> list_kernels, int current_state)
        {
            C_Go_to new_go_to;
            C_LR1_Element new_state;

            this.closure_elements_tmp = new List<C_Closure_Element>();
           // this.closure_elements_tmp.Clear();
            if (this.num_state == 0)  //OBVIAMENTE SE HACE LA CERRADURA xD
            {
                this.closure_elements_tmp.Insert(0, list_kernels[0]);
                new_go_to = new C_Go_to(current_state, list_kernels[0].Production.get_symbol_next_to_DOT()); //Generacion de un nuevo IR_A
                this.go_tos.Enqueue(new_go_to);
                this.generate_Closure(list_kernels[0].Production.get_symbol_next_to_DOT(), list_kernels[0].Forward_search_symbols);
            }
            else
            {                
                foreach (C_Closure_Element kernel in list_kernels)
                {
                    this.closure_elements_tmp.Add(kernel);
                    switch (this.what_generates(kernel))
                    {
                        case 0: /// <TERMINAL GENERA GO_TO  y se agrega  a la lista de cerraduras></TERMINAL>
                            //if (this.can_insert_closure_element(kernel) == true) {
                            new_go_to = new C_Go_to(current_state, kernel.Production.get_symbol_next_to_DOT()); //Generacion de un nuevo IR_A
                            this.go_tos.Enqueue(new_go_to);
                            //this.closure_elements_tmp.Add(kernel);
                            //}
                            break;
                        case 1:///<NOT_TERMINAL GENERA CERRADURA></NOT_TERMINAL>>
                            C_Production production_kernel = kernel.Production;
                            int index_dot;

                            //this.closure_elements_tmp.Insert(0, kernel);
                            new_go_to = new C_Go_to(current_state, list_kernels[0].Production.get_symbol_next_to_DOT()); //Generacion de un nuevo IR_A
                            this.go_tos.Enqueue(new_go_to);
                            index_dot = production_kernel.index_DOT();
                            this.generate_Closure(production_kernel.Right[index_dot + 1], kernel.Forward_search_symbols);
                            break;
                        case 2:///<EPSILON Solo genera closure_element pero con solo el punto></EPSILON>
                            break;
                    }
                }
            }
            new_state = new C_LR1_Element(closure_elements_tmp, this.num_state, list_kernels);
            this.num_state++;
            return new_state;      
        }


        /// <summary>
        /// Obtiene el tipo de accion que ejecutara este kernel dependiendo de la posicion del marcador de Analisis.
        /// </summary>
        /// <param name="a_kernel">Elemento de cerradura que se esta analizando</param>
        /// <returns>1 Si este kernel puede generar Cerradura, 2 S si solo es una transicion, 3 si el marcador de analisis se encuentra al final de la produccion</returns>
        private int what_generates(C_Closure_Element a_kernel) {
            C_Production production; //Produccion que esta contenida en el KERNEL
            List<C_Production> productions_to_Analize;
            int index_dot; //Indice del punto en las producciones;

            /*Se inicia con la evaluacion del KERNEL*/
            production = a_kernel.Production; //Obtenemos la produccion que se encuentra dentro del Kerlen [S'->.S]
            index_dot = a_kernel.Production.index_DOT(); //Obtenemos el indice del marcador de analisis(MARCADOR de analisis)
            if (index_dot < (a_kernel.Production.Right.Count - 1))
            { //Verificamos si no se encuentra al final de la produccion, es decir no ha analizado ya todo.            
                C_Symbol tmp_symbol;

                tmp_symbol = a_kernel.Production.Right[index_dot + 1];
                /*Si el siguiente simbolo es un elemento no Terminal Entonces se genera la cerradura.*/
                ///<TYPE_SYMBOL>0 = TERMINAL; 1 = NO_TERMINAL ; 2 = P </TYPE_SYMBOL>
                return tmp_symbol.Type_symbol; 

                if (tmp_symbol.Type_symbol == 2)
                    return 1; //Genera Cerradura
                else
                    return 2;//Solo genera transicion, es un simbolo TERMINAL.
            }
            return 3;//No hace nada.

        }


        /// <summary>
        /// Empezamos a generar el Primero Kernel el cual no servira para iniciar la creacion del AFD
        /// </summary>
        /// <returns></returns>
        private void creates_zero_state() {
            
            C_Closure_Element kernel_0;
            List<string> forward_search_search_simbols;
            List<C_Closure_Element> dummy_list;

            dummy_list = new List<C_Closure_Element>();
            forward_search_search_simbols = new List<string>();
            forward_search_search_simbols.Add("$");
            kernel_0 = this.creates_NUCLEAR_LR0_element(this.grammar.Get_Grammar()[0], forward_search_search_simbols);
            dummy_list.Add(kernel_0);
            this.list_states.Add(this.generate_new_state(dummy_list, 0)); //Generacion del Estado CERO del automata.
        }
        /// <summary>
        /// Busca estado del goto en la lista de estados
        /// </summary>
        /// <param name="g">estado de entrada</param>
        /// <returns>regresa el elemento lr1 correspondiente</returns>
        private C_LR1_Element search_state(int g)
        {
            for(int i = 0; i < list_states.Count; i++)
            {
                if (list_states[i].Num_state == g)
                {
                    return (list_states[i]);
                    
                }
            }
            return null;
        }


        /// <summary>
        /// Genera un nuevo elemento de cerrradura LR(0) a partir de una produccion.
        /// </summary>
        /// <param name="just_aProdcution">Produccion con la que se esta generand el elemento de cerradura</param>
        /// <param name="new_forward_search_simbols">Simbolos de busqueda hacia adelante para este elemento de cerradura</param>
        /// <returns></returns>
        private C_Closure_Element creates_NUCLEAR_LR0_element(C_Production just_aProdcution, List<string>new_forward_search_simbols) {
            C_Production nw_production;
            C_Closure_Element nuclear_element;

            nw_production = new C_Production(just_aProdcution.Producer);
            nw_production.Right.Add(new C_Symbol(".", 3));//se insertan dos puntos 
            foreach (C_Symbol simple_symbol in just_aProdcution.Right) {
                nw_production.Right.Add(simple_symbol);
            }
            nuclear_element = new C_Closure_Element(nw_production);
            foreach (string just_str in new_forward_search_simbols) {
                nuclear_element.Forward_search_symbols.Add(just_str);
            }

            return nuclear_element;
        }


        /// <summary>
        /// Determina si un elemento de cerradura se puede inserta en la lista temporal de elementos de cerradura, ya que estos deben de ser irrepetibles 
        /// Supongamos que tenemos {[S' -> .S, {$}]; [S -> .C C, {$}]; [C -> .c C,{c,d}]}
        /// Y viene a insertarse S -> .C C, {$}, Esto no es posible ya que ya existe en  la lista temporal de elementos de cerradura.
        /// </summary>
        /// <param name="initial_kernel">Elemento de Cerradura que se desea insertar</param>
        /// <returns>True si el elemento de cerradura se puede insertar en la lista temporal de elementos de cerradura.</returns>
        private bool can_insert_closure_element(C_Closure_Element initial_kernel)
        {
            bool can_insert = true;

            if (this.closure_elements_tmp.Count > 0)
            {
                C_Production production_in_list;
                C_Production production_in_kernel;

                production_in_kernel = initial_kernel.Production;
                for (int index = 0; index < this.closure_elements_tmp.Count; index++)
                {
                    production_in_list = this.closure_elements_tmp[index].Production;
                    if (string.Compare(production_in_kernel.Producer, production_in_list.Producer) == 0)
                    { // Si ambos tienen la misma parte derecha
                        if (production_in_kernel.Right.Count == production_in_list.Right.Count)
                        {         // Si ambos tienen la misma longitud en su parte derecha, de la produccion.
                            int i;

                            for (i = 0; i < production_in_kernel.Right.Count; i++) { //Inciamos el recorrido para analizar cada simbolo.
                                if (string.Compare(production_in_kernel.Right[i].Symbol, production_in_list.Right[i].Symbol) != 0)
                                    break;
                            }
                            if (i == production_in_kernel.Right.Count) {
                                can_insert = false; //No se puede insertar.
                                break;
                            }
                        }
                    }
                }
            }
            return can_insert;
        }
    }
}