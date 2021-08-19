﻿using System;
using Xamarin.Forms;
using System.Collections.Generic;
using bildapp.Pages;

namespace bildapp
{
    public class FlyoutPageItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
    public class FlyoutMenuPageCS : ContentPage
    {
        ListView listView;
        public ListView ListView { get { return listView; } }

        public FlyoutMenuPageCS()
        {
            var flyoutPageItems = new List<FlyoutPageItem>();
            flyoutPageItems.Add(new FlyoutPageItem
            {
                Title = "Create New Banner",
                TargetType = typeof(MakeImagePage)
            });
            flyoutPageItems.Add(new FlyoutPageItem
            {
                Title = "View Saved Banners",
                TargetType = typeof(MakeImagePage)
            });
            flyoutPageItems.Add(new FlyoutPageItem
            {
                Title = "Settings",
                TargetType = typeof(MakeImagePage)
            });
            flyoutPageItems.Add(new FlyoutPageItem
            {
                Title = "Logout",
                TargetType = typeof(MakeImagePage)
            });

            listView = new ListView
            {
                ItemsSource = flyoutPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var image = new Image();
                    image.SetBinding(Image.SourceProperty, "IconSource");
                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(image);
                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            IconImageSource = "hamburger.png";
            Title = "Bild App Menu";
            Padding = new Thickness(0, 40, 0, 0);
            Content = new StackLayout
            {
                Children = { listView }
            };
        }
    }
}

