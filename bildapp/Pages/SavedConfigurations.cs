using System;

using Xamarin.Forms;
using bildapp.Renderer;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using I18NPortable;

namespace bildapp.Pages
{
    public class SavedConfigurations : ContentPage
    {
        public static List<JObject> SavedConfigs = new List<JObject>();

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        public SavedConfigurations()
        {
            Title = "Public_Saved_Configurations".Translate();

            var MainStackContent = new MyStackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(25, 25, 25, 25),
                Orientation = StackOrientation.Vertical
            };

            for(int i = 0; i < SavedConfigs.Count; i++)
            {
                Button UseTemplate = new Button()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Text = "Use_Template".Translate(),
                    StyleId = i.ToString()
                };

                UseTemplate.Clicked += async (sender, e) =>
                {
                    //Console.WriteLine("ID:" + );
                    Misc.UsingTemplate = true;
                    Misc.Obj = SavedConfigs[int.Parse(UseTemplate.StyleId)];
                };
                MainStackContent.Children.Add(
                new Image()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Margin = new Thickness(25, 25, 25, 25),
                    BackgroundColor = Color.White,
                    Source = (string)SavedConfigs[i]["URL"]
                });

                MainStackContent.Children.Add(UseTemplate);
            }

            ScrollView MainContent = new ScrollView() { Content = MainStackContent };
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    MainContent
                }
            };
        }
    }
}

