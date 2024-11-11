using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Algoritmos
{
    public class NaivLoopUnrollingTwo : IMatrixMultiplication
    {
        public static int[,] AlgNaivLoopUnrollingTwo(int[,] matrizA, int[,] matrizB, int N, int P, int M)
        {
            int[,] matrizRes = new int[N, M];

            if (P % 2 == 0)
            {
                // P es par
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        int aux = 0;
                        for (int k = 0; k < P; k += 2)
                        {
                            aux += matrizA[i, k] * matrizB[k, j] + matrizA[i, k + 1] * matrizB[k + 1, j];
                        }
                        matrizRes[i, j] = aux;
                    }
                }
            }
            else
            {
                // P es impar
                int PP = P - 1;
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        int aux = 0;
                        for (int k = 0; k < PP; k += 2)
                        {
                            aux += matrizA[i, k] * matrizB[k, j] + matrizA[i, k + 1] * matrizB[k + 1, j];
                        }
                        aux += matrizA[i, PP] * matrizB[PP, j];  // Manejar el último elemento si P es impar
                        matrizRes[i, j] = aux;
                    }
                }
            }

            return matrizRes;
        }

        // Método de entrada para multiplicar matrices utilizando el algoritmo NaivLoopUnrollingTwo
        public int[,] Multiply(int[,] matrizA, int[,] matrizB)
        {
            int N = matrizA.GetLength(0); // Filas de matrizA
            int P = matrizB.GetLength(0); // Filas de matrizB (debe ser igual a columnas de matrizA)
            int M = matrizB.GetLength(1); // Columnas de matrizB

            return AlgNaivLoopUnrollingTwo(matrizA, matrizB, N, P, M);
        }
    }
}
//O(N⋅P⋅M).