using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questão2
{
    internal class Q3
    {
        // Preenchimento do vetor
        static void PreencherInt(int[] vet, int n)
        {
            Random r = new Random(10);
            for (int i = 0; i < n; i++)
            {
                vet[i] = r.Next(1, 100);
            }
        }

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
        static void CountingSort(int[] vet, int n)
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
        static int GetMaior(int[] vet, int n)
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
            Stopwatch stopWatch = new Stopwatch();
            TimeSpan ts;
            string elapsedTime;
            int[] vet = new int[1000000];

            // ------------------------- QUICK -------------------------
            PreencherInt(vet, vet.Length);
            Console.WriteLine("INT---QUICK---ALT");
            stopWatch.Start();
            Quick(vet, 0, vet.Length - 1);
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Tempo " + elapsedTime);
            stopWatch.Reset();


            // ------------------------- COUNTING -------------------------
            PreencherInt(vet, vet.Length);
            Console.WriteLine("INT---COUNTING---ALT");
            stopWatch.Start();
            CountingSort(vet, vet.Length);
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("Tempo " + elapsedTime);
            stopWatch.Reset();
        }
    }
}
