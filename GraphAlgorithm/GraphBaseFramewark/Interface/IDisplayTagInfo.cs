using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphBaseFramewark
{
	public interface IDisplayTagInfo
	{
		double CenterX
		{
			get;
			set;
		}

		double CenterY
		{
			get;
			set;
		}

		double Rect_Width
		{
			get;
			set;
		}

		double Rect_Height
		{
			get;
			set;
		}

		string DisplayName
		{
			get;
			set;
		}

		void SetColor();
	}
}
