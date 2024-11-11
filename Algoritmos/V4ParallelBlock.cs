using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Algoritmos
{
    public class V4ParallelBlock : IMatrixMultiplication
    {
        // Método que realiza la multiplicación de bloques en paralelo
        private static void BlockMultiply(int[,] matrizA, int[,] matrizB, int[,] matrizC, int i1, int size1, int size2)
        {
            for (int j1 = 0; j1 < size1; j1 += size2)
            {
                for (int k1 = 0; k1 < size1; k1 += size2)
                {
                    for (int i = i1; i < Math.Min(i1 + size2, size1); i++)
                    {
                        for (int j = j1; j < Math.Min(j1 + size2, size1); j++)
                        {
                            for (int k = k1; k < Math.Min(k1 + size2, size1); k++)
                            {
                                matrizC[k, i] += matrizA[k, j] * matrizB[j, i];
                            }
                        }
                    }
                }
            }
        }

        // Algoritmo principal que realiza la multiplicación de matrices con paralelización
        public static int[,] Alg_V_4_ParallelBlockTres(int[,] matrizA, int[,] matrizB, int size1, int size2)
        {
            // Inicializar la matriz de resultados
            int[,] matrizC = new int[size1, size1];

            // Calculamos el número de bloques
            int numBlocks = (size1 + size2 - 1) / size2;

            // Creamos las tareas en paralelo
            var tasks = new List<Task>();
            for (int i1 = 0; i1 < size1; i1 += size2)
            {
                // Llamamos al método BlockMultiply en un hilo paralelo
                int i1Copy = i1; // Necesario para evitar problemas con la captura de variables dentro del loop
                tasks.Add(Task.Run(() => BlockMultiply(matrizA, matrizB, matrizC, i1Copy, size1, size2)));
            }

            // Esperamos a que todas las tareas terminen
            Task.WhenAll(tasks).Wait();

            return matrizC;
        }

        // Método de multiplicación de matrices
        public int[,] Multiply(int[,] matrizA, int[,] matrizB)
        {
            int N = matrizA.GetLength(0);
            int P = matrizB.GetLength(1);
            return Alg_V_4_ParallelBlockTres(matrizA, matrizB, N, P);
        }
    }
}

//O(n³)