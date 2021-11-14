using System;

using Xamarin.Forms;
using bildapp.Renderer;
using System.Linq;
using System.Collections.Generic;
using FFImageLoading.Forms;
using System.IO;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Newtonsoft.Json.Linq;
using I18NPortable;

namespace bildapp.Pages
{
    public class MakeImagePage : ContentPage
    {

        public static Color backgroundColor;
        public static double FontSize;
        public static FontAttributes Font;
        public static string MainText;
        public static Thickness BannerPadding;
        public static StackLayout MainBanner;
        public static Color TextColor;
        public static Label CenterLabel;
        public static CachedImage BannerBackgroundImage;
        public static double CenterLabelMainSize = 20;
        public static ImageSource src;
        public static Entry MainEntry;

        public static double Label_Height = 0;
        public static double Label_Width = 0;

        public static double Layout_Height = 0;
        public static double Layout_Width = 0;

        protected override void OnSizeAllocated(double width, double height)
        {
            AbsoluteLayout.SetLayoutBounds(BannerBackgroundImage, new Rectangle(0.5, 0.5, MainBanner.Width, MainBanner.Height));
            AbsoluteLayout.SetLayoutFlags(BannerBackgroundImage, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(CenterLabel, new Rectangle(0.5, 0.5, MainBanner.Width, MainBanner.Height));
            AbsoluteLayout.SetLayoutFlags(CenterLabel, AbsoluteLayoutFlags.PositionProportional);
            base.OnSizeAllocated(width, height);
        }

        public string SaveToDisk(string imageFileName, Stream imageData)
        {
            /*var status = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
            if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage) && await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Photos))
                {
                    await DisplayAlert("Need storage", "Request storage permission", "OK");
                }*/

                return DependencyService.Get<IFileService>().SavePicture(imageFileName, imageData, "imagesFolder");
                //Xamarin.Essentials.Preferences.Set(imageFileName, Convert.ToBase64String(imageAsBase64String));
            //}

        }
        public MakeImagePage()
        {
            Title = "Make_Your_Banner".Translate();

            CenterLabel = new Label()
            {
                Text = "Text",
                TextColor = Color.Black,
                FontSize = 20,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment =  TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center
            };
            BannerBackgroundImage = new CachedImage()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Aspect = Aspect.AspectFit,
                IsVisible = true
            };

            AbsoluteLayout.SetLayoutBounds(BannerBackgroundImage, new Rectangle(0.5, 0.5, Misc.ScreenWidth, 0));
            AbsoluteLayout.SetLayoutFlags(BannerBackgroundImage, AbsoluteLayoutFlags.PositionProportional);

            AbsoluteLayout.SetLayoutBounds(CenterLabel, new Rectangle(0.5, 0.5, Misc.ScreenWidth, 200));
            AbsoluteLayout.SetLayoutFlags(CenterLabel, AbsoluteLayoutFlags.PositionProportional);

            MainBanner = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(25, 25, 25, 25),
                BackgroundColor = Color.White,
                Children =
                        {
                            new AbsoluteLayout()
                            {
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                VerticalOptions = LayoutOptions.FillAndExpand,
                                Children =
                                {
                                    BannerBackgroundImage,
                                    CenterLabel
                                }
                            }
                        }
            };

            /*MainBanner.OnDrawing?.Invoke((bytes) =>
            {
                MemoryStream memoryStream = new MemoryStream(bytes);
                SaveToDisk("Banner.png", memoryStream);
            });*/

            var FontButton = new Button()
            {
                Text = "Banner_Settings".Translate(),
                BackgroundColor = Color.SteelBlue,
                TextColor = Color.White,
                Margin = new Thickness(15, 2, 2, 15)
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
                Margin = new Thickness(15, 2, 2, 15),
            };

            var next_button = new Button()
            {
                Margin = new Thickness(15, 2, 2, 15),
                Text = "Next",
                BackgroundColor = Color.FromHex("303F9F"),
                TextColor = Color.White,
            };

            next_button.Clicked += async (sender, e) =>
            {
                Layout_Height = MainBanner.Height;
                Layout_Width = MainBanner.Width;
                Label_Height = CenterLabel.Height;
                Label_Width = CenterLabel.Width;
                src = BannerBackgroundImage.Source;
                BannerPadding = CenterLabel.Padding;
                MainText = CenterLabel.Text;
                TextColor = CenterLabel.TextColor;
                backgroundColor = MainBanner.BackgroundColor;
                FontSize = CenterLabel.FontSize;
                Font = CenterLabel.FontAttributes;

                var bytes = await DependencyService.Get<IScreenshotManager>().CaptureAsync(MainBanner);
                Console.WriteLine("Size:" + bytes.Length.ToString());
                Stream stream = new MemoryStream(bytes);
                Misc.stream = stream;
                var path = SaveToDisk("Banner.png", stream);
                Misc.filepath = path;
                await Navigation.PushAsync(new FinishBanner());
            };

            MainEntry.TextChanged += (sender, e) =>
            {
                CenterLabel.Text = MainEntry.Text;
            };

            if (Misc.UsingTemplate == true)
            {
                if (Misc.Obj != null)
                {
                    CenterLabel.Text = (string)Misc.Obj["Text"];
                    MainEntry.Text = (string)Misc.Obj["Text"];
                    CenterLabel.FontSize = (double)Misc.Obj["Font_Size"];
                    CenterLabel.Padding = new Thickness((double)Misc.Obj["Padding"]);
                    CenterLabel.TextColor = Color.FromHex((string)Misc.Obj["Text_Color"]);
                    MainBanner.BackgroundColor = Color.FromHex((string)Misc.Obj["Background_Color"]);
                    BannerBackgroundImage.Source = (string)Misc.Obj["Background_URL"];


                }
                Misc.UsingTemplate = false;
            }

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
                                    next_button
                                }
                            }
                        }
                    }
                }
            };

        }
    }
}

