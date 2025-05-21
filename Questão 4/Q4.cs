using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questão_4
{
    internal class Q4
    {
        static void Bolha(Jogador[] vet)
        {
            int n = vet.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - 1 - i; j++)
                {
                    bool precisaTrocar = false;

                    if (vet[j].AnoNasc > vet[j + 1].AnoNasc)
                    {
                        precisaTrocar = true;
                    }
                    else if (vet[j].AnoNasc == vet[j + 1].AnoNasc &&
                             Comparar(vet[j].Nome, vet[j + 1].Nome) > 0)
                    {
                        precisaTrocar = true;
                    }

                    if (precisaTrocar)
                    {
                        Jogador temp = vet[j];
                        vet[j] = vet[j + 1];
                        vet[j + 1] = temp;
                    }
                }
            }
        }
        static int Comparar(string a, string b)
        {
            int len = Math.Min(a.Length, b.Length);

            for (int i = 0; i < len; i++)
            {
                if (a[i] < b[i]) return -1;
                if (a[i] > b[i]) return 1;
            }

            if (a.Length < b.Length) return -1;
            if (a.Length > b.Length) return 1;

            return 0;
        }


        static void Main(string[] args)
        {
            List<Jogador> jogadoresList = new List<Jogador>();
            StreamReader ArqL = new StreamReader("players.csv");
            string cabecalho = ArqL.ReadLine();
            string linha = ArqL.ReadLine();
            while (linha != null)
            {
                string[] campos = linha.Split(',');
                Jogador jogador = new Jogador
                {
                    Id = campos[0] == "" ? 0 : int.Parse(campos[0]),
                    Nome = campos[1] == "" ? "nao informado" : campos[1],
                    Altura = campos[2] == "" ? 0 : int.Parse(campos[2]),
                    Peso = campos[3] == "" ? 0 : int.Parse(campos[3]),
                    Universidade = campos[4] == "" ? "nao informado" : campos[4],
                    AnoNasc = campos[5] == "" ? 0 : int.Parse(campos[5]),
                    CidadeNasc = campos[6] == "" ? "nao informado" : campos[6],
                    EstadoNasc = campos[7] == "" ? "nao informado" : campos[7]
                };
                jogadoresList.Add(jogador);
                linha = ArqL.ReadLine();
            }
            ArqL.Close();
            Jogador[] jogadores = jogadoresList.ToArray();
            Bolha(jogadores);
            foreach (Jogador j in jogadores)
            {
                Console.WriteLine(j);
            }
        }
    }
}
