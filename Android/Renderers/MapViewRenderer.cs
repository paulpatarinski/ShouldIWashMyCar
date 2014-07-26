using System;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps;
using Xamarin.Forms;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Maps;
using Core;

[assembly: ExportRenderer (typeof(Core.CustomMap), typeof(ShouldIWashMyCar.Android.MapViewRenderer))]
namespace ShouldIWashMyCar.Android
{
	public class MapViewRenderer : MapRenderer
	{
		bool _isDrawnDone;

		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);
			var androidMapView = (MapView)Control;
			var formsMap = (CustomMap)sender;
			 
			if (e.PropertyName.Equals ("VisibleRegion") && !_isDrawnDone) {
				androidMapView.Map.Clear ();

				androidMapView.Map.MarkerClick += HandleMarkerClick;
				androidMapView.Map.MyLocationEnabled = formsMap.IsShowingUser;

				var formsPins = formsMap.CustomPins;

				foreach (var formsPin in formsPins) {
					var markerWithIcon = new MarkerOptions ();

					markerWithIcon.SetPosition (new LatLng (formsPin.Position.Latitude, formsPin.Position.Longitude));
					markerWithIcon.SetTitle (formsPin.Label);
					markerWithIcon.SetSnippet (formsPin.Address);

					if (!string.IsNullOrEmpty (formsPin.PinIcon))
						markerWithIcon.InvokeIcon (BitmapDescriptorFactory.FromAsset (String.Format ("{0}.png", formsPin.PinIcon)));
					else
						markerWithIcon.InvokeIcon (BitmapDescriptorFactory.DefaultMarker ());
						
					androidMapView.Map.AddMarker (markerWithIcon);
				}

				_isDrawnDone = true;

			}
		}

		void HandleMarkerClick (object sender, GoogleMap.MarkerClickEventArgs e)
		{
			var marker = e.P0;
			marker.ShowInfoWindow ();

			var myMap = this.Element as CustomMap;

			var formsPin = new CustomPin {
				Label = marker.Title,
				Address = marker.Snippet,
				Position = new Position (marker.Position.Latitude, marker.Position.Longitude)
			};

			myMap.SelectedPin = formsPin;
		}

		protected override void OnLayout (bool changed, int l, int t, int r, int b)
		{
			base.OnLayout (changed, l, t, r, b);

			//NOTIFY CHANGE

			if (changed) {
				_isDrawnDone = false;
			}
		}
	}
}