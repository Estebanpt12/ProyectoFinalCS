using System;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.Windows.Forms;
using OxyPlot.WindowsForms;

namespace ProyectoFinalCS.Utils
{
    public class Plot
    {
        public static void PlotExecutionTimes(Dictionary<int, long> executionTimes)
        {
            // Comprobar los datos antes de graficar
            Console.WriteLine("Datos de ejecución:");
            foreach (var size in executionTimes)
            {
                Console.WriteLine($"{size.Key}: {size.Value} ms");
            }

            // Crear el modelo de gráfico
            var plotModel = new PlotModel { Title = "Execution Times for Matrix Multiplication" };

            // Crear la serie de barras
            var barSeries = new BarSeries
            {
                ItemsSource = new List<BarItem>(),
                LabelPlacement = LabelPlacement.Outside,
                LabelFormatString = "{0} ms",
                FillColor = OxyColors.Blue // Establecer color de las barras
            };

            // Llenar los valores de la serie con el tiempo de ejecución
            foreach (var size in executionTimes.Keys)
            {
                long time = executionTimes[size];
                if (time == 0)
                {
                    time = 1; // Asignar un valor mínimo visible (por ejemplo 1 ms)
                }

                barSeries.Items.Add(new BarItem { Value = time });
            }

            // Crear y agregar el eje X (eje de valores de tiempo de ejecución)
            var linearAxisX = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,  // Valor mínimo del eje X
                Maximum = 3, // Ajustar el máximo
                IsZoomEnabled = false,
                IsPanEnabled = false
            };

            // Crear y agregar el eje Y (eje de categorías)
            var categoryAxisY = new CategoryAxis
            {
                Position = AxisPosition.Left
            };

            // Asumimos que las claves del diccionario son las dimensiones de las matrices
            foreach (var size in executionTimes.Keys)
            {
                categoryAxisY.Labels.Add($"{size}x{size}");
            }

            // Agregar la serie y los ejes al modelo
            plotModel.Series.Add(barSeries);
            plotModel.Axes.Add(linearAxisX);
            plotModel.Axes.Add(categoryAxisY);

            // Crear la vista del gráfico
            var plotView = new OxyPlot.WindowsForms.PlotView
            {
                Model = plotModel,
                Dock = DockStyle.Fill
            };

            // Crear el formulario
            var form = new Form
            {
                Text = "Execution Times Graph",
                Width = 800,
                Height = 600
            };

            // Asegurarse de que el PlotView se agregue correctamente al formulario
            form.Controls.Clear();
            form.Controls.Add(plotView);

            // Iniciar la aplicación de Windows Forms
            Application.Run(form);
        }
    }
}