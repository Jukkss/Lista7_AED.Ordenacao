using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questão_5
{
    internal class Pais
    {
        public string Nome { get; set; }
        public int Ouro { get; set; }
        public int Prata { get; set; }
        public int Bronze { get; set; }
        public int Total { get; set; }

        public override string ToString()
        {
            return $"{Nome} - Ouro: {Ouro}, Prata: {Prata}, Bronze: {Bronze}, Total: {Total}";
        }
    }
}
