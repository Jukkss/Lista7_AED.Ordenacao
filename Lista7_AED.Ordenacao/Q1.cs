using System;
using System.Diagnostics;
using System.Security.Principal;
class Program
{
    // Prenchimento dos vetorews INT
    static void PreencherIntAlt(int[] vet, int n)
    {
        Random r = new Random(10);
        for (int i = 0; i < n; i++)
        {
            vet[i] = r.Next(1, 10);
        }
    }
    static void PreencherIntCres(int[] vet, int n)
    {
        for (int i = 0; i < n; i++)
        {
            vet[i] = i;
        }
    }
    static void PreencherIntDecs(int[] vet, int n)
    {
        for (int i = 0; i < n; i++)
        {
            vet[i] = n - i;
        }
    }

    // Prenchimento dos vetorews DOUBLE
    static void PreecnherDoubleAlt(double[] vet, int n)
    {
        Random r = new Random(10);
        for(int i = 0; i < n; i++)
        {
            vet[i] = r.NextDouble() * 9 + 1; 
        }
    }
    static void PreecnherDoubleCres(double[] vet, int n)
    {
        for (int i = 0; i < n; i++)
        {
            vet[i] = i;
        }
    }
    static void PreecnherDoubleDecs(double[] vet, int n)
    {
        for (int i = 0; i < n; i++)
        {
            vet[i] = n - i;
        }
    }

    // Algoritmos de ordenação Int
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
    static void Merge(int[] vet, int esq, int meio, int dir, ref long comps, ref long movs)
    {
        int n1 = meio - esq + 1;
        int n2 = dir - meio;

        int[] L = new int[n1];
        int[] R = new int[n2];

        for (int i = 0; i < n1; i++) { L[i] = vet[esq + i]; movs++; }
        for (int j = 0; j < n2; j++) { R[j] = vet[meio + 1 + j]; movs++; }

        int k = esq, x = 0, y = 0;

        while (x < n1 && y < n2)
        {
            comps++;
            if (L[x] <= R[y])
            {
                vet[k] = L[x]; x++;
            }
            else
            {
                vet[k] = R[y]; y++;
            }
            movs++;
            k++;
        }

        while (x < n1)
        {
            vet[k] = L[x]; x++; k++; movs++;
        }

        while (y < n2)
        {
            vet[k] = R[y]; y++; k++; movs++;
        }
    }

    static void MergeSort(int[] vet, int esq, int dir, ref long comps, ref long movs)
    {
        if (esq < dir)
        {
            int meio = (esq + dir) / 2;

            MergeSort(vet, esq, meio, ref comps, ref movs);
            MergeSort(vet, meio + 1, dir, ref comps, ref movs);
            Merge(vet, esq, meio, dir, ref comps, ref movs);
        }
    }
    static void Ajustar(int[] vet, int n, int i, ref long comps, ref long movs)
    {
        int maior = i;
        int esq = 2 * i + 1;
        int dir = 2 * i + 2;

        if (esq < n)
        {
            comps++;
            if (vet[esq] > vet[maior])
                maior = esq;
        }

        if (dir < n)
        {
            comps++;
            if (vet[dir] > vet[maior])
                maior = dir;
        }

        if (maior != i)
        {
            int temp = vet[i]; movs++;
            vet[i] = vet[maior]; movs++;
            vet[maior] = temp; movs++;

            Ajustar(vet, n, maior, ref comps, ref movs);
        }
    }
    static void HeapSort(int[] vet, int n, ref long comps, ref long movs)
    {
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            Ajustar(vet, n, i, ref comps, ref movs);
        }

