using Benner.DigitalMicrowave.Core;
using Benner.DigitalMicrowave.Core.Models;

namespace Benner.DigitalMicrowave.Tests
{
    public class MicrowaveOptionsBuilder
    {
        private int _time = 1;
        private int _power = 1;
        private string _text = "Chicken";


        public MicrowaveOptionsBuilder ForLong(int time)
        {
            _time = time;
            return this;
        }

        public MicrowaveOptionsBuilder WithPower(int power)
        {
            _power = power;
            return this;
        }

        public MicrowaveOptionsBuilder WithText(string text)
        {
            _text = text;
            return this;
        }

        public static MicrowaveOptionsBuilder New() 
            => new MicrowaveOptionsBuilder();

        public MicrowaveOptions Build()
        {
            return new MicrowaveOptions(_time, _power, _text, ".");
        }
    }
}