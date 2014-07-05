using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ShouldIWashMyCar.WinPhone.Resources;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Labs.WP.Controls.Calendar;

namespace ShouldIWashMyCar.WinPhone
{
  public partial class MainPage : PhoneApplicationPage
  {
    // Constructor
    public MainPage()
    {
      InitializeComponent();

      //Call so that the assembly is loaded into memory
      new CalendarViewRenderer();


      Forms.Init();
      FormsMaps.Init();


      Content = Core.App.GetMainPage().ConvertPageToUIElement(this);


      // Sample code to localize the ApplicationBar
      //BuildLocalizedApplicationBar();
    }

    // Sample code for building a localized ApplicationBar
    //private void BuildLocalizedApplicationBar()
    //{
    //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
    //    ApplicationBar = new ApplicationBar();

    //    // Create a new button and set the text value to the localized string from AppResources.
    //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
    //    appBarButton.Text = AppResources.AppBarButtonText;
    //    ApplicationBar.Buttons.Add(appBarButton);

    //    // Create a new menu item with the localized string from AppResources.
    //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
    //    ApplicationBar.MenuItems.Add(appBarMenuItem);
    //}
  }
}