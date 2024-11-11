using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Algoritmos
{
    public class WinogradScaled : IMatrixMultiplication
    {
        public static int[,] MultiplyWithScalar(int[,] matrix, int scalar)
        {
            int n = matrix.GetLength(0);  // Filas de la matriz
            int m = matrix.GetLength(1);  // Columnas de la matriz
            int[,] result = new int[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result[i, j] = matrix[i, j] * scalar;
                }
            }

            return result;
        }

        // Método para calcular la norma infinita de una matriz (la fila con la mayor suma de valores absolutos)
        public static int NormInf(int[,] matrix)
        {
            int maxSum = int.MinValue;

            for (int i = 0; i < matrix.GetLength(0); i++)  // Iterar sobre las filas
            {
                int rowSum = 0;
                for (int j = 0; j < matrix.GetLength(1); j++)  // Iterar sobre las columnas
                {
                    rowSum += Math.Abs(matrix[i, j]);
                }
                if (rowSum > maxSum)
                {
                    maxSum = rowSum;
                }
            }

            return maxSum;
        }

        // Algoritmo Winograd Original
        public static int[,] AlgWinogradOriginal(int[,] A, int[,] B, int N, int P, int M)
        {
            int upsilon = P % 2;
            int gamma = P - upsilon;
            int[] y = new int[M];
            int[] z = new int[N];
            int[,] res = new int[M, N];

            // Precomputar y
            for (int i = 0; i < M; i++)
            {
                int aux = 0;
                for (int j = 0; j < gamma; j += 2)
                {
                    aux += A[i, j] * A[i, j + 1];
                }
                y[i] = aux;
            }

            // Precomputar z
            for (int i = 0; i < N; i++)
            {
                int aux = 0;
                for (int j = 0; j < gamma; j += 2)
                {
                    aux += B[j, i] * B[j + 1, i];
                }
                z[i] = aux;
            }

            // Calcular la matriz resultante
            if (upsilon == 1)
            {
                int PP = P - 1;
                for (int i = 0; i < M; i++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        int aux = 0;
                        for (int j = 0; j < gamma; j += 2)
                        {
                            aux += (A[i, j] + B[j + 1, k]) * (A[i, j + 1] + B[j, k]);
                        }
                        res[i, k] = aux - y[i] - z[k] + A[i, PP] * B[PP, k];
                    }
                }
            }
            else
            {
                for (int i = 0; i < M; i++)
                {
                    for (int k = 0; k < N; k++)
                    {
                        int aux = 0;
                        for (int j = 0; j < gamma; j += 2)
                        {
                            aux += (A[i, j] + B[j + 1, k]) * (A[i, j + 1] + B[j, k]);
                        }
                        res[i, k] = aux - y[i] - z[k];
                    }
                }
            }

            return res;
        }

        // Algoritmo Winograd Escalado
        public static int[,] AlgWinogradScaled(int[,] A, int[,] B, int N, int P, int M)
        {
            // Calcular la norma infinita de las matrices A y B
            int a = NormInf(A);
            int b = NormInf(B);
            int lambda = (int)Math.Floor(0.5 + Math.Log(b / (double)a) / Math.Log(4));

            // Multiplicar las matrices A y B por los escalares correspondientes
            int[,] copyA = MultiplyWithScalar(A, (int)Math.Pow(2, lambda));
            int[,] copyB = MultiplyWithScalar(B, (int)Math.Pow(2, -lambda));

            // Llamar al algoritmo Winograd Original con las matrices escaladas
            return AlgWinogradOriginal(copyA, copyB, N, P, M);
        }

        // Método para multiplicar dos matrices
        public int[,] Multiply(int[,] A, int[,] B)
        {
            int N = A.GetLength(0); // Filas de matrizA
            int P = B.GetLength(0); // Filas de matrizB (debe ser igual a columnas de matrizA)
            int M = B.GetLength(1); // Columnas de matrizB

            return AlgWinogradScaled(A, B, N, P, M);
        }
    }
}

//O(N * M * P / 2)