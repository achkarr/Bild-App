using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace bildapp.Pages
{
    public class BackgroundView : ContentPage
    {
        public BackgroundView()
        {

            Color[] ColorArray = { Color.Black, Color.Orange, Color.MidnightBlue, Color.Navy, Color.Orchid, Color.Pink, Color.Turquoise, Color.Aqua, Color.BurlyWood, Color.DarkViolet };
            //List<Button> ButtonList = new List<Button>();

            var ColorListStack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(1, 1, 1, 1),
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 80
            };

            for (int i = 0; i < ColorArray.Length; i++)
            {
                var ColorButton = new Button()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    BackgroundColor = ColorArray[i],
                    HeightRequest = 50,
                    WidthRequest = 50,
                    Margin = new Thickness(2, 2, 2, 2),
                };
                ColorButton.Clicked += (sender, e) =>
                {
                    MakeImagePage.MainBanner.BackgroundColor = ColorButton.BackgroundColor;
                };
                ColorListStack.Children.Add(ColorButton);
            }

            Dictionary<string, double> FontType = new Dictionary<string, double>
            {
                { "10", 10 },
                { "12", 12 },
                { "14", 14 },
                { "16", 16 },
                { "18", 18 },
                { "20", 20 },
                { "22", 22 },
                { "24", 24 },
                { "26", 26 },
                { "28", 28 },
                { "30", 30 },
                { "32", 32 },
                { "34", 34 }
            };

            Dictionary<string, double> FontStyle = new Dictionary<string, double>
            {
                { "Normal", 1 },
                { "Bold", 2 },
                { "Italic", 2 }
            };

            Picker FontPicker = new Picker
            {
                Title = "20",
                Margin = new Thickness(0, 20, 0, 15),
                BackgroundColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            foreach (string Shop_Type in FontType.Keys)
            {
                FontPicker.Items.Add(Shop_Type);
            }

            FontPicker.SelectedIndexChanged += (sender, args) =>
            {
                if (FontPicker.SelectedIndex == -1)
                {
                    MakeImagePage.CenterLabel.FontSize = double.Parse(FontPicker.Items[FontPicker.SelectedIndex]);
                }
            };

            Picker StylePicker = new Picker
            {
                Title = "Normal",
                Margin = new Thickness(0, 20, 0, 15),
                BackgroundColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            foreach (string Shop_Type in FontStyle.Keys)
            {
                StylePicker.Items.Add(Shop_Type);
            }

            StylePicker.SelectedIndexChanged += (sender, args) =>
            {
                if (StylePicker.SelectedIndex == -1)
                {
                    var Num = double.Parse(StylePicker.Items[StylePicker.SelectedIndex]);
                    MakeImagePage.CenterLabelMainSize = Num;
                    MakeImagePage.CenterLabel.FontSize = Num;
                }
            };

            ScrollView MainContent = new ScrollView();

            MainContent.Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new Label()
                    {
                        Text = "Background Color",
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Bold,
                        FontSize  = 28,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    new ScrollView()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Orientation = ScrollOrientation.Horizontal,
                        Content = ColorListStack
                    },
                    new Label()
                    {
                        Text = "Font Size",
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Bold,
                        FontSize  = 28,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    FontPicker,
                    new Label()
                    {
                        Text = "Font Style",
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Bold,
                        FontSize  = 28,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    StylePicker
                }
            };
            Content = MainContent;
        }
    }
}

