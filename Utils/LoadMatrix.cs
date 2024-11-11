using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Utils
{
    public class LoadMatrix
    {
        public static int[,] LoadMatrixFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int cols = lines[0].Split(' ').Length;

            int[,] matrix = new int[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                string[] values = lines[i].Split(' ');
                for (int j = 0; j < values.Length; j++)
                {
                    matrix[i, j] = int.Parse(values[j]);
                }
            }
            return matrix;
        }

    }
}