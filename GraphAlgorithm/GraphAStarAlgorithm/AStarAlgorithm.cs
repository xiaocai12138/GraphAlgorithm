using GraphBaseFramewark;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GraphAStarAlgorithm
{
    /// <summary>
    /// A星寻路算法
    /// </summary>
    class AStarAlgorithm
    {
        ShapeSquare[,] PlotShapeSquare = null;
        int CrosswiseNodeCount = 0;
        int LengthwaysNodeCount = 0;
        public AStarAlgorithm(ShapeSquare[,] plotShapeSquare, int crosswiseNodeCount,int lengthwaysNodeCount)
        {
            PlotShapeSquare = plotShapeSquare;
            CrosswiseNodeCount = crosswiseNodeCount;
            LengthwaysNodeCount = lengthwaysNodeCount;
        }

        /// <summary>
        /// 关闭列表
        /// </summary>
        Dictionary<ShapeSquare, ShapeSquare> dicClose = new Dictionary<ShapeSquare, ShapeSquare>();

        /// <summary>
        /// 算法运行
        /// </summary>
        /// <param name="pStartShapeSquare"></param>
        /// <param name="pEndShapeSquare"></param>
        public void AlgorithmRun(ShapeSquare pStartShapeSquare, ShapeSquare pEndShapeSquare)
        {
            Tuple<int, int> pStartIndex = pStartShapeSquare.Tag as Tuple<int, int>;
            dicClose.Add(pStartShapeSquare, null);

            FindWayInfo(pStartIndex, pStartShapeSquare, pEndShapeSquare);

            ShapeSquare pWayShapeSquare = pEndShapeSquare;
            while (pWayShapeSquare!= pStartShapeSquare)
            {
                pWayShapeSquare.Fill = Brushes.YellowGreen;
                Thread.Sleep(10);
                System.Windows.Forms.Application.DoEvents();

                pWayShapeSquare = dicClose[pWayShapeSquare];
            }

        }

        private bool FindWayInfo(Tuple<int, int> pStartIndex, ShapeSquare pStartShapeSquare, ShapeSquare pEndShapeSquare)
        {
            ConcurrentDictionary<ShapeSquare, double> dicOpen = new ConcurrentDictionary<ShapeSquare, double>();

            IsRange(pStartIndex.Item1-1, pStartIndex.Item2-1,1.4, pStartShapeSquare, dicOpen);
            IsRange(pStartIndex.Item1-1, pStartIndex.Item2,1, pStartShapeSquare, dicOpen);
            IsRange(pStartIndex.Item1-1, pStartIndex.Item2+1,1.4, pStartShapeSquare, dicOpen);
            IsRange(pStartIndex.Item1, pStartIndex.Item2+1,1, pStartShapeSquare, dicOpen);
            IsRange(pStartIndex.Item1+1, pStartIndex.Item2+1,1.4, pStartShapeSquare, dicOpen);
            IsRange(pStartIndex.Item1+1, pStartIndex.Item2,1, pStartShapeSquare, dicOpen);
            IsRange(pStartIndex.Item1+1, pStartIndex.Item2-1,1.4, pStartShapeSquare, dicOpen);
            IsRange(pStartIndex.Item1, pStartIndex.Item2-1,1, pStartShapeSquare, dicOpen);

            Tuple<int, int> pEndIndex = pEndShapeSquare.Tag as Tuple<int, int>;
            foreach (var item in dicOpen.Keys)
            {
                Tuple<int, int>  pItemIndex = item.Tag as Tuple<int, int>;
                dicOpen[item] += Math.Abs((pEndIndex.Item1 - pItemIndex.Item1)) + Math.Abs((pEndIndex.Item2 - pItemIndex.Item2));
                if (item == pEndShapeSquare)
                {
                    return true;
                }
            }

            List<KeyValuePair<ShapeSquare, double>> listSortOpen =dicOpen.OrderBy(a => a.Value).ToList();

            foreach (var item in listSortOpen)
            {
                Tuple<int, int> pIndex = item.Key.Tag as Tuple<int, int>;
                if (FindWayInfo(pIndex, item.Key, pEndShapeSquare))
                {
                    return true;
                }
            }
            return false;

        }
        private bool IsRange(int index1, int index2,double dStartLength,ShapeSquare pStartShapeSquare, ConcurrentDictionary<ShapeSquare,double> dicOpen)
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

            if (dicClose.ContainsKey(shapeSquare))
            {
                return false;
            }
            else
            {
                dicClose.Add(shapeSquare, pStartShapeSquare);
                dicOpen.TryAdd(shapeSquare, dStartLength);

                shapeSquare.Fill = Brushes.BurlyWood;
                Thread.Sleep(10);
                System.Windows.Forms.Application.DoEvents();
            }

            return true;
        }
    }
}
