using System;

using Xamarin.Forms;
using OxyPlot;

namespace Epitech.Intra.SharedApp
{
	public abstract class IntraColor
	{
		public static Color DarkGray = Color.FromHex ("2A2A2A");
		public static Color LightGray = Color.FromHex ("616063");

		public static Color DarkBlue = Color.FromHex ("004A89");
		public static Color LightBlue = Color.FromHex ("64AFD7");

		public abstract class GraphColor
		{
			public static OxyColor ActiveOut = OxyColor.FromRgb (byte.Parse ("255"), byte.Parse ("128"), byte.Parse ("128"));
			public static OxyColor IdleOut = OxyColor.FromRgb (byte.Parse ("255"), byte.Parse ("220"), byte.Parse ("220"));

			public static OxyColor ActiveIn = OxyColor.FromRgb (byte.Parse ("8"), byte.Parse ("255"), byte.Parse ("142"));
			public static OxyColor IdleIn = OxyColor.FromRgb (byte.Parse ("143"), byte.Parse ("255"), byte.Parse ("142"));

			public static OxyColor Average = OxyColor.FromRgb (byte.Parse ("255"), byte.Parse ("116"), byte.Parse ("56"));

			public static OxyColor GPA3 = OxyColor.FromArgb (byte.Parse ("255"), byte.Parse ("0"), byte.Parse ("74"), byte.Parse ("137"));
			public static OxyColor GPA2 = OxyColor.FromArgb (byte.Parse ("150"), byte.Parse ("0"), byte.Parse ("74"), byte.Parse ("137"));
			public static OxyColor GPA1 = OxyColor.FromArgb (byte.Parse ("90"), byte.Parse ("0"), byte.Parse ("74"), byte.Parse ("137"));
			public static OxyColor GPA0 = OxyColor.FromArgb (byte.Parse ("40"), byte.Parse ("0"), byte.Parse ("74"), byte.Parse ("137"));
		}
	}
}

