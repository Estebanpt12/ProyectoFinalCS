using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Algoritmos
{
    public class WinogradOriginal : IMatrixMultiplication
    {
        public int[,] Multiply(int[,] A, int[,] B)
        {
            int m = A.GetLength(0); // Número de filas de A
            int n = A.GetLength(1); // Número de columnas de A (filas de B)
            int p = B.GetLength(1); // Número de columnas de B

            // Inicializar la matriz resultante C con ceros
            int[,] C = new int[m, p];

            // Paso 1: Calcular los productos parciales
            int[] rowFactors = new int[m];
            int[] colFactors = new int[p];

            for (int i = 0; i < m; i++)
            {
                rowFactors[i] = 0;
                for (int k = 0; k < n; k++)
                {
                    rowFactors[i] += A[i, k] * B[k, k]; // Producto para fila de A y diagonal de B
                }
            }

            for (int j = 0; j < p; j++)
            {
                colFactors[j] = 0;
                for (int k = 0; k < n; k++)
                {
                    colFactors[j] += A[k, j] * B[k, j]; // Producto para columna de B y diagonal de A
                }
            }

            // Paso 2: Calcular el producto resultante usando los factores parciales
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    C[i, j] = rowFactors[i] + colFactors[j] - (A[i, j] * B[j, i]);
                }
            }

            return C;
        }
    }
}