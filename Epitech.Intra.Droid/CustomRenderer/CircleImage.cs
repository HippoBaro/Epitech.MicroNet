using Xamarin.Forms;

using XLabs.Forms.Controls;
using Epitech.Intra.SharedApp;

[assembly: ExportRenderer (typeof(CircleImage), typeof(CircleImageRenderer))]
namespace XLabs.Forms.Controls
{
	using System;
	using System.ComponentModel;

	using Android.Graphics;
	using Android.Views;

	using Xamarin.Forms;
	using Xamarin.Forms.Platform.Android;

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

			if (e.OldElement == null) {

				if ((int)Android.OS.Build.VERSION.SdkInt < 18)
					SetLayerType (LayerType.Software, null);
			}
		}

		/// <summary>
		/// Handles the <see cref="E:ElementPropertyChanged" /> event.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			if (e.PropertyName == Image.IsLoadingProperty.PropertyName && !this.Element.IsLoading
			    && this.Control.Drawable != null) {
				//Should only be true right after an image is loaded
				if (this.Element.Aspect != Aspect.AspectFit) {
					using (var sourceBitmap = Bitmap.CreateBitmap (this.Control.Drawable.IntrinsicWidth, this.Control.Drawable.IntrinsicHeight, Bitmap.Config.Argb8888)) {
						Canvas canvas = new Canvas (sourceBitmap);
						this.Control.Drawable.SetBounds (0, 0, canvas.Width, canvas.Height);
						this.Control.Drawable.Draw (canvas);
						this.ReshapeImage (sourceBitmap);
					}

				}
			}
		}

		/// <summary>
		/// Draw one child of this View Group.
		/// </summary>
		/// <param name="canvas">The canvas on which to draw the child</param>
		/// <param name="child">Who to draw</param>
		/// <param name="drawingTime">The time at which draw is occurring</param>
		/// <returns>To be added.</returns>
		/// <since version="Added in API level 1" />
		/// <remarks><para tool="javadoc-to-mdoc">Draw one child of this View Group. This method is responsible for getting
		/// the canvas in the right state. This includes clipping, translating so
		/// that the child's scrolled origin is at 0, 0, and applying any animation
		/// transformations.</para>
		/// <para tool="javadoc-to-mdoc">
		///   <format type="text/html">
		///     <a href="http://developer.android.com/reference/android/view/ViewGroup.html#drawChild(android.graphics.Canvas, android.view.View, long)" target="_blank">[Android Documentation]</a>
		///   </format>
		/// </para></remarks>
		protected override bool DrawChild (Canvas canvas, global::Android.Views.View child, long drawingTime)
		{
			if (this.Element.Aspect == Aspect.AspectFit) {
				var radius = Math.Min (Width, Height) / 2;
				var strokeWidth = 10;
				radius -= strokeWidth / 2;

				Path path = new Path ();
				path.AddCircle (Width / 2, Height / 2, radius, Path.Direction.Ccw);
				canvas.Save ();
				canvas.ClipPath (path);

				var result = base.DrawChild (canvas, child, drawingTime);

				path.Dispose ();

				return result;

			}

			return base.DrawChild (canvas, child, drawingTime);
		}

		/// <summary>
		/// Reshapes the image.
		/// </summary>
		/// <param name="sourceBitmap">The source bitmap.</param>
		private void ReshapeImage (Bitmap sourceBitmap)
		{
			if (sourceBitmap != null) {
				var sourceRect = GetScaledRect (sourceBitmap.Height, sourceBitmap.Width);
				var rect = this.GetTargetRect (sourceBitmap.Height, sourceBitmap.Width);
				using (var output = Bitmap.CreateBitmap (rect.Width (), rect.Height (), Bitmap.Config.Argb8888)) {
					var canvas = new Canvas (output);

					var paint = new Paint ();
					var rectF = new RectF (rect);
					var roundRx = rect.Width () / 2;
					var roundRy = rect.Height () / 2;

					paint.AntiAlias = true;
					canvas.DrawARGB (0, 0, 0, 0);
					paint.Color = Android.Graphics.Color.ParseColor ("#ff424242");
					canvas.DrawRoundRect (rectF, roundRx, roundRy, paint);

					paint.SetXfermode (new PorterDuffXfermode (PorterDuff.Mode.SrcIn));
					canvas.DrawBitmap (sourceBitmap, sourceRect, rect, paint);

					//this.DrawBorder(canvas, rect.Width(), rect.Height());

					this.Control.SetImageBitmap (output);
					// Forces the internal method of InvalidateMeasure to be called.
					this.Element.WidthRequest = this.Element.WidthRequest;
				}
			}
		}

		/// <summary>
		/// Gets the scaled rect.
		/// </summary>
		/// <param name="sourceHeight">Height of the source.</param>
		/// <param name="sourceWidth">Width of the source.</param>
		/// <returns>Rect.</returns>
		/// <exception cref="System.NotImplementedException"></exception>
		private Rect GetScaledRect (int sourceHeight, int sourceWidth)
		{
			int height = 0;
			int width = 0;
			int top = 0;
			int left = 0;

			switch (this.Element.Aspect) {
			case Aspect.AspectFill:
				height = sourceHeight;
				width = sourceWidth;
				height = this.MakeSquare (height, ref width);
				left = (int)((sourceWidth - width) / 2);
				top = (int)((sourceHeight - height) / 2);
				break;
			case Aspect.Fill:
				height = sourceHeight;
				width = sourceWidth;
				break;
			case Aspect.AspectFit:
				height = sourceHeight;
				width = sourceWidth;
				height = this.MakeSquare (height, ref width);
				left = (int)((sourceWidth - width) / 2);
				top = (int)((sourceHeight - height) / 2);
				break;
			default:
				throw new NotImplementedException ();
			}

			var rect = new Rect (left, top, width + left, height + top);

			return rect;
		}

		/// <summary>
		/// Makes the square.
		/// </summary>
		/// <param name="height">The height.</param>
		/// <param name="width">The width.</param>
		/// <returns>System.Int32.</returns>
		private int MakeSquare (int height, ref int width)
		{
			if (height < width) {
				width = height;
			} else {
				height = width;
			}
			return height;
		}

		/// <summary>
		/// Gets the target rect.
		/// </summary>
		/// <param name="sourceHeight">Height of the source.</param>
		/// <param name="sourceWidth">Width of the source.</param>
		/// <returns>Rect.</returns>
		private Rect GetTargetRect (int sourceHeight, int sourceWidth)
		{
			int height = 0;
			int width = 0;

			height = this.Element.HeightRequest > 0
				? (int)System.Math.Round (this.Element.HeightRequest, 0)
				: sourceHeight; 
			width = this.Element.WidthRequest > 0
				? (int)System.Math.Round (this.Element.WidthRequest, 0)
				: sourceWidth; 

			// Make Square
			height = MakeSquare (height, ref width);

			return new Rect (0, 0, width, height);
		}
	}
}