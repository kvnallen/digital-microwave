using System;

namespace Benner.DigitalMicrowave.Core.Models
{
    public class MicrowaveTime
    {
        private readonly TimeSpan _maxTimeAllowed = TimeSpan.FromMinutes(2);

        private readonly TimeSpan _minTimeAllowed = TimeSpan.FromSeconds(1);

        public MicrowaveTime(int timeInSeconds)
        {
            var time = TimeSpan.FromSeconds(timeInSeconds);
            if (time < _minTimeAllowed || time > _maxTimeAllowed)
                throw new ArgumentException(Errors.TIME_OUT_OF_RANGE);

            Value = timeInSeconds;
        }

        public int Value { get; }

        public static implicit operator MicrowaveTime(int timeInSeconds)
        {
            return new MicrowaveTime(timeInSeconds);
        }

        public static implicit operator int(MicrowaveTime time)
        {
            return time.Value;
        }
    }
}