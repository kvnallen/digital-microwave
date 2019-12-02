using System.Threading.Tasks;
using Benner.DigitalMicrowave.Core.Models;

namespace Benner.DigitalMicrowave.Core.Events
{
    public interface IMicrowaveNotifier
    {
        bool IsSatisfied(MicrowaveOptions options);

        Task Notify(string currentText);

        Task NotifyFinish(string currentText);

        Task Cancel(string originalText);
        Task Pause(string text, int currentSecond);
    }
}