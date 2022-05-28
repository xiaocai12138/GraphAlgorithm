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
	public class ShapeCornersPolygon : Shape
	{
		private readonly Path _path;
		private PointCollection _points;
		private bool _isClosed;
		private bool _useRoundnessPercentage;
		private double _arcRoundness;
		public PointCollection Points
		{
			get
			{
				return this._points;
			}
			set
			{
				this._points = value;
				this.RedrawShape();
			}
		}
		public bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
			set
			{
				this._isClosed = value;
				this.RedrawShape();
			}
		}
		public bool UseRoundnessPercentage
		{
			get
			{
				return this._useRoundnessPercentage;
			}
			set
			{
				this._useRoundnessPercentage = value;
				this.RedrawShape();
			}
		}
		public double ArcRoundness
		{
			get
			{
				return this._arcRoundness;
			}
			set
			{
				this._arcRoundness = value;
				this.RedrawShape();
			}
		}
		public Geometry Data
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
				return this._path.Data;
			}
		}
		public ShapeCornersPolygon()
		{
			PathGeometry pathGeometry = new PathGeometry();
			pathGeometry.Figures.Add(new PathFigure());
			this._path = new System.Windows.Shapes.Path
			{
				Data = pathGeometry
			};
			this.Points = new PointCollection();
			this.Points.Changed += new EventHandler(this.Points_Changed);
		}
		private void Points_Changed(object sender, EventArgs e)
		{
			this.RedrawShape();
		}
		private void RedrawShape()
		{
			PathGeometry pathGeometry = this._path.Data as PathGeometry;
			bool flag = pathGeometry == null;
			if (!flag)
			{
				PathFigure pathFigure = pathGeometry.Figures[0];
				pathFigure.Segments.Clear();
				for (int i = 0; i < this.Points.Count; i++)
				{
					int num = i;
					if (num != 0)
					{
						if (num != 1)
						{
							this.AddPointToPath(this.Points[i], new Point?(this.Points[i - 1]), new Point?(this.Points[i - 2]));
						}
						else
						{
							Point arg_A3_1 = this.Points[i];
							Point? arg_A3_2 = new Point?(this.Points[i - 1]);
							Point? point = null;
							this.AddPointToPath(arg_A3_1, arg_A3_2, point);
						}
					}
					else
					{
						Point arg_71_1 = this.Points[i];
						Point? point = null;
						Point? arg_71_2 = point;
						point = null;
						this.AddPointToPath(arg_71_1, arg_71_2, point);
					}
				}
				bool isClosed = this.IsClosed;
				if (isClosed)
				{
					this.CloseFigure(pathFigure);
				}
			}
		}
		private void AddPointToPath(Point currentPoint, Point? prevPoint, Point? prevPrevPoint)
		{
			bool flag = this.Points.Count == 0;
			if (!flag)
			{
				PathGeometry pathGeometry = this._path.Data as PathGeometry;
				bool flag2 = pathGeometry == null;
				if (!flag2)
				{
					PathFigure pathFigure = pathGeometry.Figures[0];
					bool flag3 = !prevPoint.HasValue;
					if (flag3)
					{
						pathFigure.StartPoint = currentPoint;
					}
					else
					{
						bool flag4 = !prevPrevPoint.HasValue;
						if (flag4)
						{
							LineSegment value = new LineSegment
							{
								Point = currentPoint
							};
							pathFigure.Segments.Add(value);
						}
						else
						{
							ShapeCornersPolygon.ConnectLinePoints(pathFigure, prevPrevPoint.Value, prevPoint.Value, currentPoint, this.ArcRoundness, this.UseRoundnessPercentage);
						}
					}
				}
			}
		}
		private void CloseFigure(PathFigure pathFigure)
		{
			bool flag = this.Points.Count < 3;
			if (!flag)
			{
				bool useRoundnessPercentage = this.UseRoundnessPercentage;
				Point p;
				Point point;
				if (useRoundnessPercentage)
				{
					p = ShapeCornersPolygon.GetPointAtDistancePercent(this.Points[this.Points.Count - 1], this.Points[0], this.ArcRoundness, false);
					point = ShapeCornersPolygon.GetPointAtDistancePercent(this.Points[0], this.Points[1], this.ArcRoundness, true);
				}
				else
				{
					p = ShapeCornersPolygon.GetPointAtDistance(this.Points[this.Points.Count - 1], this.Points[0], this.ArcRoundness, false);
					point = ShapeCornersPolygon.GetPointAtDistance(this.Points[0], this.Points[1], this.ArcRoundness, true);
				}
				ShapeCornersPolygon.ConnectLinePoints(pathFigure, this.Points[this.Points.Count - 2], this.Points[this.Points.Count - 1], p, this.ArcRoundness, this.UseRoundnessPercentage);
				QuadraticBezierSegment value = new QuadraticBezierSegment
				{
					Point1 = this.Points[0],
					Point2 = point
				};
				pathFigure.Segments.Add(value);
				pathFigure.StartPoint = point;
			}
		}
		private static void ConnectLinePoints(PathFigure pathFigure, Point p1, Point p2, Point p3, double roundness, bool usePercentage)
		{
			Point point;
			Point point2;
			if (usePercentage)
			{
				point = ShapeCornersPolygon.GetPointAtDistancePercent(p1, p2, roundness, false);
				point2 = ShapeCornersPolygon.GetPointAtDistancePercent(p2, p3, roundness, true);
			}
			else
			{
				point = ShapeCornersPolygon.GetPointAtDistance(p1, p2, roundness, false);
				point2 = ShapeCornersPolygon.GetPointAtDistance(p2, p3, roundness, true);
			}
			int index = pathFigure.Segments.Count - 1;
			((LineSegment)pathFigure.Segments[index]).Point = point;
			QuadraticBezierSegment value = new QuadraticBezierSegment
			{
				Point1 = p2,
				Point2 = point2
			};
			pathFigure.Segments.Add(value);
			LineSegment value2 = new LineSegment
			{
				Point = p3
			};
			pathFigure.Segments.Add(value2);
		}
		private static Point GetPointAtDistancePercent(Point p1, Point p2, double distancePercent, bool firstPoint)
		{
			double num = firstPoint ? (distancePercent / 100.0) : ((100.0 - distancePercent) / 100.0);
			return new Point(p1.X + num * (p2.X - p1.X), p1.Y + num * (p2.Y - p1.Y));
		}
		private static Point GetPointAtDistance(Point p1, Point p2, double distance, bool firstPoint)
		{
			double num = Math.Sqrt(Math.Pow(p2.X - p1.X, 2.0) + Math.Pow(p2.Y - p1.Y, 2.0));
			bool flag = distance > num / 2.0;
			if (flag)
			{
				distance = num / 2.0;
			}
			double num2 = firstPoint ? (distance / num) : ((num - distance) / num);
			return new Point(p1.X + num2 * (p2.X - p1.X), p1.Y + num2 * (p2.Y - p1.Y));
		}
	}
}
