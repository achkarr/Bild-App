using System;

using Xamarin.Forms;
using bildapp.Renderer;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using System.Text;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using FFImageLoading.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using I18NPortable;

namespace bildapp.Pages
{

    public class FinishBanner : ContentPage
    {
        public static string EncodeBase64(string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }
        public static StackLayout MainBanner;
        public static Label CenterLabel;
        public static double CenterLabelMainSize = 20;
        public static Image MyImage;
        public static CachedImage BannerBackgroundImage;

        private async Task<string> Upload_Image(string url)
        {
            try
            {
                using (var client = new HttpClient())
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Headers.ContentType.MediaType = "multipart/form-data";
                     FileStream fs = new FileStream(Misc.filepath, FileMode.Open, FileAccess.Read);
                        StreamContent content = new StreamContent(fs);
                        //string fileExt = fileName.Substring(fileName.Length - 4);
                        //string fName = "User-Name-Here-123" + fileExt.ToLower();

                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = "image",
                            FileName = "Banner.png"
                        };
                        formData.Add(content);

                    var response = await client.PostAsync(url, formData);

                    var header = response.Headers;
                    var encoding = System.Text.Encoding.ASCII;
                    var stream = await response.Content.ReadAsStreamAsync();

                    using (var reader = new System.IO.StreamReader(stream, encoding))
                    {
                        return await reader.ReadToEndAsync();
                    };
                }
            }
            catch (HttpRequestException e)
            {
            }
            return "";
        }

        protected override void OnAppearing()
        {
            CenterLabel.WidthRequest = MainBanner.Width;
            CenterLabel.HeightRequest = MainBanner.Height;

            BannerBackgroundImage.WidthRequest = MainBanner.Width;
            BannerBackgroundImage.HeightRequest = MainBanner.Height;

            base.OnAppearing();
        }
        public FinishBanner()
        {
            Title = "Complete_Banner".Translate();

            CenterLabel = new Label()
            {
                Text = MakeImagePage.MainText,
                TextColor = MakeImagePage.TextColor,
                Padding = MakeImagePage.BannerPadding,
                FontSize = MakeImagePage.FontSize,
                FontAttributes = MakeImagePage.Font,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = MakeImagePage.Label_Height,
                WidthRequest = MakeImagePage.Label_Width
            };

            BannerBackgroundImage = new CachedImage()
            {
                WidthRequest = MakeImagePage.MainBanner.Width,
                HeightRequest = MakeImagePage.MainBanner.Height,
                Source = MakeImagePage.src,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Aspect = Aspect.AspectFit,
                IsVisible = true
            };

            MainBanner = new MyStackLayout()
            {
                BackgroundColor = MakeImagePage.backgroundColor,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = MakeImagePage.MainBanner.Width,
                HeightRequest = MakeImagePage.MainBanner.Height,
                Margin = new Thickness(25, 25, 25, 25),
                Children =
                {
                    new Image()
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Source = Misc.filepath
                    }
                }
            };
            /*MainBanner = new MyStackLayout()
            {
                BackgroundColor = MakeImagePage.backgroundColor,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                WidthRequest = MakeImagePage.MainBanner.Width,
                HeightRequest = MakeImagePage.MainBanner.Height,
                Margin = new Thickness(25, 25, 25, 25),
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
            };*/

            /*MainBanner = new MyStackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(25, 25, 25, 25),
                BackgroundColor = MakeImagePage.backgroundColor,
                Children =
                        {
                            CenterLabel
                        }
            };*/

            /*var SaveBanner = new Button()
            {
                Text = "Save Banner",
                BackgroundColor = Color.SteelBlue,
                TextColor = Color.White,
                Margin = new Thickness(15, 2, 2, 15)
            };*/

            var UploadImage = new Button()
            {
                Text = "Upload_Banner".Translate(),
                BackgroundColor = Color.DeepSkyBlue,
                TextColor = Color.White,
                Margin = new Thickness(15, 2, 2, 15)
            };

            UploadImage.Clicked += async (sender, e) =>
            {
                if(Misc.Token != null)
                {
                    string data = EncodeBase64(MakeImagePage.MainText);

                    Console.WriteLine("http://34.136.168.234/Api/Upload.php?TEXT=" + data.ToString() + "&FONT_SIZE=" + MakeImagePage.FontSize + "&PADDING=" + MakeImagePage.BannerPadding.Top + "&TEXT_COLOR=" + MakeImagePage.TextColor.ToHex() + "&BACKGROUND_COLOR=" + MakeImagePage.backgroundColor.ToHex());
                    await Upload_Image("http://34.136.168.234/Api/Upload.php?TEXT=" + data.ToString() + "&FONT_SIZE=" + MakeImagePage.FontSize + "&PADDING=" + MakeImagePage.BannerPadding.Top + "&TEXT_COLOR=" + EncodeBase64(MakeImagePage.TextColor.ToHex()) + "&BACKGROUND_COLOR=" + EncodeBase64(MakeImagePage.backgroundColor.ToHex()));

                    SavedConfigurations.SavedConfigs.Clear();
                    MainPage.LoadSavedConfigurations();

                    await DisplayAlert("Image_Uploaded_Header".Translate(), "Image_Uploaded_Body".Translate(), "OK");
                }
                else
                {
                    await DisplayAlert("No_Account".Translate(), "No_Account_Body".Translate(), "OK");
                }
            };


            /*SaveBanner.Clicked += async (sender, e) =>
            {
                if (Misc.Token != null)
                {
                    
                }
                else
                {
                    await DisplayAlert("Account Required", "You must have an account in order to save an image to the database.", "OK");
                }
            };*/
            var ShareButton = new Button()
            {
                Margin = new Thickness(15, 2, 2, 15),
                Text = "Share_Banner".Translate(),
                BackgroundColor = Color.FromHex("303F9F"),
                TextColor = Color.White,
            };
            ShareButton.Clicked += async (sender, e) =>
            {
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Share_Banner".Translate(),
                    File = new ShareFile(Misc.filepath)
                });
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
                                    ShareButton,
                                    //SaveBanner,
                                    UploadImage
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}

/*namespace UploadPicToServer
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnUpload_Clicked(object sender, EventArgs e)
        {

            
        }
        

        public static byte[] ToArray(Stream s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            if (!s.CanRead)
                throw new ArgumentException("Stream cannot be read");

            MemoryStream ms = s as MemoryStream;
            if (ms != null)
                return ms.ToArray();

            long pos = s.CanSeek ? s.Position : 0L;
            if (pos != 0L)
                s.Seek(0, SeekOrigin.Begin);

            byte[] result = new byte[s.Length];
            s.Read(result, 0, result.Length);
            if (s.CanSeek)
                s.Seek(pos, SeekOrigin.Begin);
            return result;
        }
    }
}
*/