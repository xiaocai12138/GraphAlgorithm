using GraphBaseFramewark;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphTSPAlgorithm
{
    public class GraphTSPAlgorithm
	{
		double CANVAS_WIDTH = 200;
		double CANVAS_HEIGHT = 200;
		private List<ShapeCircle> mNodeList;
		private List<ShapeRelationshipLine> mEdgeList;

		public GraphTSPAlgorithm(List<ShapeCircle> nodeList, List<ShapeRelationshipLine> edgeList, double canvas_width, double canvas_height)
		{
			this.mNodeList = nodeList;
			this.mEdgeList = edgeList;
			this.CANVAS_WIDTH = canvas_width;
			this.CANVAS_HEIGHT = canvas_height;
		}

		internal void Collide()
        {
            throw new NotImplementedException();
        }
    }
}
