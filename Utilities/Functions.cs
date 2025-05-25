using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
namespace TimTro.Utilities
{
    public class Functions
    {
        public static int userid = 0;
        public static string userphone = string.Empty;
        public static string username = string.Empty;
        public static byte[] useravatar = null;
        public static string useravatartype = string.Empty;
        public static int? userrole = 0; 

        public static string message = string.Empty;
        public static string returnlink = string.Empty;


        private static readonly HttpClient httpClient = new HttpClient();

        public static string MD5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                stringBuilder.Append(result[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static string MD5Password(string text)
        {
            string str = MD5(text);
            str = MD5(str + str);
            return str;
        }

        public static bool IsLogin()
        {
            if (userid == 0 || string.IsNullOrEmpty(userphone) || string.IsNullOrEmpty(username))
                return false;
            return true;
        }

        public static async Task<string> GetNameByType(string type, int? code)
        {
            var url = $"https://provinces.open-api.vn/api/{type}/{code}";

            try
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var doc = JsonDocument.Parse(json);
                    if (doc.RootElement.TryGetProperty("name", out var nameProp))
                    {
                        return nameProp.GetString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy {type} vấn đề: {ex.Message}");
            }

            return "Không xác định";
        }

    }
}
