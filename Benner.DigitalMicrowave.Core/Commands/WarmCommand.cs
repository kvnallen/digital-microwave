using Benner.DigitalMicrowave.Core.Models;

namespace Benner.DigitalMicrowave.Core.Commands
{
    public class WarmCommand
    {
        public string Text { get; set; }
        public string ProgramName { get; set; }
        public int Time { get; set; }
        public int? CurrentTime { get; set; }
        public int Power { get; set; }

        internal MicrowaveTime GetTime()
        {
            return CurrentTime ?? Time;
        }
    }
}