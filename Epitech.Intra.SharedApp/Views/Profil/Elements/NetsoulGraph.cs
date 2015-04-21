using Xamarin.Forms;
using Epitech.Intra.API.Data;
using OxyPlot.Xamarin.Forms;
using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;
using OxyPlot.Axes;

namespace Epitech.Intra.SharedApp
{
	public class NetsoulGraph : StackLayout
	{
		static PlotModel model;
		PlotView plotview;

		public NetsoulGraph (User user, PlotView plotv)
		{
			if (!user.Close && user.Netsoul != null) {
				plotview = plotv;
				Padding = new Thickness (5, 10, 5, 10);
				Orientation = StackOrientation.Horizontal;
				VerticalOptions = LayoutOptions.Start;
				HorizontalOptions = LayoutOptions.FillAndExpand;
				Spacing = 0;
				HeightRequest = 150;

				plotview = new PlotView ();
				plotview.BackgroundColor = Color.Transparent;
				plotview.Model = LineSeriesWithLabels (user.Netsoul.GetRange ((int)(0.75 * user.Netsoul.Count), user.Netsoul.Count - (int)(0.75 * user.Netsoul.Count)));
				plotview.VerticalOptions = LayoutOptions.Fill;
				plotview.HorizontalOptions = LayoutOptions.Fill;
				plotview.HeightRequest = 80;
				plotview.WidthRequest = App.ScreenWidth - 10;
				Children.Add (plotview);
			}
		}

		static public PlotModel LineSeriesWithLabels (List<API.Data.UserJsonTypes.NetsoulRawData> netsoul)
		{
			model = new PlotModel ();
			model.Background = OxyColors.Transparent;
			model.LegendBorderThickness = 0;
			model.PlotAreaBorderThickness = new OxyThickness (0, 0, 0, 0);
			model.Padding = new OxyThickness (0, 0, 0, 0);
			model.PlotAreaBorderColor = OxyColors.Transparent;
			model.Series.Clear ();
			if (model.PlotView != null)
				model.Axes.Clear ();

			CategoryAxis a3 = new CategoryAxis {
				IsAxisVisible = false,
				Position = AxisPosition.Top,
				AbsoluteMinimum = 0,
				AbsoluteMaximum = netsoul.Count - 0.5
			};
			a3.Zoom (netsoul.Count - 8, netsoul.Count - 0.5);

			LinearAxis a1 = new LinearAxis {
				IsAxisVisible = true,
				Position = AxisPosition.Left,
				AbsoluteMinimum = 0,
				AbsoluteMaximum = 24
			};
			a1.Zoom (0, 24);

			model.Axes.Add (a3);
			model.Axes.Add (a1);

			model.Series.Add (GetActiveScoolLog (netsoul));
			model.Series.Add (GetIdleScoolLog (netsoul));
			model.Series.Add (GetActiveOutLog (netsoul));
			model.Series.Add (GetIdleOutLog (netsoul));
			model.Series.Add (GetAverageLog (netsoul));

			return model;
		}

		static private ColumnSeries GetActiveScoolLog (IList<Epitech.Intra.API.Data.UserJsonTypes.NetsoulRawData> netsoul)
		{
			var s2 = new ColumnSeries {
				LabelFormatString = "",
				IsStacked = true,
				LabelMargin = 5,
				TextColor = OxyColors.White,
				FillColor = IntraColor.GraphColor.ActiveIn,
				StrokeColor = OxyColors.Transparent
			};
			int b = 1;
			for (int i = 0; i < netsoul.Count; i++, b++) {
				s2.Items.Add (new ColumnItem { CategoryIndex = b - 1, Value = netsoul [i].TimeActiveScool / 3600 });
			}
			return s2;
		}

		static private ColumnSeries GetIdleScoolLog (IList<Epitech.Intra.API.Data.UserJsonTypes.NetsoulRawData> netsoul)
		{
			var s2 = new ColumnSeries {
				LabelFormatString = "",
				LabelMargin = 5,
				IsStacked = true,
				TextColor = OxyColors.White,
				FillColor = IntraColor.GraphColor.IdleIn,
				StrokeColor = OxyColors.Transparent
			};
			int b = 1;
			for (int i = 0; i < netsoul.Count; i++, b++) {
				s2.Items.Add (new ColumnItem { CategoryIndex = b - 1, Value = netsoul [i].TimeIldleScool / 3600 });
			}
			return s2;
		}

		static private ColumnSeries GetActiveOutLog (IList<Epitech.Intra.API.Data.UserJsonTypes.NetsoulRawData> netsoul)
		{
			var s2 = new ColumnSeries {
				LabelFormatString = "",
				IsStacked = true,
				LabelMargin = 5,
				TextColor = OxyColors.White,
				FillColor = IntraColor.GraphColor.ActiveOut,
				StrokeColor = OxyColors.Transparent
			};
			int b = 1;
			for (int i = 0; i < netsoul.Count; i++, b++) {
				s2.Items.Add (new ColumnItem { CategoryIndex = b - 1, Value = netsoul [i].TimeActiveOut / 3600 });
			}
			return s2;
		}

		static private ColumnSeries GetIdleOutLog (IList<Epitech.Intra.API.Data.UserJsonTypes.NetsoulRawData> netsoul)
		{
			var s2 = new ColumnSeries {
				LabelFormatString = "",
				IsStacked = true,
				TextColor = OxyColors.White,
				FillColor = IntraColor.GraphColor.IdleOut,
				StrokeColor = OxyColors.Transparent
			};
			int b = 1;
			for (int i = 0; i < netsoul.Count; i++, b++) {
				s2.Items.Add (new ColumnItem { CategoryIndex = b - 1, Value = netsoul [i].TimeIldleOut / 3600 });
			}
			return s2;
		}

		static private LineSeries GetAverageLog (IList<Epitech.Intra.API.Data.UserJsonTypes.NetsoulRawData> netsoul)
		{
			var s2 = new LineSeries {
				LabelFormatString = "",
				LabelMargin = 5,
				TextColor = OxyColors.White,
				Color = IntraColor.GraphColor.Average,
				MarkerSize = 0,
				Smooth = true,
				MarkerStrokeThickness = 1.5,
			};
			int b = 0;
			for (int i = 0; i < netsoul.Count; i++, b++) {
				s2.Points.Add (new DataPoint ((double)b, netsoul [i].Average / 3600));
			}
			return s2;
		}
	}
}


