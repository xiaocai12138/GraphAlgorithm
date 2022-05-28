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
	public class ShapeCircle : Shape, IDisplayTagInfo
	{
		public static double CircleRadius = 20;
		private readonly Path _path;
		public double Radius
		{
			get
			{
				return CircleRadius;
			}
			private set
			{
				CircleRadius = value;
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
		public string DisplayName
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

		public override string ToString()
		{
			//return $"{this.DisplayName}:X={this.CenterX},Y:{this.CenterY}";
			return $"{this.DisplayName}";
		}

		private void SetRadius()
		{
			double num = (base.Width < base.Height) ? base.Width : base.Height;
			this.Radius = (double)Convert.ToInt32((num - base.StrokeThickness) / 2.0);
		}

		public ShapeCircle()
		{
			PathGeometry pathGeometry = new PathGeometry();
			pathGeometry.Figures.Add(new PathFigure());
			this._path = new Path
			{
				Data = pathGeometry
			};
			this.RedrawShape();                
			//this.StrokeThickness = 5;
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
					RadiusX = this.Radius,
					RadiusY = this.Radius,
					Center = new Point(this.Radius, this.Radius)
				});
			}
		}

		protected override void OnRender(DrawingContext dc)
		{
			base.OnRender(dc);
			dc.DrawText(new FormattedText(this.DisplayName, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("YaHei"), 12.0, Brushes.Green), new Point(0.0, this.Radius));
		}

		public void SetColor()
		{
			base.Fill = Brushes.Red;
			base.Stroke = Brushes.Black;
		}

		public void SetDisplayName()
		{
		}
	}
}
