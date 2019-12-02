using System.Text;

namespace Benner.DigitalMicrowave.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Repeat(this string value, int count)
        {
            return new StringBuilder(value.Length * count)
                .Insert(0, value, count)
                .ToString();
        }
    }
}