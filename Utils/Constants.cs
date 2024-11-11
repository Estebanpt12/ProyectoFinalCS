using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCS.Utils
{
    public static class Constants
    {
        public readonly static int[] Sizes = [2, 4, 8, 16, 32, 64, 128, 256];
        public readonly static List<string> Algorithms = [
                "StrassenWinograd",
                "WinogradOriginal",
                "WinogradScaled",
                "NaivLoopUnrollingFour",
                "NaivLoopUnrollingTwo",
                "NaivOnArray",
                "V3SequentialBloc",
                "V4ParallelBlock",
                "StrassenNaiv",
                "IV3SequentialBlock"
        ];
    }
}