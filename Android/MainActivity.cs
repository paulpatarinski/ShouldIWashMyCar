
using Android.App;
using Android.OS;
using Core;

using Xamarin.Forms.Platform.Android;


namespace ShouldIWashMyCar.Android
{
	[Activity (Label = "ShouldIWashMyCar", Icon = "@drawable/icon", MainLauncher = true)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			SetPage (App.GetMainPage ());
		}
	}
}

