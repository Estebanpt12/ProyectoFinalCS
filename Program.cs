using System;
using System.Collections.Generic;
using System.IO;
using ProyectoFinalCS.Algoritmos;
using ProyectoFinalCS.AlgoritmosImpl;
using ProyectoFinalCS.Utils;

int[] sizes = { 2, 4, 8, 16, 32, 64, 128, 256 };
var executionTimes = new Dictionary<int, long>();

foreach (int size in sizes)
{
    string LogFilePathA = Path.Combine(AppDomain.
            CurrentDomain.BaseDirectory, "..", "..", "..", "Matrices", $"MatrixA_{size}x{size}.txt");
    string LogFilePathB = Path.Combine(AppDomain.
            CurrentDomain.BaseDirectory, "..", "..", "..", "Matrices", $"MatrixB_{size}x{size}.txt");
    // Cargar las matrices desde los archivos generados previamente
    int[,] matrixA = LoadMatrix.LoadMatrixFromFile(LogFilePathA);
    int[,] matrixB = LoadMatrix.LoadMatrixFromFile(LogFilePathB);
    
    // Usando el algoritmo de Strassen
    var context = new MatrixMultiplicationContext("strassenNaiv");
    (int[,] result, executionTimes[size]) = context.Execute(matrixA, matrixB);
}

Plot.PlotExecutionTimes(executionTimes);