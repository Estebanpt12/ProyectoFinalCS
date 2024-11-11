using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Algoritmos
{
    public class IV3SequentialBlock : IMatrixMultiplication
    {
        // Método que realiza la multiplicación de bloques secuencial
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
                                matrizC[i, k] += matrizA[i, j] * matrizB[j, k];
                            }
                        }
                    }
                }
            }
        }

        // Algoritmo principal que realiza la multiplicación de matrices por bloques secuencial
        public static int[,] Alg_IV_3_Sequential_Block(int[,] matrizA, int[,] matrizB, int size1, int size2)
        {
            // Inicializar la matriz de resultados
            int[,] matrizC = new int[size1, size1];

            // Realizamos la multiplicación de bloques
            for (int i1 = 0; i1 < size1; i1 += size2)
            {
                for (int j1 = 0; j1 < size1; j1 += size2)
                {
                    for (int k1 = 0; k1 < size1; k1 += size2)
                    {
                        // Llamamos al método BlockMultiply para cada bloque
                        BlockMultiply(matrizA, matrizB, matrizC, i1, size1, size2);
                    }
                }
            }

            return matrizC;
        }

        // Método de multiplicación de matrices
        public int[,] Multiply(int[,] matrizA, int[,] matrizB)
        {
            int N = matrizA.GetLength(0);
            int P = matrizB.GetLength(1);
            return Alg_IV_3_Sequential_Block(matrizA, matrizB, N, P);
        }
    }
}
//O(n^3)