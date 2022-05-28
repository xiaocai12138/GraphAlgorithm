using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GraphBaseFramewark
{
	public class GraphCanvas : Canvas
	{
		private bool isDragging;
		private Point startPosition;
		public GraphCanvas()
		{
			base.Focusable = true;
			base.HorizontalAlignment = HorizontalAlignment.Stretch;
			base.VerticalAlignment = VerticalAlignment.Stretch;
			base.Background = Brushes.Transparent;
			//InitializeChildControl();
		}
		public void InitializeChildControl()
		{
			Random random = new Random();
			for (int i = 0; i < 10; i++)
			{
				ShapeSquare roundedCircle = new ShapeSquare
				{
					DisplayName = "xiaocai测试" + i,
					Width = 100.0,
					Height = 100.0
				};
				roundedCircle.StrokeThickness = 4.0;
				roundedCircle.Stroke = Brushes.YellowGreen;
				roundedCircle.Fill = Brushes.BlanchedAlmond;
				Canvas.SetLeft(roundedCircle, random.NextDouble() * base.ActualWidth);
				Canvas.SetTop(roundedCircle, random.NextDouble() * base.ActualHeight);
				base.Children.Add(roundedCircle);
				roundedCircle.MouseLeftButtonDown += new MouseButtonEventHandler(this.Circle_MouseLeftButtonDown);
				roundedCircle.MouseLeftButtonUp += new MouseButtonEventHandler(this.Circle_MouseLeftButtonUp);
				roundedCircle.MouseMove += new MouseEventHandler(this.Circle_MouseMove);
				roundedCircle.MouseDown += new MouseButtonEventHandler(this.Circle_MouseDown);
			}
		}

		public void InitGraphShapeSquare(List<ShapeSquare> listShapeSquare)
		{
			base.Children.Clear();

            foreach (ShapeSquare pShapeSquare in listShapeSquare)
			{
				Canvas.SetLeft(pShapeSquare, pShapeSquare.StartX);
				Canvas.SetTop(pShapeSquare, pShapeSquare.StartY);
				pShapeSquare.SetColor();
				base.Children.Add(pShapeSquare);
			}
		}

		public void InitGraphShapePoint(List<ShapePoint> listShapePoint)
		{
			base.Children.Clear();

			foreach (ShapePoint pShapePoint in listShapePoint)
			{
				Canvas.SetLeft(pShapePoint, pShapePoint.CenterX);
				Canvas.SetTop(pShapePoint, pShapePoint.CenterY);
				pShapePoint.SetColor();
				base.Children.Add(pShapePoint);
			}
		}

		public void AddGraphShapeSquareMBR(ShapeSquareMBR pShapeSquareMBR)
		{
			pShapeSquareMBR.SetColor();
			base.Children.Add(pShapeSquareMBR);
		}

		private List<ShapeRelationshipLine> ListShapeRelationshipLine = null;
		private List<ShapeCircle> ListShapeCircle=null;

		public void IninGraphRelNode(List<ShapeRelationshipLine> listShapeRelationshipLine,List<ShapeCircle> listRoundedCircle)
		{
			ListShapeRelationshipLine = listShapeRelationshipLine;
			ListShapeCircle = listRoundedCircle;
			base.Children.Clear();
			foreach (var relationshipLine in ListShapeRelationshipLine)
			{
				relationshipLine.Stroke = Brushes.YellowGreen;
				relationshipLine.Fill = Brushes.BlanchedAlmond;
				Canvas.SetLeft(relationshipLine,0);
				Canvas.SetTop(relationshipLine, 0);
				relationshipLine.Margin = new Thickness(0);
				base.Children.Add(relationshipLine);
			}

			foreach (var roundedCircle in ListShapeCircle)
			{
				roundedCircle.Stroke = Brushes.YellowGreen;
				roundedCircle.Fill = Brushes.BlanchedAlmond;
				Canvas.SetLeft(roundedCircle, roundedCircle.CenterX);
				Canvas.SetTop(roundedCircle, roundedCircle.CenterY);
				base.Children.Add(roundedCircle);

				roundedCircle.MouseLeftButtonDown += new MouseButtonEventHandler(this.Circle_MouseLeftButtonDown);
				roundedCircle.MouseLeftButtonUp += new MouseButtonEventHandler(this.Circle_MouseLeftButtonUp);
				roundedCircle.MouseMove += new MouseEventHandler(this.Circle_MouseMove);
			}
		}

		private void Circle_MouseDown(object sender, MouseButtonEventArgs e)
		{
			bool flag = e.ClickCount == 2;
			if (flag)
			{
				IDisplayTagInfo displayTagInfo = sender as IDisplayTagInfo;
				displayTagInfo.SetColor();
			}
		}

		private void Circle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.isDragging = true;
			UIElement uIElement = sender as UIElement;
			Point position = e.GetPosition(this);
			this.startPosition.X = position.X - Canvas.GetLeft(uIElement);
			this.startPosition.Y = position.Y - Canvas.GetTop(uIElement);
			uIElement.CaptureMouse();
		}

		private void Circle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.isDragging = false;
			UIElement uIElement = sender as UIElement;
			uIElement.ReleaseMouseCapture();
		}

		private void Circle_MouseMove(object sender, MouseEventArgs e)
		{
			UIElement uIElement = sender as UIElement;
			bool flag = this.isDragging && uIElement != null;
			if (flag)
			{
				Point position = e.GetPosition(base.Parent as UIElement);
				TranslateTransform translateTransform = uIElement.RenderTransform as TranslateTransform;
				
				Canvas.SetLeft(uIElement, position.X - this.startPosition.X);
				Canvas.SetTop(uIElement, position.Y - this.startPosition.Y);

				if (uIElement is ShapeCircle)
				{
					ShapeCircle shapeCircle = uIElement as ShapeCircle;
					
					Point shapeCirclePosition = e.GetPosition(uIElement);
					shapeCircle.CenterX = position.X - this.startPosition.X;
					shapeCircle.CenterY = position.Y - this.startPosition.Y;

					List<ShapeRelationshipLine> listFindAllLine=ListShapeRelationshipLine.FindAll(a => a.StartShapeCircle == shapeCircle || a.EndShapeCircle == shapeCircle);
					foreach (var item in listFindAllLine)
					{
						item.InvalidateVisual();
					}
				}
			}
		}
	}
}
