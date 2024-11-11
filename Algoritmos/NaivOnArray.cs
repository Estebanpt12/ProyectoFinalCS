using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Algoritmos
{
    public class NaivOnArray : IMatrixMultiplication
    {
        public static int[,] AlgNaivOnArray(int[,] matrizA, int[,] matrizB, int N, int P, int M)
        {
            // Crear la matriz resultado de tamaño N x M
            int[,] matrizRes = new int[N, M];

            // Realizar la multiplicación de matrices
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    for (int k = 0; k < P; k++)
                    {
                        matrizRes[i, j] += matrizA[i, k] * matrizB[k, j];
                    }
                }
            }
            return matrizRes;
        }

        // Función para la multiplicación de matrices
        public int[,] Multiply(int[,] matrizA, int[,] matrizB)
        {
            int N = matrizA.GetLength(0); // Número de filas de A
            int P = matrizB.GetLength(0); // Número de filas de B
            int M = matrizB.GetLength(1); // Número de columnas de B
            return AlgNaivOnArray(matrizA, matrizB, N, P, M);
        }
    }
}

// O(N * P * M), donde:

// N es el número de filas de la matrizA (primer argumento).
// P es el número de columnas de la matrizA (y el número de filas de la matrizB).
// M es el número de columnas de la matrizB (segundo argumento).