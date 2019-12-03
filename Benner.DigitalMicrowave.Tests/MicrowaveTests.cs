using Benner.DigitalMicrowave.Core.Events;
using Benner.DigitalMicrowave.Core.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Benner.DigitalMicrowave.Tests
{
    public class MicrowaveTests
    {
        private readonly Mock<IMicrowaveNotifier> _notifier;

        public MicrowaveTests()
        {
            _notifier = new Mock<IMicrowaveNotifier>();
        }

        [Theory]
        [InlineData(3, 2, "Chicken......")]
        [InlineData(1, 1, "Chicken.")]
        [InlineData(1, 3, "Chicken...")]
        [InlineData(2, 1, "Chicken..")]
        public async void Warm_WhenHeated_ReturnsHeatedValue(int time, int power, string expected)
        {
            var options = MicrowaveOptionsBuilder
                .New()
                .ForLong(time)
                .WithPower(power)
                .Build();

            var microwave = new Microwave(options);

            var result = await microwave.Warm(new[] { _notifier.Object });

            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(3, 2, 2, "Chicken......")]
        [InlineData(1, 1, 1, "Chicken.")]
        public async void Warm_WhenExistsCurrentTime_ShouldReturnValidTimer(int time, int power, int currentTime, string expected)
        {
            var options = MicrowaveOptionsBuilder
                .New()
                .ForLong(time)
                .WithPower(power)
                .CurrentTime(currentTime)
                .Build();

            var microwave = new Microwave(options);

            var result = await microwave.Warm(new[] { _notifier.Object });

            result.Should().Be(expected);
        }

        [Fact]
        public void Cancel_WhenCalled_ShouldMarkMicrowaveAsCancelled()
        {
            var options = MicrowaveOptionsBuilder.New().Build();
            var microwave = new Microwave(options);
            microwave.Cancel();
            microwave.State.Should().Be(MicrowaveState.Cancelled);
        }
    }
}
