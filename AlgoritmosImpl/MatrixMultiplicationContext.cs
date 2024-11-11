using System;
using System.Collections.Generic;
using System.Diagnostics;
using ProyectoFinalCS.Algoritmos;
using ProyectoFinalCS.Utils;

namespace ProyectoFinalCS.AlgoritmosImpl
{
    public class MatrixMultiplicationContext
    {
        private readonly IMatrixMultiplication _algorithm;
        private readonly Dictionary<string, IMatrixMultiplication> _diccionario = 
            new Dictionary<string, IMatrixMultiplication>()
            {
                {"StrassenNaiv", new StrassenNaiv()},
                {"StrassenWinograd", new StrassenWinograd()},
                {"WinogradOriginal", new WinogradOriginal()},
                {"WinogradScaled", new WinogradScaled()},
                {"NaivLoopUnrollingFour", new NaivLoopUnrollingFour()},
                {"NaivLoopUnrollingTwo", new NaivLoopUnrollingTwo()},
                {"NaivOnArray", new NaivOnArray()},
                {"V3SequentialBloc", new V3SequentialBloc()},
                {"V4ParallelBlock", new V4ParallelBlock()},
                {"IV3SequentialBlock", new IV3SequentialBlock()}
            };

        public MatrixMultiplicationContext(string nombreAlgoritmo)
        {
            _algorithm = _diccionario[nombreAlgoritmo];
        }

        public (int[,], long) Execute(int[,] matrixA, int[,] matrixB)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int[,] result = _algorithm.Multiply(matrixA, matrixB);

            stopwatch.Stop();

            //PrintMat(result);

            // Registrar el tiempo de ejecución y las dimensiones de las matrices
            Logger.LogExecutionTime(_algorithm.GetType().Name, stopwatch.ElapsedMilliseconds, 
                matrixA.GetLength(0), matrixA.GetLength(1), matrixB.GetLength(0), matrixB.GetLength(1));

            if(matrixA.GetLength(0) == 4)
            {
                PrintMat(result);
                Console.WriteLine("------------------------------------------------");
            }

            return (result, stopwatch.ElapsedMilliseconds);
        }

        private void PrintMat(int[, ] a){
            for(int i = 0; i < a.GetLength(0); i++){
            for(int j = 0; j < a.GetLength(1); j++){
                Console.Write(a[i, j]+" ");
            }
            Console.WriteLine("");
            }
            Console.WriteLine("");
        }
    }
}
