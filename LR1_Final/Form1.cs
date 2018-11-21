using LR1_Final.Grammar_Stuffs;
using LR1_Final.Tokenizer_Stuffs;
using LR1_Final.LR1_Stuffs;
using LR1_Final.LR1_Stuffs.First_Stuffs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR1_Final
{
    public partial class Form1 : Form
    {
        C_Grammar grammar;
        string path;
        C_Checker pattern_checker;
        C_LR1 lr1;

        public Form1()
        {
            this.path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\Grammars";
            this.pattern_checker = new C_Checker();
            this.grammar = new C_Grammar();
            this.lr1 = new C_LR1(this.grammar);
            InitializeComponent();
        }

        

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_file_dialog = new OpenFileDialog();

            this.directory_grammars_exsit();
            open_file_dialog.InitialDirectory = this.path;
            if (open_file_dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox_Input.Text = File.ReadAllText(open_file_dialog.FileName);
                this.textBox_Input.SelectionStart = this.textBox_Input.Text.Length;
                this.textBox_Input.SelectionLength = 0;
            }
        }

        /// <summary>
        /// Verifica si el Directorio donde se guardan las gramaticas existe, de lo contrario lo crea.
        /// </summary>
        private void directory_grammars_exsit()
        {
            string[] a_path = Regex.Split(this.path, "file:\\\\");
            var info_path = new System.IO.FileInfo(a_path[1]);

            if (!info_path.Exists)
            {
                System.IO.Directory.CreateDirectory(info_path.FullName);
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_file_dialog = new SaveFileDialog();

            this.directory_grammars_exsit();
            save_file_dialog.InitialDirectory = this.path;
            if (save_file_dialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(save_file_dialog.FileName, this.textBox_Input.Text);
            }
        }

        private void button_build_Parser_Click(object sender, EventArgs e)
        {
            List<string[]> f = this.validate_checked();

            if (f != null)
            {
                this.load_NON_TERMINALS(f); //Carga de los simbolos No Terminales a partir de la lista de cadenas ya validadas con anterioridad.
                this.lr1.add_seeds_Sets(this.grammar.No_terminals1);//Esta funcion basicamente carga la lista de NO TERMINALES dispoibles en esta gramatica.
                foreach (String[] line in f)
                {//Aqui se inicia la carga de de la gramatica a partir de la lista de cadenas.
                    this.grammar.add_prodution_from_source(line[0], line[1]);//Agregar produccion(desde texto plano) a la gramatica, desde el fuente.
                }
                this.lr1.generate_first_set_to_LR();
                this.lr1.generates_LR1_Automate();
                this.fill_afd_table();
                this.fill_first_table();
                create_table();
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
                if (!this.grammar.No_terminals1.Contains(line[0]))
                    this.grammar.No_terminals1.Add(line[0]); //Agrega el no Terminal a la lista de No terminales pertenecientes a esta Gramatica.
            }
        }

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

        private void fill_afd_table() {
            //this.dgrid_first_table.
            string this_kernel = "[";
            string this_closure = "[";
            string forward_symbol_search = string.Empty;


            //this.dgrid_AFD.Items.Clear();
            foreach (C_LR1_Element lr1_element in this.lr1.List_states)
            {
                foreach (C_Closure_Element cl_element in lr1_element.Kernel)
                {
                    this_kernel += "[";
                    this_kernel += cl_element.Production.Producer + "=>";
                    //Ciclo para recorrer los simbolos de la izquierda de la Produccion
                    foreach (C_Symbol symb in cl_element.Production.Right)
                        this_kernel += symb.Symbol;
                    this_kernel += "  {";
                    foreach (string str in cl_element.Forward_search_symbols)
                        this_kernel += str + " ";
                    this_kernel += "}]  ";
                }
                this_kernel += "]";


                //Ciclo de CLOSURE 
                forward_symbol_search = string.Empty;
                foreach (C_Closure_Element cl_element in lr1_element.Closure)
                {
                    this_closure += "[";
                    this_closure += cl_element.Production.Producer + "=>";
                    foreach (C_Symbol symb in cl_element.Production.Right)
                        this_closure += " " + symb.Symbol;
                    this_closure += "  {";
                    foreach (string str in cl_element.Forward_search_symbols)
                        this_closure += " " + str;
                    this_closure += "}] ";
                }
                this_closure += "]";

                var data = new Dgrid_AFD_Content
                {
                    go_to = "(" + lr1_element.My_go_to.State.ToString() + "," + lr1_element.My_go_to.Symbol_state.Symbol + ")",
                    kernel = this_kernel,
                    state = lr1_element.Num_state.ToString(),
                    closure = this_closure
                };
                DataGridViewRow nw_row = (DataGridViewRow)this.dataGrid_LR1.Rows[0].Clone();
                nw_row.Cells[0].Value = (Object)data.go_to;
                nw_row.Cells[1].Value = (Object)data.kernel;
                nw_row.Cells[2].Value = (Object)data.state;
                nw_row.Cells[3].Value = (Object)data.closure;
                this.dataGrid_LR1.Rows.Add(nw_row);
                this_kernel = "[";
                this_closure = "[";
                forward_symbol_search = "/";
            }
        }

        private void fill_first_table()
        {
            foreach (C_First_Element first_element in this.lr1.First_set.First_set)
            {
                var data = new Dgrid_First_Set_Content { non_terminal = first_element.No_terminal, first_set = string.Empty };
                foreach (string terminal in first_element.First)
                {
                    if (string.Compare(data.first_set, string.Empty) != 0)
                        data.first_set = data.first_set + ", " + terminal;
                    else
                        data.first_set = "{ " + terminal;
                }
                data.first_set = data.first_set + " }";

                DataGridViewRow nw_row = (DataGridViewRow)this.dgrid_first_table.Rows[0].Clone();

                nw_row.Cells[0].Value = data.non_terminal;
                nw_row.Cells[1].Value = data.first_set;
                this.dgrid_first_table.Rows.Add(nw_row);
            }
        }

        private void create_table()
        {
            List<string> list = colocar_list();
            Table_LR1.RowCount = lr1.num_state_max()+1;
            Table_LR1.ColumnHeadersVisible = true;
            Table_LR1.ColumnCount = this.grammar.No_terminals1.Count + lr1.TERMINALES().Count+1;

            for(int j = 0; j < list.Count; j++)
            {
                Table_LR1.Columns[j].Name = list[j];
            }
            for (int j = 0; j < lr1.num_state_max() + 1; j++)
            {
                Table_LR1.Rows[j].HeaderCell.Value =j.ToString();
            }



            DataGridViewCellStyle columnHeaderStyle =
            new DataGridViewCellStyle();
            columnHeaderStyle.BackColor = Color.Aqua;
            columnHeaderStyle.Font =
                new Font("Verdana", 10, FontStyle.Bold);
            Table_LR1.ColumnHeadersDefaultCellStyle =
                columnHeaderStyle;


            //para los desplazamientos y los ira

            for (int i = 0; i < lr1.List_states.Count; i++)
            {
                if (lr1.List_states[i].My_go_to.State != -1)
                {
                    if (lr1.TERMINALES().Contains(lr1.List_states[i].My_go_to.Symbol_state))
                    {

                        Table_LR1.Rows[lr1.List_states[i].My_go_to.State].Cells[regresaindex(lr1.List_states[i].My_go_to.Symbol_state.Symbol, list)].Value = "d" + lr1.List_states[i].Num_state.ToString();
                    }
                    if (this.grammar.No_terminals1.Contains(lr1.List_states[i].My_go_to.Symbol_state.Symbol))
                    {
                        Table_LR1.Rows[lr1.List_states[i].My_go_to.State].Cells[regresaindex(lr1.List_states[i].My_go_to.Symbol_state.Symbol, list)].Value = lr1.List_states[i].Num_state.ToString();
                    }
                }

                //reduciones
                
                    for (int j = 0; j < lr1.List_states[i].Kernel.Count; j++)
                    {
                        if (lr1.List_states[i].Kernel[j].Production.get_symbol_next_to_DOT() == null)
                        {
                            for (int k = 0; k < lr1.List_states[i].Kernel[j].Forward_search_symbols.Count; k++)
                            {

                                int rnum = Compare_Production(eliminapunto(lr1.List_states[i].Kernel[j].Production));
                                Table_LR1.Rows[lr1.List_states[i].Num_state].Cells[regresaindex(lr1.List_states[i].Kernel[j].Forward_search_symbols[k], list)].Value = "r" + rnum.ToString();
                            }
                        }
                    }
                

            }

            



        }

        private int Compare_Production(C_Production p)
        {
            List<C_Production> gr = grammar.Get_Grammar();
            for (int i = 0; i < gr.Count; i++)
            {
                if(list_compare(gr[i].Right,p.Right) && gr[i].Producer == p.Producer)
                {
                    return grammar.Get_Grammar()[i].NumProduc;
                }
            }
            return 0;
        }

        private bool list_compare(List<C_Symbol> p, List<C_Symbol> a)
        {
            bool avisoli = true;
            if (p.Count == a.Count)
            {
                for(int i = 0; i < p.Count; i++)
                {
                    if (p[i].Symbol != a[i].Symbol)
                    {
                        avisoli= false;
                    } 
                }
            }
            else { avisoli = false; }

            return avisoli;
            

        }

        private C_Production eliminapunto(C_Production p)
        {
            C_Production t= new C_Production(p);
            t.Right.Clear();
            for(int i = 0; i < p.Right.Count; i++)
            {
                if (p.Right[i].Symbol != ".")
                {
                    t.Right.Add((p.Right[i]));
                }
            }
            return t;
        }

        private int regresaindex(string p,List<string>l)
        {
            for(int i = 0; i < l.Count; i++)
            {
                if (p == l[i])
                {
                    return i;
                }
            }
            return 0;
        }

        private List<string> colocar_list()
        {
            List<string> aux = new List<string>();
            
            for(int i = 0; i < lr1.TERMINALES().Count; i++)
            {
                aux.Add(lr1.TERMINALES()[i].Symbol);
            }
            aux.Add("$");
            for (int i = 0; i < grammar.No_terminals1.Count; i++)
            {
                aux.Add(grammar.No_terminals1[i]);
            }
            return aux;
        }


        class Dgrid_First_Set_Content
        {
            public string non_terminal { get; set; }
            public string first_set { get; set; }
        }
        class Dgrid_AFD_Content
        {
            public string go_to { get; set; }
            public string kernel { get; set; }
            public string state { get; set; }
            public string closure { get; set; }
        }

    }
}
