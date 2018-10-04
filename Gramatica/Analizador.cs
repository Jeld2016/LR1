using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gramatica
{
    public partial class Analizador : Form
    {
        string[] aux;
        char w;
        List<string> Producciones = new List<string>();
        List<Simbolos> primero = new List<Simbolos>();
        List<Simbolos> siguiente = new List<Simbolos>();
        List<String> Terminales = new List<string>();
        List<String> NTerminales = new List<string>();

        public Analizador()
        {
            InitializeComponent();
        }

        private void calculaPrimero(string c, string sss)
        {
            char[] aux2;
            char[] r = { '|' };
            string[] aux1;

            bool insert = false, w = false;

            aux1 = c.Split(r);
            foreach (string ss in aux1)
            {
                aux2 = ss.ToCharArray();
                int ind = 0;
                if (Char.IsUpper(aux2[0]))
                {
                    if (ss.Length > 1)
                    {
                        if (ss[1] != '´')
                        {
                            foreach (string s in aux)
                            {
                                if (Char.IsUpper(ss[ind]) || ind == 0)
                                {
                                    if (s[0] == ss[ind])
                                    {
                                        string cadenita = s.Substring(s.IndexOf("➳") + 1);
                                        calculaPrimero(cadenita.Replace('£', ' '), sss);
                                        if (s.Contains("£"))
                                            ind++;
                                        w = true;
                                    }
                                }
                                else
                                    calculaPrimero(ss[ind].ToString(), sss);
                            }
                            if (w)
                            {
                                calculaPrimero(ss[0].ToString(), sss);
                                w = false;
                            }
                        }
                        else
                        {
                            foreach (string s in aux)
                            {
                                if (s[0] == ss[0] && s[1] == ss[1])
                                {
                                    calculaPrimero(s.Substring(s.IndexOf("➳") + 1), sss);
                                    w = true;
                                }
                            }
                            if (!w)
                            {
                                calculaPrimero(ss[0].ToString(), sss);
                            }
                        }
                    }


                }
                else
                {
                    try
                    {
                        foreach (Simbolos sim in primero)
                        {
                            if (sim.cadena == sss.Substring(0, sss.IndexOf("➳")))
                            {
                                if (!sim.list.Contains(aux2[0].ToString()))
                                {
                                    sim.list.Add(aux2[0].ToString());
                                    insert = true;
                                }
                                else
                                {
                                    insert = true;
                                }
                            }
                        }
                        if (!insert)
                        {
                            primero.Add(new Simbolos(sss.Substring(0, sss.IndexOf("➳"))));
                            primero.Last().list.Add(aux2[0].ToString());
                        }
                        else insert = false;
                    }
                    catch (Exception e) { }
                }
            }
        }

        private void factoriza()
        {
            string aux2;
            int n;
            foreach (string s in aux)
            {
                if (s.Contains("|"))
                {
                    aux2 = s.Substring(s.IndexOf("➳") + 1);
                    w = s[0];
                    if (aux2.IndexOf("|") != aux2.LastIndexOf("|"))
                    {
                        //varios(aux2.Split('|'));
                    }
                    else
                    {
                        solo2(aux2.Split('|'), 1);
                    }
                }
            }
        }

        private void calculaSiguiente(string c, string sss)
        {
            string[] aux1;
            bool insert = false;

            aux1 = c.Split('|');
            if (siguiente.Count == 0)
            {
                try
                {
                    foreach (Simbolos sim in siguiente)
                    {
                        if (sim.cadena == sss.Substring(0, sss.IndexOf("➳")))
                        {
                            if (!sim.list.Contains("$"))
                            {
                                sim.list.Add("$");
                                insert = true;
                            }
                            else
                            {
                                insert = true;
                            }
                        }
                    }
                    if (!insert)
                    {
                        siguiente.Add(new Simbolos(sss[0].ToString()));
                        siguiente.Last().list.Add("$");
                    }
                    else insert = false;
                }
                catch (Exception e) { }
            }

            foreach (string s in aux1)
            {
                if (s.Length > 1)
                {
                    if (s[1] != '\r')
                    {
                        if (Char.IsUpper(s[1]))
                        {
                            foreach (Simbolos a in primero)
                            {
                                if (a.cadena[0] == s[1])
                                {
                                    foreach (string w in a.list)
                                    {
                                        if (w[0] == '£')
                                        {
                                            foreach (string sa in aux)
                                            {
                                                if (sa[0] == s[2] && Char.IsUpper(s[2]))
                                                {
                                                    calculaSiguiente(sa.Substring(sa.IndexOf("➳") + 1), sss);
                                                }
                                                else if (Char.IsLower(s[2]))
                                                {
                                                    try
                                                    {
                                                        foreach (Simbolos sim in siguiente)
                                                        {
                                                            if (sim.cadena == sss.Substring(0, sss.IndexOf("➳")))
                                                            {
                                                                if (!sim.list.Contains(s[2].ToString()))
                                                                {
                                                                    sim.list.Add(s[2].ToString());
                                                                    insert = true;
                                                                }
                                                                else
                                                                {
                                                                    insert = true;
                                                                }
                                                            }
                                                        }
                                                        if (!insert)
                                                        {
                                                            siguiente.Add(new Simbolos(sss[0].ToString()));
                                                            siguiente.Last().list.Add(s[2].ToString());
                                                        }
                                                    }
                                                    catch (Exception e) { }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                foreach (Simbolos sim in siguiente)
                                                {
                                                    if (sim.cadena == sss.Substring(0, sss.IndexOf("➳")))
                                                    {
                                                        if (!sim.list.Contains(w.ToString()))
                                                        {
                                                            sim.list.Add(w.ToString());
                                                            insert = true;
                                                        }
                                                        else
                                                        {
                                                            insert = true;
                                                        }
                                                    }
                                                }
                                                if (!insert)
                                                {
                                                    siguiente.Add(new Simbolos(sss[0].ToString()));
                                                    siguiente.Last().list.Add(w);
                                                }
                                                else insert = false;
                                            }
                                            catch (Exception e) { }
                                        }
                                    }
                                }
                            }
                        }
                        else if (s[1] != '£')
                        {
                            try
                            {
                                foreach (Simbolos sim in siguiente)
                                {
                                    if (sim.cadena == sss.Substring(0, sss.IndexOf("➳")))
                                    {
                                        if (!sim.list.Contains(s[1].ToString()))
                                        {
                                            sim.list.Add(s[1].ToString());
                                            insert = true;
                                        }
                                        else
                                        {
                                            insert = true;
                                        }
                                    }
                                }
                                if (!insert)
                                {
                                    siguiente.Add(new Simbolos(sss[0].ToString()));
                                    siguiente.Last().list.Add(s[1].ToString());
                                }
                                else insert = false;
                            }
                            catch (Exception e) { }
                        }
                    }
                    else
                    {
                        if (s[0] == '£')
                        {
                            foreach (string ra in aux)
                            {
                                if (ra.Contains(sss.Substring(0, sss.IndexOf("➳"))))
                                {
                                    for (int i = 0; i < siguiente.Count - 1; i++)
                                    {
                                        Simbolos sac = siguiente[i];
                                        if (ra[0].ToString() == sac.cadena)
                                        {
                                            foreach (string sabe in sac.list)
                                            {
                                                try
                                                {
                                                    foreach (Simbolos sim in siguiente)
                                                    {
                                                        if (sim.cadena == sss.Substring(0, sss.IndexOf("➳")))
                                                        {
                                                            if (!sim.list.Contains(sabe[0].ToString()))
                                                            {
                                                                sim.list.Add(sabe[0].ToString());
                                                                insert = true;
                                                            }
                                                            else
                                                            {
                                                                insert = true;
                                                            }
                                                        }
                                                    }
                                                    if (!insert)
                                                    {
                                                        siguiente.Add(new Simbolos(sss[0].ToString()));
                                                        siguiente.Last().list.Add(sabe[0].ToString());
                                                    }
                                                    else insert = false;
                                                }
                                                catch (Exception e) { }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                foreach (Simbolos sim in siguiente)
                                {
                                    if (sim.cadena == sss.Substring(0, sss.IndexOf("➳")))
                                    {
                                        if (!sim.list.Contains("$"))
                                        {
                                            sim.list.Add("$");
                                            insert = true;
                                        }
                                        else
                                        {
                                            insert = true;
                                        }
                                    }
                                }
                                if (!insert)
                                {
                                    siguiente.Add(new Simbolos(sss[0].ToString()));
                                    siguiente.Last().list.Add("$");
                                }
                                else insert = false;
                            }
                            catch (Exception e) { }
                        }
                    }
                }
                else
                {
                    try
                    {
                        foreach (Simbolos sim in siguiente)
                        {
                            if (sim.cadena == sss.Substring(0, sss.IndexOf("➳")))
                            {
                                if (!sim.list.Contains("$"))
                                {
                                    sim.list.Add("$");
                                    insert = true;
                                }
                                else
                                {
                                    insert = true;
                                }
                            }
                        }
                        if (!insert)
                        {
                            siguiente.Add(new Simbolos(sss[0].ToString()));
                            siguiente.Last().list.Add("$");
                        }
                        else insert = false;
                    }
                    catch (Exception e) { }
                }
            }
        }

        private void solo2(string[] alpha, int x)
        {
            if (alpha[0][x - 1] == alpha[1][x - 1])
            {
                solo2(alpha, x + 1);
            }
            else
            {

                aux[aux.Length + 1] = w + "´" + "➳" + alpha[0].Substring(x - 1) + "|£";
                aux[aux.Length + 1] = w + "´" + "➳" + alpha[1].Substring(x - 1) + " |£";
            }
        }

        private void BFactorizacion_Click(object sender, EventArgs e)
        {
            aux = textBox1.Text.Split('\n');
            factoriza();


        }

        private void Siguiente_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            //button2.Visible = true;
            aux = textBox1.Text.Split('\n');
            foreach (string s in aux)
            {
                calculaPrimero(s.Substring(s.IndexOf("➳") + 1), s);
            }
            textBox2.Text = "Primero:";
            foreach (Simbolos s in primero)
            {
                textBox2.Text += "\r\n" + s.cadena + ": ";
                foreach (string x in s.list)
                {
                    textBox2.Text += x.ToString() + " ";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (string s in aux)
            {
                calculaSiguiente(s.Substring(s.IndexOf("→") + 1), s);
            }
            textBox2.Text = "\r\n Siguiente:";
            foreach (Simbolos s in siguiente)
            {
                textBox2.Text += "\r\n" + s.cadena + ": ";
                foreach (string x in s.list)
                {
                    textBox2.Text += x.ToString() + " ";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool iguales = false;
            String[] subMatriz;
            Producciones = textBox1.Text.Split(new[] { "\r\n" },//Buscar las Producciones que se escribieron
                 StringSplitOptions.RemoveEmptyEntries).ToList();
            //leer todas las produciones existentes 
            foreach (string Produccion in Producciones)
            {

                subMatriz = Produccion.Split('➳');//Dividir la produccion en izquierda y derecha
                //leer las letras de la subdivision de la izquierda
                foreach (char Letra in subMatriz[1])
                {
                    int Let = Letra;
                    if (Let >= 65 && Let <= 90 || Letra == '|' || Letra == ' ' || Letra == '£') { }
                    else// Revisa si es un terminal
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
                }
                foreach (char Letra in subMatriz[0])
                {
                    if (NTerminales.Count != 0) // Asegurarse de que no se repitan los terminos
                    {
                        for (int j = 0; j < NTerminales.Count; j++)
                        {
                            if (NTerminales[j] != Letra.ToString()) ;
                            else
                                iguales = true;
                        }
                        if (!iguales)
                            NTerminales.Add(Letra.ToString());
                        iguales = false;
                    }
                    else
                        NTerminales.Add(Letra.ToString());
                }
            }

            dataGridView1.Columns.Add("", "");
            for (int i = 0; i < Terminales.Count; i++)
            {  
                dataGridView1.Columns.Add(Terminales[i], Terminales[i]);
            }
            for (int i = 0; i < NTerminales.Count; i++)
            {
                dataGridView1.Rows.Add(NTerminales[i]);
                int prod=0;
                List<string> c= new List<string>();
                for (int j = 0; j < primero.Count; j++)
                {
                    if (primero[j].cadena == NTerminales[i])
                        prod = j;
                }
                for (int j = 0; j < Terminales.Count; j++)
                {
                    for(int k =0; k < primero[prod].list.Count;k++)
                    if(Terminales[j] == primero[prod].list[k])
                        {
                            dataGridView1.Rows[i].Cells[j+1].Value = Producciones[prod];
                            c.Add(Producciones[prod]);
                        }
                }
            }

        }
    }
}
