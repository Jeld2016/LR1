using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1_Final.LR1_Stuffs.First_Stuffs
{
    class C_First_Set
    {
        List<C_First_Element> first_set;

        public C_First_Set()
        {
            this.first_set = new List<C_First_Element>();
        }

        /// <summary>
        /// Obtiene o Establece la lista de conjuntos de primero(First_Element).
        /// </summary>
        internal List<C_First_Element> First_set { get => first_set; set => first_set = value; }


        /// <summary>
        /// Inicializa un elemento de Primero. Se agrega el nombre del Conjunto y se inicializa su lista de simbolos.
        /// </summary>
        /// <param name="name_set"></param>
        public void add_seed_first_set(string name_set)
        {
            C_First_Element new_first_elem = new C_First_Element(name_set);

            this.first_set.Add(new_first_elem);
        }


        /// <summary>
        ///  Inicializa los conjuntos primero con sus respectivos nombres.
        /// </summary>
        /// <param name="seeds_name_set">Lista de No Terminales que Representan los nombre de loos conjuntos</param>
        public void add_seed_first_set(List<string> seeds_name_set)
        {
            foreach (string seed_name in seeds_name_set)
            {
                C_First_Element new_first_elem = new C_First_Element(seed_name);



                this.first_set.Add(new_first_elem);
            }
        }


        /// <summary>
        /// Obtiene el indice del elemento primero(Conjunto Primero de este NOI_TEMRINAL) que se esta analizando
        /// </summary>
        /// <param name="no_terminal">No Terminal del que se esta buscando el indice</param>
        /// <returns></returns>
        public int get_index_of_first_element(string no_terminal)
        {
            int index = 0;

            foreach (C_First_Element a_first_element in this.first_set)
            {
                if (string.Compare(a_first_element.No_terminal, no_terminal) == 0)
                    break;
                index++;
            }
            return index;
        }


        /// Determina si la lista de conjuntos primero es igual a otra lista de conjuntos de primero.
        /// Esto se hace para comprobar si hubo, o no cambios entre el estado actual(this), y el estado anterior(a_first_set)
        /// </summary>
        /// <param name="a_first_set">Lista de conjuntos primero que representan el estado anterior</param>
        /// <returns></returns>
        public bool first_set_match(List<C_First_Element> a_first_set)
        {
            int length = this.first_set.Count;
            C_First_Element here_first_element;
            C_First_Element foreign_first_element;

            for (int index = 0; index < length; index++)
            { //Ambos contienen los mismos conjuntos, en principio podemos usar el mismo indice para hacer el recorrido.
                here_first_element = this.first_set[index];
                foreign_first_element = a_first_set[index];

                int lenght_lists = here_first_element.First.Count;

                if (lenght_lists != foreign_first_element.First.Count)
                { //Los indices no coinciden. Los conjuntos son diferentes.
                    return false;
                }
                else
                { //Los indices son iguales, verificamos que el contenido sea el mismo.
                    for (int index2 = 0; index2 < lenght_lists; index2++)
                    {
                        if (string.Compare(here_first_element.First[index2], foreign_first_element.First[index2]) != 0)
                        {//Aqui se hace una segunda comprobacion donde se determina si cada elemento de los conjunto primero son diferentes, entre si.
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// Determina si la longitud de cada conjunto de primero es diferente a los valores esperados.
        /// </summary>
        /// <param name="list_of_length">Lista de valores que son la longitud de algun otro conjunto de primero.</param>
        /// <returns>TRUE si todas las longitudes coinciden</returns>
        public bool match_length_of_Sets(List<int> list_of_length)
        {
            if (list_of_length.Count > 0)
            {
                int leght_of = this.first_set.Count;

                for (int index = 0; index < leght_of; index++)
                { //Ambas listas tienen la misma longitud, so podemos realizar el recorrido en base al mismo indice.
                    if (this.first_set[index].First.Count != list_of_length[index])
                        return false;
                }
            }
            else
                return false;

            return true;
        }



        /// <summary>
        /// Limpia la lista de elementos de Primero.
        /// </summary>
        public void clear_first_sets() { foreach (C_First_Element a_first_element in this.first_set) { a_first_element.clear_set(); } }


        /// <summary>
        /// Copia una lista de elementos de Primero.
        /// </summary>
        /// <param name="list_first_element">Lista de elementos de Primero que se va a copiar</param>
        public void copy_Set_Firts_on_self(List<C_First_Element> list_first_element)
        {
            int lenght_list = this.first_set.Count;
            C_First_Element here_element;
            C_First_Element incoming;

            if (lenght_list == list_first_element.Count)
            { //Si la longitud es la misma entre listas, todo chingon!, podemos copiar.
                for (int i = 0; i < lenght_list; i++)
                {
                    here_element = this.first_set[i];  //Se supone que este ya este elemento ya esta limpio. 
                    incoming = list_first_element[i]; // Es el que contiene los nuevo valores.
                    foreach (string just_aSymbol in incoming.First)
                    {
                        here_element.add_symbol(just_aSymbol);
                    }
                }
            }
        }
    }
}
