using GraphBaseFramewark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphAStarAlgorithm
{
    public class ShapeSquare_BlockingPoint:ShapeSquare, IDisplayTagInfo
    {
        public override void SetColor()
        {
            base.Fill = Brushes.Red;
            base.Stroke = Brushes.Black;
        }
    }
}
