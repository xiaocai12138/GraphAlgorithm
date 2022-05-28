using GraphBaseFramewark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphTSPAlgorithm
{
    public delegate void DrawGeneticAlgorithmResultDelegate(List<ShapeCircle> listBestShapeCircle);

    public class GeneticAlgorithm
    {
        /// <summary>
        /// 种群规模  
        /// </summary>
        int Scale;

        /// <summary>
        /// 城市数量，染色体长度  
        /// </summary>
        int CityNum;
        /// <summary>
        /// 运行代数  
        /// </summary>
        int MAX_GEN;
        /// <summary>
        /// 距离矩阵
        /// </summary>
        double[,] distance;
        /// <summary>
        /// 最佳出现代数  
        /// </summary>
        int bestT;
        /// <summary>
        /// 最佳长度
        /// </summary>
        double bestLength;
        /// <summary>
        /// 最佳路径 
        /// </summary>
        public List<ShapeCircle> bestTour;
        /// <summary>
        /// 初始种群，父代种群，行数表示种群规模，一行代表一个个体，即染色体，列表示染色体基因片段  
        /// </summary>
        List<List<ShapeCircle>> oldPopulation;
        /// <summary>
        /// 新的种群，子代种群
        /// </summary>
        List<List<ShapeCircle>> newPopulation;
        /// <summary>
        /// 种群适应度，表示种群中各个个体的适应度  
        /// </summary>
        double[] fitness;
        /// <summary>
        /// 种群中各个个体的累计概率
        /// </summary>
        double[] Pi;
        /// <summary>
        /// 交叉概率  
        /// </summary>
        double Pc;
        /// <summary>
        /// 变异概率  
        /// </summary>
        double Pm;
        /// <summary>
        /// 当前代数  
        /// </summary>
        int t;

        Random random;

        DrawGeneticAlgorithmResultDelegate CurrentDrawGeneticAlgorithmResultDelegate;


        public GeneticAlgorithm(int scale, int cityNum, int maxGen, double pc, double pm, DrawGeneticAlgorithmResultDelegate drawGeneticAlgorithmResult)
        {
            CurrentDrawGeneticAlgorithmResultDelegate = drawGeneticAlgorithmResult;
            Scale = scale;
            CityNum = cityNum;
            MAX_GEN = maxGen;
            Pc = pc;
            Pm = pm;
        }


        private List<ShapeCircle> ListShapeCircle = null;
        /// <summary>
        /// 初始化GA算法类 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Init(List<ShapeCircle> listShapeCircles)
        {
            ListShapeCircle = listShapeCircles;
            // 读取数据  
            distance = new double[CityNum, CityNum];
            int i;

            // 计算距离矩阵  
            // ，针对具体问题，距离计算方法也不一样，此处用的是att48作为案例，它有48个城市，距离计算方法为伪欧氏距离，最优值为10628  
            for (i = 0; i < CityNum - 1; i++)
            {
                distance[i, i] = 0; // 对角线为0  
                for (int j = i + 1; j < CityNum; j++)
                {
                    double rij = Math
                            .Sqrt(((ListShapeCircle[i].CenterX - ListShapeCircle[j].CenterX) * (ListShapeCircle[i].CenterX - ListShapeCircle[j].CenterX) +
                                   (ListShapeCircle[i].CenterY - ListShapeCircle[j].CenterY) * (ListShapeCircle[i].CenterY - ListShapeCircle[j].CenterY)));
                    // 四舍五入，取整  
                    int tij = (int)Math.Round(rij);
                    if (tij < rij)
                    {
                        distance[i, j] = tij + 1;
                        distance[j, i] = distance[i, j];
                    }
                    else
                    {
                        distance[i, j] = tij;
                        distance[j, i] = distance[i, j];
                    }
                }
            }
            distance[CityNum - 1, CityNum - 1] = 0;

            bestLength = 99999;
            bestTour = new List<ShapeCircle>(); ;
            bestT = 0;
            t = 0;

            newPopulation = new List<List<ShapeCircle>>();
            oldPopulation = new List<List<ShapeCircle>>();
            fitness = new double[Scale];
            Pi = new double[Scale];

            //通过时间种子
            random = new Random(unchecked((int)DateTime.Now.Ticks));

            Console.Write("\t");
            for (int index = 0; index < CityNum; index++)
            {
                Console.Write($"测试{index}\t");
            }
            Console.WriteLine("\t");
            for (int RowIndex = 0; RowIndex < CityNum; RowIndex++)
            {
                Console.Write($"测试{RowIndex}\t");
                for (int ColumnIndex = 0; ColumnIndex < CityNum; ColumnIndex++)
                {
                    Console.Write($"{distance[RowIndex, ColumnIndex]}\t");
                }
                Console.WriteLine("\t");
            }
        }

        /// <summary>
        /// 初始化种群  
        /// </summary>
        void InitGroup()
        {
            int i, j, k;
            for (k = 0; k < Scale; k++)// 种群数  
            {
                List<ShapeCircle> listShapeCircleTemps = new List<ShapeCircle>();
                oldPopulation.Add(listShapeCircleTemps);
                listShapeCircleTemps.Add(ListShapeCircle[random.Next(0, 65535) % CityNum]);
                for (i = 1; i < CityNum;)// 染色体长度  
                {
                    ShapeCircle pShapeCircle = ListShapeCircle[random.Next(0, 65535) % CityNum];
                    if (!listShapeCircleTemps.Contains(pShapeCircle))
                    {
                        listShapeCircleTemps.Add(pShapeCircle);
                        i++;
                    }
                }
                newPopulation.Add(listShapeCircleTemps.ToList());
            }
        }

        public double Evaluate(List<ShapeCircle> listShapeCircles)
        {
            // 0123  
            double len = 0;
            // 染色体，起始城市,城市1,城市2...城市n  
            for (int i = 1; i < CityNum; i++)
            {
                len += distance[ListShapeCircle.IndexOf(listShapeCircles[i-1]), ListShapeCircle.IndexOf(listShapeCircles[i])];
            }
            // 城市n,起始城市  
            len += distance[ListShapeCircle.IndexOf(listShapeCircles[CityNum - 1]), ListShapeCircle.IndexOf(listShapeCircles[0])];
            return len;
        }

        /// <summary>
        /// 计算种群中各个个体的累积概率，前提是已经计算出各个个体的适应度fitness[max]，作为赌轮选择策略一部分，Pi[max]  
        /// </summary>
        void CountRate()
        {
            int k;
            double sumFitness = 0;// 适应度总和  

            double[] tempf = new double[Scale];

            for (k = 0; k < Scale; k++)
            {
                tempf[k] = 10.0 / fitness[k];
                sumFitness += tempf[k];
            }

            Pi[0] = (float)(tempf[0] / sumFitness);
            for (k = 1; k < Scale; k++)
            {
                Pi[k] = (float)(tempf[k] / sumFitness + Pi[k - 1]);
            }

            /* 
             * for(k=0;k<scale;k++) { System.out.println(fitness[k]+" "+Pi[k]); } 
             */
        }


        /// <summary>
        /// 挑选某代种群中适应度最高的个体，直接复制到子代中 
        /// 前提是已经计算出各个个体的适应度Fitness[max]
        /// </summary>
        public void SelectBestGh()
        {
            int k, i, maxid;
            double maxevaluation;

            maxid = 0;
            maxevaluation = fitness[0];
            for (k = 1; k < Scale; k++)
            {
                if (maxevaluation > fitness[k])
                {
                    maxevaluation = fitness[k];
                    maxid = k;
                }
            }

            if (bestLength > maxevaluation)
            {
                bestLength = maxevaluation;
                bestT = t;// 最好的染色体出现的代数;  
                if (oldPopulation != null&& oldPopulation.Count> maxid)
                {
                    bestTour = new List<ShapeCircle>();
                    bestTour.AddRange(oldPopulation[maxid]);
                    CurrentDrawGeneticAlgorithmResultDelegate.Invoke(bestTour);
                }
                Console.WriteLine($"当前为{bestT}代族群，最优路径长度为{bestLength},{string.Join("->", bestTour)}");

                //WriteEthnicGroups($"第{bestT}代族群");
            }

            // 复制染色体，k表示新染色体在种群中的位置，kk表示旧的染色体在种群中的位置  
            CopyGh(0, maxid);// 将当代种群中适应度最高的染色体k复制到新种群中，排在第一位0  
        }

         
        public void CopyGh(int k, int kk)
        {
            //newPopulation[k].Clear();
            //newPopulation[k].AddRange(oldPopulation[kk]);

            for (int i = 0; i < CityNum; i++)
            {
                newPopulation[k][i] = oldPopulation[kk][i];
            }
        }

        /// <summary>
        /// 赌轮选择策略挑选
        /// </summary>
        public void select()
        {
            int k, i, selectId;
            float ran1;
            // Random random = new Random(System.currentTimeMillis());  
            for (k = 1; k < Scale; k++)
            {
                ran1 = (float)(random.Next(0, 65535) % 1000 / 1000.0);
                // System.out.println("概率"+ran1);  
                // 产生方式  
                for (i = 0; i < Scale; i++)
                {
                    if (ran1 <= Pi[i])
                    {
                        break;
                    }
                }
                selectId = i;
                // System.out.println("选中" + selectId);  
                CopyGh(k, selectId);
            }
        }

        /// <summary>
        /// 进化函数，正常交叉变异
        /// </summary>
        public void Evolution()
        {
            int k;
            // 挑选某代种群中适应度最高的个体  
            SelectBestGh();

            // 赌轮选择策略挑选scale-1个下一代个体  
            select();

            // Random random = new Random(System.currentTimeMillis());  
            double r;

            // 交叉方法  
            for (k = 0; k < Scale; k = k + 2)
            {
                r = random.Next(0, 65535) % 1000 / 1000.0; ;// /产生概率  
                // System.out.println("交叉率..." + r);  
                if (r < Pc)
                {
                    // System.out.println(k + "与" + k + 1 + "进行交叉...");  
                    //OXCross(k, k + 1);// 进行交叉  
                    OXCross1(k, k + 1);
                }
                else
                {
                    r = random.Next(0, 65535) % 1000 / 1000.0;// /产生概率  
                    // System.out.println("变异率1..." + r);  
                    // 变异  
                    if (r < Pm)
                    {
                        // System.out.println(k + "变异...");  
                        OnCVariation(k);
                    }
                    r = random.Next(0, 65535) % 1000 / 1000.0;// /产生概率  
                    // System.out.println("变异率2..." + r);  
                    // 变异  
                    if (r < Pm)
                    {
                        // System.out.println(k + 1 + "变异...");  
                        OnCVariation(k + 1);
                    }
                }

            }
        }

        /// <summary>
        /// 进化函数，保留最好染色体不进行交叉变异  
        /// </summary>
        public void Evolution1()
        {
            int k;
            // 挑选某代种群中适应度最高的个体  
            SelectBestGh();

            // 赌轮选择策略挑选scale-1个下一代个体  
            select();

            // Random random = new Random(System.currentTimeMillis());  
            double r;

            for (k = 1; k + 1 < Scale / 2; k = k + 2)
            {
                r = random.Next(0, 65535) % 1000 / 1000.0;// /产生概率  
                if (r < Pc)
                {
                    OXCross1(k, k + 1);// 进行交叉  
                    //OXCross(k,k+1);//进行交叉  
                }
                else
                {
                    r = random.Next(0, 65535) % 1000 / 1000.0;// /产生概率  
                    // 变异  
                    if (r < Pm)
                    {
                        OnCVariation(k);
                    }
                    r = random.Next(0, 65535) % 1000 / 1000.0;// /产生概率  
                    // 变异  
                    if (r < Pm)
                    {
                        OnCVariation(k + 1);
                    }
                }
            }
            if (k == Scale / 2 - 1)// 剩最后一个染色体没有交叉L-1  
            {
                r = random.Next(0, 65535) % 1000 / 1000.0;// /产生概率  
                if (r < Pm)
                {
                    OnCVariation(k);
                }
            }

        }

        /// <summary>
        /// 交叉算子,相同染色体交叉产生不同子代染色体  
        /// </summary>
        /// <param name="k1"></param>
        /// <param name="k2"></param>
        public void OXCross1(int k1, int k2)
        {
            int i, j, k, flag;
            int ran1, ran2, temp;
            ShapeCircle[] Gh1 = new ShapeCircle[ListShapeCircle.Count];
            ShapeCircle[] Gh2 = new ShapeCircle[ListShapeCircle.Count];
            // Random random = new Random(System.currentTimeMillis());  

            ran1 = random.Next(0, 65535) % CityNum;
            ran2 = random.Next(0, 65535) % CityNum;
            while (ran1 == ran2)
            {
                ran2 = random.Next(0, 65535) % CityNum;
            }

            if (ran1 > ran2)// 确保ran1<ran2  
            {
                temp = ran1;
                ran1 = ran2;
                ran2 = temp;
            }

            List<ShapeCircle> listShapeCircle1 = newPopulation[k1];
            List<ShapeCircle> listShapeCircle2 = newPopulation[k2];
            // 将染色体1中的第三部分移到染色体2的首部  
            for (i = 0, j = ran2; j < CityNum; i++, j++)
            {
                Gh2[i] = listShapeCircle1[j];
            }

            flag = i;// 染色体2原基因开始位置  

            for (k = 0, j = flag; j < CityNum;)// 染色体长度  
            {
                Gh2[j] = listShapeCircle2[k++];
                for (i = 0; i < flag; i++)
                {
                    if (Gh2[i] == Gh2[j])
                    {
                        break;
                    }
                }
                if (i == flag)
                {
                    j++;
                }
            }

            flag = ran1;
            for (k = 0, j = 0; k < CityNum;)// 染色体长度  
            {
                Gh1[j] = listShapeCircle1[k++];
                for (i = 0; i < flag; i++)
                {
                    if (listShapeCircle2[i] == Gh1[j])
                    {
                        break;
                    }
                }
                if (i == flag)
                {
                    j++;
                }
            }

            flag = CityNum - ran1;

            for (i = 0, j = flag; j < CityNum; j++, i++)
            {
                Gh1[j] = listShapeCircle2[i];
            }

            for (i = 0; i < CityNum; i++)
            {
                listShapeCircle1[i] = Gh1[i];// 交叉完毕放回种群  
                listShapeCircle2[i] = Gh2[i];// 交叉完毕放回种群  
            }
        }

        /// <summary>
        /// 多次对换变异算子  
        /// </summary>
        /// <param name="k"></param>
        public void OnCVariation(int k)
        {
            int ran1, ran2;
            ShapeCircle temp;
            int count;// 对换次数  

            // Random random = new Random(System.currentTimeMillis());  
            count = random.Next(0, 65535) % CityNum;

            List<ShapeCircle> listShapeCircle = newPopulation[k];
            for (int i = 0; i < count; i++)
            {

                ran1 = random.Next(0, 65535) % CityNum;
                ran2 = random.Next(0, 65535) % CityNum;
                while (ran1 == ran2)
                {
                    ran2 = random.Next(0, 65535) % CityNum;
                }
                temp = listShapeCircle[ran1];
                listShapeCircle[ran1] = listShapeCircle[ ran2];
                listShapeCircle[ran2] = temp;
            }

            /* 
             * for(i=0;i<L;i++) { printf("%d ",newGroup[k][i]); } printf("\n"); 
             */
        }

        public void Solve()
        {
            int i;
            int k;

            // 初始化种群  
            InitGroup();
            // 计算初始化种群适应度，Fitness[max]  
            for (k = 0; k < Scale; k++)
            {
                fitness[k] = Evaluate(ListShapeCircle);
            }
            // 计算初始化种群中各个个体的累积概率，Pi[max]  
            CountRate();

            WriteEthnicGroups("初始族群");

            for (t = 0; t < MAX_GEN; t++)
            {
                Evolution1();
                // 将新种群newGroup复制到旧种群oldGroup中，准备下一代进化  
                for (k = 0; k < Scale; k++)
                {
                    oldPopulation[k] = newPopulation[k];
                }
                // 计算种群适应度  
                for (k = 0; k < Scale; k++)
                {
                    List<ShapeCircle> tempGA = oldPopulation[k];
                    fitness[k] = Evaluate(tempGA);
                }
                // 计算种群中各个个体的累积概率  
                CountRate();
            }

        }

        private void WriteEthnicGroups(string strTitle)
        {
            Console.WriteLine($"*****************************************{strTitle}****************************************");
            for (int k = 0; k < Scale; k++)
            {
                Console.WriteLine($"第{k + 1}个体：{string.Join("->", oldPopulation[k])}");
            }
            Console.WriteLine($"*****************************************{strTitle}****************************************");
        }
    }
}
