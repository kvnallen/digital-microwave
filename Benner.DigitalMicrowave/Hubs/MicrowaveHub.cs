using System.IO;
using System.Threading.Tasks;
using Benner.DigitalMicrowave.Core;
using Benner.DigitalMicrowave.Core.Events;
using Benner.DigitalMicrowave.Core.Models;
using Microsoft.AspNetCore.SignalR;

namespace Benner.DigitalMicrowave.Hubs
{
    public class MicrowaveHub : Hub, IMicrowaveNotifier
    {
        private readonly IHubContext<MicrowaveHub> _context;

        public MicrowaveHub(IHubContext<MicrowaveHub> context)
        {
            _context = context;
        }

        public bool IsSatisfied(MicrowaveOptions options)
        {
            return !File.Exists(options.Text);
        }

        public async Task Notify( string text)
        {
            await _context.Clients.All
                .SendAsync("timerUpdated", text);
        }


        public async Task NotifyFinish(string text)
        {
            await _context.Clients.All.SendAsync("timerFinished", text);
        }

        public async Task Cancel(string originalText)
        {
            await _context.Clients.All.SendAsync("timerCancelled", originalText);
        }

        public async Task Pause(string text, int currentSecond)
        {
            await _context.Clients.All.SendAsync("timerPaused", text, currentSecond);
        }
    }
}