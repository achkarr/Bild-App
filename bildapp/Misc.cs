using System;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Resources;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

//using Microsoft
namespace bildapp
{
    /*public static class StringTranslationExtensions
    {
        public static string Translate(this string text)
        {
            if (text != null)
            {
                var assembly = typeof(StringExtensions).GetTypeInfo().Assembly;
                var assemblyName = assembly.GetName();
                ResourceManager resourceManager = new ResourceManager($"{assemblyName.Name}.Resources", assembly);
                var lg = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                return resourceManager.GetString(text, new CultureInfo(lg));
            }

            return null;
        }
    }*/
    public class Misc
    {
        public static List<string> BackgroundImageArray = new List<string>();
        public static string Token = null;
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string Email { get; set; }
        public static int CurrentLanguage { get; set; }
        public static Stream stream { get; set; }
        public static bool UsingTemplate = false;
        public static JObject Obj = null;
        public static string text = "";
        public static string filepath;
        public static string CurrentURL = "";
        public static double ScreenHeight { get; set; }
        public static double ScreenWidth { get; set; }

        public Misc()
        {
        }
        public static async Task<string> MakeConnection(string url, string data)
        {
            int RetryCount = 3;
            int CurrentTry = 0;
            string Status = "";

            while (CurrentTry < RetryCount)
            {
                HttpWebRequest request = System.Net.WebRequest.Create(url+ data) as HttpWebRequest;
                request.Timeout = 3000;
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "GET";

                try
                {
                    HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                    WebHeaderCollection header = response.Headers;
                    var encoding = System.Text.Encoding.ASCII;
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                    {
                        Status = await reader.ReadToEndAsync();
                    };
                }
                catch (WebException ex)
                {
                    return "Connection Error";
                }

                if (Status == "Connection Error")
                    CurrentTry++;
                else
                    break;

            }

            return Status;
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
