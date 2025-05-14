using System;
using System.Diagnostics;
class Program
{
    static void PreencherInt(int[] vet, int n)
    {
        Random r = new Random(10);
        for (int i = 0; i < n; i++)
        {
            vetI[i] = r.Next(1, 10);
        }
    }
    static void Selecao(int[] vet, int n, out long Comps, out long Movs)
    {
        Comps = 0; Movs = 0;
        for (int i = 0; i < (n - 1); i++)
        {
            int menor = i;
            for (int j = (i + 1); j < n; j++)
            {
                if (vet[menor] > vet[j]) Comps++;
                {
                    menor = j;
                }
            }
            int temp = vet[menor]; Movs++;
            vet[menor] = vet[i]; Movs++;
            vet[i] = temp; Movs++;
        }
    }
    static void Insercao(int[] vet, int n)
    {
     
    }

    static void Main(string[] args)
    {
        Stopwatch stopWatch = new Stopwatch();

        Console.WriteLine("(1). Inteiros\n(2). Decimais\n(3). Sair");
        int op1 = int.Parse(Console.ReadLine());
        do
        {
            switch (op1) // Decimais ou inteiros
            {
                case 1: // Inteiros
                    int[] vetI;
                    Console.WriteLine("(1). 1.000 elementos\n(2). 500.000 elementos\n(3). Sair");
                    int op2 = int.Parse(Console.ReadLine());
                    do
                    {
                        switch (op2) // Quantidade de elemento
                        {
                            case 1: // 1.000 elementos
                                long Comps, Movs = 0;
                                vetI = new int[1000];
                                PreencherInt(vetI, 1000);
                                
                                // Seleção
                                Console.WriteLine("----SELEÇÃO----");
                                stopWatch.Start();
                                Selecao(vetI, vetI.Length, out Comps, out Movs);
                                stopWatch.Stop();
                                TimeSpan ts = stopWatch.Elapsed; // Formata e exibe o valor TimeSpan.
                                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours,
                                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                                Console.WriteLine("Tempo " + elapsedTime + $"\nMovimentaçõe:{Movs}\nComparações:{Comps}" + "\n");
                                stopWatch.Reset();


                                break;
                            case 2: // 500.000 elementos

                                break;
                            default:
                                Console.WriteLine("Opção inválida");
                                break;
                        }
                        Console.WriteLine("(1). 1.000 elementos\n(2). 500.000 elementos\n(3). Sair");
                        op2 = int.Parse(Console.ReadLine());
                    } while (op2 != 3);
                break;
                case 2: // Decimais
                    double[] vetD;
                break;
            }

            Console.WriteLine("(1). Inteiros\n(2). Decimais\n(3). Sair");
            op1 = int.Parse(Console.ReadLine());
        } while (op1!= 3);
    }
}
