using System;

using Xamarin.Forms;
using bildapp.Renderer;
using System.Linq;
using System.Collections.Generic;

namespace bildapp.Pages
{
    public class MakeImagePage : ContentPage
    {
        public static MyStackLayout MainBanner;
        public static Label CenterLabel;
        public static double CenterLabelMainSize = 20;
        public MakeImagePage()
        {
            Title = "Make Your Banner";

            CenterLabel = new Label()
            {
                Text = "Text",
                TextColor = Color.Black,
                FontSize = 20,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            MainBanner = new MyStackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(10, 10, 10, 10),
                BackgroundColor = Color.White,
                Children =
                        {
                            CenterLabel
                        }
            };

            var FontButton = new Button()
            {
                Text = "Banner Settings",
                BackgroundColor = Color.MediumBlue,
                TextColor = Color.White,
                Margin = new Thickness(2, 2, 2, 2)
            };

            FontButton.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new BackgroundView());
            };

            var MainEntry = new Entry()
            {
                Text = "Text",
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 40
            };

            MainEntry.TextChanged += (sender, e) =>
            {
                CenterLabel.Text = MainEntry.Text;
            };
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    MainBanner,
                    new StackLayout()
                    {
                        BackgroundColor = Color.Black,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.End,
                        HeightRequest = 200,
                        Children =
                        {
                            new StackLayout()
                            {
                                BackgroundColor = Color.White,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                Margin = new Thickness(1,1,1,1),
                                Children = {
                                    MainEntry,
                                    FontButton,
                                    new Button()
                                    {
                                        Text = "Next",
                                        BackgroundColor = Color.DarkBlue,
                                        TextColor = Color.White,
                                        Margin = new Thickness(2,2,2,2)
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}

