using Android.Graphics.Drawables;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Labs.Enums;
using Button = Android.Widget.Button;

[assembly: ExportRenderer (typeof(Core.ImageButton), typeof(ShouldIWashMyCar.Android.ImageButtonRenderer))]
namespace ShouldIWashMyCar.Android
{
	public class ImageButtonRenderer : ButtonRenderer
	{
		private Core.ImageButton ImageButton {
			get { return (Core.ImageButton)Element; }
		}


		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null) {
				return;
			}
			var targetButton = this.Control;


			if (this.Element != null && !string.IsNullOrEmpty (this.ImageButton.Image)) {
				this.SetImageSource (targetButton, this.ImageButton);
			}

		}

		private void SetImageSource (Button targetButton, Core.ImageButton model)
		{
			var packageName = Context.PackageName;
			const int Padding = 10;
			const string ResourceType = "drawable";
			var resId = int.Parse (Resource.Drawable.Refresh.ToString ());

//			var resId = Resources.GetIdentifier (model.Image, ResourceType, packageName);
			if (resId > 0) {
				var scaledDrawable = GetScaleDrawableFromResourceId (resId, GetWidth (model.ImageWidthRequest),
					                     GetHeight (model.ImageHeightRequest));


				Drawable left = null;
				Drawable right = null;
				Drawable top = null;
				Drawable bottom = null;
//				targetButton.CompoundDrawablePadding = Padding;
				switch (model.Orientation) {
				case ImageOrientation.ImageToLeft:
					targetButton.Gravity = GravityFlags.Left | GravityFlags.CenterVertical;
					left = scaledDrawable;
					break;
				case ImageOrientation.ImageToRight:
					targetButton.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
					right = scaledDrawable;
					break;
				case ImageOrientation.ImageOnTop:
					top = scaledDrawable;
					break;
				case ImageOrientation.ImageOnBottom:
					bottom = scaledDrawable;
					break;
				}


				targetButton.SetCompoundDrawables (left, top, right, bottom);
			}
		}

		private Drawable GetScaleDrawableFromResourceId (int resId, int width, int height)
		{
			var drawable = Resources.GetDrawable (resId);


			var returnValue = new ScaleDrawable (drawable, 0, width, height).Drawable;
			returnValue.SetBounds (0, 0, width, height);
			return returnValue;
		}


		/// <summary>
		/// Gets the width based on the requested width, if request less than 0, returns 50.
		/// </summary>
		/// <param name="requestedWidth">The requested width.</param>
		/// <returns>The width to use.</returns>
		private int GetWidth (int requestedWidth)
		{
			const int DefaultWidth = 50;
			return requestedWidth <= 0 ? DefaultWidth : requestedWidth;
		}


		/// <summary>
		/// Gets the height based on the requested height, if request less than 0, returns 50.
		/// </summary>
		/// <param name="requestedHeight">The requested height.</param>
		/// <returns>The height to use.</returns>
		private int GetHeight (int requestedHeight)
		{
			const int DefaultHeight = 50;
			return requestedHeight <= 0 ? DefaultHeight : requestedHeight;
		}


	}
}

