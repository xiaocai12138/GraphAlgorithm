using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphBaseFramewark
{
    public class ShapePoint : Shape, IDisplayTagInfo
    {
        private readonly Path _path;
        public Geometry CurrentGeometry
        {
            get
            {
                return this._path.Data;
            }
        }
        public double CenterX
        {
            get;
            set;
        }
        public double CenterY
        {
            get;
            set;
        }
        public double Rect_Width
        {
            get;
            set;
        }
        public double Rect_Height
        {
            get;
            set;
        }
        public string DisplayName
        {
            get;
            set;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                return this.CurrentGeometry;
            }
        }

        public void SetColor()
        {
            base.Fill = Brushes.Red;
            base.Stroke = Brushes.Black;
        }

        public ShapePoint()
        {
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(new PathFigure());
            this._path = new Path
            {
                Data = pathGeometry
            };
            this.RedrawShape();
        }

        public void RedrawShape()
        {
            PathGeometry pathGeometry = this._path.Data as PathGeometry;
            bool flag = pathGeometry == null;
            if (!flag)
            {
                PathFigure pathFigure = pathGeometry.Figures[0];
                pathGeometry.AddGeometry(new EllipseGeometry
                {
                    RadiusX = 1,
                    RadiusY = 1,
                    Center = new Point(this.CenterX, this.CenterY)
                });
            }
        }
    }
}
