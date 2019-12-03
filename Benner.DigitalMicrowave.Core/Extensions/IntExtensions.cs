using System;
using System.Threading.Tasks;

namespace Benner.DigitalMicrowave.Core.Extensions
{
    public static class IntExtensions
    {
        public static async Task Timer(this int startFrom, int end, Action action)
        {
            for (; startFrom < end; startFrom++)
            {
                action.Invoke();
                startFrom++;
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
