using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questão_4
{
    internal class Jogador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Altura { get; set; }
        public int Peso { get; set; }
        public string Universidade { get; set; }
        public int AnoNasc { get; set; }
        public string CidadeNasc { get; set; }
        public string EstadoNasc { get; set; }

        public override string ToString()
            {
                return $"{Id} {Nome} {Altura} {Peso} {AnoNasc} {Universidade} {CidadeNasc} {EstadoNasc}";
            }
    }
}
