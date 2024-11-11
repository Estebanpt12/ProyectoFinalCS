using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Algoritmos
{
    public class WinogradOriginal : IMatrixMultiplication
    {
        public static int[,] AlgWinogradOriginal(int[,] matrizA, int[,] matrizB, int N, int P, int M)
        {
            int upsilon = P % 2;
            int gamma = P - upsilon;
            
            // Inicialización de los arreglos y y z
            int[] y = new int[N];
            int[] z = new int[M];
            int[,] matrizRes = new int[N, M];

            // Precomputar y
            for (int i = 0; i < N; i++)
            {
                int aux = 0;
                for (int j = 0; j < gamma; j += 2)
                {
                    aux += matrizA[i, j] * matrizA[i, j + 1];
                }
                y[i] = aux;
            }

            // Precomputar z
            for (int k = 0; k < M; k++)
            {
                int aux = 0;
                for (int j = 0; j < gamma; j += 2)
                {
                    aux += matrizB[j, k] * matrizB[j + 1, k];
                }
                z[k] = aux;
            }

            // Calcular el resultado
            if (upsilon == 1)
            {
                int PP = P - 1;
                for (int i = 0; i < N; i++)
                {
                    for (int k = 0; k < M; k++)
                    {
                        int aux = 0;
                        for (int j = 0; j < gamma; j += 2)
                        {
                            aux += (matrizA[i, j] + matrizB[j + 1, k]) * (matrizA[i, j + 1] + matrizB[j, k]);
                        }
                        matrizRes[i, k] = aux - y[i] - z[k] + matrizA[i, PP] * matrizB[PP, k];
                    }
                }
            }
            else
            {
                for (int i = 0; i < N; i++)
                {
                    for (int k = 0; k < M; k++)
                    {
                        int aux = 0;
                        for (int j = 0; j < gamma; j += 2)
                        {
                            aux += (matrizA[i, j] + matrizB[j + 1, k]) * (matrizA[i, j + 1] + matrizB[j, k]);
                        }
                        matrizRes[i, k] = aux - y[i] - z[k];
                    }
                }
            }

            return matrizRes;
        }

        public int[,] Multiply(int[,] matrizA, int[,] matrizB)
        {
            int N = matrizA.GetLength(0); // Filas de matrizA
            int P = matrizB.GetLength(0); // Filas de matrizB (debe ser igual a columnas de matrizA)
            int M = matrizB.GetLength(1); // Columnas de matrizB

            return AlgWinogradOriginal(matrizA, matrizB, N, P, M);
        }
    }
}
// O(N * M * P / 2)