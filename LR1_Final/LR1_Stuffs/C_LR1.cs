using System;
using LR1_Final.Grammar_Stuffs;
using LR1_Final.LR1_Stuffs.First_Stuffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1_Final.LR1_Stuffs
{
    class C_LR1
    {
        /// <summary>
        /// Conjunto primero de la gramatica que se esta analizando.
        /// </summary>
        private C_First_Set first_set;
        /// <summary>
        /// Gramatica con la que se esta generando el automata.
        /// </summary>
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
        internal List<C_LR1_Element> List_states { get => list_states; set => list_states = value; }


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
        public void generates_LR1_Automate()
        {
            this.grammar.extend_grammar(); //Se hace la gramatica extendida.}
            //Inicia el analisis del estado 0.
            creates_zero_state();

            C_LR1_Element new_LR1_Element;

            while (this.go_tos.Count > 0)
            {//checar 
                C_Go_to tmp_go_to = this.go_tos.Dequeue();
                C_LR1_Element a_lr1_element = search_state(tmp_go_to.State);
                List<C_Closure_Element> list = a_lr1_element.generates_new_Kernel(tmp_go_to.Symbol_state);

                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Production.swap_point();
                }

                new_LR1_Element = this.create_new_state(list, tmp_go_to.State, tmp_go_to);//this.generate_new_state(list, tmp_go_to.State, tmp_go_to);
                list_states.Add(new_LR1_Element);
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
                    foreach (C_Production production_to_analize in productions_to_Analize)
                    {
                        first_set = this.get_first_simple_set(production_to_analize.Right);
                        /*Se  insertan los simbolos en la tabla de primeros y en el */
                        foreach (string simple_simbol in first_set.First)
                        {
                            this.first_set.First_set[index_OF_current_set_of_first].add_symbol(simple_simbol);
                        }
                    }
                }
                if (this.first_set.match_length_of_Sets(length_of_sets) == true)
                    complete_first_set = true;
                else
                {
                    length_of_sets.Clear();
                    foreach (C_First_Element an_element in this.first_set.First_set)
                    {
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
                        foreach (string simple_symbol in first_set.First_set[index_first_element_to_search].First)
                        {
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


        private void do_expansion(string producer_from, C_Symbol expansion_symbol, List<C_Symbol> gamma, List<string> look_up_symbols)
        {
            List<C_Production> productions = this.grammar.get_Productions(expansion_symbol.Symbol); //Producciones que son producidas por expansion_Symbol.v
            C_Closure_Element new_LR1_Element;
            C_Production tmp_production;
            List<string> first_OF_gamma_alfa;
            int index_element_in_expansion;

            for (int i = 0; i < productions.Count; i++) {
                tmp_production = productions[i];
                new_LR1_Element = this.creates_LR1_Element_no_look_Up_Symbols(tmp_production);//Creacion de elemento LR1 pero sin simbolos de busqueda hacia adelante.
                index_element_in_expansion = this.exist_LR1_element_IN_Expansion(new_LR1_Element);
                first_OF_gamma_alfa = this.calculate_First_gamma_alfa(gamma, look_up_symbols);//Calculo de simbolos de busqueda hacia adelante.
                if (index_element_in_expansion == -1) { //El nuevo elemento LR1, no existe en la expansion.                    
                    C_Symbol nw_expansion_symbol; 
                    
                    new_LR1_Element.add_look_up_symbols(first_OF_gamma_alfa); //Agregamos dichos simbolos al nuevo elemento LR1
                    if (string.Compare(producer_from, new_LR1_Element.Production.Producer) == 0)
                        new_LR1_Element.append_look_up_symbols(look_up_symbols);
                    this.closure_elements_tmp.Add(new_LR1_Element);
                    nw_expansion_symbol = new_LR1_Element.Production.get_symbol_next_to_DOT();  //Obtenemos el siguiente simbolo que podria seguir generando Expansion
                    if (nw_expansion_symbol != null) {
                        if(nw_expansion_symbol.Type_symbol == 1)
                        {
                            List<C_Symbol> nw_gamma;
                            nw_gamma = new_LR1_Element.Production.get_gamma();
                            this.do_expansion(new_LR1_Element.Production.Producer, nw_expansion_symbol, nw_gamma, new_LR1_Element.Forward_search_symbols);
                        }
                    }
                }
                else {
                    this.closure_elements_tmp[index_element_in_expansion].append_look_up_symbols(first_OF_gamma_alfa);
                }
                    
            }
        }


        /// <summary>
        /// Realiza el calculo de Primero(Gamma-Alfa)
        /// </summary>

        /// <param name="gamma">Si gamma es null se retorna directamente Alfa</param>
        /// <param name="a">Lista de cadenas que representa cada simbolo de Alfa</param>
        /// <returns>El calculo realizado si, Gamma != null, de lo contrario se retorna Alfa</returns>
        private List<string> calculate_First_gamma_alfa(List<C_Symbol>gamma, List<string>a) {
            List<List<C_Symbol>> main_list; //Lista de listas para contener las diversas concatenaciones que pudiesen existir.
            List<string> first_gamma_alfa; //Lista donde se acumularan todos los primero calculados.
            List<C_Symbol> list;//Lista donde se generan almacenan las concatenaciones de los simbolos.
            C_First_Element tmp_first_element;// Aqui se guarda el conjunto primero final....Final.

            first_gamma_alfa = new List<string>();
            main_list = new List<List<C_Symbol>>();
            //<<<<<PARTE CONCATENACION>>>>>>>>>>
            if (gamma != null)

            {
                foreach (string s in a)
                {
                    list = new List<C_Symbol>(gamma);
                    list.Add(new C_Symbol(s, 0));
                    main_list.Add(list);
                }
            }
            else            
                return a;           
             first_gamma_alfa = new List<string>();
            foreach (List<C_Symbol>symb_list in main_list) 
                first_gamma_alfa.AddRange(this.get_first_simple_set(symb_list).First);//Get first simple regresa una lista             
            tmp_first_element = new C_First_Element();
            foreach (string s in first_gamma_alfa) 
                tmp_first_element.add_symbol(s);           
            return tmp_first_element.First;

        }


        /// <summary>
        /// Este es el nuevo METODO VERGA!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="list_kernels"></param>
        /// <param name="current_state"></param>
        /// <param name="state_go_to"></param>
        /// <returns></returns>
        private C_LR1_Element create_new_state(List<C_Closure_Element> list_kernels, int current_state, C_Go_to state_go_to)
        {
            C_Go_to new_go_to = new C_Go_to();
            C_LR1_Element new_state = new C_LR1_Element();
           bool is_repeated_state = false;

            this.closure_elements_tmp = new List<C_Closure_Element>();

            if (this.num_state == 0) { //ES EL ESTADO 0. AQUI obviamente se hace todo bien y bonito
                this.closure_elements_tmp.Insert(0, list_kernels[0]);//Se inserta sin pedos el primero elemento de la cerradura.
                this.do_expansion(list_kernels[0].Production.Producer , list_kernels[0].Production.get_symbol_next_to_DOT(), list_kernels[0].Production.get_gamma(), list_kernels[0].Forward_search_symbols);
                new_state = new C_LR1_Element(closure_elements_tmp, this.num_state, list_kernels, state_go_to);
            }
            else {
                int state_repeated = this.exist_kernel(list_kernels);

                if (state_repeated != -1) { /*Si state_repeated = 1, indica que el estado no esta repetido*/
                    C_LR1_Element lr1_element_tmp = list_states[state_repeated];
                    new_state = new C_LR1_Element(closure_elements_tmp, lr1_element_tmp.Num_state, list_kernels, state_go_to);
                    is_repeated_state = true;
                } else {// Si el estado no esta Repetido hace Cosas.
                    foreach (C_Closure_Element kernel in list_kernels)
                    {
                        this.closure_elements_tmp.Add(kernel);

                        int action = this.what_generates(kernel);

                        switch (action)
                        {
                            case 0: /*es un TERMINAL*/
                                    ///<TERMINAL GENERA GO_TO  y se agrega  a la lista de cerraduras></TERMINAL>       
                                break;
                            case 1:///<NOT_TERMINAL GENERA EXPANSION></NOT_TERMINAL>>
                                //C_Symbol simbol_generates_closure = kernel.Production.get_symbol_next_to_DOT();

                                //this.generate_Closure(simbol_generates_closure, kernel.Forward_search_symbols);
                                this.do_expansion(kernel.Production.Producer, kernel.Production.get_symbol_next_to_DOT(), kernel.Production.get_gamma(), kernel.Forward_search_symbols);
                                break;
                            case 2:///<EPSILON Solo genera closure_element pero con solo el punto></EPSILON>                               
                                break;
                        }
                    }                   
                    new_state = new C_LR1_Element(closure_elements_tmp, this.num_state, list_kernels, state_go_to);                    
                }                                
            }           
            if (!is_repeated_state) {//Si el estado no es un estado repetido, entonces podemos prosegu  ir con la creacion e insercion de los IR_A                              
                this.create_and_insert_go_to_S();
                this.num_state++;
            }
            return new_state;
        }


        /// <summary>
        /// Genera los nuevos estados IR_A y los inserta en la Cola.
        /// </summary>
        private void create_and_insert_go_to_S() {
            C_Go_to nw_go_to;
            
            foreach (C_Closure_Element cl_element in this.closure_elements_tmp) {
                C_Symbol a_symb = cl_element.Production.get_symbol_next_to_DOT();

                if (a_symb != null) {
                    nw_go_to = new C_Go_to(this.num_state, a_symb);
                    if (this.go_to_is_in_queue(nw_go_to) == false)
                        this.go_tos.Enqueue(nw_go_to);
                }
            }
        }


        /// <summary>
        /// Obtiene el tipo de accion que ejecutara este kernel dependiendo de la posicion del marcador de Analisis.
        /// </summary>
        /// <param name="a_kernel">Elemento de cerradura que se esta analizando</param>
        /// <returns>1 Si este kernel puede generar Cerradura, 2 S si solo es una transicion, 3 si el marcador de analisis se encuentra al final de la produccion</returns>
        private int what_generates(C_Closure_Element a_kernel)
        {
            C_Production production; //Produccion que esta contenida en el KERNEL
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
            }
            return 3;//No hace nada.

        }



        /// <summary>
        /// Empezamos a generar el Primero Kernel el cual no servira para iniciar la creacion del AFD
        /// </summary>
        /// <returns></returns>
        private void creates_zero_state()
        {
               
            C_Closure_Element kernel_0;
            List<string> forward_search_search_simbols;
            List<C_Closure_Element> dummy_list;

            dummy_list = new List<C_Closure_Element>();
            forward_search_search_simbols = new List<string>();
            forward_search_search_simbols.Add("$");
            kernel_0 = this.creates_NUCLEAR_LR0_element(this.grammar.Get_Grammar()[0], forward_search_search_simbols);
            dummy_list.Add(kernel_0);
            //this.list_states.Add(this.generate_new_state(dummy_list, 0, new C_Go_to())); //Generacion del Estado CERO del automata.
            this.list_states.Add(this.create_new_state(dummy_list, 0, new C_Go_to())); //Generacion del Estado CERO del automata.
        }


        /// <summary>
        /// Busca estado del goto en la lista de estados
        /// </summary>
        /// <param name="g">estado de entrada</param>
        /// <returns>regresa el elemento lr1 correspondiente</returns>
        private C_LR1_Element search_state(int g)
        {
            for (int i = 0; i < list_states.Count; i++)
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
        /// <param name="just_aProduction">Produccion con la que se esta generand el elemento de cerradura</param>
        /// <param name="new_forward_search_simbols">Simbolos de busqueda hacia adelante para este elemento de cerradura</param>
        /// <returns></returns>
        private C_Closure_Element creates_NUCLEAR_LR0_element(C_Production just_aProduction, List<string> new_forward_search_simbols) {
            C_Production nw_production;
            C_Closure_Element nuclear_element;

            nw_production = new C_Production(just_aProduction.Producer);
            nw_production.Right.Add(new C_Symbol(".", 3));//se insertan dos puntos 
            foreach (C_Symbol simple_symbol in just_aProduction.Right) {
                nw_production.Right.Add(simple_symbol);
            }
            nuclear_element = new C_Closure_Element(nw_production);
            foreach (string just_str in new_forward_search_simbols) {
                nuclear_element.Forward_search_symbols.Add(just_str);
            }

            return nuclear_element;
        }



        private C_Closure_Element creates_LR1_Element_no_look_Up_Symbols(C_Production production) {
            C_Production nw_production;
            C_Closure_Element nw_closure_element;

            nw_production = new C_Production(production.Producer);
            nw_production.Right.Add(new C_Symbol(".", 3));//se insertan dos puntos 

            foreach (C_Symbol symbol in production.Right) {
                nw_production.Right.Add(symbol);
            }
            nw_closure_element = new C_Closure_Element(nw_production);

            return nw_closure_element;
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

                            for (i = 0; i < production_in_kernel.Right.Count; i++)
                            { //Inciamos el recorrido para analizar cada simbolo.
                                if (string.Compare(production_in_kernel.Right[i].Symbol, production_in_list.Right[i].Symbol) != 0)
                                    break;
                            }
                            if (i == production_in_kernel.Right.Count)
                            {
                                can_insert = false; //No se puede insertar.
                                break;
                            }
                        }
                    }
                }
            }
            return can_insert;
        }

        

        /// <summary>
        /// Determina si un KERNEL existe en algun LR1_Element(ESTADO), del AFD. 
        /// </summary>
        /// <param name="list_kernels">KERNEL(Lista de Closure_Element)</param>
        /// <returns>El indice del LR1 element que contiene el mismo KERNEL. -1 si no existe este kernel.</returns>
        private int exist_kernel(List<C_Closure_Element> list_kernels)
        {
            int index;
            int lenght_lr1_states = this.list_states.Count;

            for (index = 0; index < lenght_lr1_states; index++) {//Recorrido de cada uno de los LR1 elements que estan en el AFD.
                if (list_states[index].kernel_Exist(list_kernels) == true)
                    break;
            }
            if (index == lenght_lr1_states)
                return -1;
            return index;
        }


        private int exist_LR1_element_IN_Expansion(C_Closure_Element lr1_element_incoming) {
            int index;
            int lenght_elements_in_expansion = this.closure_elements_tmp.Count;
            C_Closure_Element closure_element_in_Expansion;

            for (  index = 0; index < lenght_elements_in_expansion; index++) {
                closure_element_in_Expansion = this.closure_elements_tmp[index];
                if (closure_element_in_Expansion.Production.is_equal_to_Other_C_Production(lr1_element_incoming.Production) == true)//Determinamos si el Elemento LR1 esta dentro de la expansion, esto comparando las producciones de ambos
                    break;
            }
            if (index == lenght_elements_in_expansion)
                return -1;//No se encontro ningun elemento en la expansion
            return index;//Retornamos el indice del elemento dentro de la expansion que es igual.
        }


        /// <summary>
        /// Verifica si un IR_A ya existe en la cola.
        /// </summary>
        /// <param name="a_go_to">Go_To que se desea saber si ya existe</param>
        /// <returns>true si el elemento Go_To ya existe</returns>
        private bool go_to_is_in_queue(C_Go_to a_go_to) {
            Queue<C_Go_to> tmp_queue = new Queue<C_Go_to>(this.go_tos);
            C_Go_to tmp_go_to; 

            while(tmp_queue.Count > 0) {
                tmp_go_to = tmp_queue.Dequeue();
                if (tmp_go_to.this_Goto_EXIST(a_go_to))
                    return true;
            }
            return false;
        }
    }
}
