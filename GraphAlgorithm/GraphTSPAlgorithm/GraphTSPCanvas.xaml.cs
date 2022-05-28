using GraphBaseFramewark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphTSPAlgorithm
{
    /// <summary>
    /// GraphTSPCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class GraphTSPCanvas : UserControl, IGraphAlgorithmCanvas
    {
        public GraphTSPCanvas()
        {
            InitializeComponent();
        }

        public string GraphAlgorithmName
        {
            get
            {
                return "TSP算法-遗传算法";
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
            ListRoundedCircle = new List<ShapeCircle>();

            for (int i = 0; i < NodeCount; i++)
            {
                int startX = random.Next(radius, GraphWidth);
                int startY = random.Next(radius, GraphHeight);
                ShapeCircle roundedCircle = new ShapeCircle()
                {
                    DisplayName = string.Format("点{0}", i),
                    CenterX = startX,
                    CenterY = startY,
                    Width = 2 * radius,
                    Height = 2 * radius
                };
                ListRoundedCircle.Add(roundedCircle);
            }
            ListShapeRelationshipLine = new List<ShapeRelationshipLine>();

            pGraphTSPAlgorithm = new GraphTSPAlgorithm(ListRoundedCircle, ListShapeRelationshipLine, GraphWidth, GraphHeight);
            ucGraphCanvas.IninGraphRelNode(ListShapeRelationshipLine, ListRoundedCircle);
        }

        GraphTSPAlgorithm pGraphTSPAlgorithm = null;

        GeneticAlgorithm pGeneticAlgorithm = null;
        private void btnIterationLocation_Click(object sender, RoutedEventArgs e)
        {

            //txtScale.Text = "30";
            //txtCityNum.Text = "48";
            //txtMAX_GEN.Text = "2000";
            //txtPc.Text = "0.8";
            //txtPm.Text = "0.9";
            //txtRatio.Text = "0.07";

            //种群规模，城市个数，最大迭代代数，交叉概率，变异概率，窗体对象
            pGeneticAlgorithm = new GeneticAlgorithm(30, ListRoundedCircle.Count, 5000, 0.8, 0.9, DrawGraphRelNode);
            pGeneticAlgorithm.Init(ListRoundedCircle);
            pGeneticAlgorithm.Solve();
            

            //int IterationCount = Convert.ToInt32(tbIterationCount.Text);
            //int ShowIterationIndex = Convert.ToInt32(tbShowIterationIndex.Text);
            //for (int i = 0; i < IterationCount; i++)
            //{
            //    pGraphTSPAlgorithm.Collide();
            //    if (i % ShowIterationIndex == 0)
            //    {
            //        ucGraphCanvas.IninGraphRelNode(ListShapeRelationshipLine, ListRoundedCircle);
            //        System.Windows.Forms.Application.DoEvents();
            //        Thread.Sleep(200);
            //    }
            //}
            //ucGraphCanvas.IninGraphRelNode(ListShapeRelationshipLine, ListRoundedCircle);
        }

        public void DrawGraphRelNode(List<ShapeCircle> listShapes)
        {
            ListShapeRelationshipLine = new List<ShapeRelationshipLine>();
            for (int i = 0; i < listShapes.Count; i++)
            {
                ShapeCircle StartShapeCircle = listShapes[i];
                ShapeCircle EndShapeCircle = listShapes[i == listShapes.Count - 1 ? 0 : i + 1];
                ShapeRelationshipLine pShapeRelationshipLine = new ShapeRelationshipLine()
                {
                    StartShapeCircle = StartShapeCircle,
                    EndShapeCircle = EndShapeCircle
                };
                ListShapeRelationshipLine.Add(pShapeRelationshipLine);
            }
            ucGraphCanvas.IninGraphRelNode(ListShapeRelationshipLine, ListRoundedCircle);
            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(20);
        }

        public override string ToString()
        {
            return this.GraphAlgorithmName;
        }

        private void btnGenerateInOrder_Click(object sender, RoutedEventArgs e)
        {
            ListShapeRelationshipLine = new List<ShapeRelationshipLine>();
            for (int i = 0; i < ListRoundedCircle.Count; i++)
            {
                ShapeCircle StartShapeCircle = ListRoundedCircle[i];
                ShapeCircle EndShapeCircle = ListRoundedCircle[i == ListRoundedCircle.Count - 1 ? 0 : i + 1];
                ShapeRelationshipLine pShapeRelationshipLine = new ShapeRelationshipLine()
                {
                    StartShapeCircle = StartShapeCircle,
                    EndShapeCircle = EndShapeCircle
                };
                ListShapeRelationshipLine.Add(pShapeRelationshipLine);
            }
            ucGraphCanvas.IninGraphRelNode(ListShapeRelationshipLine, ListRoundedCircle);
        }

        private void btnExhaustiveGeneration_Click(object sender, RoutedEventArgs e)
        {
            double MixSumLength = Double.PositiveInfinity;
            ListShapeRelationshipLine = new List<ShapeRelationshipLine>();
            List<int> listIndex = new List<int>();

            for (int i = 0; i < ListRoundedCircle.Count; i++)
            {
                listIndex.Add(i);
            }
            int StartIndex = listIndex[0];
            listIndex.Remove(0);
            List<int[]> listResult =PermutationAndCombination<int>.GetPermutation(listIndex.ToArray(), listIndex.Count);

            foreach (var item in listResult)
            {
                double TempSumLength = 0;
                List<ShapeRelationshipLine> listTemp = new List<ShapeRelationshipLine>();

                ShapeCircle pStartShapeCircle = ListRoundedCircle[0];
                ShapeCircle pEndShapeCircle = null;
                foreach (var NodeIndex in item)
                {
                    pEndShapeCircle = ListRoundedCircle[NodeIndex];

                    TempSumLength += Math.Sqrt(Math.Pow(pEndShapeCircle.CenterX - pStartShapeCircle.CenterX, 2) + Math.Pow(pEndShapeCircle.CenterY - pStartShapeCircle.CenterY, 2));
                    
                    ShapeRelationshipLine pShapeRelationshipLine = new ShapeRelationshipLine()
                    {
                        StartShapeCircle = pStartShapeCircle,
                        EndShapeCircle = pEndShapeCircle
                    };
                    listTemp.Add(pShapeRelationshipLine);
                    pStartShapeCircle = pEndShapeCircle;
                }

                pEndShapeCircle = ListRoundedCircle[0];

                TempSumLength += Math.Sqrt(Math.Pow(pEndShapeCircle.CenterX - pStartShapeCircle.CenterX, 2) + Math.Pow(pEndShapeCircle.CenterY - pStartShapeCircle.CenterY, 2));

                ShapeRelationshipLine pShapeRelationshipLineEnd = new ShapeRelationshipLine()
                {
                    StartShapeCircle = pStartShapeCircle,
                    EndShapeCircle = pEndShapeCircle
                };
                listTemp.Add(pShapeRelationshipLineEnd);
                pStartShapeCircle = pEndShapeCircle;

                if (MixSumLength > TempSumLength)
                {
                    MixSumLength = TempSumLength;
                    ListShapeRelationshipLine = listTemp;

                    ucGraphCanvas.IninGraphRelNode(ListShapeRelationshipLine, ListRoundedCircle);
                    System.Windows.Forms.Application.DoEvents();
                    Thread.Sleep(200);
                }
            }

            Console.WriteLine("完成");
        }
    }
}
