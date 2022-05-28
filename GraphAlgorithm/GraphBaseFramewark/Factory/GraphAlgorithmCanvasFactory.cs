using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GraphBaseFramewark
{
    public static class GraphAlgorithmCanvasFactory
    {
        private static List<IGraphAlgorithmCanvas> listGraphAlgorithmCanvas = null;
        public static List<IGraphAlgorithmCanvas> ListGraphAlgorithmCanvas
        {
            get 
            {
                if (listGraphAlgorithmCanvas == null)
                {
                    listGraphAlgorithmCanvas = GetAllGraphCanvas();
                }
                return listGraphAlgorithmCanvas;
            }
        }

        private static string GraphCanvasFolderPath = System.IO.Path.Combine(Application.StartupPath, "GraphAlgorithmPlugin");
        private static List<IGraphAlgorithmCanvas> GetAllGraphCanvas()
        {
            List<string> ListPlugInFilePath = System.IO.Directory.GetFiles(GraphCanvasFolderPath, "*.dll").ToList();
            List<IGraphAlgorithmCanvas> list = new List<IGraphAlgorithmCanvas>();
            foreach (string strFilePath in ListPlugInFilePath)
            {
                List<IGraphAlgorithmCanvas> listTemp = GeGraphAlgorithmCanvasByDllPath(strFilePath);
                list.AddRange(listTemp);
            }
            return list;
        }

        private static readonly string IGraphAlgorithmCanvasInterfaceName = "IGraphAlgorithmCanvas";
        private static List<IGraphAlgorithmCanvas> GeGraphAlgorithmCanvasByDllPath(string strDllPath)
        {
            List<IGraphAlgorithmCanvas> listGraphAlgorithmCanvas = new List<IGraphAlgorithmCanvas>();
            Assembly myAssembly = Assembly.LoadFrom(strDllPath);

            //获取类型
            Type[] typeArr = myAssembly.GetTypes();
            //针对每个类型获取详细信息
            foreach (Type type in typeArr)
            {
                Type temp = type.GetInterface(IGraphAlgorithmCanvasInterfaceName);
                if (temp == null)
                {
                    continue;
                }
                try
                {
                    object obj = Activator.CreateInstance(type, true);
                    if (obj is IGraphAlgorithmCanvas)
                    {
                        listGraphAlgorithmCanvas.Add(obj as IGraphAlgorithmCanvas);
                    }
                }
                catch
                { }
            }
            return listGraphAlgorithmCanvas;
        }
    }
}
