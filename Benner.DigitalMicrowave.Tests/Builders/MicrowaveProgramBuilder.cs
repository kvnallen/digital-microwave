using Benner.DigitalMicrowave.Core.Models;

namespace Benner.DigitalMicrowave.Tests.Builders
{
    public class MicrowaveProgramBuilder
    {
        private string _name = "Chicken";
        private string _instructions = "...";
        private int _power = 1;
        private int _time = 1;

        private MicrowaveProgramBuilder()
        {

        }

        public static MicrowaveProgramBuilder New()
        {
            return new MicrowaveProgramBuilder();
        }

        public MicrowaveProgramBuilder Name(string name)
        {
            _name = name;
            return this;
        }

        public MicrowaveProgramBuilder Instructions(string instructions)
        {
            _instructions = instructions;
            return this;
        }
        public MicrowaveProgramBuilder Time(int time)
        {
            _time = time;
            return this;
        }

        public MicrowaveProgramBuilder Power(int power)
        {
            _power = power;
            return this;
        }

        public MicrowaveProgram Build()
        {
            return new MicrowaveProgram(_name, _instructions, _time, _power, "*");
        }
    }
}