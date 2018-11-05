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




        public void generate_first_set_to_LR(C_Grammar gram)
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
                foreach (string antecedent in gram.No_terminals1)
                {
                    /*Se obtiene las producciones que que corresponden al no terminal que estamos analizando*/
                    productions_to_Analize = gram.get_Productions(antecedent);
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
            /// <param name="initial_closure">Cerradura de donde se podrian derivar mas estados </param>
            private void generates_closure(C_Closure_Element initial_closure) {

            }

           /* private C_LR1_Element Create_Zero_State(C_Grammar gram)
            {
                C_LR1_Element Zero = new C_LR1_Element();
                Zero.Kernel = null;
                for (int i = 0; i < gram.Get_Grammar().Count; i++)
                {
                C_Closure_Element element = new C_Closure_Element();

                }
            }*/
    }
}