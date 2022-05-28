using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GraphBaseFramewark
{
    public static class GraphAlgorithmCanvasExpand
    {
        public static bool IsCurrentGraphAlgorithm(this IGraphAlgorithmCanvas graphAlgorithmCanvas, string strGraphAlgorithmName)
        {
            return graphAlgorithmCanvas.GraphAlgorithmName == strGraphAlgorithmName;
        }

        public static void DoEvents(this IGraphAlgorithmCanvas graphAlgorithmCanvas)
        {
            Thread.Sleep(10);
            System.Windows.Forms.Application.DoEvents();
        }
    }
}
