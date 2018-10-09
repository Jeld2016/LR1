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

namespace Gramatica
{
    public partial class LR_Canonico : Form
    {
        List<string> Producciones = new List<string>();
        List<string> Izq = new List<string>();
        List<string> Der = new List<string>();
        List<Gramatica> C = new List<Gramatica>();
        List<string> ProSP = new List<string>();
        List<string> Acciones = new List<string>();
        List<int> orAc = new List<int>();
        List<char> orPri = new List<char>();
        List<List<Gramatica>> Conjuntos = new List<List<Gramatica>>();
        List<char> X = new List<char>();

        public LR_Canonico()
        {
            InitializeComponent();
        }

        //correigida

        private void Aumentada()
        {

           
            bool aviso = false;
            List<string> Producciones2=new List<string>();
            string cad = "";
            Producciones = textBox1.Text.Split(new[] { "\r\n" },//Buscar las Producciones que se escribieron
                 StringSplitOptions.RemoveEmptyEntries).ToList();

            //Produccion Aumentada
            if (!Producciones[0].Contains("'"))
            {
             // string[] partes = Producciones[0].Split('➳');
               string[] partes = SplitFlecha(Producciones[0]);
               cad = Producciones[0][0] + "'->" + partes[0];
                Producciones2 = new List<string>(Producciones);
                Producciones.Clear();
                Producciones.Add(cad);
                for(int j = 0; j < Producciones2.Count; j++)
                {
                    Producciones.Add(Producciones2[j]);
                }
            }
            //Ors
            Producciones2.Clear();
            for(int k = 0; k < Producciones.Count; k++)
            {
                if (Producciones[k].Contains("|"))
                {
                    //string[] partes = Producciones[k].Split('➳');
                    string[] partes = SplitFlecha(Producciones[k]);
                    string[] ors = partes[1].Split('|');
                    for(int p = 0; p < ors.Length; p++)
                    {
                        Producciones2.Add(partes[0] + "->" + ors[p]);
                    }
                    aviso = true;
                }
                else
                {
                    Producciones2.Add(Producciones[k]);
                }
            }
            if (aviso == true)
            {
                Producciones.Clear();
                for (int i = 0; i < Producciones2.Count; i++)
                    Producciones.Add(Producciones2[i]);
            }
            //aqui 
            for (int i = 0; i < Producciones.Count; i++)
               Producciones[i]= Producciones[i].Replace("->", "->°");
            //Producciones[i]=Producciones[i].Insert(Producciones[i].IndexOf('>')+1, "°");

            String[] subMatriz;
            foreach (string Produccion in Producciones)
            {
                subMatriz =SplitFlecha(Produccion);//Dividir la produccion en izquierda y derecha
               // subMatriz = Produccion.Split('➳');
               Izq.Add(subMatriz[0]);
                Der.Add(subMatriz[1]);
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Aumentada();
            button2.Visible = true;
            List<Gramatica> E = new List<Gramatica>();
            E.Add(new Gramatica(Producciones[0], '$'));


            C = Cerradura(E);
            Conjuntos.Add(C);


            bool repeat = true;
            for (int i = 0; i < Der.Count(); i++)
                for (int j = 0; j < Der[i].Count(); j++)
                {
                    if (X.Count == 0 && Der[i][j] != 176)

                        X.Add(Der[i][j]);
                    else
                    {
                        for (int k = 0; k < X.Count; k++)
                        {
                            if (Der[i][j] == X[k] || Der[i][j] == 176)
                            {
                                repeat = true;
                                k = X.Count;
                            }
                            else
                                repeat = false;
                        }

                        if (!repeat)
                            X.Add(Der[i][j]);

                        repeat = true;
                    }

                }
            for (int i = 0; i < Conjuntos.Count(); i++)

                for (int k = 0; k < X.Count; k++)
                {
                    List<Gramatica> Aux = new List<Gramatica>();
                    for (int j = 0; j < Conjuntos[i].Count; j++)
                    {
                        for (int l = 0; l < Conjuntos[i][j].Produc.Count(); l++)
                            if (Conjuntos[i][j].Produc[l] == 176)
                            {
                                if (l + 1 != Conjuntos[i][j].Produc.Count())
                                    if (Conjuntos[i][j].Produc[l + 1] == X[k])
                                    {
                                        Gramatica result = Ir_a(Conjuntos[i][j], X[k]);
                                        Aux.Add(result);
                                    }
                            }
                    }
                    for (int result = 0; result < Aux.Count; result++)
                        for (int rec = 0; rec < Aux[result].Produc.Count(); rec++)
                            if (Aux[result].Produc[rec] == 176)
                                if (rec + 1 < Aux[result].Produc.Count())
                                    if (Aux[result].Produc[rec + 1] >= 65 && Aux[result].Produc[rec + 1] <= 90)
                                    {
                                        Aux = AgregaProdS(Aux, Aux[result].Produc[rec + 1], Aux[result].Prim);
                                    }

                    bool repetir = false;
                    for (int rp = 0; rp < Conjuntos.Count; rp++)
                    {
                        repetir = Repeticion(Conjuntos[rp], Aux);
                        if (repetir)
                            break;
                    }

                    if (Aux.Count != 0 && !repetir)
                        Conjuntos.Add(Aux);
                }
            CreaTabla();

            TAnalisisS.Columns.Add("Estado","Estado");
            for (int i=0;i<X.Count;i++)
            {
                if(X[i] >= 97 )
                 TAnalisisS.Columns.Add(X[i].ToString(), X[i].ToString());
            }
            TAnalisisS.Columns.Add("$", "$");
            for (int i = 0; i < X.Count; i++)
            {
                if (X[i] < 97)
                    TAnalisisS.Columns.Add(X[i].ToString(), X[i].ToString());
            }
            for (int i=0; i < Conjuntos.Count;i++)
            {
                TAnalisisS.Rows.Add();
            }
            for (int i = 0; i < orAc.Count; i++)
            {
                TAnalisisS.Rows[orAc[i]].Cells[0].Value = orAc[i];
                for (int j=0;j<TAnalisisS.ColumnCount;j++)
                {
                    if(orPri[i].ToString() == TAnalisisS.Columns[j].HeaderText)
                        TAnalisisS.Rows[orAc[i]].Cells[j].Value = Acciones[i];
                }
            }
        }


        private bool Repeticion(List<Gramatica> A, List<Gramatica> B)
        {
            bool repetir = false;
            int igualdad = 0;
            if (A.Count == B.Count)
            {
                for (int dif = 0; dif < B.Count; dif++)
                {
                    if (A[dif].Produc == B[dif].Produc && A[dif].Prim == B[dif].Prim)
                        igualdad++;
                }
                if (igualdad == A.Count)
                    repetir = true;

            }
            else
                repetir = false;


            return repetir;
        }


        private List<Gramatica> CerraduraS(Gramatica C)
        {
            int ApProd = 0;
            char B;

            List<Gramatica> E = new List<Gramatica>();
            ApProd = Producciones.IndexOf(C.Produc);
            for (int j = 0; j < Der[ApProd].Count(); j++)
                if (Der[ApProd][j] >= 65 && Der[ApProd][j] <= 90)
                {
                    B = Der[ApProd][j];
                    if (j - 1 >= 0)
                    {
                        char ant = Der[ApProd][j - 1];
                        if (ant < 65 || ant > 90 && ant == 176)
                            E = AgregaProd(Conjuntos[0], B, Der[ApProd][j + 1]);
                        else
                            E = AgregaProd(Conjuntos[0], B, '$');
                    }
                    else
                        E = AgregaProd(Conjuntos[0], B, '$');

                }
            return E;
        }


        private List<Gramatica> Cerradura(List<Gramatica> E)
        {
            int ApB;
            int ApProd;
            char B;
            for (int i = 0; i < E.Count(); i++)
            {
                ApProd = Producciones.IndexOf(E[i].Produc);
                    if (Der[ApProd][1] >= 65 && Der[ApProd][1] <= 90)
                    {
                        B = Der[ApProd][1];
                        ApB = 1;
                        if (2 != Der[ApProd].Count())
                        {
                            char ant = Der[ApProd][2];
                            if (ant < 65 || ant > 90 && ant == 176)
                                E = AgregaProd(E, B, Der[ApProd][2]);
                            else
                                E = AgregaProd(E, B, '$');
                        }
                        else
                            E = AgregaProd(E, B, '$');
                    }
            }
            return E;
        }

        private List<Gramatica> AgregaProdS(List<Gramatica> E, char B, char Primero)
        {
            bool repeat = false;
            for (int i = 0; i < Producciones.Count; i++)
                if (Izq[i].Count() == 1 && B == Izq[i][0])
                {
                    for (int l = 0; l < E.Count; l++)
                        if (i < E.Count)
                        {
                            if (Producciones[i] == E[l].Produc && E[l].Prim == Primero)
                            {
                                repeat = true;
                                l = E.Count;
                            }
                        }

                    if (!repeat)
                        E.Add(new Gramatica(Producciones[i], Primero));
                }
            return E;
        }

        private List<Gramatica> AgregaProd(List<Gramatica> E, char B, char Primero)
        {
            bool repeat = false;
            for (int i = 0; i < Producciones.Count; i++)
                if (Izq[i].Count() == 1 && B == Izq[i][0])
                {
                    for (int l = 0; l < E.Count; l++)
                        if (i < E.Count)
                            if (E[l].Produc == Producciones[i] && E[i].Prim == Primero)
                            {
                                repeat = true;
                                l = E.Count;
                            }
                    if (!repeat)
                        E.Add(new Gramatica(Producciones[i], Primero));
                }
            return E;
        }

        private Gramatica Ir_a(Gramatica E, char X)
        {
            Gramatica Aux;
            string RePun;

            RePun = E.Produc.Replace("°" + X, X + "°");
            Aux = new Gramatica(RePun, E.Prim);
            //AgregaProd(E, X, E.Prim);

            return Aux;
        }

        private void CreaTabla()
        {
            //Funcion Ir_Falsa
            for (int i = 0; i < Conjuntos.Count(); i++)

                for (int k = 0; k < X.Count; k++)
                {
                    bool term = true;
                    List<Gramatica> Aux = new List<Gramatica>();
                    for (int j = 0; j < Conjuntos[i].Count; j++)
                    {
                        for (int l = 0; l < Conjuntos[i][j].Produc.Count(); l++)
                            if (Conjuntos[i][j].Produc[l] == 176)
                            {

                                if (X[k] > 65 && X[k] < 90)
                                    term = false;
                                if (l + 1 != Conjuntos[i][j].Produc.Count())
                                    if (Conjuntos[i][j].Produc[l + 1] == X[k])
                                    {
                                        Gramatica result = Ir_a(Conjuntos[i][j], X[k]);
                                        Aux.Add(result);
                                    }
                            }
                    }
                    for (int result = 0; result < Aux.Count; result++)
                        for (int rec = 0; rec < Aux[result].Produc.Count(); rec++)
                            if (Aux[result].Produc[rec] == 176)
                                if (rec + 1 < Aux[result].Produc.Count())
                                    if (Aux[result].Produc[rec + 1] >= 65 && Aux[result].Produc[rec + 1] <= 90)
                                    {
                                        Aux = AgregaProdS(Aux, Aux[result].Produc[rec + 1], Aux[result].Prim);
                                    }


                    bool repetir = false;
                    for (int rp = 0; rp < Conjuntos.Count; rp++)
                    {
                        repetir = Repeticion(Conjuntos[rp], Aux);
                        if (repetir)
                        {
                            if (term)
                            {
                                orAc.Add(i);
                                Acciones.Add("d" + rp);
                                orPri.Add(X[k]);
                                break;
                            }
                            else
                            {
                                orAc.Add(i);
                                Acciones.Add("" + rp);
                                orPri.Add(X[k]);
                                break;
                            }
                        }
                    }
                }
            string A;
            string[] div;
            // Producciones sin el punto
            for (int i = 0; i < Producciones.Count; i++)
            {
                A = Producciones[i].Replace("°", "");
                ProSP.Add(A);
            }

            for (int i = 0; i < Conjuntos.Count; i++)
                for (int j = 0; j < Conjuntos[i].Count; j++)
                    for (int k = 0; k < Conjuntos[i][j].Produc.Count(); k++)
                    {
                        //aqui
                        //div = Conjuntos[i][j].Produc.Split('➳');
                        div = SplitFlecha(Conjuntos[i][j].Produc);
                        if (div[0].Length == 1)
                        {
                            if (Conjuntos[i][j].Produc[k] == '°' && k == Conjuntos[i][j].Produc.Count() - 1)
                            {
                                string cambio = Conjuntos[i][j].Produc.Replace("°", "");
                                for (int l = 0; l < ProSP.Count; l++)
                                    if (cambio == ProSP[l])
                                    {
                                        orAc.Add(i);
                                        Acciones.Add("r" + l);
                                        orPri.Add(Conjuntos[i][j].Prim);
                                    }
                            }
                        }
                    }
            orAc.Add(1);
            Acciones.Add("Aceptado");
            orPri.Add('$');

        }


        private void Analiza()
        {
            string Pila ="0";
            textBox2.Text += "$";
            string Vacio = "";
            string Entrada = textBox2.Text;
            int itera = 0;
            dataGridView1.Rows.Add();
            dataGridView1.Rows[itera].Cells[0].Value = Pila;
            dataGridView1.Rows[itera].Cells[1].Value = Entrada;
            bool aceptar = false;

            while (!aceptar)
            {
                string number = "";
                for (int i = 0; i < orAc.Count; i++)
                {
                    if (Pila.Length > 1)
                    {
                        if (!char.IsNumber(Pila[Pila.Count() - 2]))
                        {
                            number = Pila[Pila.Count() - 1].ToString();
                        }
                        else
                        {
                            number = Pila[Pila.Count() - 2].ToString();
                            number += Pila[Pila.Count() - 1].ToString();
                        }
                    }
                    else
                        number = Pila[Pila.Count() - 1].ToString();
                    if (orAc[i].ToString() == number && orPri[i].ToString() == Entrada[0].ToString())

                    {
                        dataGridView1.Rows[itera].Cells[2].Value = Acciones[i];
                        itera++;
                        dataGridView1.Rows.Add();
                        if (Acciones[i][0] == 'd')
                        {
                            Pila += Entrada[0];
                            Entrada = Entrada.Remove(0, 1);
                            if (Acciones[i].Length == 1)
                                Pila += Acciones[i][1];
                            else
                                for (int l = 1; l < Acciones[i].Length; l++)
                                    Pila += Acciones[i][l];
                        }
                        else if (Acciones[i][0] == 'r')
                        {
                            int num, ant = 0, remove = 0;

                            Pila = Pila.Replace(Pila[Pila.Count() - 1].ToString(), Vacio);
                            num = Int32.Parse(Acciones[i][1].ToString());
                            //remove = Pila.IndexOf(ProSP[num][2]);
                            //aqui el error 
                            for (int l = Pila.Length - 1; l > 0; l--)
                                if (Pila[l] == ProSP[num][3])
                                {
                                    remove = l;
                                    break;
                                }
                            Pila = Pila.Remove(remove, Pila.Count() - (remove));
                            if (Pila.Length > 1)
                            {
                                if (!char.IsNumber(Pila[Pila.Count() - 2]) && char.IsNumber(Pila[Pila.Count() - 1]))
                                {
                                    ant = Int32.Parse(Pila[Pila.Count() - 1].ToString());
                                }
                                else
                                {
                                    string Aux = Pila[Pila.Count() - 2].ToString() + Pila[Pila.Count() - 1].ToString();
                                    ant = Int32.Parse(Aux);
                                }
                            }
                            Pila += ProSP[num][0];

                            for (int j = 0; j < orAc.Count; j++)
                            {
                                if (orAc[j].ToString() == ant.ToString() && orPri[j] == ProSP[num][0])
                                {
                                    if (Acciones[j].Length == 1)
                                        Pila += Acciones[j][0].ToString();
                                    else
                                    {
                                        Pila += Acciones[j][0].ToString();
                                        Pila += Acciones[j][1].ToString();
                                    }
                                }
                            }
                        }
                        else if (Acciones[i][0] == 'A')
                        {
                            MessageBox.Show("Aceptada");
                            aceptar = true;
                        }
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[itera].Cells[0].Value = Pila;
                        dataGridView1.Rows[itera].Cells[1].Value = Entrada;
                        break;
                    }
                    if (i == orAc.Count-1)
                    {
                        aceptar = true;
                        MessageBox.Show("Error");
                    }
                }

            }
                
        }


        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            Analiza();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void LR_Canonico_Load(object sender, EventArgs e)
        {

        }

        //aqui
        private void Flecha_Click(object sender, EventArgs e)
        {
            textBox1.Text += "->";
            textBox1.Select(textBox1.Text.Length, 0);
            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "~";
            textBox1.Select(textBox1.Text.Length, 0);
            textBox1.Focus();
        }

        private void Abrir_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
           
            OpenFileDialog Open;
            Open = new OpenFileDialog();
            Open.ShowDialog();
            Open.Filter = "File text|*.txt";
            String s = File.ReadAllText(Open.FileName);
            textBox1.Text += s + "\r\n";
        }

        private void Guardar_Click(object sender, EventArgs e)
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

        
        private string [] SplitFlecha(string f)
        {
            string[] P = new string[2];
            string[] Parte = f.Split('-', '>');

            P[0] = Parte[0];
            P[1] = Parte[2];

            return P;
        }
    }
}
