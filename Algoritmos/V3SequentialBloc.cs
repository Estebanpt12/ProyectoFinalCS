using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Algoritmos
{
    public class V3SequentialBloc : IMatrixMultiplication
    {
        public static int[,] Alg_V_3_Sequential_Block(int[,] matrizA, int[,] matrizB, int size1, int size2)
        {
            // Inicialización de la matriz de resultados
            int[,] matrizRes = new int[size1, size1];
            
            // Multiplicación de matrices por bloques
            for (int i1 = 0; i1 < size1; i1 += size2)
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
                                    matrizRes[k, i] += matrizA[k, j] * matrizB[j, i];
                                }
                            }
                        }
                    }
                }
            }

            return matrizRes;
        }

        public int[,] Multiply(int[,] matrizA, int[,] matrizB)
        {
            int size1 = matrizA.GetLength(0); // Número de filas en la matriz A
            int size2 = matrizB.GetLength(1); // Número de columnas en la matriz B
            return Alg_V_3_Sequential_Block(matrizA, matrizB, size1, size2);
        }
    }
}
//O(size1³)