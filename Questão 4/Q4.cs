using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questão_4
{
    internal class Q4
    {
        static void Quick(int[] vet, int esq, int dir)
        {
            int i = esq, j = dir;
            int pivo = vet[(esq + dir) / 2];

            while (i <= j)
            {
                while (i <= dir && vet[i] < pivo) { i++; }
                while (j >= esq && vet[j] > pivo) { j--; }

                if (i <= j)
                {
                    int tmp = vet[i];
                    vet[i] = vet[j];
                    vet[j] = tmp;

                    i++;
                    j--;
                }
            }

            if (esq < j) Quick(vet, esq, j);
            if (i < dir) Quick(vet, i, dir);
        }

        static void Main(string[] args)
        {

        }
    }
}
