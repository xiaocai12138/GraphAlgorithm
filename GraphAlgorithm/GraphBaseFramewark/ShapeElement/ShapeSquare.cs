using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphBaseFramewark
{
    public class ShapeSquare : Shape, IDisplayTagInfo
	{
		public string DisplayName
		{
			get;
			set;
		}

		public double g_width = 40;

        public double StartX
		{
			get;
			set;
		}
        public double StartY
		{
			get;
			set;
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

		protected Path _path;

		public Geometry CurrentGeometry
		{
			get
			{
				if (this._path == null)
				{
					PathGeometry pathGeometry = new PathGeometry();
					pathGeometry.Figures.Add(new PathFigure());
					this._path = new Path
					{
						Data = pathGeometry
					};
					this.RedrawShape();
				}
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
		public ShapeSquare()
		{
		}

		public virtual void RedrawShape()
		{
			PathGeometry pathGeometry = this._path.Data as PathGeometry;
			bool flag = pathGeometry == null;
			if (!flag)
			{
				PathFigure pathFigure = pathGeometry.Figures[0];
				pathGeometry.AddGeometry(new RectangleGeometry()
				{
					Rect = new System.Windows.Rect(CenterX-(g_width/2), CenterY - (g_width / 2), g_width, g_width)
				});
			}
		}

		protected override void OnRender(DrawingContext dc)
		{
			base.OnRender(dc);
		}

		public virtual void SetColor()
		{
			base.Fill = Brushes.White;
			base.Stroke = Brushes.Black;
			base.StrokeThickness = 4.0;
		}
    }
}
