using System;
using System.Text;
using System.Net;
using System.Threading.Tasks;



namespace bildapp
{
    public class Misc
    {
        public static string Token { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string Email { get; set; }

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
