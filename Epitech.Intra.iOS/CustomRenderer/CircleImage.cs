﻿using Xamarin.Forms;

using XLabs.Forms.Controls;
using Epitech.Intra.SharedApp;

[assembly: ExportRenderer (typeof(CircleImage), typeof(CircleImageRenderer))]
namespace XLabs.Forms.Controls
{
	using System;
	using System.ComponentModel;
	using CoreGraphics;

	using UIKit;

	using Xamarin.Forms;
	using Xamarin.Forms.Platform.iOS;

	/// <summary>
	/// Class CircleImageRenderer.
	/// </summary>
	public class CircleImageRenderer : ImageRenderer
	{
		/// <summary>
		/// Called when [element changed].
		/// </summary>
		/// <param name="e">The e.</param>
		protected override void OnElementChanged (ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged (e);

			if (Control == null || e.OldElement != null || Element == null || Element.Aspect != Aspect.Fill)
				return;

			var min = Math.Min (Element.Width, Element.Height);
			Control.Layer.CornerRadius = (float)(min / 2.0);
			Control.Layer.MasksToBounds = false;
			Control.ClipsToBounds = true;
		}

		/// <summary>
		/// Handles the <see cref="E:ElementPropertyChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			if (Control == null)
				return;

			if (Element.Aspect == Aspect.Fill) {
				if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
				    e.PropertyName == VisualElement.WidthProperty.PropertyName) {
					DrawFill ();               
				}
			} else {
				if (e.PropertyName == Image.IsLoadingProperty.PropertyName
				    && !Element.IsLoading && Control.Image != null) {
					DrawOther ();
				}
			}
		}

		/// <summary>
		/// Draws the other.
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		void DrawOther ()
		{
			int height;
			int width;

			switch (Element.Aspect) {
			case Aspect.AspectFill:
				height = (int)Control.Image.Size.Height;
				width = (int)Control.Image.Size.Width;
				height = MakeSquare (height, ref width);
				break;
			case Aspect.AspectFit:
				height = (int)Control.Image.Size.Height;
				width = (int)Control.Image.Size.Width;
				height = MakeSquare (height, ref width);
				break;
			default:
				throw new NotImplementedException ();
			}

			UIImage image = Control.Image;
			var clipRect = new CGRect (0, 0, width, height);
			var scaled = image.Scale (new CGSize (width, height));
			UIGraphics.BeginImageContextWithOptions (new CGSize (width, height), false, 0f);
			UIBezierPath.FromRoundedRect (clipRect, Math.Max (width, height) / 2).AddClip ();

			scaled.Draw (new CGRect (0, 0, scaled.Size.Width, scaled.Size.Height));
			UIImage final = UIGraphics.GetImageFromCurrentImageContext ();
			UIGraphics.EndImageContext ();
			Control.Image = final;
		}

		/// <summary>
		/// Draws the fill.
		/// </summary>
		void DrawFill ()
		{
			double min = Math.Min (Element.Width, Element.Height);
			Control.Layer.CornerRadius = (float)(min / 2.0);
			Control.Layer.BorderWidth = 2;
			Control.Layer.BorderColor = IntraColor.LightBlue.ToCGColor ();
			Control.Layer.MasksToBounds = false;
			Control.ClipsToBounds = true;
		}

		/// <summary>
		/// Makes the square.
		/// </summary>
		/// <param name="height">The height.</param>
		/// <param name="width">The width.</param>
		/// <returns>System.Int32.</returns>
		static int MakeSquare (int height, ref int width)
		{
			if (height < width) {
				width = height;
			} else {
				height = width;
			}
			return height;
		}
	}
}