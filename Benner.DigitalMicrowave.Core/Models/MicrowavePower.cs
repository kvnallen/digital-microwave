using System;

namespace Benner.DigitalMicrowave.Core.Models
{
    public class MicrowavePower
    {
        private readonly int MIN_POWER_ALLOWED = 1;
        private readonly int MAX_POWER_ALLOWED = 10;

        public MicrowavePower(int power)
        {
            if (power < MIN_POWER_ALLOWED || power > MAX_POWER_ALLOWED)
                throw new ArgumentException(Errors.POWER_OUT_OF_RANGE);

            Value = power;
        }

        public int Value { get; }

        public static implicit operator MicrowavePower(int timeInSeconds)
        {
            return new MicrowavePower(timeInSeconds);
        }

        public static implicit operator int(MicrowavePower power)
        {
            return power.Value;
        }
    }
}