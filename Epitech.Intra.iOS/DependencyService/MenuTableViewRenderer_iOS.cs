using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly:ExportRenderer(typeof(Epitech.Intra.iOS.Views.MenuTableView), typeof(Epitech.Intra.iOS.iOS.MenuTableViewRenderer))]
namespace Epitech.Intra.iOS.iOS
{
	public class MenuTableViewRenderer : TableViewRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<TableView> e)
		{
			base.OnElementChanged (e);

			if (Control == null)
				return;

			var tableView = Control as UITableView;
			tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
		}
	}
}