using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.FSharp.Core;
using Plotly.NET;
using Plotly.NET.LayoutObjects;
using ProyectoFinalCS.Model;

namespace ProyectoFinalCS.Utils
{
    public class Plot
    {
        public static void PlotExecutionTimes(List<ExecutionResult> results)
        {
            int lengthAlgorithms = Constants.Algorithms.Count;
            foreach (var size in Constants.Sizes)
            {
                int i = 0;
                var tiempos = new long[lengthAlgorithms];
                foreach (var algorithm in Constants.Algorithms)
                {
                    tiempos[i] = results.First(x => x.GetName() == algorithm)
                        .GetTime($"{size}x{size}");
                    i += 1;
                }

                // Crear el gráfico de columnas
                var chart = Chart2D.Chart.Column<long, string, int, int, int>(
                    tiempos, Constants.Algorithms)
                    .WithTitle($"Tiempos de ejecución para el tamaño {size}x{size}") // Título del gráfico
                    .WithXAxisStyle(title: Title.init("Algoritmos")) // Título del eje X
                    .WithYAxisStyle(title: Title.init("Tiempo (ms)")); // Título del eje Y

                // Especificamos explícitamente los tipos en Layout.init
                var layout = Layout.init<int>(
                    Margin: FSharpOption<Margin>.Some(Margin.init<int, int, int, int, int, int>( // Especificamos explícitamente los tipos
                        Left: FSharpOption<int>.Some(50), // Margen izquierdo
                        Right: FSharpOption<int>.Some(50), // Margen derecho
                        Top: FSharpOption<int>.Some(50), // Margen superior
                        Bottom: FSharpOption<int>.Some(50) // Margen inferior
                    )),
                    Width: FSharpOption<int>.Some(1000), // Ancho del gráfico
                    Height: FSharpOption<int>.Some(600) // Alto del gráfico
                );

                // Aplicar el layout al gráfico
                chart.WithLayout(layout);

                // Mostrar el gráfico
                chart.Show();

                // Limpiar los tiempos para el siguiente tamaño
                Array.Clear(tiempos, 0, tiempos.Length);
            }
        }
    }
}
