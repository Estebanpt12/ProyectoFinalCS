using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Algoritmos
{
    using System;

    public class StrassenWinograd : IMatrixMultiplication
    {
        // Método principal para multiplicar dos matrices usando Strassen-Winograd
        public int[, ] Multiply(int[, ] A,int[, ] B)
        {
            int n = A.GetLength(0);

            // Base case for recursion
            if (n == 1)
            {
                int[,] result = new int[1, 1];
                result[0, 0] = A[0, 0] * B[0, 0];
                return result;
            }

            // Divide matrices A and B into 4 submatrices
            int mid = n / 2;
            int[,] A11 = new int[mid, mid];
            int[,] A12 = new int[mid, mid];
            int[,] A21 = new int[mid, mid];
            int[,] A22 = new int[mid, mid];

            int[,] B11 = new int[mid, mid];
            int[,] B12 = new int[mid, mid];
            int[,] B21 = new int[mid, mid];
            int[,] B22 = new int[mid, mid];

            // Fill the submatrices
            for (int i = 0; i < mid; i++)
            {
                for (int j = 0; j < mid; j++)
                {
                    A11[i, j] = A[i, j];
                    A12[i, j] = A[i, j + mid];
                    A21[i, j] = A[i + mid, j];
                    A22[i, j] = A[i + mid, j + mid];

                    B11[i, j] = B[i, j];
                    B12[i, j] = B[i, j + mid];
                    B21[i, j] = B[i + mid, j];
                    B22[i, j] = B[i + mid, j + mid];
                }
            }

            // Compute the 7 products
            int[,] P1 = Multiply(A11, Subtract(B12, B22));  // P1 = A11 * (B12 - B22)
            int[,] P2 = Multiply(Add(A11, A12), B22);      // P2 = (A11 + A12) * B22
            int[,] P3 = Multiply(Add(A21, A22), B11);      // P3 = (A21 + A22) * B11
            int[,] P4 = Multiply(A22, Subtract(B21, B11));  // P4 = A22 * (B21 - B11)
            int[,] P5 = Multiply(Add(A11, A22), Add(B11, B22)); // P5 = (A11 + A22) * (B11 + B22)
            int[,] P6 = Multiply(Subtract(A12, A22), Add(B21, B22)); // P6 = (A12 - A22) * (B21 + B22)
            int[,] P7 = Multiply(Subtract(A11, A21), Add(B11, B12)); // P7 = (A11 - A21) * (B11 + B12)

            // Now combine the products to get the result
            int[,] C11 = Add(Subtract(Add(P5, P4), P2), P6); // C11 = P5 + P4 - P2 + P6
            int[,] C12 = Add(P1, P2);                         // C12 = P1 + P2
            int[,] C21 = Add(P3, P4);                         // C21 = P3 + P4
            int[,] C22 = Add(Subtract(Add(P5, P1), P3), P7); // C22 = P5 + P1 - P3 + P7

            // Combine submatrices into a single result matrix
            int[,] C = new int[n, n];
            for (int i = 0; i < mid; i++)
            {
                for (int j = 0; j < mid; j++)
                {
                    C[i, j] = C11[i, j];
                    C[i, j + mid] = C12[i, j];
                    C[i + mid, j] = C21[i, j];
                    C[i + mid, j + mid] = C22[i, j];
                }
            }

            return C;
        }

        // Helper function to add two matrices
        public static int[,] Add(int[,] A, int[,] B)
        {
            int n = A.GetLength(0);
            int[,] result = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = A[i, j] + B[i, j];
                }
            }
            return result;
        }

        // Helper function to subtract two matrices
        public static int[,] Subtract(int[,] A, int[,] B)
        {
            int n = A.GetLength(0);
            int[,] result = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = A[i, j] - B[i, j];
                }
            }
            return result;
        }

        // Function to display matrices
        public static void DisplayMatrix(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
    }
}