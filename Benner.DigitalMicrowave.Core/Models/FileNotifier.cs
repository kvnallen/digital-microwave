using System;
using System.IO;
using System.Threading.Tasks;
using Benner.DigitalMicrowave.Core.Events;

namespace Benner.DigitalMicrowave.Core.Models
{
    public class FileNotifier : IMicrowaveNotifier
    {
        private MicrowaveOptions _options;
        private StreamWriter _writer;

        public bool IsSatisfied(MicrowaveOptions options)
        {
            _options = options;
            var fileExists = File.Exists(options.Text);

            if (fileExists)
            {
                _writer = new StreamWriter(options.Text, false);
            }

            return fileExists;
        }
        
        private async Task WriteInFile(string text)
        {
            var toWrite = GetText(text);
            await _writer.WriteAsync(toWrite);
            await _writer.FlushAsync();
        }

        private string GetText(string text)
        {
            return text.Replace(_options.Text, string.Empty);
        }

        public async Task Notify(string currentText)
        {
            await WriteInFile(currentText);
        }

        public async Task NotifyFinish(string currentText)
        {
            await WriteInFile(currentText);
            await _writer.DisposeAsync();
        }

        public async Task Cancel(string originalText)
        {
            await WriteInFile(originalText);
            await _writer.DisposeAsync();
        }

        public Task Pause(string text, int currentSecond)
        {
            return Task.CompletedTask;
        }
    }
}