using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questão2
{
    internal class Q3
    {
        // Algoritmos de ordenação
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
        void CountingSort(int[] vet, int n)
        {
            int[] count = new int[GetMaior(vet, n) + 1];
            int[] ordenado = new int[n];

            for (int i = 0; i < n; i++)
            {
                count[vet[i]]++;
            }

            for (int i = 1; i < count.Length; i++)
            {
                count[i] += count[i - 1];
            }

            for (int i = n - 1; i >= 0; i--)
            {
                ordenado[count[vet[i]] - 1] = vet[i];
                count[vet[i]]--;
            }

            for (int i = 0; i < n; i++)
            {
                vet[i] = ordenado[i];
            }
        }
        int GetMaior(int[] vet, int n)
        {
            int maior = vet[0];
            for (int i = 1; i < n; i++)
            {
                if (vet[i] > maior)
                {
                    maior = vet[i];
                }
            }
            return maior;
        }

        static void Main(string[] args)
        {

        }
    }
}
