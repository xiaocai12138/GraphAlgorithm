using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphBaseFramewark
{
    public class ShapeSquareMBR: ShapeSquare
	{
		public override void RedrawShape()
		{
			PathGeometry pathGeometry = _path.Data as PathGeometry;
			bool flag = pathGeometry == null;
			if (!flag)
			{
				PathFigure pathFigure = pathGeometry.Figures[0];
				RectangleGeometry rectangle = new RectangleGeometry()
				{
					Rect = new System.Windows.Rect(StartX, StartY, Rect_Width, Rect_Height)
				};
				pathGeometry.AddGeometry(rectangle);
			}
		}
		public override void SetColor()
        {
            base.Fill = Brushes.Transparent;
            base.Stroke = Brushes.Black;
            base.StrokeThickness = 0.5;
        }
    }
}
