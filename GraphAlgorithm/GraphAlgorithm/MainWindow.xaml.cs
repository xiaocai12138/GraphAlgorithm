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

namespace GraphAlgorithm
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitLoadAllGraphAlgorithm();
        }

        private void InitLoadAllGraphAlgorithm()
        {
            cmbAlgorithmType.ItemsSource=GraphAlgorithmCanvasFactory.ListGraphAlgorithmCanvas.Select(a=>a.GraphAlgorithmName).ToList();
            cmbAlgorithmType.SelectedIndex = 0;
        }

        private void cmbAlgorithmType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string strTypeName = cmbAlgorithmType.SelectedItem.ToString();

            IGraphAlgorithmCanvas graphAlgorithmCanvas = GraphAlgorithmCanvasFactory.ListGraphAlgorithmCanvas.Find(a=>a.GraphAlgorithmName==strTypeName);

            if (graphAlgorithmCanvas is UserControl)
            {
                gdGraphAlgorithm.Children.Clear();
                UserControl userControl = graphAlgorithmCanvas as UserControl;
                gdGraphAlgorithm.Children.Add(userControl);
            }
        }
    }
}
