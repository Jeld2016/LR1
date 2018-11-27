using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LR1_Final.Grammar_Stuffs;

namespace LR1_Final.LR1_Stuffs
{
    class C_Expansion_stuffs
    {
        string coming_symbol;
        C_Symbol expansion_symbol;
        List<C_Symbol> gamma;
        List<string> an_A;

        public C_Expansion_stuffs(string a_coming_symb, C_Symbol an_expansion_symb, List<C_Symbol>a_gamma, List<string>another_A) {
            this.coming_symbol = a_coming_symb;
            this.expansion_symbol = new C_Symbol(an_expansion_symb.Symbol, an_expansion_symb.Type_symbol);
            this.gamma = (a_gamma != null) ? new List<C_Symbol>(a_gamma) : null;
            //this.gamma = new List<C_Symbol>(a_gamma);
            this.An_A = another_A;
            //this.An_A = new List<string>(another_A);
        }

        public List<string> An_A { get => an_A; set => an_A = value; }
        internal C_Symbol Expansion_symbol { get => expansion_symbol; set => expansion_symbol = value; }
        internal List<C_Symbol> Gamma { get => gamma; set => gamma = value; }
        internal string Coming_symbol { get => coming_symbol; set => coming_symbol = value; }
    }
}
