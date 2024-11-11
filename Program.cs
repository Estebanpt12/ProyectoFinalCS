using System;
using System.Collections.Generic;
using System.IO;
using ProyectoFinalCS.AlgoritmosImpl;
using ProyectoFinalCS.Model;
using ProyectoFinalCS.Utils;

var executionTimes = new Dictionary<string, long>();
var results = new List<ExecutionResult>();

foreach (var algorithm in Constants.Algorithms)
{
    foreach (int size in Constants.Sizes)
    {
        string LogFilePathA = Path.Combine(AppDomain.
                CurrentDomain.BaseDirectory, "..", "..", "..", "Matrices", $"MatrixA_{size}x{size}.txt");
        string LogFilePathB = Path.Combine(AppDomain.
                CurrentDomain.BaseDirectory, "..", "..", "..", "Matrices", $"MatrixB_{size}x{size}.txt");
        // Cargar las matrices desde los archivos generados previamente
        int[,] matrixA = LoadMatrix.LoadMatrixFromFile(LogFilePathA);
        int[,] matrixB = LoadMatrix.LoadMatrixFromFile(LogFilePathB);

        // Usando el algoritmo de Strassen
        var context = new MatrixMultiplicationContext(algorithm);
        (_, executionTimes[$"{size}x{size}"]) = context.Execute(matrixA, matrixB);
    }
    results.Add(new ExecutionResult(algorithm, new Dictionary<string, long>(executionTimes)));
    executionTimes.Clear();
}

Plot.PlotExecutionTimes(results);