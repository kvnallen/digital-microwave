namespace Benner.DigitalMicrowave.Core.Commands
{
    public class CreateProgramCommand
    {
        public string Name { get; set; }
        public string Instructions { get; set; }
        public int Time { get; set; }
        public int Power { get; set; }
        public string HeatingCharacter { get; set; }
    }
}