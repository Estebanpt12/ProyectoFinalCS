using System;
using System.IO;

namespace ProyectoFinalCS.Utils
{
    public static class Logger
    {
        private static readonly string LogFilePath = Path.Combine(AppDomain.
            CurrentDomain.BaseDirectory, "..", "..", "..", "Logs", "ExecutionTimes.log");

        public static void LogExecutionTime(string algorithmName, long time, int rowsA, int colsA, int rowsB, int colsB)
        {
            using (StreamWriter writer = new StreamWriter(LogFilePath, true))
            {
                writer.WriteLine($"{DateTime.Now} - Algorithm: {algorithmName}, Time: {time} ms, Matrix A: {rowsA}x{colsA}, Matrix B: {rowsB}x{colsB}");
            }
        }
    }
}
