using Benner.DigitalMicrowave.Core.Models;
using Benner.DigitalMicrowave.Tests.Builders;
using FluentAssertions;
using Xunit;

namespace Benner.DigitalMicrowave.Tests
{
    public class MicrowaveProgramTests
    {

        [Theory]
        [InlineData("Chi")]
        [InlineData(" Chi ")]
        [InlineData(" chi ")]
        [InlineData(" cken ")]
        [InlineData(" chicken ")]
        [InlineData(" ChiCken ")]
        public void IsCompatibleForFood_WhenProgramContainsText_ReturnsTrue(string food)
        {
            var program = MicrowaveProgramBuilder
                .New()
                .Name("ChiCken").Build();

            program.IsCompatibleForFood(food).Should().BeTrue();
        }

        
        [Theory]
        [InlineData("")]
        [InlineData("eggs")]
        [InlineData("chick3n")]
        [InlineData(null)]
        
        public void IsCompatibleForFood_WhenProgramNotContainsWord_ReturnsFalse(string food)
        {
            var program = MicrowaveProgramBuilder
                .New()
                .Name("ChiCken").Build();

            program.IsCompatibleForFood(food).Should().BeFalse();
        }
    }
}