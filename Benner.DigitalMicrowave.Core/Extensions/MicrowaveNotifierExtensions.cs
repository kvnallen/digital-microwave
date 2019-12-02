using System.Collections.Generic;
using System.Threading.Tasks;
using Benner.DigitalMicrowave.Core.Events;

namespace Benner.DigitalMicrowave.Core.Extensions
{
    public static class MicrowaveNotifierExtensions
    {
        public static async Task NotifyAll(this IEnumerable<IMicrowaveNotifier> notifiers, string text)
        {
            foreach (var notifier in notifiers)
            {
                await notifier.Notify(text);
            }
        }

        public static async Task NotifyFinished(this IEnumerable<IMicrowaveNotifier> notifiers, string text)
        {
            foreach (var notifier in notifiers)
            {
                await notifier.NotifyFinish(text);
            }
        }

        public static async Task NotifyCancellation(this IEnumerable<IMicrowaveNotifier> notifiers, string text, int currentSecond)
        {
            foreach (var notifier in notifiers)
            {
                await notifier.Cancel(text);
            }
        }

        public static async Task NotifyPause(this IEnumerable<IMicrowaveNotifier> notifiers, string text, int currentSecond)
        {
            foreach (var notifier in notifiers)
            {
                await notifier.Pause(text, currentSecond);
            }
        }
    }
}