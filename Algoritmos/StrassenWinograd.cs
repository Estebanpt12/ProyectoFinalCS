using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Algoritmos
{
    using System;

    public class StrassenWinograd : IMatrixMultiplication
    {
        private static int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        // Función para sumar dos matrices
        private static void Add(int[,] A, int[,] B, int[,] C, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    C[i, j] = A[i, j] + B[i, j];
                }
            }
        }

        // Función para restar dos matrices
        private static void Subtract(int[,] A, int[,] B, int[,] C, int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    C[i, j] = A[i, j] - B[i, j];
                }
            }
        }

        // Multiplicación estándar
        private static void StandardMultiply(int[,] A, int[,] B, int[,] C, int N, int P, int M)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    C[i, j] = 0;
                    for (int k = 0; k < P; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
        }

        // Paso recursivo del algoritmo de Strassen-Winograd
        private static void StrassenWinogradStep(int[,] A, int[,] B, int[,] C, int size, int minSize)
        {
            if (size <= minSize)
            {
                StandardMultiply(A, B, C, size, size, size);
                return;
            }

            int newSize = size / 2;
            
            // Crear submatrices
            int[,] A11 = new int[newSize, newSize];
            int[,] A12 = new int[newSize, newSize];
            int[,] A21 = new int[newSize, newSize];
            int[,] A22 = new int[newSize, newSize];
            int[,] B11 = new int[newSize, newSize];
            int[,] B12 = new int[newSize, newSize];
            int[,] B21 = new int[newSize, newSize];
            int[,] B22 = new int[newSize, newSize];
            int[,] C11 = new int[newSize, newSize];
            int[,] C12 = new int[newSize, newSize];
            int[,] C21 = new int[newSize, newSize];
            int[,] C22 = new int[newSize, newSize];
            int[,] tempA = new int[newSize, newSize];
            int[,] tempB = new int[newSize, newSize];

            // Dividir matrices en submatrices
            for (int i = 0; i < newSize; i++)
            {
                for (int j = 0; j < newSize; j++)
                {
                    A11[i, j] = A[i, j];
                    A12[i, j] = A[i, j + newSize];
                    A21[i, j] = A[i + newSize, j];
                    A22[i, j] = A[i + newSize, j + newSize];
                    B11[i, j] = B[i, j];
                    B12[i, j] = B[i, j + newSize];
                    B21[i, j] = B[i + newSize, j];
                    B22[i, j] = B[i + newSize, j + newSize];
                }
            }

            // Pasos recursivos de Strassen-Winograd
            Add(A11, A22, tempA, newSize);
            Add(B11, B22, tempB, newSize);
            StrassenWinogradStep(tempA, tempB, C11, newSize, minSize);  // P1

            Add(A21, A22, tempA, newSize);
            StrassenWinogradStep(tempA, B11, C21, newSize, minSize);  // P2

            Subtract(B12, B22, tempB, newSize);
            StrassenWinogradStep(A11, tempB, C12, newSize, minSize);  // P3

            Subtract(B21, B11, tempB, newSize);
            StrassenWinogradStep(A22, tempB, C22, newSize, minSize);  // P4

            Add(A11, A12, tempA, newSize);
            StrassenWinogradStep(tempA, B22, tempA, newSize, minSize);  // P5
            Add(tempA, C11, C11, newSize);
            Subtract(C12, tempA, C12, newSize);

            Subtract(A21, A11, tempA, newSize);
            Add(B11, B12, tempB, newSize);
            StrassenWinogradStep(tempA, tempB, tempA, newSize, minSize);  // P6
            Add(C22, tempA, C22, newSize);

            Subtract(A12, A22, tempA, newSize);
            Add(B21, B22, tempB, newSize);
            StrassenWinogradStep(tempA, tempB, tempA, newSize, minSize);  // P7
            Subtract(C21, tempA, C21, newSize);

            // Combinar los resultados en la matriz resultado C
            for (int i = 0; i < newSize; i++)
            {
                for (int j = 0; j < newSize; j++)
                {
                    C[i, j] = C11[i, j];
                    C[i, j + newSize] = C12[i, j];
                    C[i + newSize, j] = C21[i, j];
                    C[i + newSize, j + newSize] = C22[i, j];
                }
            }
        }

        // Función principal de multiplicación StrassenWinograd
        public static void AlgStrassenWinograd(int[,] A, int[,] B, int[,] C, int N, int P, int M)
        {
            int maxSize = Max(N, P);
            maxSize = Max(maxSize, M);
            if (maxSize < 16)
            {
                maxSize = 16;
            }

            int valorK = (int)Math.Floor(Math.Log(maxSize, 2) - 4);
            int valorM = (int)(maxSize * Math.Pow(2, -valorK) + 1);
            int newSize = valorM * (int)Math.Pow(2, valorK);

            int[,] newA = new int[newSize, newSize];
            int[,] newB = new int[newSize, newSize];
            int[,] auxResultado = new int[newSize, newSize];

            // Inicializar las matrices A y B con ceros si las dimensiones son menores a las dimensiones nuevas
            for (int i = 0; i < newSize; i++)
            {
                for (int j = 0; j < newSize; j++)
                {
                    newA[i, j] = (i < N && j < P) ? A[i, j] : 0;
                    newB[i, j] = (i < P && j < M) ? B[i, j] : 0;
                }
            }

            // Llamar al paso recursivo
            StrassenWinogradStep(newA, newB, auxResultado, newSize, 16);

            // Copiar los resultados en la matriz C
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    C[i, j] = auxResultado[i, j];
                }
            }
        }

        // Función para multiplicar dos matrices utilizando StrassenWinograd
        public int[,] Multiply(int[,] A, int[,] B)
        {
            int N = A.GetLength(0);
            int P = B.GetLength(0);
            int M = B.GetLength(1);
            int[,] C = new int[N, M];
            AlgStrassenWinograd(A, B, C, N, P, M);
            return C;
        }
    }
}

// (n^2.81)