using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questão_6
{

    internal class Q6
    {
        // Ordenações
        static void Main(string[] args)
        {
            int[] original = new int[] { 10, 1, 3, 20, 5, 6, 1, 4, 9, 2 };

            Console.WriteLine("=== Seleção ===");
            int[] vet1 = (int[])original.Clone();
            Selecao(vet1, vet1.Length);
            Console.WriteLine();

            Console.WriteLine("=== Inserção ===");
            int[] vet2 = (int[])original.Clone();
            Insercao(vet2, vet2.Length);
            Console.WriteLine();

            Console.WriteLine("=== Bolha ===");
            int[] vet3 = (int[])original.Clone();
            Bolha(vet3, vet3.Length);
            Console.WriteLine();

            Console.WriteLine("=== Quick ===");
            int[] vet4 = (int[])original.Clone();
            Quick(vet4, 0, vet4.Length - 1);
            Console.WriteLine();

            Console.WriteLine("=== Merge ===");
            int[] vet5 = (int[])original.Clone();
            MergeSort(vet5, 0, vet5.Length - 1);
            Console.WriteLine();

            Console.WriteLine("=== Heap ===");
            int[] vet6 = (int[])original.Clone();
            HeapSort(vet6, vet6.Length);
            Console.WriteLine();

            Console.WriteLine("=== Counting ===");
            int[] vet7 = (int[])original.Clone();
            CountingSort(vet7, vet7.Length);
            Console.WriteLine();
        }
        static void Imprime(int[] vet)
        {
            Console.WriteLine(string.Join(", ", vet));
        }
        static void Selecao(int[] vet, int n)
        {
            for (int i = 0; i < n - 1; i++)
            {
                int menor = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (vet[j] < vet[menor])
                    {
                        menor = j;
                    }
                }
                int tmp = vet[menor];
                vet[menor] = vet[i];
                vet[i] = tmp;
                Imprime(vet);
            }
        }
        static void Insercao(int[] vet, int n)
        {
            for (int i = 1; i < n; i++)
            {
                int tmp = vet[i];
                int j = i - 1;
                while (j >= 0 && vet[j] > tmp)
                {
                    vet[j + 1] = vet[j];
                    j--;
                }
                vet[j + 1] = tmp;
                Imprime(vet);
            }
        }
        static void Bolha(int[] vet, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = n - 1; j > i; j--)
                {
                    if (vet[j] < vet[j - 1])
                    {
                        int tmp = vet[j];
                        vet[j] = vet[j - 1];
                        vet[j - 1] = tmp;
                        Imprime(vet);
                    }
                }
            }
        }
        static void Quick(int[] vet, int esq, int dir)
        {
            int i = esq;
            int j = dir;
            int pivo = vet[(esq + dir) / 2];
            while (i <= j)
            {
                while (i <= dir && vet[i] < pivo) i++;
                while (j >= esq && vet[j] > pivo) j--;
                if (i <= j)
                {
                    int tmp = vet[i];
                    vet[i] = vet[j];
                    vet[j] = tmp;
                    Imprime(vet);
                    i++;
                    j--;
                }
            }
            if (esq < j) Quick(vet, esq, j);
            if (i < dir) Quick(vet, i, dir);
        }
        static void Merge(int[] vet, int esq, int meio, int dir)
        {
            int n1 = meio - esq + 1;
            int n2 = dir - meio;
            int[] L = new int[n1];
            int[] R = new int[n2];
            for (int x = 0; x < n1; x++) L[x] = vet[esq + x];
            for (int y = 0; y < n2; y++) R[y] = vet[meio + 1 + y];
            int k = esq, i = 0, j = 0;
            while (i < n1 && j < n2)
            {
                if (L[i] <= R[j])
                    vet[k++] = L[i++];
                else
                    vet[k++] = R[j++];
                Imprime(vet);
            }
            while (i < n1)
            {
                vet[k++] = L[i++];
                Imprime(vet);
            }
            while (j < n2)
            {
                vet[k++] = R[j++];
                Imprime(vet);
            }
        }
        static void MergeSort(int[] vet, int esq, int dir)
        {
            if (esq < dir)
            {
                int meio = (esq + dir) / 2;
                MergeSort(vet, esq, meio);
                MergeSort(vet, meio + 1, dir);
                Merge(vet, esq, meio, dir);
            }
        }
        static void Ajustar(int[] vet, int n, int i)
        {
            int maior = i;
            int e = 2 * i + 1;
            int d = 2 * i + 2;
            if (e < n && vet[e] > vet[maior]) maior = e;
            if (d < n && vet[d] > vet[maior]) maior = d;
            if (maior != i)
            {
                int tmp = vet[i];
                vet[i] = vet[maior];
                vet[maior] = tmp;
                Imprime(vet);
                Ajustar(vet, n, maior);
            }
        }
        static void HeapSort(int[] vet, int n)
        {
            for (int i = n / 2 - 1; i >= 0; i--)
                Ajustar(vet, n, i);
            for (int i = n - 1; i > 0; i--)
            {
                int tmp = vet[0];
                vet[0] = vet[i];
                vet[i] = tmp;
                Imprime(vet);
                Ajustar(vet, i, 0);
            }
        }
        static void CountingSort(int[] vet, int n)
        {
            int max = GetMaior(vet, n);
            int[] count = new int[max + 1];
            for (int i = 0; i < n; i++)
                count[vet[i]]++;
            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];
            int[] output = new int[n];
            for (int i = n - 1; i >= 0; i--)
            {
                output[count[vet[i]] - 1] = vet[i];
                count[vet[i]]--;
                Imprime(output);
            }
            for (int i = 0; i < n; i++)
                vet[i] = output[i];
            Imprime(vet);
        }
        static int GetMaior(int[] vet, int n)
        {
            int maior = vet[0];
            for (int i = 1; i < n; i++)
                if (vet[i] > maior) maior = vet[i];
            return maior;
        }
    }
}
