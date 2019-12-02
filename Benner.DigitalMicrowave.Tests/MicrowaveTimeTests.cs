using System;
using Benner.DigitalMicrowave.Core;
using Benner.DigitalMicrowave.Core.Models;
using FluentAssertions;
using Xunit;

namespace Benner.DigitalMicrowave.Tests
{
    public class MicrowaveTimeTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(120)]
        public void New_WhenCreateInValidRange_ShouldNotThrowException(int time)
        {
            Action act = () => new MicrowaveTime(time);
            act.Should().NotThrow<Exception>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-4)]
        [InlineData(2000)]
        [InlineData(2001)]
        public void New_WhenTimeIsOutOfBound_ThrowsException(int time)
        {
            Action act = () => new MicrowaveTime(time);;
            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage(Errors.TIME_OUT_OF_RANGE);
        }
    }
}