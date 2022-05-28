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

namespace GraphNQueensAlgorithm
{
    /// <summary>
    /// GraphNQueensCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class GraphNQueensCanvas : UserControl, IGraphAlgorithmCanvas
    {
        public GraphNQueensCanvas()
        {
            InitializeComponent();
        }

        public string GraphAlgorithmName
        {
            get
            {
                return "回溯算法-N皇后问题";
            }
        }

        List<ShapeSquare> listShapeSquare = new List<ShapeSquare>();

        ShapeSquare[,] PlotShapeSquare = null;
        int QueensNodeCount = 0;

        private void btnCreateRelNode_Click(object sender, RoutedEventArgs e)
        {
            listShapeSquare = new List<ShapeSquare>();

            QueensNodeCount = Convert.ToInt32(tbQueensNodeCount.Text);

            PlotShapeSquare = new ShapeSquare[QueensNodeCount, QueensNodeCount];

            int startX = 50;
            int startY = 50;


            for (int i = 0; i < QueensNodeCount; i++)
            {
                startX = 50;
                for (int j = 0; j < QueensNodeCount; j++)
                {
                    startX = startX + 40;
                    ShapeSquare pShapeSquare  = new ShapeSquare
                    {
                        DisplayName = "",
                        Width = 40,
                        Height = 40,
                        StartX = startX,
                        StartY = startY
                    };
                    pShapeSquare.Fill = Brushes.BurlyWood;
                    PlotShapeSquare[i, j] = pShapeSquare;

                    pShapeSquare.Tag = new Tuple<int, int>(i, j);
                    listShapeSquare.Add(pShapeSquare);
                }
                startY = startY + 40;
            }
            ucGraphCanvas.Width = 2000;
            ucGraphCanvas.Height = 2000;
            ucGraphCanvas.InitGraphShapeSquare(listShapeSquare);
        }

        public override string ToString()
        {
            return this.GraphAlgorithmName;
        }

        List<ShapeSquare> StackShapeSquare = new List<ShapeSquare>();
        int ResultCount = 0;
        /// <summary>
        /// 按钮回溯解法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReCallBack_Click(object sender, RoutedEventArgs e)
        {
            ResultCount = 0;
            for (int i = 0; i < QueensNodeCount; i++)
            {
                ShapeSquare pFirstShapeSquare = PlotShapeSquare[0, i];
                StackShapeSquare.Clear();

                PushShapeSquare(pFirstShapeSquare);

                for (int j = 1; j < QueensNodeCount; j++)
                {
                    ReCallBackInfo(j);
                }
                PopShapeSquare();
            }
            Console.WriteLine($"一共：{ResultCount}解法");
        }

        /// <summary>
        /// 回溯解法
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private bool ReCallBackInfo(int rowIndex)
        {
            if (StackShapeSquare.Count == QueensNodeCount)
            {
                ResultCount++;
                Console.WriteLine(string.Format("第{0}种解法：{1}", ResultCount,string.Join(" ", StackShapeSquare.Select(a=>$"[{a.Tag}]"))));
                return false;
            }
            if (rowIndex >= QueensNodeCount)
            {
                return false;
            }
            else
            {
                for (int columnIndex = 0; columnIndex < QueensNodeCount; columnIndex++)
                {
                    ShapeSquare pShapeSquare = GetShapeSquare(rowIndex, columnIndex);
                    pShapeSquare.Fill = Brushes.Aqua;
                    if (CheckClash(pShapeSquare))
                    {
                        pShapeSquare.Fill = Brushes.Transparent;
                        this.DoEvents();
                    }
                    else
                    {
                        PushShapeSquare(pShapeSquare);
                        if (ReCallBackInfo(rowIndex + 1))
                        {
                            return true;
                        }
                        else
                        {
                            PopShapeSquare();
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 检查皇后是否冲突
        /// </summary>
        /// <param name="pShapeSquare"></param>
        /// <returns></returns>
        private bool CheckClash(ShapeSquare pShapeSquare)
        {
            Tuple<int, int> pShapeSquareTag = pShapeSquare.Tag as Tuple<int, int>;
            foreach (var item in StackShapeSquare)
            {
                Tuple<int, int>  itemTag=item.Tag as Tuple<int, int>;
                if (itemTag.Item2 == pShapeSquareTag.Item2)
                {
                    return true;
                }

                if ((itemTag.Item1 - pShapeSquareTag.Item1) == (itemTag.Item2 - pShapeSquareTag.Item2))
                {
                    return true;
                }


                if ((itemTag.Item1 - pShapeSquareTag.Item1) == -(itemTag.Item2 - pShapeSquareTag.Item2))
                {
                    return true;
                }
            }
            return false;
        }

        private ShapeSquare GetShapeSquare(int rowIndex, int columnIndex)
        {
            return listShapeSquare[rowIndex * QueensNodeCount + columnIndex];
        }

        private void PopShapeSquare()
        {
            ShapeSquare pShapeSquare= StackShapeSquare[StackShapeSquare.Count - 1];
            StackShapeSquare.RemoveAt(StackShapeSquare.Count - 1);
            pShapeSquare.Fill = Brushes.Transparent;
            this.DoEvents();
        }

        private void PushShapeSquare(ShapeSquare pShapeSquare)
        {
            pShapeSquare.Fill = Brushes.BurlyWood;
            StackShapeSquare.Add(pShapeSquare);
            this.DoEvents();
        }
    }
}