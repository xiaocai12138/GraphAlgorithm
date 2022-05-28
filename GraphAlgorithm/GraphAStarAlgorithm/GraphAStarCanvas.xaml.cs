using GraphAStarAlgorithm;
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

namespace GraphLayoutAlgorithm
{
    /// <summary>
    /// GraphLayoutCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class GraphAStarCanvas : UserControl, IGraphAlgorithmCanvas
    {
        public GraphAStarCanvas()
        {
            InitializeComponent();
        }


        public string GraphAlgorithmName
        {
            get
            {
                return "A星算法";
            }
        }

        List<ShapeSquare> listShapeSquare = new List<ShapeSquare>();

        Random random = null;

        ShapeSquare[,] PlotShapeSquare = null;
        int CrosswiseNodeCount = 0;
        int LengthwaysNodeCount =0;
        private void btnCreateRelNode_Click(object sender, RoutedEventArgs e)
        {
            listShapeSquare = new List<ShapeSquare>();

            CrosswiseNodeCount = Convert.ToInt32(tbCrosswiseNodeCount.Text);
            LengthwaysNodeCount = Convert.ToInt32(tbLengthwaysNodeCount.Text);

            int BlockingPointCount = Convert.ToInt32(tbBlockingPointCount.Text);

            PlotShapeSquare = new ShapeSquare[CrosswiseNodeCount, LengthwaysNodeCount];

            int startX = 50;
            int startY = 50;

            string strDateTime = DateTime.Now.ToString("MMddHHmmss").ToString();
            random = new Random(Convert.ToInt32(strDateTime));

            for (int i = 0; i < CrosswiseNodeCount; i++)
            {
                startX = 50;
                for (int j = 0; j < LengthwaysNodeCount; j++)
                {
                    startX = startX + 40;
                    ShapeSquare pShapeSquare = null;

                    if (random.Next(0, 100) < BlockingPointCount)
                    {
                        pShapeSquare = new ShapeSquare_BlockingPoint
                        {
                            DisplayName = "xiaocai测试" + i,
                            Width = 40,
                            Height = 40,
                            StartX = startX,
                            StartY = startY
                        };
                    }
                    else
                    {
                        pShapeSquare = new ShapeSquare
                        {
                            DisplayName = "xiaocai测试" + i,
                            Width = 40,
                            Height = 40,
                            StartX = startX,
                            StartY = startY
                        };
                    }
                    PlotShapeSquare[i, j] = pShapeSquare;

                    pShapeSquare.Tag =new Tuple<int, int>(i,j);

                    pShapeSquare.MouseLeftButtonDown += new MouseButtonEventHandler(this.ShapeSquare_MouseLeftButtonDown);
                    listShapeSquare.Add(pShapeSquare);
                }
                startY = startY + 40;
            }
            ucGraphCanvas.Width = 2000;
            ucGraphCanvas.Height = 2000;
            ucGraphCanvas.InitGraphShapeSquare(listShapeSquare);
        }

        ShapeSquare pStartShapeSquare = null;
        ShapeSquare pEndShapeSquare = null;

        bool isStartPoint = true;

        private void ShapeSquare_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool flag = e.ClickCount == 2;
            if (flag)
            {
                if (isStartPoint)
                {
                    pStartShapeSquare = sender as ShapeSquare;
                    pStartShapeSquare.Fill = Brushes.DarkCyan;
                }
                else
                {
                    pEndShapeSquare = sender as ShapeSquare;
                    pEndShapeSquare.Fill = Brushes.DarkCyan;
                }
                isStartPoint = !isStartPoint;
            }
        }

        public override string ToString()
        {
            return this.GraphAlgorithmName;
        }


        Dictionary<string, bool> dicCache = new Dictionary<string, bool>();

        /// <summary>
        /// 深度优先
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDFS_Click(object sender, RoutedEventArgs e)
        {
            dicCache = new Dictionary<string, bool>();

            Tuple<int, int> pStartIndex = pStartShapeSquare.Tag as Tuple<int, int>;
            //dicCache.Add($"{pStartIndex.Item1},{pStartIndex.Item2}", true);

            
            DFS_WayFinding(pStartIndex.Item1, pStartIndex.Item2);

        }

