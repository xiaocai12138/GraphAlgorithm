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

namespace GraphShortestPathAlgorithm
{
    /// <summary>
    /// GraphShortestPathCnavas.xaml 的交互逻辑
    /// </summary>
    public partial class GraphShortestPathCnavas : UserControl, IGraphAlgorithmCanvas
    {
        public GraphShortestPathCnavas()
        {
            InitializeComponent();
        }

        public string GraphAlgorithmName
        {
            get
            {
                return "最短路径算法";
            }
        }


        Random random = new Random();
        int radius = 40;

        List<ShapeCircle> ListRoundedCircle = null;
        List<ShapeRelationshipLine> ListShapeRelationshipLine = null;
        private void btnCreateRelNode_Click(object sender, RoutedEventArgs e)
        {

            int GraphWidth = Convert.ToInt32(this.stackPanelFather.ActualWidth) - radius;
            int GraphHeight = Convert.ToInt32(this.stackPanelFather.ActualHeight) - radius;

            int NodeCount = Convert.ToInt32(tbNodeCount.Text);
            int RelCount = Convert.ToInt32(tbRelCount.Text);


            ListRoundedCircle = new List<ShapeCircle>();

            for (int i = 0; i < NodeCount; i++)
            {
                int startX = random.Next(radius, GraphWidth);
                int startY = random.Next(radius, GraphHeight);
                ShapeCircle roundedCircle = new ShapeCircle()
                {
                    DisplayName = string.Format("测试{0}", i),
                    CenterX = startX,
                    CenterY = startY,
                    Width = 2 * radius,
                    Height = 2 * radius
                };
                ListRoundedCircle.Add(roundedCircle);
            }

            Dictionary<string, Tuple<int, int>> dicHas = new Dictionary<string, Tuple<int, int>>();

            for (int i = 0; i < RelCount; i++)
            {
                int StartNodeIndex = random.Next(0, ListRoundedCircle.Count);
                int EndNodeIndex = random.Next(0, ListRoundedCircle.Count);

                if (StartNodeIndex == EndNodeIndex)
                {
                    continue;
                }
                if (StartNodeIndex > EndNodeIndex)
                {
                    int TempIndex = EndNodeIndex;
                    EndNodeIndex = StartNodeIndex;
                    StartNodeIndex = TempIndex;
                }
                string strKey = $"{StartNodeIndex},{EndNodeIndex}";
                if (dicHas.ContainsKey(strKey))
                {
                    continue;
                }
                else
                {
                    dicHas.Add(strKey, new Tuple<int, int>(StartNodeIndex, EndNodeIndex));
                }
            }

            ListShapeRelationshipLine = new List<ShapeRelationshipLine>();
            foreach (var item in dicHas)
            {
                int StartNodeIndex = item.Value.Item1;
                int EndNodeIndex = item.Value.Item2;
                ShapeCircle StartShapeCircle = ListRoundedCircle[StartNodeIndex];
                ShapeCircle EndShapeCircle = ListRoundedCircle[EndNodeIndex];
                ShapeRelationshipLine pShapeRelationshipLine = new ShapeRelationshipLine()
                {
                    StartShapeCircle = StartShapeCircle,
                    EndShapeCircle = EndShapeCircle
                };
                ListShapeRelationshipLine.Add(pShapeRelationshipLine);
            }

            ucGraphCanvas.IninGraphRelNode(ListShapeRelationshipLine, ListRoundedCircle);
        }

        private void btnIterationLocation_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
