using System;
using System.Diagnostics;
using System.Security.Principal;
class Program
{
    static void PreencherInt(int[] vet, int n)
    {
        Random r = new Random(10);
        for (int i = 0; i < n; i++)
        {
            vet[i] = r.Next(1, 10);
        }
    }

    // Algoritmos de ordenação
    static void Selecao(int[] vet, int n, out long comps, out long movs)
    {
        comps = 0; movs = 0;
        for (int i = 0; i < (n - 1); i++)
        {
            int menor = i;
            for (int j = (i + 1); j < n; j++)
            {
                if (vet[menor] > vet[j]) comps++;
                {
                    menor = j;
                }
            }
            int temp = vet[menor]; movs++;
            vet[menor] = vet[i]; movs++;
            vet[i] = temp; movs++;
        }
    }
    static void Insercao(int[] vet, int n, out long comps, out long movs)
    {
        comps = 0; movs = 0;
        for (int i = 1; i < n; i++)
        {
            int tmp = vet[i]; 
            int j = i - 1;
            while ((j >= 0) && (vet[j] > tmp)) 
            {
                comps++;
                vet[j+1] = vet[j]; movs++;
                j--;
            }
            vet[j+1] = tmp; movs++;
        }
    }
    static void Bolha(int[] vet, int n, out long movs, out long comps)
    {
        comps = 0; movs = 0;
        int temp;
        for (int i = 0; i < n; i++)
        {
            for (int j = n-1; j>i; j--)
            {
                if (vet[j] > vet[j - 1]) 
                {
                    comps++;

                    temp = vet[j]; movs++;
                    vet[j] = vet[j - 1]; movs++;
                    vet[j - 1] = temp; movs++;
                }
            }
        }
    }
    static void Quick(int[] vet, int esq, int dir, ref long comps, ref long movs)
    {
        int i = esq, j = dir;
        int pivo = vet[(esq + dir) / 2];

        while (i <= j)
        {
            while (i <= dir && vet[i] < pivo) { i++; comps++; }
            while (j >= esq && vet[j] > pivo) { j--; comps++; }

            if (i <= j)
            {
                int tmp = vet[i]; movs++;
                vet[i] = vet[j]; movs++;
                vet[j] = tmp; movs++;

                i++;
                j--;
            }
        }

        if (esq < j) Quick(vet, esq, j, ref comps, ref movs);
        if (i < dir) Quick(vet, i, dir, ref comps, ref movs);
    }
    static void Merge(int[] array, int esquerda, int meio, int direita, ref long comps, ref long movs)
    {
        int n1 = meio - esquerda + 1;
        int n2 = direita - meio;

        int[] L = new int[n1];
        int[] R = new int[n2];

        for (int i = 0; i < n1; i++) { L[i] = array[esquerda + i]; movs++; }
        for (int j = 0; j < n2; j++) { R[j] = array[meio + 1 + j]; movs++; }

        int k = esquerda, x = 0, y = 0;

        while (x < n1 && y < n2)
        {
            comps++;
            if (L[x] <= R[y])
            {
                array[k] = L[x]; x++;
            }
            else
            {
                array[k] = R[y]; y++;
            }
            movs++;
            k++;
        }

        while (x < n1)
        {
            array[k] = L[x]; x++; k++; movs++;
        }

        while (y < n2)
        {
            array[k] = R[y]; y++; k++; movs++;
        }
    }

    static void MergeSort(int[] array, int esquerda, int direita, ref long comps, ref long movs)
    {
        if (esquerda < direita)
        {
            int meio = (esquerda + direita) / 2;

            MergeSort(array, esquerda, meio, ref comps, ref movs);
            MergeSort(array, meio + 1, direita, ref comps, ref movs);
            Merge(array, esquerda, meio, direita, ref comps, ref movs);
        }
    }
    static void Ajustar(int[] array, int n, int i, ref long comps, ref long movs)
    {
        int maior = i;
        int esquerda = 2 * i + 1;
        int direita = 2 * i + 2;

        if (esquerda < n)
        {
            comps++;
            if (array[esquerda] > array[maior])
                maior = esquerda;
        }

        if (direita < n)
        {
            comps++;
            if (array[direita] > array[maior])
                maior = direita;
        }

        if (maior != i)
        {
            int temp = array[i]; movs++;
            array[i] = array[maior]; movs++;
            array[maior] = temp; movs++;

            Ajustar(array, n, maior, ref comps, ref movs);
        }
    }
    static void HeapSort(int[] array, int n, ref long comps, ref long movs)
    {
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            Ajustar(array, n, i, ref comps, ref movs);
        }