        private bool DFS_WayFinding(int index1, int index2)
        {
            if (index1 < 0 || index1 >= CrosswiseNodeCount)
            {
                return false;
            }

            if (index2 < 0 || index2 >= LengthwaysNodeCount)
            {
                return false;
            }
            string strTag = $"{index1},{index2}";
            if (dicCache.ContainsKey(strTag))
            {
                return false;
            }
            else
            {
                dicCache.Add(strTag,true);
            }

            ShapeSquare shapeSquare = PlotShapeSquare[index1, index2];

            if (shapeSquare is ShapeSquare_BlockingPoint)
            {
                return false;
            }
            else if (shapeSquare == pEndShapeSquare)
            {
                return true;
            }
            //dicCache.Add(, true);

            shapeSquare.Fill = Brushes.BurlyWood;
            Thread.Sleep(10);
            System.Windows.Forms.Application.DoEvents();

            if (DFS_WayFinding(index1 - 1, index2))
            {
                return true;
            }
            else if (DFS_WayFinding(index1, index2 - 1))
            {
                return true;
            }
            else if (DFS_WayFinding(index1 + 1, index2))
            {
                return true;
            }
            else if (DFS_WayFinding(index1, index2 + 1))
            {
                return true;
            }
            return false;
        }

        

        private void btnBFS_Click(object sender, RoutedEventArgs e)
        {
            dicCache = new Dictionary<string, bool>();

            Tuple<int, int> pStartIndex = pStartShapeSquare.Tag as Tuple<int, int>;

            BFS_WayFinding(pStartIndex);
        }
        
        /// <summary>
         /// 广度优先
         /// </summary>
         /// <param name=""></param>
         /// <returns></returns>
        private bool BFS_WayFinding(Tuple<int, int> pIndex)
        {
            Queue<Tuple<int, int>> BFSQueue = new Queue<Tuple<int, int>>();
            BFSQueue.Enqueue(pIndex);
            string strTag = $"{pIndex.Item1},{pIndex.Item2}";
            dicCache.Add(strTag, true);

            while (BFSQueue.Count!=0)
            {
                Tuple<int, int> pShapeSquareIndex= BFSQueue.Dequeue();

                ShapeSquare shapeSquare = PlotShapeSquare[pShapeSquareIndex.Item1, pShapeSquareIndex.Item2]; 

                if (shapeSquare == pEndShapeSquare)
                {
                    return true;
                }

                shapeSquare.Fill = Brushes.BurlyWood;
                Thread.Sleep(10);
                System.Windows.Forms.Application.DoEvents();

                if (IsRange(pShapeSquareIndex.Item1-1, pShapeSquareIndex.Item2))
                {
                    BFSQueue.Enqueue(new Tuple<int, int>(pShapeSquareIndex.Item1 - 1, pShapeSquareIndex.Item2));
                }

                if (IsRange(pShapeSquareIndex.Item1 , pShapeSquareIndex.Item2 - 1))
                {
                    BFSQueue.Enqueue(new Tuple<int, int>(pShapeSquareIndex.Item1, pShapeSquareIndex.Item2 - 1));
                }

                if (IsRange(pShapeSquareIndex.Item1 + 1, pShapeSquareIndex.Item2))
                {
                    BFSQueue.Enqueue(new Tuple<int, int>(pShapeSquareIndex.Item1 + 1, pShapeSquareIndex.Item2));
                }

                if (IsRange(pShapeSquareIndex.Item1 , pShapeSquareIndex.Item2 + 1))
                {
                    BFSQueue.Enqueue(new Tuple<int, int>(pShapeSquareIndex.Item1, pShapeSquareIndex.Item2 + 1));
                }

            }
            return false;
        }

        private bool IsRange(int index1, int index2)
        {
            if (index1 < 0 || index1 >= CrosswiseNodeCount)
            {
                return false;
            }

            if (index2 < 0 || index2 >= LengthwaysNodeCount)
            {
                return false;
            }

            ShapeSquare shapeSquare = PlotShapeSquare[index1, index2];

            if (shapeSquare is ShapeSquare_BlockingPoint)
            {
                return false;
            }

            string strTag = $"{index1},{index2}";
            if (dicCache.ContainsKey(strTag))
            {
                return false;
            }
            else
            {
                dicCache.Add(strTag, true);
            }

            return true;
        }

        private void btnAStar_Click(object sender, RoutedEventArgs e)
        {
            AStarAlgorithm pAStarAlgorithm = new AStarAlgorithm(PlotShapeSquare, CrosswiseNodeCount, LengthwaysNodeCount);
            pAStarAlgorithm.AlgorithmRun(pStartShapeSquare, pEndShapeSquare);
        }
    }
}
