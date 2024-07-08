using System.Text;

namespace Demo.Helpers
{
    public class Utilities
    {
        public static string GenerateRandomPassword(int length = 10)
        {
            var pattern = @"qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!@#%^&*0123456789";
            var sub = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sub.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            return sub.ToString();
        }
    }
}
