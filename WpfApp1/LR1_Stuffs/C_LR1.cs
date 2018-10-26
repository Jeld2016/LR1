using System;
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
        /// Cola de IR_A, cada vez que se genera un nuevo elemento en cerradura se encola.
        /// </summary>
        Queue<C_Go_to> go_tos;
        /// <summary>
        /// Lista de elementos LR1 que contiene todos los estados del automata.
        /// </summary>
        List<C_LR1_Element> list_states;
        

        public C_LR1()
        {
            this.first_set = new C_First_Set();
            this.go_tos = new Queue<C_Go_to>();
            this.list_states = new List<C_LR1_Element>();
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
        /// Genera el conjunto primero de una gramatica dada.
        /// </summary>
        /// <param name="gram">Gramatica con la que calcularemos los conjuntos Primero</param>
        public void generates_first(C_Grammar gram)
        {
            C_First_Set prev_state_of_FIRST = new C_First_Set();
            C_First_Set current_state_of_FIRST = new C_First_Set(); //Conjunto primero auxiliar con el que guardaremos el estado actual, del calculo de primero.
            bool complete_first_set = false;
            string first_symbol = string.Empty;
            List<C_Production> productions_to_Analize;
            List<int> length_of_sets = new List<int>();
            int index_OF_current_set_of_first;
            bool we_can_brake_travel = false;

            prev_state_of_FIRST.add_seed_first_set(gram.No_terminals1);
            current_state_of_FIRST.add_seed_first_set(gram.No_terminals1);
            while (!complete_first_set)
            {
                foreach (string antecedent in gram.No_terminals1) //Hacemos recorrido por cada uno de los No TERMINALES de esta gramatica.
                {
                    productions_to_Analize = gram.get_Productions(antecedent); //Obtenemos las producciones correspondientes al no terminal que se esta analizando.
                    index_OF_current_set_of_first = this.first_set.get_index_of_first_element(antecedent);  //Obtenemos el indice del conjunto PRIMERO del No Terminal al que se le tienen que agregar simbolos.
                    bool have_epsilon;
                    foreach (C_Production production_to_analize in productions_to_Analize) { //Analizamos cada una de las producciones correspondientes al NO TERMINAL que se esta analizando.
                        int max_index_list_of_symbols = production_to_analize.Right.Count; //Obtenemos la cantidad de simbolos que tiene la Produccion que estamos analizando
                        we_can_brake_travel = false;
                        have_epsilon = false;
                        for (int index_of_symbol = 0; index_of_symbol < max_index_list_of_symbols && !we_can_brake_travel; index_of_symbol++) { //Analizamos cada uno de los simbolos de la produccion que se esta analizando
                            C_Symbol consequent = production_to_analize.Right[index_of_symbol];
                            
                            switch (consequent.Type_symbol) {
                                case 0: //En este caso el simbolo es un terminal, o es un Epsilon lo cual es una agregacion directa.
                                case 2:
                                    this.first_set.First_set[index_OF_current_set_of_first].add_symbol(consequent.Symbol);  
                                    current_state_of_FIRST.First_set[index_OF_current_set_of_first].add_symbol(consequent.Symbol);
                                    we_can_brake_travel = true; //Indicamos que Ya No se analizaran mas simbolos de esta produccion.
                                    break;
                                default: //Obviamente el simbolo es un No TERMINAL. 
                                    int index_first_element_to_search = first_set.get_index_of_first_element(consequent.Symbol); //Obtenemos el INDICE del conjunto Primero del no terminal encontrado en la Produccion

                                    foreach (string simple_symbol in first_set.First_set[index_first_element_to_search].First) { //Recorrido de todos los simbolos de Primero  de este conjunto.
                                        //Aqui se estara checando si este conjunto primero contiene EPSILON.
                                        if (string.Compare(simple_symbol, "~") != 0) { //Si el simbolo simplemente no es un terminal se agrega.

                                            this.first_set.First_set[index_OF_current_set_of_first].add_symbol(simple_symbol);
                                            current_state_of_FIRST.First_set[index_OF_current_set_of_first].add_symbol(simple_symbol);
                                        }
                                        else
                                            have_epsilon = true;
                                    }
                                    /*
                                    * Si SI se encontro un epsilon dentro de este conjunto primero, entonces determinamos que tenemos que seguir analizando los simbolos de la produccion.
                                    * Si NO se encontro un epsilon dentro de este conjunto Primero, determinamos que no debemos de seguir analizando los simbolos de produccion.
                                    */
                                    we_can_brake_travel = (have_epsilon) ? false : true; //Determinamos si el simbolo es anulable o no.
                                    break;
                            }
                            if (index_of_symbol == (max_index_list_of_symbols - 1) && have_epsilon) { //Averiguo si estoy en el ultimo simbolo, si esta contiene EPSILON de ser asi se agrega epsilon al conjunto primero actual.
                                this.first_set.First_set[index_OF_current_set_of_first].add_symbol("~");
                                current_state_of_FIRST.First_set[index_OF_current_set_of_first].add_symbol("~");
                            }
                        }
                    }
                }
                //Comprobacion de cambios entre los estados y Actualizacion de estos.
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
        /// Empieza la creacion del automata del LR1
        /// </summary>
        /// <param name="gram">Gramatica con la que se genera el automata.</param>
        public void generates_LR1_Automate(C_Grammar gram) {
            string start_symbols;

            start_symbols = gram.extend_grammar(); //Se hace la gramatica extendida.}
            //Inicia el analisis del estado 0.
            C_Closure_Element a_closure_element = new C_Closure_Element(start_symbols);
            //this.generates_closure(a_closure_element);            
        }



        /// <summary>
        /// Le llegan chingaderas como esta:
        /// {[S' -> .S, $]}
        /// {[S -> C.C, $]}
        /// {[F -> (.E ), $/+/-/*]}
        /// Que basicamente es un elemento de Cerradura.
        /// </summary>
        /// <param name="initial_closure">Cerradura de donde se podrian derivar mas estados </param>
        private void generates_closure(C_Closure_Element initial_closure) {

        }
    }
}