        for (int i = n - 1; i > 0; i--)
        {
            int temp = vet[0]; movs++;  
            vet[0] = vet[i]; movs++;
            vet[i] = temp; movs++;

            Ajustar(vet, i, 0, ref comps, ref movs);
        }
    }

    // Algoritmos de ordenação Double
    static void Selecao(double[] vet, int n, out long comps, out long movs)
    {
        comps = 0; movs = 0;
        for (int i = 0; i < (n - 1); i++)
        {
            int menor = i;
            for (int j = (i + 1); j < n; j++)
            {
                comps++;
                if (vet[menor] > vet[j])
                {
                    menor = j;
                }
            }
            double temp = vet[menor]; movs++;
            vet[menor] = vet[i]; movs++;
            vet[i] = temp; movs++;
        }
    }
    static void Insercao(double[] vet, int n, out long comps, out long movs)
    {
        comps = 0; movs = 0;
        for (int i = 1; i < n; i++)
        {
            double tmp = vet[i];
            int j = i - 1;
            while ((j >= 0) && (vet[j] > tmp))
            {
                comps++;
                vet[j + 1] = vet[j]; movs++;
                j--;
            }
            vet[j + 1] = tmp; movs++;
        }
    }
    static void Bolha(double[] vet, int n, out long movs, out long comps)
    {
        comps = 0; movs = 0;
        double temp;
        for (int i = 0; i < n; i++)
        {
            for (int j = n - 1; j > i; j--)
            {
                comps++;
                if (vet[j] < vet[j - 1])
                {
                    temp = vet[j]; movs++;
                    vet[j] = vet[j - 1]; movs++;
                    vet[j - 1] = temp; movs++;
                }
            }
        }
    }
    static void Quick(double[] vet, int esq, int dir, ref long comps, ref long movs)
    {
        int i = esq, j = dir;
        double pivo = vet[(esq + dir) / 2];

        while (i <= j)
        {
            while (i <= dir && vet[i] < pivo) { i++; comps++; }
            while (j >= esq && vet[j] > pivo) { j--; comps++; }

            if (i <= j)
            {
                double tmp = vet[i]; movs++;
                vet[i] = vet[j]; movs++;
                vet[j] = tmp; movs++;

                i++;
                j--;
            }
        }

        if (esq < j) Quick(vet, esq, j, ref comps, ref movs);
        if (i < dir) Quick(vet, i, dir, ref comps, ref movs);
    }
    static void Merge(double[] vet, int esq, int meio, int dir, ref long comps, ref long movs)
    {
        int n1 = meio - esq + 1;
        int n2 = dir - meio;

        double[] L = new double[n1];
        double[] R = new double[n2];

        for (int i = 0; i < n1; i++) { L[i] = vet[esq + i]; movs++; }
        for (int j = 0; j < n2; j++) { R[j] = vet[meio + 1 + j]; movs++; }

        int k = esq, x = 0, y = 0;

        while (x < n1 && y < n2)
        {
            comps++;
            if (L[x] <= R[y])
            {
                vet[k] = L[x]; x++;
            }
            else
            {
                vet[k] = R[y]; y++;
            }
            movs++;
            k++;
        }

        while (x < n1)
        {
            vet[k] = L[x]; x++; k++; movs++;
        }

        while (y < n2)
        {
            vet[k] = R[y]; y++; k++; movs++;
        }
    }
    static void MergeSort(double[] vet, int esq, int dir, ref long comps, ref long movs)
    {
        if (esq < dir)
        {
            int meio = (esq + dir) / 2;

            MergeSort(vet, esq, meio, ref comps, ref movs);
            MergeSort(vet, meio + 1, dir, ref comps, ref movs);
            Merge(vet, esq, meio, dir, ref comps, ref movs);
        }
    }
    static void Ajustar(double[] vet, int n, int i, ref long comps, ref long movs)
    {
        int maior = i;
        int esq = 2 * i + 1;
        int dir = 2 * i + 2;

        if (esq < n)
        {
            comps++;
            if (vet[esq] > vet[maior])
                maior = esq;
        }

        if (dir < n)
        {
            comps++;
            if (vet[dir] > vet[maior])
                maior = dir;
        }

        if (maior != i)
        {
            double temp = vet[i]; movs++;
            vet[i] = vet[maior]; movs++;
            vet[maior] = temp; movs++;

            Ajustar(vet, n, maior, ref comps, ref movs);
        }
    }
    static void HeapSort(double[] vet, int n, ref long comps, ref long movs)
    {
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            Ajustar(vet, n, i, ref comps, ref movs);
        }

        for (int i = n - 1; i > 0; i--)
        {
            double temp = vet[0]; movs++;
            vet[0] = vet[i]; movs++;
            vet[i] = temp; movs++;

            Ajustar(vet, i, 0, ref comps, ref movs);
        }
    }


    // Main
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
                    Console.WriteLine("|---MENU (2)---|\n(1). 1.000 elementos\n(2). 500.000 elementos\n(3). Voltar");
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

                                // ------------------------- SELEÇÃO -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---SELEÇÃO---1K---ALT");
                                stopWatch.Start();
                                Selecao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---SELEÇÃO---1K---CRES");
                                stopWatch.Start();
                                Selecao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---SELEÇÃO---1K---DECS");
                                stopWatch.Start();
                                Selecao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- INSERÇÃO -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---INSERÇÃO---1K---ALT");
                                stopWatch.Start();
                                Insercao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---INSERÇÃO---1K---CRES");
                                stopWatch.Start();
                                Insercao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---INSERÇÃO---1K---DECS");
                                stopWatch.Start();
                                Insercao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- BOLHA -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---BOLHA---1K---ALT");
                                stopWatch.Start();
                                Bolha(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---BOLHA---1K---CRES");
                                stopWatch.Start();
                                Bolha(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---BOLHA---1K---DECS");
                                stopWatch.Start();
                                Bolha(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- QUICK -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---QUICK---1K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---QUICK---1K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---QUICK---1K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- MERGE -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---MERGE---1K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---MERGE---1K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---MERGE---1K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- HEAP -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---HEAP---1K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                HeapSort(vetI, vetI.Length, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---HEAP---1K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                HeapSort(vetI, vetI.Length, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---HEAP---1K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
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

                                // ------------------------- SELEÇÃO -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---SELEÇÃO---500K---ALT");
                                stopWatch.Start();
                                Selecao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---SELEÇÃO---500K---CRES");
                                stopWatch.Start();
                                Selecao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---SELEÇÃO---500K---DECS");
                                stopWatch.Start();
                                Selecao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- INSERÇÃO -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---INSERÇÃO---500K---ALT");
                                stopWatch.Start();
                                Insercao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---INSERÇÃO---500K---CRES");
                                stopWatch.Start();
                                Insercao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---INSERÇÃO---500K---DECS");
                                stopWatch.Start();
                                Insercao(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- BOLHA -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---BOLHA---500K---ALT");
                                stopWatch.Start();
                                Bolha(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---BOLHA---500K---CRES");
                                stopWatch.Start();
                                Bolha(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---BOLHA---500K---DECS");
                                stopWatch.Start();
                                Bolha(vetI, vetI.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- QUICK -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---QUICK---500K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---QUICK---500K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---QUICK---500K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- MERGE -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---MERGE---500K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---MERGE---500K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---MERGE---500K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetI, 0, vetI.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- HEAP -------------------------
                                PreencherIntAlt(vetI, vetI.Length);
                                Console.WriteLine("INT---HEAP---500K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                HeapSort(vetI, vetI.Length, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntCres(vetI, vetI.Length);
                                Console.WriteLine("INT---HEAP---500K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                HeapSort(vetI, vetI.Length, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreencherIntDecs(vetI, vetI.Length);
                                Console.WriteLine("INT---HEAP---500K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
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
                            Console.WriteLine("|---MENU (2)---|\n(1). 1.000 elementos\n(2). 500.000 elementos\n(3). Voltar");
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
                    Console.WriteLine("|---MENU (2)---|\n(1). 1.000 elementos\n(2). 500.000 elementos\n(3). Voltar");
                    op2 = int.Parse(Console.ReadLine());
                    do
                    {
                        // Declarando o contador 
                        TimeSpan ts;
                        string elapsedTime;
                        long comps, movs;

                        switch (op2) // Quantidade de elementos
                        {

                            case 1: // 1.000 elementos
                                vetD = new double[1000];

                                // ------------------------- SELEÇÃO -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---SELEÇÃO---1K---ALT");
                                stopWatch.Start();
                                Selecao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---SELEÇÃO---1K---CRES");
                                stopWatch.Start();
                                Selecao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---SELEÇÃO---1K---DECS");
                                stopWatch.Start();
                                Selecao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- INSERÇÃO -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---INSERÇÃO---1K---ALT");
                                stopWatch.Start();
                                Insercao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---INSERÇÃO---1K---CRES");
                                stopWatch.Start();
                                Insercao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---INSERÇÃO---1K---DECS");
                                stopWatch.Start();
                                Insercao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- BOLHA -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---BOLHA---1K---ALT");
                                stopWatch.Start();
                                Bolha(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---BOLHA---1K---CRES");
                                stopWatch.Start();
                                Bolha(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---BOLHA---1K---DECS");
                                stopWatch.Start();
                                Bolha(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- QUICK -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---QUICK---1K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---QUICK---1K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---QUICK---1K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- MERGE -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---MERGE---1K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---MERGE---1K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---MERGE---1K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- HEAP -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---HEAP---1K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                HeapSort(vetD, vetD.Length, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---HEAP---1K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                HeapSort(vetD, vetD.Length, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---HEAP---1K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                HeapSort(vetD, vetD.Length, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                            break;

                            case 2: // 500.000 elementos
                                vetD = new double[500000];

                                // ------------------------- SELEÇÃO -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---SELEÇÃO---500K---ALT");
                                stopWatch.Start();
                                Selecao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---SELEÇÃO---500K---CRES");
                                stopWatch.Start();
                                Selecao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---SELEÇÃO---500K---DECS");
                                stopWatch.Start();
                                Selecao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- INSERÇÃO -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---INSERÇÃO---500K---ALT");
                                stopWatch.Start();
                                Insercao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---INSERÇÃO---500K---CRES");
                                stopWatch.Start();
                                Insercao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---INSERÇÃO---500K---DECS");
                                stopWatch.Start();
                                Insercao(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- BOLHA -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---BOLHA---500K---ALT");
                                stopWatch.Start();
                                Bolha(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---BOLHA---500K---CRES");
                                stopWatch.Start();
                                Bolha(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---BOLHA---500K---DECS");
                                stopWatch.Start();
                                Bolha(vetD, vetD.Length, out comps, out movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- QUICK -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---QUICK---500K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---QUICK---500K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---QUICK---500K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                Quick(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- MERGE -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---MERGE---500K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---MERGE---500K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---MERGE---500K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                MergeSort(vetD, 0, vetD.Length - 1, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                // ------------------------- HEAP -------------------------
                                PreecnherDoubleAlt(vetD, vetD.Length);
                                Console.WriteLine("DOB---HEAP---500K---ALT");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                HeapSort(vetD, vetD.Length, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleCres(vetD, vetD.Length);
                                Console.WriteLine("DOB---HEAP---500K---CRES");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                HeapSort(vetD, vetD.Length, ref comps, ref movs);
                                stopWatch.Stop();
                                ts = stopWatch.Elapsed;
                                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{movs}\nComparações:{comps}\n");
                                stopWatch.Reset();

                                PreecnherDoubleDecs(vetD, vetD.Length);
                                Console.WriteLine("DOB---HEAP---500K---DECS");
                                stopWatch.Start();
                                comps = 0; movs = 0;
                                HeapSort(vetD, vetD.Length, ref comps, ref movs);
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
                            Console.WriteLine("|---MENU (2)---|\n(1). 1.000 elementos\n(2). 500.000 elementos\n(3). Voltar");
                            op2 = int.Parse(Console.ReadLine());
                        }
                        else
                        {
                            Console.WriteLine("Voltando...");
                        }

                    } while (op2 != 3);
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
