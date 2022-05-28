using GraphBaseFramewark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphDelaunayAlgorithm
{
    /// <summary>
    /// GraphDelaunaryCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class GraphDelaunaryCanvas : UserControl, IGraphAlgorithmCanvas
    {
        public GraphDelaunaryCanvas()
        {
            InitializeComponent();
        }

        public string GraphAlgorithmName
        {
            get
            {
                return "Delaunary三角网生成算法";
            }
        }

        private void btnCreateRandomPoint_Click(object sender, RoutedEventArgs e)
        {
            CreateRandomPoint();
        }


        List<ShapePoint> ListShapePoint = new List<ShapePoint>();


        /// <summary>
        /// 创建随机点
        /// </summary>
        private void CreateRandomPoint()
        {
            Random random = new Random(GetTimeStamp());

            int GraphWidth = Convert.ToInt32(this.stackPanelFather.ActualWidth);
            int GraphHeight = Convert.ToInt32(this.stackPanelFather.ActualHeight);

            int PointCounts = Convert.ToInt32(tbNodeCount.Text);
            ListShapePoint = new List<ShapePoint>();
            for (int i = 0; i < PointCounts; i++)
            {
                int x = random.Next(0, GraphWidth);
                int y = random.Next(0, GraphHeight);

                ListShapePoint.Add(new ShapePoint() { CenterX = x, CenterY = y });
            }

            ucGraphCanvas.InitGraphShapePoint(ListShapePoint);

            InitShapeSquareMBR(GraphWidth/2, GraphHeight/2,1, 1);
        }


        private void InitShapeSquareMBR(int minX, int minY, int width, int height)
        {
            if (width > 0 && height > 0)
            {
                ShapeSquareMBR CurrentShapeSquareMBR = new ShapeSquareMBR()
                {
                    Rect_Width = width,
                    Rect_Height = height,
                    StartX = minX,
                    StartY = minY
                };

                ucGraphCanvas.AddGraphShapeSquareMBR(CurrentShapeSquareMBR);
            }
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static int GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds);
        }

        private void btnCreateMBR_Click(object sender, RoutedEventArgs e)
        {
            CreateMBR();
        }

        /// <summary>
        /// 创建最小外包矩形
        /// </summary>
        private void CreateMBR()
        {
            int max_x = 0;
            int max_y = 0;

            int GraphWidth = Convert.ToInt32(this.stackPanelFather.ActualWidth);
            int GraphHeight = Convert.ToInt32(this.stackPanelFather.ActualHeight);

            int min_x = GraphWidth;
            int min_y = GraphHeight;

            foreach (var item in ListShapePoint)
            {
                if (item.CenterX < min_x)
                {
                    min_x = Convert.ToInt32(item.CenterX);

                    ucGraphCanvas.InitGraphShapePoint(ListShapePoint);
                    InitShapeSquareMBR(min_x, min_y, max_x - min_x, max_y - min_y);
                    this.DoEvents();
                }
                if (item.CenterX > max_x)
                {
                    max_x = Convert.ToInt32(item.CenterX);

                    ucGraphCanvas.InitGraphShapePoint(ListShapePoint);
                    InitShapeSquareMBR(min_x, min_y, max_x - min_x, max_y - min_y);
                    this.DoEvents();
                }
                if (item.CenterY < min_y)
                {
                    min_y = Convert.ToInt32(item.CenterY);

                    ucGraphCanvas.InitGraphShapePoint(ListShapePoint);
                    InitShapeSquareMBR(min_x, min_y, max_x - min_x, max_y - min_y);
                    this.DoEvents();
                }
                if (item.CenterY > max_y)
                {
                    max_y = Convert.ToInt32(item.CenterY);

                    ucGraphCanvas.InitGraphShapePoint(ListShapePoint);
                    InitShapeSquareMBR(min_x, min_y, max_x - min_x, max_y - min_y);
                    this.DoEvents();
                }
            }
        }
    }
}
