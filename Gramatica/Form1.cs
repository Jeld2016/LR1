using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Gramatica
{
    public partial class Form1 : Form
    {
        List<string> Producciones = new List<string>();
        List<String> NoTerminales = new List<string>();
        List<String> SimboloInicial = new List<string>();
        List<String> Terminales = new List<string>();
        List<string> Paso1 = new List<string>();
        List<string> Paso2 = new List<string>();
        List<string> Paso3 = new List<string>();
        List<Nodo> Nodos = new List<Nodo>();
        List<int> ProdAnt = new List<int>();
        List<int> ProdSig = new List<int>();
        int R1 = 0, R2 = 1;
        bool par = false;
        bool regular;

        public Form1()
        {
            InitializeComponent();
            regular = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "➳";
            textBox1.Select(textBox1.Text.Length, 0);
            textBox1.Focus();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "£";
            textBox1.Select(textBox1.Text.Length, 0);
            textBox1.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            String[] subMatriz;
            bool iguales = false, valido = true;
            int Menor = 0, LibreContexto = 0, izq = 0, der = 0, Vt = 0, Vn = 0, Par = 0;
            Producciones = textBox1.Text.Split(new[] { "\r\n" },//Buscar las Producciones que se escribieron
                 StringSplitOptions.RemoveEmptyEntries).ToList();
            //leer todas las produciones existentes 
            foreach (string Produccion in Producciones)
            {

                subMatriz = Produccion.Split('➳');//Dividir la produccion en izquierda y derecha
                if (subMatriz[0][0] == 'S') // Buscar los simbolos iniciales
                    SimboloInicial.Add(Produccion);
                if (subMatriz[0].Count() <= subMatriz[1].Count())
                {
                    Menor++; // revisar si la subdivision izquierda es menor a la derecha
                }
                //leer las letras de la subdivision de la izquierda
                foreach (char Letra in subMatriz[0])
                {
                    int Let = Letra;
                    izq++;
                    if (Let >= 97 && Let <= 122 && Let != 163) // Revisa si es un terminal
                    {
                        if (Terminales.Count != 0) // Asegurarse de que no se repitan los terminos
                        {
                            for (int j = 0; j < Terminales.Count; j++)
                            {
                                if (Terminales[j] != Letra.ToString()) ;
                                else
                                    iguales = true;
                            }
                            if (!iguales)
                                Terminales.Add(Letra.ToString());
                            iguales = false;
                        }
                        else
                            Terminales.Add(Letra.ToString());
                    }
                    else if (Let == 163) // revisa que el epsilon no este en el lado izquierdo
                    {
                        MessageBox.Show("No puede existir £ del lado izquierdo");
                        valido = false;
                        textBox1.Text = "";
                    }
                    else if (Let >= 65 && Let <= 90)//Revisa si es un  NoTerminal
                    {
                        LibreContexto++; //Revisa que los elementos del lado izquierdo sean NoTerminales
                        if (NoTerminales.Count != 0) //Agrega no terminales Asegurandose que no se repitan
                        {
                            for (int j = 0; j < NoTerminales.Count; j++)
                            {
                                if (NoTerminales[j] != Letra.ToString()) ;
                                else
                                    iguales = true;
                            }
                            if (!iguales)
                                NoTerminales.Add(Letra.ToString());
                            iguales = false;
                        }
                        else
                            NoTerminales.Add(Letra.ToString());
                    }
                }
                //Verifica que solo existan 2 elementos del lado derecho para la Gramatica Regular
                if (subMatriz[1].Count() <= 2)
                    Par++;
                foreach (char Letra in subMatriz[1])//Busca los elementos que tiene la subdivision derecha
                {
                    int Let = Letra;
                    der++;

                    if (Let >= 97 && Let <= 122)//verifica si hay un terminal del lado derecho
                    {
                        Vt++;//aumenta si hay un terminal del lado derecho
                        if (Terminales.Count != 0) // agrega los terminales cuidando que no sean iguales 
                        {
                            for (int j = 0; j < Terminales.Count; j++)
                            {
                                if (Terminales[j] != Letra.ToString()) ;
                                else
                                    iguales = true;
                            }
                            if (!iguales)
                                Terminales.Add(Letra.ToString());
                            iguales = false;
                        }
                        else
                            Terminales.Add(Letra.ToString());
                    }
                    else if (Let >= 65 && Let <= 90)//verifica si hay un no terminal
                    {
                        Vn++;//aumenta si hay un Noterminal del lado derecho 
                        if (NoTerminales.Count != 0)//agrega un no terminal si es que no se repite
                        {
                            for (int j = 0; j < NoTerminales.Count; j++)
                            {
                                if (NoTerminales[j] != Letra.ToString()) ;
                                else
                                    iguales = true;
                            }
                            if (!iguales)
                                NoTerminales.Add(Letra.ToString());
                            iguales = false;
                        }
                        else
                            NoTerminales.Add(Letra.ToString());
                    }
                }
            }

            if (valido)//Se habilita solo si no hay un epsilon del lado izquierdo
            {
                textBox2.Text += "(S:";
                if (SimboloInicial.Count == 0)
                    SimboloInicial.Add(Producciones[1]);
                else
                    for (int i = 0; i < SimboloInicial.Count; i++)
                    {
                        textBox2.Text += SimboloInicial[i] + ","; // imprime los simbolos iniciales
                    }


                textBox2.Text += ",Vt:";
                for (int i = 0; i < Terminales.Count; i++)
                {
                    textBox2.Text += Terminales[i] + ",";//imprime los terminales
                }
                textBox2.Text += ",Vn:";
                for (int i = 0; i < NoTerminales.Count; i++)
                {
                    textBox2.Text += NoTerminales[i] + ",";//imprime los NoTerminales
                }
                textBox2.Text += "Ø:";
                for (int i = 0; i < Producciones.Count; i++)
                {
                    textBox2.Text += Producciones[i] + ",";//Imprime las Producciones
                }
                textBox2.Text += ")";
                //Identifica el tipo de Gramatica
                if (Par == Producciones.Count && Vt + Vn == der && izq == LibreContexto && Menor == Producciones.Count)
                {
                    textBox2.Text += "\r\nGramatica Regular";
                    regular = true;
                }
                else if (izq == LibreContexto && Menor == Producciones.Count)
                    textBox2.Text += "\r\nGramatica Libre de Contexto";
                else if (Menor == Producciones.Count)
                    textBox2.Text += "\r\nGramatica Sensitiva al Contexto";
                else
                    textBox2.Text += "\r\nGramatica Sin Restricciones";
            }
        }
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            Paso1 = new List<string>();
            Paso2 = new List<string>();
            Paso3 = new List<string>();

            regular = false;
            OpenFileDialog Open;
            Open = new OpenFileDialog();
            Open.ShowDialog();
            Open.Filter = "File text|*.txt";
            String s = File.ReadAllText(Open.FileName);
            textBox1.Text += s + "\r\n";
        }
        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            String s = null;
            saveDialog.Filter = "File text |*.txt";
            if (saveDialog.ShowDialog() != DialogResult.OK) return;
            List<string> Lineas = textBox1.Text.Split(new[] { "\r\n" },//Buscar las Producciones que se escribieron
                 StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (string l in Lineas)
            {
                s = s + l + "\r\n";
            }
            File.WriteAllText(saveDialog.FileName, s);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (regular)
                ExpresionRegular();
            else
                MessageBox.Show("La Expresion no es Regular o no se comprobo antes");
        }
        private void ExpresionRegular()
        {

            if (Producciones.Count == 0)
                MessageBox.Show("no hay producciones");
            else
            {

                List<string> ExpRegular = Producciones;
                paso1(ExpRegular);

            }
        }
        private void paso1(List<string> ExpRegular)
        {
            string[] Div, Div2;

            for (int i = 0; i < ExpRegular.Count; i++)
            {
                Div = ExpRegular[i].Split('➳');//Dividir la produccion en izquierda y derecha
                for (int j = i + 1; j < ExpRegular.Count; j++)
                {
                    Div2 = ExpRegular[j].Split('➳');//Dividir la produccion en izquierda y derecha
                    if (Div[0] == Div2[0])
                    {
                        Div[1] += "|" + Div2[1];
                        ExpRegular.RemoveAt(j);
                        j = j - 1;
                    }
                }
                Paso1.Add(Div[0] + "➳" + Div[1]);
            }


            textBox2.Text += "\r\n Paso1:";
            for (int i = 0; i < Paso1.Count; i++)
            {
                textBox2.Text += "\r\n" + Paso1[i];
            }

            paso2();
        }
        private void paso2()
        {
            string[] Div;
            textBox2.Text += "\r\n Paso2:";
            for (int i = 0; i < Paso1.Count - 1; i++)
            {
                Div = Paso1[i].Split('➳');//Dividir la produccion en izquierda y derecha
                for (int j = 0; j < Div[1].Count(); j++)
                {
                    if (Div[1][j] == char.Parse(Div[0]))
                    {
                        if (j - 1 >= 0 && j + 2 <= Div[1].Count() - 1)
                        {
                            if (Div[1][j + 1] != '|')
                                Div[1] = Div[1].Replace(Div[1][j - 1].ToString() + Div[0], "{" + Div[1][j - 1].ToString() + "}");
                            else
                                Div[1] = Div[1].Replace(Div[1][j - 1].ToString() + Div[0] + "|", "{" + Div[1][j - 1].ToString() + "}");
                        }
                        else
                            Div[1] = Div[1].Replace(Div[0], "{" + Div[1][j + 1].ToString() + "}");
                    }
                }
                for (int k = i + 1; k < Paso1.Count; k++)
                {
                    Paso1[k] = Paso1[k].Replace(Div[0], "(" + Div[1] + ")");
                    
                }
                string Produccion = Div[0] + "➳" + Div[1];
                textBox2.Text += "\r\n" + Produccion;
                Produccion = MultiplicaExp(Div[0] + "➳" + Div[1]);
                Paso2.Add(Produccion);
            }
            string Produccio = MultiplicaExp(Paso1[Paso1.Count - 1]);
            Paso2.Add(Produccio);

            textBox2.Text += "\r\n Paso2 Reduccion:";
            for (int i = 0; i < Paso2.Count; i++)
            {
                textBox2.Text += "\r\n" + Paso2[i];
            }
            paso3();
        }
        private void paso3()
        {
            string[] Div;
            Div = Paso2[Paso2.Count - 1].Split('➳');//Dividir la produccion en izquierda y derecha

            for (int i = 0; i < Div[1].Count(); i++)
            {
                if (Div[1][i] == char.Parse(Div[0]))
                {
                    if (i - 1 >= 0 && i + 2 <= Div[1].Count() - 1)
                    {
                        if (Div[1][i + 1] != '|')
                            Div[1] = Div[1].Replace(Div[1][i - 1].ToString() + Div[0], "{" + Div[1][i - 1].ToString() + "}");
                        else
                            Div[1] = Div[1].Replace(Div[1][i - 1].ToString() + Div[0] + "|", "{" + Div[1][i - 1].ToString() + "}");
                    }
                    else
                        Div[1] = Div[1].Replace(Div[0], "{" + Div[1][i - 1].ToString() + "}");
                }

            }
            string Produccion = Div[0] + "➳" + Div[1];
            Produccion = MultiplicaExp(Div[0] + "➳" + Div[1]);
            Paso3.Add(Produccion);
            Paso2[Paso2.Count - 1] = Paso3[0];



            int sig = 0;
            textBox2.Text += "\r\n Paso3:";
            textBox2.Text += "\r\n" + Paso3[0];
            for (int i = Paso2.Count - 1; i > 0; i--)
            {
                if (i - 1 >= 0)
                    sig = i - 1;

                Div = Paso2[i].Split('➳');//Dividir la produccion en izquierda y derecha
                Paso2[sig] = Paso2[sig].Replace(Div[0], "(" + Div[1] + ")");
                textBox2.Text += "\r\n" + Paso2[sig];
                Paso2[sig] = MultiplicaExp(Paso2[sig]);
                Paso3.Add(Paso2[sig]);
            }

            textBox2.Text += "\r\n Paso3 Reduccion:";
            for (int i = Paso3.Count - 1; i >= 0; i--)
            {
                textBox2.Text += "\r\n" + Paso3[i];
            }
            
            paso4();
        }
        private void paso4()
        {
            string[] Div;

            textBox2.Text += "\r\n Paso4:";
            if (Paso3.Count > 0)
            {
                string Paso4 = Paso3[Paso3.Count - 1];
                Div = Paso4.Split('➳');


                textBox2.Text += "\r\n" + "E ➳" + Div[1];

                for (int i = 0; i < Div[1].Count(); i++)
                {
                    if (Div[1][i] == '{')
                    {

                        Div[1] = Div[1].Replace(Div[1][i].ToString() + Div[1][i + 1].ToString() + Div[1][i + 2].ToString(), Div[1][i + 1].ToString() + "*");
                    }

                }
                //textBox2.Text += "\r\n" + "E ➳" + Div[1];

                for(int i= 0;i< Div[1].Count();i++)
                {
                    if (Div[1][i] == '*')
                    {
                        if (i - 2 >= 0)
                        {
                            if (Div[1][i - 1].ToString() == Div[1][i - 2].ToString())
                            {
                                Div[1] = Div[1].Replace(Div[1][i - 2].ToString() + Div[1][i - 1].ToString() + Div[1][i].ToString(), Div[1][i - 1].ToString() + "+");
                            }
                        }
                        else
                        if (i + 1 <= Div[1].Count() - 1)
                            if (Div[1][i - 1].ToString() == Div[1][i + 1].ToString())
                            {
                                Div[1] = Div[1].Replace(Div[1][i - 1].ToString() + Div[1][i].ToString() + Div[1][i + 1].ToString(), Div[1][i - 1].ToString() + "+");
                            }
                    }

                }
                for (int i = 0; i < Div[1].Count(); i++)
                {
                    if (i - 1 >= 0 && i + 1 <= Div[1].Count() - 1)
                        if (Div[1][i] == Div[1][i - 1])
                            if (Div[1][i + 1] == '+')
                                Div[1] = Div[1].Replace(Div[1][i - 1].ToString() + Div[1][i].ToString() + Div[1][i+1].ToString(), Div[1][i].ToString() + "+");
                            else
                                Div[1] = Div[1].Replace(Div[1][i - 1].ToString() + Div[1][i].ToString(), Div[1][i].ToString() + "*");


                }
                textBox2.Text += "\r\n" + "E ➳" + Div[1];
                //GramaticaAum(Div[1]);
                //Posfija(Div[1]);
            }
        }
        private string MultiplicaExp(string Mult)
        {
            string NuevaExpresion = "";
            string[] Divide,Divide2,Divide3;

            Divide = Mult.Split('➳');
            char[] separador = { '(', ')' };
            string nueva ="";
            Divide2 = Divide[1].Split(separador);
            if (Divide2.Count() > 1)
                {
                Divide3 = Divide2[1].Split('|');
                if (Divide3.Count() != 0)
                    {
                        for (int j = 0; j < Divide3.Count(); j++)
                        {
                            if (j != 0)
                                Divide3[j] = "|" + Divide2[0] + Divide3[j];
                            else
                                Divide3[j] = Divide2[0] + Divide3[j];
                            nueva += Divide3[j];
                        }
                        
                    }

                    else
                        nueva = Divide2[0] + Divide2[1];

                for (int i = 2; i < Divide2.Count(); i++)
                    nueva += Divide2[i];
                NuevaExpresion = Divide[0] + "➳" + nueva;
            }
                else
                    NuevaExpresion= Divide[0] + "➳" + Divide2[0];

            return NuevaExpresion;
        }
        private void GramaticaAum(string ExpRegular)
        {
            string GramaticaAumentada="";
            GramaticaAumentada = textBox1.Text;
            GramaticaAumentada=Posfija(GramaticaAumentada);
            GramaticaAumentada += "°#";
            textBox2.Text += GramaticaAumentada + "\r\n";
        }
        private string Posfija(string ExpRegular)
        {
            string k = ExpRegular;
            int x = 0;
            while(k.Contains("|"))
            {
                ExpRegular = ExpRegular.Substring(0, ExpRegular.IndexOf("")) + "°" + ExpRegular.Substring(ExpRegular.IndexOf("|") + 1);
                ExpRegular += "°|";
                k = k.Substring(k.IndexOf("|") + 1);
            }
            ExpRegular = ExpRegular.Replace("(", "°");
            ExpRegular = ExpRegular.Replace(")", "°");
            return ExpRegular;
        }
        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            Paso1.Clear();
            Paso2.Clear();
            Paso3.Clear();
            Nodos.Clear();
            ProdAnt.Clear();
            ProdSig.Clear();
            Producciones.Clear();
            R1 = 0;
            R2 = 1;
        }

        private void analizadorSintacticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analizador An = new Analizador();
            An.ShowDialog();
        }

        private void canonicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LR_Canonico LR = new LR_Canonico();
            LR.ShowDialog();
        }

        private void automatasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Automatas Au = new Automatas();
            Au.Show();
        }
    }
}
