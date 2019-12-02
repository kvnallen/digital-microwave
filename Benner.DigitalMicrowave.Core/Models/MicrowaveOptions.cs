using System.IO;

namespace Benner.DigitalMicrowave.Core.Models
{
    public class MicrowaveOptions
    {
        public MicrowaveOptions(
            MicrowaveTime time,
            MicrowavePower power,
            string text,
            string heatingCharacter,
            MicrowaveProgram program = null,
            MicrowaveTime currentTime = null
            )
        {
            Time = time;
            CurrentTime = currentTime ?? 1;
            Power = power;
            HeatingCharacter = heatingCharacter;
            Program = program;
            Text = text;
        }

        public MicrowavePower Power { get; set; }
        public MicrowaveTime Time { get; set; }
        public MicrowaveTime CurrentTime { get; set; }
        public MicrowaveProgram Program { get; set; }
        public string HeatingCharacter { get; set; }
        public string Text { get; set; }

        public bool IsFile() => File.Exists(Text);
    }
}