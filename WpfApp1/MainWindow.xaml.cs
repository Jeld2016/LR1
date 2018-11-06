using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls; //Para los controles he MahaApps
using System.Text.RegularExpressions;
using WpfApp1.Grammar_Stuffs; //Cosas acerca de la gramatica
using Microsoft.Win32;
using System.IO; //Se usa para escribir y leer archivos
using WpfApp1.LR1_Stuffs;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        C_Grammar grammar;
        string path;
        C_Checker pattern_checker;
        C_LR1 lr1;
        

        public MainWindow()
        {
            InitializeComponent();
            this.pattern_checker = new C_Checker();
            this.grammar = new C_Grammar();
            this.path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Grammars";
            this.lr1 = new C_LR1(this.grammar);
        }

        private void textBox_Input_KeyDown(object sender, KeyEventArgs e)

        {

        }


        private void button_Build_Parser_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> f = this.validate_checked();

            if (f!= null) {
                this.load_NON_TERMINALS(f); //Carga de los simbolos No Terminales a partir de la lista de cadenas ya validadas con anterioridad.
                this.lr1.add_seeds_Sets(this.grammar.No_terminals1);//Esta funcion basicamente carga la lista de NO TERMINALES dispoibles en esta gramatica.
                foreach (String[] line in f) {//Aqui se inicia la carga de de la gramatica a partir de la lista de cadenas.
                    this.grammar.add_prodution_from_source(line[0], line[1]);//Agregar produccion(desde texto plano) a la gramatica, desde el fuente.
                }
                //this.lr1.generates_first(this.grammar);//Generacion del Conjunto de Primero.
                this.lr1.generate_first_set_to_LR(this.grammar);
                this.lr1.generates_LR1_Automate(this.grammar);
                this.fill_first_table();
            }
        }


        /// <summary>
        /// Carga de los No Terminales de esta gramatica.
        /// </summary>
        /// <param name="pure_lines"></param>
        private void load_NON_TERMINALS(List<string[]> pure_lines)
        {
            foreach (String[] line in pure_lines)
            {
                this.grammar.No_terminals1.Add(line[0]); //Agrega el no Terminal a la lista de No terminales pertenecientes a esta Gramatica.
            }
        }
        /// <summary>
        /// Valida que todo el texto tenga el formato correcto de Produccion para escribir Gramaticas.
        /// </summary>
        /// <returns></returns>
        private List<string[]> validate_checked()
        {
            List<string[]> pure_lines = new List<string[]>();
            string[] one_line;
            string[] all_text = this.textBox_Input.Text.Split('\n');
            int index = 1;

            foreach (string a_str in all_text)
            {
                one_line = Regex.Split(a_str, "->");
                if (one_line.Length == 2)
                {
                    if (!this.pattern_checker.match_string_left(one_line[0]))
                    {
                        /*Se encontraron errores al analisar la parte izquierda de la produccion*/
                        MessageBox.Show("Grammar ERROR:NL = " + index);
                        return null;
                    }
                    else
                    {
                        one_line[1] = one_line[1].TrimEnd('\r', '\n');//Eliminacion salto de linea.
                        if (!this.pattern_checker.match_string_right(one_line[1]))
                        {
                            /*Se encontraron errores al analisar la parte derecha de la produccion*/
                            MessageBox.Show("Grammar ERROR:NL = " + index);
                            return null;
                        }
                    }
                }
                else
                {
                    /*Existen errores mas obvio y evidentes*/
                    MessageBox.Show("Grammar ERROR:NL = " + index);
                    return null;
                }
                pure_lines.Add(one_line);
                index++;
            }
            return pure_lines;
        }

        

        /// <summary>
        /// Evento del Boton Guardar del meuBar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_item_save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save_file_dialog = new SaveFileDialog();

            this.directory_grammars_exsit();
            save_file_dialog.InitialDirectory = this.path;
            if (save_file_dialog.ShowDialog() == true) {
                File.WriteAllText(save_file_dialog.FileName, this.textBox_Input.Text);
            }
        }


        /// <summary>
        /// Evento del Boton Abrir del menuBar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_item_Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();

            this.directory_grammars_exsit();
            open_file_dialog.InitialDirectory = this.path;
            if (open_file_dialog.ShowDialog() == true)
            {
                this.textBox_Input.Text = File.ReadAllText(open_file_dialog.FileName);               
            }
        }



        /// <summary>
        /// Verifica si el Directorio donde se guardan las gramaticas existe, de lo contrario lo crea.
        /// </summary>
        private void directory_grammars_exsit() {
            string[] a_path = Regex.Split(this.path, "file:\\\\");
            var info_path = new System.IO.FileInfo(a_path[1]);

            if (!info_path.Exists) {
                System.IO.Directory.CreateDirectory(info_path.FullName);
            }
        }

        private void fill_first_table() {
            this.dgrid_first_table.Items.Clear();
            foreach (C_First_Element first_element in this.lr1.First_set.First_set) {
                var data = new Dgrid_First_Set_Content { non_terminal = first_element.No_terminal, first_set = string.Empty};
                foreach (string terminal in first_element.First) { 
                    if(string.Compare(data.first_set, string.Empty) != 0)
                        data.first_set = data.first_set +", "+ terminal;
                    else
                        data.first_set = "{ " + terminal;
                }
                data.first_set = data.first_set + " }";

                this.dgrid_first_table.Items.Add(data);
            }
        }
    }

    class Dgrid_First_Set_Content
    {
        public string non_terminal { get; set; }
        public string first_set { get; set; }
    }
}


