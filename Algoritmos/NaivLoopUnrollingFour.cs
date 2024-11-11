using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Algoritmos
{
    public class NaivLoopUnrollingFour : IMatrixMultiplication
    {
        public static int[,] AlgNaivLoopUnrollingFour(int[,] matrizA, int[,] matrizB, int N, int P, int M)
        {
            int[,] matrizRes = new int[N, M];

            if (P % 4 == 0)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        int aux = 0;
                        for (int k = 0; k < P; k += 4)
                        {
                            aux += matrizA[i, k] * matrizB[k, j] +
                                matrizA[i, k + 1] * matrizB[k + 1, j] +
                                matrizA[i, k + 2] * matrizB[k + 2, j] +
                                matrizA[i, k + 3] * matrizB[k + 3, j];
                        }
                        matrizRes[i, j] = aux;
                    }
                }
            }
            else if (P % 4 == 1)
            {
                int PP = P - 1;
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        int aux = 0;
                        for (int k = 0; k < PP; k += 4)
                        {
                            aux += matrizA[i, k] * matrizB[k, j] +
                                matrizA[i, k + 1] * matrizB[k + 1, j] +
                                matrizA[i, k + 2] * matrizB[k + 2, j] +
                                matrizA[i, k + 3] * matrizB[k + 3, j];
                        }
                        aux += matrizA[i, PP] * matrizB[PP, j];
                        matrizRes[i, j] = aux;
                    }
                }
            }
            else if (P % 4 == 2)
            {
                int PP = P - 2;
                int PPP = P - 1;
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        int aux = 0;
                        for (int k = 0; k < PP; k += 4)
                        {
                            aux += matrizA[i, k] * matrizB[k, j] +
                                matrizA[i, k + 1] * matrizB[k + 1, j] +
                                matrizA[i, k + 2] * matrizB[k + 2, j] +
                                matrizA[i, k + 3] * matrizB[k + 3, j];
                        }
                        aux += matrizA[i, PP] * matrizB[PP, j] +
                            matrizA[i, PPP] * matrizB[PPP, j];
                        matrizRes[i, j] = aux;
                    }
                }
            }
            else
            {
                int PP = P - 3;
                int PPP = P - 2;
                int PPPP = P - 1;
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        int aux = 0;
                        for (int k = 0; k < PP; k += 4)
                        {
                            aux += matrizA[i, k] * matrizB[k, j] +
                                matrizA[i, k + 1] * matrizB[k + 1, j] +
                                matrizA[i, k + 2] * matrizB[k + 2, j] +
                                matrizA[i, k + 3] * matrizB[k + 3, j];
                        }
                        aux += matrizA[i, PP] * matrizB[PP, j] +
                            matrizA[i, PPP] * matrizB[PPP, j] +
                            matrizA[i, PPPP] * matrizB[PPPP, j];
                        matrizRes[i, j] = aux;
                    }
                }
            }

            return matrizRes;
        }

        // Método de entrada para multiplicar matrices utilizando el algoritmo NaivLoopUnrollingFour
        public int[,] Multiply(int[,] matrizA, int[,] matrizB)
        {
            int N = matrizA.GetLength(0); // Filas de matrizA
            int P = matrizB.GetLength(0); // Filas de matrizB (debe ser igual a columnas de matrizA)
            int M = matrizB.GetLength(1); // Columnas de matrizB

            return AlgNaivLoopUnrollingFour(matrizA, matrizB, N, P, M);
        }
    }
}

//O(N * P * M)