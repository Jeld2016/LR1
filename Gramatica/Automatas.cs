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
    public partial class Automatas : Form
    {
        List<Nodo> Nodos = new List<Nodo>();
        List<int> ProdAnt = new List<int>();
        List<int> ProdSig = new List<int>();
        List<int> NodosAcep = new List<int>();
        List<int> NodossinF = new List<int>();
        Thomson NodosAFD = new Thomson();
        Thomson NodosAFDMin = new Thomson();
        int R1 = 0, R2 = 1,nodoap=0,nodoOr=0;
        int let = 65, let2 = 66;
        bool par = false, ultimo = false;
        public Automatas()
        {
            InitializeComponent();
        }
        private void Automatas_Load(object sender, EventArgs e)
        {

        }
        private void ConstruccionAFN(string ExpRegular)
        {
            Nodos.Clear();
            ProdAnt.Clear();
            ProdSig.Clear();
            R1 = 0;
            R2 = 1;
            StringBuilder b = new StringBuilder();
            var fileName = "Afn.txt";
            TextWriter tw = new StreamWriter(fileName);
            b.Append("digraph G {" + Environment.NewLine);
            char[] separador = { '(', ')' };
            char com = '"';

            //Separa por parentesis
            string[] Parent = ExpRegular.Split(separador);
            //si no hay parentesis executa solo para el primer termino
            if (Parent.Count() == 1)
                GraphPatent(Parent[0]);

            //Busca todos los terminos dentro y fuera del parentesis
            else
            {
                if (Parent[0] == "")
                {
                    for (int i = 1; i < Parent.Count() - 1; i++)
                    {
                        GraphPatent(Parent[i]);
                        par = true;
                        GraphMult(Parent[i + 1]);
                    }
                }
                else
                {
                    GraphPatent(Parent[1]);
                    for (int k = 0; k < Nodos.Count; k++)
                    {
                        Nodos[k].Name = Nodos[k].Name + 1;
                        for (int j = 0; j < Nodos[k].Relaciones.Count; j++)
                            Nodos[k].Relaciones[j] = Nodos[k].Relaciones[j] + 1;
                    }

                    R1 = 0;
                    R2 = 1;
                    GraphPatent(Parent[0]);
                    Nodos = Nodos.OrderBy(o => o.Name).ToList();
                    if (Parent[2] != "")
                    {
                        R1 = Nodos[Nodos.Count - 1].Name;
                        R2 = R1++;
                        GraphPatent(Parent[2]);
                    }
                    par = true;
                }

            }
            /*for (int i = 0; i < Parent.Count()-1; i++)
            {
                if (Parent[i] != "")
                {
                    GraphPatent(Parent[i]);
                    par = true;
                    GraphMult(Parent[i + 1]);
                }
            }*/

            //Guardar el Archivo con el formato que necesita el GraphViz
            Nodos = Nodos.OrderBy(o => o.Name).ToList();
            for (int i = 0; i < Nodos.Count; i++)
            {
                for (int j = 0; j < Nodos[i].Relaciones.Count; j++)
                {
                    b.AppendFormat("{0}->{1}{2}{3}", com + Nodos[i].Name.ToString() + com, com + Nodos[i].Relaciones[j].ToString() + com, string.Format("[label={0}]", Nodos[i].l), Environment.NewLine);
                }
            }
            b.AppendFormat("{0} [style=dashed]", Nodos[Nodos.Count - 1].Name);
            b.Append("}");
            tw.WriteLine(b.ToString());
            tw.Close();

            //Iniciar los comandos desde el codigo
            string path = Directory.GetCurrentDirectory();
            GeneraGraph(fileName, path);
            //Mostrar el resultado en un PictureBox
            Bitmap image1 = new Bitmap(@"Afn.jpg");
            pictureBox1.Image = image1;
        }
        private void GraphMult(string Cadena)
        {
            if (par)
            {
                if (Cadena.Count() == 1)
                {
                    if (Cadena != "*")
                    {
                        Nodos[Nodos.Count() - 1].Relaciones.Add(R1);
                        Nodos[Nodos.Count() - 1].l = Cadena;
                        Nodos.Add(new Nodo(R1, Cadena.ToString()));
                    }
                    else
                    {
                        GraphAst(Cadena);
                    }
                }
                else
                {
                    for (int i = 0; i < Cadena.Count(); i++)
                    {
                        if (Cadena[i] != '*')
                        {
                            Nodos[Nodos.Count() - 1].Relaciones.Add(R1);
                            Nodos[Nodos.Count() - 1].l = Cadena[i].ToString();
                            Nodos.Add(new Nodo(R1, Cadena[i].ToString()));
                            R1++;
                            R2++;
                        }
                        else
                        {
                            if (i == Cadena.Count() - 2)
                                ultimo = true;
                            GraphAst(Cadena);
                            R1 += 2;
                            R2 = R1 + 1;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < Cadena.Count(); i++)
                {
                    if (Cadena[i] != '*')
                    {
                        Nodos.Add(new Nodo(R1, Cadena[i].ToString()));
                        Nodos[Nodos.Count - 1].Relaciones.Add(R2);
                        R1++;
                        R2++;
                    }
                    else
                    {

                        Nodos.Add(new Nodo(R1, Cadena));
                        GraphAst(Cadena[i].ToString());
                    }
                }
                Nodos.Add(new Nodo(R1, Cadena));

            }
        }
        private void GraphAst(string Cadena)
        {
            if (par)
            {
                Nodos[Nodos.Count() - 1].Relaciones.Add(R1);
                Nodos[Nodos.Count() - 1].Relaciones.Add(0);
                Nodos[Nodos.Count() - 1].l = "£";
                Nodos.Add(new Nodo(R1, Cadena.ToString()));
            }
            else
            {
                Nodos[Nodos.Count() - 1].Relaciones.Add(R2);
                Nodos[Nodos.Count() - 1].Relaciones.Add(0);
                Nodos[Nodos.Count() - 1].l = "£";
                Nodos.Add(new Nodo(R2, Cadena.ToString()));
            }
            for (int k = 0; k < Nodos.Count; k++)
            {
                Nodos[k].Name = Nodos[k].Name + 1;
                for (int j = 0; j < Nodos[k].Relaciones.Count; j++)
                    Nodos[k].Relaciones[j] = Nodos[k].Relaciones[j] + 1;
            }
            Nodos.Add(new Nodo(0, "£"));
            Nodos[Nodos.Count() - 1].Relaciones.Add(Nodos[0].Name);
            Nodos = Nodos.OrderBy(o => o.Name).ToList();
            Nodos[0].Relaciones.Add(Nodos[Nodos.Count() - 1].Name);
            if (!ultimo)
            {
                R1++;
                R2++;
                R1++;
                R2++;
            }
        }
        private void GraphPatent(string Expresion)
        {
            int Or = 0;
            string[] Div = Expresion.Split('|');

            if (Div.Count() > 1)
                for (int i = 0; i < Div.Count(); i++)
                {
                    Or++;
                    ProdAnt.Add(R1);
                    if (Div[i].Count() > 1)
                    {
                        par = false;
                        GraphMult(Div[i]);
                        R1++;
                        R2++;
                    }
                    else
                    {
                        Nodos.Add(new Nodo(R1, Div[i]));
                        Nodos[Nodos.Count - 1].Relaciones.Add(R2);
                        Nodos.Add(new Nodo(R2, Div[i]));
                        R1 += 2;
                        R2 = R1 + 1;
                    }
                    ProdSig.Add(R1);
                    if (Or % 2 == 0)
                    {
                        GraphOr(R2);
                        Or = 1;
                        R1 += 2;
                        R2 = R1 + 1;
                    }
                }
            else
            {
                if (Div[0].Count() > 1)
                {
                    par = false;
                    GraphMult(Div[0]);
                }

                else
                {
                    Nodos.Add(new Nodo(R1, Div[0]));
                    Nodos[Nodos.Count - 1].Relaciones.Add(R2);
                    Nodos.Add(new Nodo(R2, Div[0]));
                }
            }
        }
        private void GraphOr(int Ult)
        {
            Nodos.Add(new Nodo(0, "£"));
            Nodos[Nodos.Count - 1].Relaciones.Add(ProdAnt[0] + 1);
            Nodos[Nodos.Count - 1].Relaciones.Add(ProdAnt[1] + 1);
            for (int k = 0; k < Nodos.Count - 1; k++)
            {
                Nodos[k].Name = Nodos[k].Name + 1;
                for (int j = 0; j < Nodos[k].Relaciones.Count; j++)
                    Nodos[k].Relaciones[j] = Nodos[k].Relaciones[j] + 1;
            }
            Nodos.Add(new Nodo(Ult, "£"));
            Nodos[ProdSig[0] - 1].Relaciones.Add(Nodos.Count - 1);
            Nodos[ProdSig[1] - 1].Relaciones.Add(Nodos.Count - 1);
            Nodos[ProdSig[0] - 1].l = "£";
            Nodos[ProdSig[1] - 1].l = "£";
            ProdAnt.Clear();
            ProdSig.Clear();
            Nodos = Nodos.OrderBy(o => o.Name).ToList();
            ProdAnt.Add(0);
            ProdSig.Add(Nodos[Nodos.Count - 1].Name + 1);
        }
        private static void GeneraGraph(string fileName, string path)
        {
            try
            {
                var comando = string.Format("dot -Tjpg {0} -o {1} -Grankdir=LR", Path.Combine(path, fileName), Path.Combine(path, fileName.Replace(".txt", ".jpg")));

                var IniciaComando = new System.Diagnostics.ProcessStartInfo("cmd", "/C" + comando);

                var proc = new System.Diagnostics.Process();

                proc.StartInfo = IniciaComando;

                proc.Start();

                proc.WaitForExit();

            }
            catch (Exception x)
            {

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ConstruccionAFN(textBox1.Text);
            BotonAFD.Visible = true;
        }
        private void ConvertAFD()
        {
            List<int> Q = new List<int>();
            List<string> E = new List<string>();
            Nodo F = Nodos[Nodos.Count - 1];
            for (int i = 0; i < Nodos.Count; i++)
            {
                Q.Add(Nodos[i].Name);
                if (E.Count == 0)
                    E.Add(Nodos[i].l);
                else
                    for (int j = 0; j < E.Count; j++)
                        if (!E.Contains(Nodos[i].l))
                            E.Add(Nodos[i].l);

            }

            List<int> T = new List<int>();
            T.Add(0);
            T = CerraduraS(T, 0, "");
            let = 0;
            let2 = let;
            for (int j = 0; j < E.Count; j++)
            {
                T = NodosAFD.C[0];
                if (E[j] != "£")
                {
                    mueve(T, E[j], let, E[j]);
                }
            }
            CerraduraA(E, let, "");
            GeneraAFD(F);
        }
        private void BotonAFD_Click(object sender, EventArgs e)
        {
            ConvertAFD();
            MinimizarAFD.Visible = true;
        }
        private List<int> CerraduraS(List<int> T, int let, string rel)
        {
            for (int i = 0; i < T.Count(); i++)
            {
                if (Nodos[T[i]].l == "£" && Nodos[T[i]].visit == true)
                    for (int j = 0; j < Nodos[T[i]].Relaciones.Count; j++)
                    {
                        T.Add(Nodos[T[i]].Relaciones[j]);
                        Nodos[T[i]].visit = false;
                    }
                else
                    if (T[i] + 1 <= Nodos.Count - 1)
                    Nodos[T[i] + 1].visit = false;
            }
            NoRepetir(T, let, rel);
            return T;
        }
        private void CerraduraA(List<string> E, int let, string rel)
        {
            for (int i = 1; i < NodosAFD.C.Count; i++)
                for (int j = 0; j < E.Count; j++)
                {
                    List<int> T = NodosAFD.GetLista(i);
                    if (E[j] != "£")
                    {
                        mueve(T, E[j], i, E[j]);
                    }
                }
        }
        private void NoRepetir(List<int> T, int let, string rel)
        {
            if (NodosAFD.C.Count() >= 1)
            {
                bool Diferente = false;
                int Des = 0;
                for (int i = 0; i < NodosAFD.C.Count; i++)
                {
                    Diferente = NodosAFD.SetLista(i, T);
                    if (Diferente)
                    {
                        Des = i;
                        break;
                    }
                }
                if (!Diferente)
                {
                    NodosAFD.CreaLista(T);
                    let2++;
                    NodosAFD.newNodos(let, let2, rel);
                }
                else
                {
                    let2 = Des;
                    NodosAFD.newNodos(let, let2, rel);
                }
            }
            else
                NodosAFD.CreaLista(T);
        }
        private void mueve(List<int> list, string Simbolo, int let, string rel)
        {
            List<int> l = new List<int>();

            for (int i = 0; i < list.Count; i++)
            {
                if (Nodos[list[i]].l == Simbolo)
                    for (int j = 0; j < Nodos[list[i]].Relaciones.Count; j++)
                    {
                        l.Add(Nodos[list[i]].Relaciones[j]);
                        Nodos[Nodos[list[i]].Relaciones[j]].visit = true;
                    }
            }
            for (int i = 0; i < Nodos.Count; i++)
                Nodos[i].visit = true;
            l = CerraduraS(l, let, rel);
        }
        private void GeneraAFD(Nodo F)
        {
            StringBuilder b = new StringBuilder();
            var fileName = "Afd.txt";
            TextWriter tw = new StreamWriter(fileName);
            b.Append("digraph G {" + Environment.NewLine);
            char[] separador = { '(', ')' };
            char com = '"';

            //Guardar el Archivo con el formato que necesita el GraphViz
            NodosAFD.Nodos = NodosAFD.Nodos.OrderBy(o => o.Name).ToList();
            for (int i = 0; i < NodosAFD.Nodos.Count; i++)
            {
                for (int j = 0; j < NodosAFD.Nodos[i].Relaciones.Count; j++)
                {
                    b.AppendFormat("{0}->{1}{2}{3}", com + NodosAFD.Nodos[i].Name.ToString() + com, com + NodosAFD.Nodos[i].Relaciones[j].ToString() + com, string.Format("[label={0}]", NodosAFD.Nodos[i].l), Environment.NewLine);
                }
            }
            for (int i = 0; i < NodosAFD.C.Count; i++)
                for (int j = 0; j < NodosAFD.C[i].Count; j++)
                {
                    if (NodosAFD.C[i][j] == F.Name)
                    {
                        NodosAcep.Add(i);
                        b.AppendFormat("{0} [style=dashed]", i);
                    }
                    else
                    {
                        bool repeat = false;
                        if (NodossinF.Count != 0)
                        {
                            for (int k = 0; k < NodossinF.Count(); k++)
                                if (i == NodossinF[k])
                                    repeat = true;
                            if(!repeat)
                                NodossinF.Add(i);
                        }
                        else
                            NodossinF.Add(i);
                    }
                }
            for (int i = 0; i < NodosAcep.Count; i++)
                NodossinF.Remove(NodosAcep[i]);
            b.Append("}");
            tw.WriteLine(b.ToString());
            tw.Close();

            //Iniciar los comandos desde el codigo
            string path = Directory.GetCurrentDirectory();
            GeneraGraph(fileName, path);
            //Mostrar el resultado en un PictureBox
            Bitmap image1 = new Bitmap(@"Afd.jpg");
            pictureBox1.Image = image1;
        }
        private void MinimizarAFD_Click(object sender, EventArgs e)
        {
            List<int> Q = new List<int>();
            List<string> E = new List<string>();
            for (int i = 0; i < Nodos.Count; i++)
            {
                Q.Add(Nodos[i].Name);
                if (E.Count == 0)
                    E.Add(Nodos[i].l);
                else
                    for (int j = 0; j < E.Count; j++)
                        if (!E.Contains(Nodos[i].l))
                            E.Add(Nodos[i].l);
            }
            E.RemoveAt(0);
            List<List<int>> G = new List<List<int>>();
            G.Add(NodosAcep);
            G.Add(NodossinF);
            NodosAFDMin.C = G;
            Repetir(E, G);
            GeneraAFDMin();

        }
        private void Repetir(List<string> Letrero, List<List<int>> G)
        {
            bool Particion = false;
            List<int> Aux = new List<int>(); ;
            for (int i = 0; i < Letrero.Count; i++)
            {
                for (int j = 0; j < G.Count; j++)
                    for (int k = 0; k < G[j].Count; k++)
                    {
                        bool repeat = false;
                        for (int cont = 0; cont < NodosAFDMin.Nodos.Count; cont++)
                            if (NodosAFDMin.Nodos[cont].Name == j && NodosAFDMin.Nodos[cont].Relaciones[0] == k && NodosAFDMin.Nodos[cont].l==Letrero[i])
                            {
                                repeat = true;
                                cont = NodosAFDMin.Nodos.Count;
                            }
                        for (int c = 0; c < G.Count; c++)
                            for (int co = 0; co < G[c].Count; co++)
                                if (nodoap == G[c][co])
                                    nodoOr = c;
                        if (!repeat)
                            NodosAFDMin.newNodos(nodoOr,j, Letrero[i]);
                        Particion = Relacion(Letrero[i], G[j][k], j);
                        if (!Particion)
                        {
                            G[G.Count - 1].Remove(G[j][k]);
                            Aux.Add(G[j][k]);
                            G.Add(Aux);
                            NodosAFDMin.C = G;
                            Repetir(Letrero, G);
                        }
                    }
            }
        }
        private bool Relacion(string Letra, int nodin, int act)
        {
            bool par = false;
            int relAct = 0;
            for (int k = 0; k < NodosAFD.C.Count; k++)
                for (int i = 0; i < NodosAFD.Nodos.Count; i++)
                {

                    for (int j = 0; j < NodosAFD.Nodos[i].Relaciones.Count; j++)
                        if (Letra == NodosAFD.Nodos[i].l)
                        {
                            if (k == NodosAFD.Nodos[i].Relaciones[0])
                                relAct++;

                        }
                    if (relAct == NodosAFD.C.Count)
                    {
                        par = true;
                        nodoap = k;
                        i = NodosAFD.Nodos.Count;
                        k = NodosAFD.C.Count;
                    }
                    
                    //for (int l = 0; l < NodosAFDMin.C.Count; l++)
                    //  if (i != act)
                    //NodosAFDMin.newNodos(act, i, Letra.ToString());
                }
            
            return par;
        }
        private void GeneraAFDMin()
        {
            StringBuilder b = new StringBuilder();
            var fileName = "AfdMin.txt";
            TextWriter tw = new StreamWriter(fileName);
            b.Append("digraph G {" + Environment.NewLine);
            char[] separador = { '(', ')' };
            char com = '"';

            //Guardar el Archivo con el formato que necesita el GraphViz
            NodosAFDMin.Nodos = NodosAFDMin.Nodos.OrderBy(o => o.Name).ToList();
            for (int i = 0; i < NodosAFDMin.Nodos.Count; i++)
            {
                for (int j = 0; j < NodosAFDMin.Nodos[i].Relaciones.Count; j++)
                {
                    b.AppendFormat("{0}->{1}{2}{3}", com + NodosAFDMin.Nodos[i].Name.ToString() + com, com + NodosAFDMin.Nodos[i].Relaciones[j].ToString() + com, string.Format("[label={0}]", NodosAFDMin.Nodos[i].l), Environment.NewLine);
                }

            }

            b.AppendFormat("{0} [style=dashed]", NodosAFDMin.Nodos[NodosAFDMin.Nodos.Count - 1].Name);

            b.Append("}");
            tw.WriteLine(b.ToString());
            tw.Close();

            //Iniciar los comandos desde el codigo
            string path = Directory.GetCurrentDirectory();
            GeneraGraph(fileName, path);
            //Mostrar el resultado en un PictureBox
            Bitmap image1 = new Bitmap(@"AfdMin.jpg");
            pictureBox1.Image = image1;
        }
    }
}