        for (int i = n - 1; i > 0; i--)
        {
            int temp = array[0]; movs++;  
            array[0] = array[i]; movs++;
            array[i] = temp; movs++;

            Ajustar(array, i, 0, ref comps, ref movs);
        }
    }





    static void Main(string[] args)
    {
        Stopwatch stopWatch = new Stopwatch();

        Console.WriteLine("|---MENU (1)---|\n(1). Inteiros\n(2). Decimais\n(3). Sair");
        int op1 = int.Parse(Console.ReadLine());
        do
        {
            switch (op1) // Decimais ou inteiros
            {
                case 1: // Inteiros
                    int[] vetI;
                    Console.WriteLine("|---MENU (2)---|\n(1). 1.000 elementos\n(2). 500.000 elementos\n(3). Sair");
                    int op2 = int.Parse(Console.ReadLine());
                    do
                    {
                        // Declarando o contador 
                        TimeSpan ts;
                        string elapsedTime;
                        long comps, movs;

                        switch (op2) // Quantidade de elementos
                        {

                            case 1: // 1.000 elementos
                                vetI = new int[1000];

                                // Seleção
                                PreencherInt(vetI, vetI.Length);
                                Console.WriteLine("----SELEÇÃO----");
                                stopWatch.Start();
                                Selecao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // Inserção
                                PreencherInt(vetI, vetI.Length);
                                Console.WriteLine("----INSERÇÃO----");
                                stopWatch.Start();
                                Insercao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // Bolha
                                PreencherInt(vetI, vetI.Length);
                                Console.WriteLine("----BOLHA----");
                                stopWatch.Start();
                                Bolha(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // Quick
                                PreencherInt(vetI, vetI.Length);
                                int esq = 0, dir = vetI.Length - 1;
                                Console.WriteLine("----QUICK----");
                                stopWatch.Start();
                                movs = 0; comps = 0;
                                Quick(vetI, esq, dir, ref comps, ref movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // Merge
                                PreencherInt(vetI, vetI.Length);
                                esq = 0; dir = vetI.Length - 1;
                                Console.WriteLine("----MERGE----");
                                stopWatch.Start();
                                movs = 0; comps = 0;
                                MergeSort(vetI, esq, dir, ref comps, ref movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // Heap
                                PreencherInt(vetI, vetI.Length);
                                esq = 0; dir = vetI.Length - 1;
                                Console.WriteLine("----HEAP----");
                                stopWatch.Start();
                                movs = 0; comps = 0;
                                HeapSort(vetI, vetI.Length, ref comps, ref movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                break;

                            case 2: // 500.000 elementos
                                vetI = new int[500000];

                                // Seleção
                                PreencherInt(vetI, vetI.Length);
                                Console.WriteLine("----SELEÇÃO----");
                                stopWatch.Start();
                                Selecao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // Inserção
                                PreencherInt(vetI, vetI.Length);
                                Console.WriteLine("----INSERÇÃO----");
                                stopWatch.Start();
                                Insercao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // Bolha
                                PreencherInt(vetI, vetI.Length);
                                Console.WriteLine("----BOLHA----");
                                stopWatch.Start();
                                Bolha(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // Quick
                                PreencherInt(vetI, vetI.Length);
                                esq = 0;  dir = vetI.Length - 1;
                                Console.WriteLine("----QUICK----");
                                stopWatch.Start();
                                movs = 0; comps = 0;
                                Quick(vetI, esq, dir, ref comps, ref movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // Merge
                                PreencherInt(vetI, vetI.Length);
                                esq = 0; dir = vetI.Length - 1;
                                Console.WriteLine("----MERGE----");
                                stopWatch.Start();
                                movs = 0; comps = 0;
                                MergeSort(vetI, esq, dir, ref comps, ref movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // Heap
                                PreencherInt(vetI, vetI.Length);
                                esq = 0; dir = vetI.Length - 1;
                                Console.WriteLine("----HEAP----");
                                stopWatch.Start();
                                movs = 0; comps = 0;
                                HeapSort(vetI, vetI.Length, ref comps, ref movs);
                                stopWatch.Stop();

                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                break;
                        }

                        if (op2 != 3)
                        {
                            Console.WriteLine("|---MENU (2)---|\n(1). 1.000 elementos\n(2). 500.000 elementos\n(3). Sair");
                            op2 = int.Parse(Console.ReadLine());
                        }
                        else
                        {
                            Console.WriteLine("Voltando...");
                        }

                    } while (op2 != 3);
                break;
                case 2: // Decimais
                    double[] vetD;
                break;
            }
            if (op1 != 3)
            {
                Console.WriteLine("|---MENU (1)---|\n(1). Inteiros\n(2). Decimais\n(3). Sair");
                op1 = int.Parse(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Saindo...");
            }

        } while (op1!= 3);
    }
}
