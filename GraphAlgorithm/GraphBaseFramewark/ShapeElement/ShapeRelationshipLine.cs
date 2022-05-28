using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphBaseFramewark
{
    public class ShapeRelationshipLine : Shape, IDisplayTagInfo
    {
        private Path _path;
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

        public ShapeCircle StartShapeCircle
        {
            get;
            set;
        }

        public ShapeCircle EndShapeCircle
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

        private string m_DisplayName = "";
        public string DisplayName
        {
            get
            {
                return m_DisplayName;
            }
            set
            {
                m_DisplayName = value;
            }
        }

        public Geometry CurrentGeometry
        {
            get
            {
                return this._path.Data;
            }
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
        }
        public ShapeRelationshipLine()
        {
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(new PathFigure());
            this._path = new Path
            {
                Data = pathGeometry
            };
        }

        LineGeometry lineGeometry = null;
        public void RedrawShape()
        {
            PathGeometry pathGeometry = this._path.Data as PathGeometry;
            bool flag = pathGeometry == null;
            if (!flag)
            {
                PathFigure pathFigure = pathGeometry.Figures[0];
                if (pathGeometry.Figures.Count > 1)
                {
                    pathGeometry.Figures.RemoveAt(1);
                }
                lineGeometry = new LineGeometry
                {
                    StartPoint = new Point(StartShapeCircle.CenterX + ShapeCircle.CircleRadius, StartShapeCircle.CenterY + ShapeCircle.CircleRadius),
                    EndPoint = new Point(EndShapeCircle.CenterX + ShapeCircle.CircleRadius, EndShapeCircle.CenterY + ShapeCircle.CircleRadius)
                };
                pathGeometry.AddGeometry(lineGeometry);

            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            this.RedrawShape();
            base.OnRender(dc);
            dc.DrawText(new FormattedText(this.DisplayName, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("YaHei"), 12.0, Brushes.Green), new Point(StartShapeCircle.CenterX + ShapeCircle.CircleRadius, StartShapeCircle.CenterY + ShapeCircle.CircleRadius));
        }
    }
}
