using System;
using Xamarin.Forms;
using Core.Services;
using System.Net.Http;

namespace Core
{
	public class MainPage : ContentPage
	{
		public MainPage ()
		{	
			NavigationPage.SetHasNavigationBar (this, false);

			Navigation.PushAsync (new LoadingPage ());
		}
	}
}