using System;

using Xamarin.Forms;
using System.Collections.Generic;
using I18NPortable;

namespace bildapp.Pages
{
    public class BackgroundView : ContentPage
    {
        public static Entry UrlEntry;
        public BackgroundView()
        {

            Color[] ColorArray = { Color.White, Color.Black, Color.Orange, Color.MidnightBlue, Color.Navy, Color.Orchid, Color.Pink, Color.Turquoise, Color.Aqua, Color.BurlyWood, Color.DarkViolet };

            //List<Button> ButtonList = new List<Button>();

            var ImageListStack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(1, 1, 1, 1),
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 90
            };

            for (int i = 0; i < Misc.BackgroundImageArray.Count; i++)
            {
                var ImageButton = new ImageButton()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Source = Misc.BackgroundImageArray[i],
                    
                    HeightRequest = 50,
                    WidthRequest = 70,
                    Margin = new Thickness(2, 2, 2, 2),
                    StyleId = i.ToString()
                };
                ImageButton.Clicked += (sender, e) =>
                {
                    Misc.CurrentURL = Misc.BackgroundImageArray[int.Parse(ImageButton.StyleId)];
                    MakeImagePage.BannerBackgroundImage.Source = ImageButton.Source;
                };
                ImageListStack.Children.Add(ImageButton);
            }

            var ColorListStack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(1, 1, 1, 1),
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 60
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


            var TextColorList = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(1, 1, 1, 1),
                BackgroundColor = Color.White,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 60
            };

            for (int i = 0; i < ColorArray.Length; i++)
            {
                var TextColorButton = new Button()
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    BackgroundColor = ColorArray[i],
                    HeightRequest = 50,
                    WidthRequest = 50,
                    Margin = new Thickness(2, 2, 2, 2),
                };
                TextColorButton.Clicked += (sender, e) =>
                {
                    MakeImagePage.CenterLabel.TextColor = TextColorButton.BackgroundColor;
                };
                TextColorList.Children.Add(TextColorButton);
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
                { "Italic", 3 }
            };

            Dictionary<string, double> PaddingOptions = new Dictionary<string, double>
            {
                { "10", 10 },
                { "12", 12 },
                { "14", 14 },
                { "16", 16 },
                { "18", 18 },
                { "20", 20 },
            };

            Picker PaddingPicker = new Picker
            {
                Title = "20",
                Margin = new Thickness(0, 20, 0, 15),
                BackgroundColor = Color.White,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            foreach (string PaddingOption in PaddingOptions.Keys)
            {
                PaddingPicker.Items.Add(PaddingOption);
            }

            PaddingPicker.SelectedIndexChanged += (sender, args) =>
            {
                var num = double.Parse(PaddingPicker.Items[PaddingPicker.SelectedIndex]);
                MakeImagePage.CenterLabel.Padding = num;
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
                var num = double.Parse(FontPicker.Items[FontPicker.SelectedIndex]);
                MakeImagePage.CenterLabel.FontSize = num;
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
                if (StylePicker.SelectedIndex == 0)
                    MakeImagePage.CenterLabel.FontAttributes = FontAttributes.None;
                else if (StylePicker.SelectedIndex == 1)
                    MakeImagePage.CenterLabel.FontAttributes = FontAttributes.Bold;
                else if (StylePicker.SelectedIndex == 2)
                    MakeImagePage.CenterLabel.FontAttributes = FontAttributes.Italic;
            };

            UrlEntry = new Entry()
            {
                Text = "",
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(15, 2, 2, 15),
            };
            UrlEntry.TextChanged += (sender, e) =>
            {
                Misc.CurrentURL = UrlEntry.Text;
                MakeImagePage.BannerBackgroundImage.Source = UrlEntry.Text;
            };

            ScrollView MainContent = new ScrollView();

            MainContent.Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new Label()
                    {
                        Text = "Background_Color".Translate(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Bold,
                        FontSize  = 22,
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
                        Text = "Text_Color".Translate(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Bold,
                        FontSize  = 22,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    new ScrollView()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Orientation = ScrollOrientation.Horizontal,
                        Content = TextColorList
                    },
                    new Label()
                    {
                        Text = "Background_Image".Translate(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 22,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    new ScrollView()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Orientation = ScrollOrientation.Horizontal,
                        Content = ImageListStack
                    },
                    new Label()
                    {
                        Text = "Background_Image_Custom".Translate(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 22,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    UrlEntry,
                    new Label()
                    {
                        Text = "Font_Size".Translate(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Bold,
                        FontSize  = 22,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    FontPicker,
                    new Label()
                    {
                        Text = "Font_Style".Translate(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Bold,
                        FontSize  = 22,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    StylePicker,
                    new Label()
                    {
                        Text = "Text_Padding".Translate(),
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontAttributes = FontAttributes.Bold,
                        FontSize  = 22,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    },
                    PaddingPicker
                }
            };
            Content = MainContent;
        }
    }
}

