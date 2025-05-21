using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questão_5
{
    internal class Q5
    {
        static void Bolha(Pais[] paises)
        {
            for (int i = 0; i < paises.Length - 1; i++)
            {
                for (int j = 0; j < paises.Length - i - 1; j++)
                {
                    if (Comparar(paises[j], paises[j + 1]) > 0)
                    {
                        Pais temp = paises[j];
                        paises[j] = paises[j + 1];
                        paises[j + 1] = temp;
                    }
                }
            }
        }
        static int Comparar(Pais a, Pais b)
        {
            if (a.Ouro != b.Ouro)
                return b.Ouro - a.Ouro;

            if (a.Prata != b.Prata)
                return b.Prata - a.Prata;

            if (a.Bronze != b.Bronze)
                return b.Bronze - a.Bronze;

            int minLen = a.Nome.Length < b.Nome.Length ? a.Nome.Length : b.Nome.Length;
            for (int i = 0; i < minLen; i++)
            {
                if (a.Nome[i] != b.Nome[i])
                    return a.Nome[i] - b.Nome[i];
            }
            return a.Nome.Length - b.Nome.Length;
        }
        static void Main(string[] args)
        {
            List<Pais> paises = new List<Pais>();
            using (StreamReader ArqL = new StreamReader("olimpiadas.txt"))
            {
                while (true)
                {
                    string nome = ArqL.ReadLine();
                    string ouroStr = ArqL.ReadLine();
                    string prataStr = ArqL.ReadLine();
                    string bronzeStr = ArqL.ReadLine();
                    string totalStr = ArqL.ReadLine();

                    if (nome == null || ouroStr == null || prataStr == null || bronzeStr == null || totalStr == null)
                        break;
                    Pais p = new Pais
                    {
                        Nome = nome,
                        Ouro = int.Parse(ouroStr),
                        Prata = int.Parse(prataStr),
                        Bronze = int.Parse(bronzeStr),
                        Total = int.Parse(totalStr)
                    };

                    paises.Add(p);
                }
            }

            Pais[] vetor = paises.ToArray();
            Bolha(vetor);

            foreach (Pais p in vetor)
            {
                Console.WriteLine(p);
            }
        }
    }
}
