using System;
using Xamarin.Forms;
using bildapp.Pages;

namespace bildapp
{
    public class MainPageCS : FlyoutPage
    {

        FlyoutMenuPageCS flyoutPage;

        public MainPageCS()
        {
            flyoutPage = new FlyoutMenuPageCS();
            Flyout = flyoutPage;
            Detail = new NavigationPage(new MakeImagePage());

            flyoutPage.ListView.ItemSelected += OnItemSelected;

            FlyoutLayoutBehavior = FlyoutLayoutBehavior.Popover;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FlyoutPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                flyoutPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}

