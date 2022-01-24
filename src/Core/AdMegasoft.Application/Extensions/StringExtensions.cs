using System.Security.Cryptography;
using System.Text;

namespace AdMegasoft.Application.Extensions
{
    public static class StringExtensions
    {
        public static string ToMD5(this string input)
        {
            var provider = MD5.Create();
            string salt = "S0m3R@nd0mSalt";
            byte[] bytes = provider.ComputeHash(Encoding.UTF32.GetBytes(salt + input));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
