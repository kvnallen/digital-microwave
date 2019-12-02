using System;
using Benner.DigitalMicrowave.Core;
using Benner.DigitalMicrowave.Core.Models;
using FluentAssertions;
using Xunit;

namespace Benner.DigitalMicrowave.Tests
{
    public class MicrowavePowerTests
    {

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void New_WhenCreateInValidRange_ShouldNotThrowException(int time)
        {
            Action act = () => new MicrowavePower(time);
            act.Should().NotThrow<Exception>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(11)]
        [InlineData(-3)]
        public void Warm_WhenPowerIsOutOfBound_ThrowsException(int power)
        {
            Action act = () => new MicrowavePower(power);
            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage(Errors.POWER_OUT_OF_RANGE);
        }
    }
}