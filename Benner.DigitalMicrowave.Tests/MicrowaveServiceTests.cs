using System;
using AutoMoqCore;
using Benner.DigitalMicrowave.Core;
using Benner.DigitalMicrowave.Core.Models;
using Benner.DigitalMicrowave.Core.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Benner.DigitalMicrowave.Tests
{
    public class MicrowaveServiceTests
    {
        private readonly MicrowaveService _service;
        private readonly AutoMoqer _autoMoqer;
        private readonly Mock<IMicrowaveRepository> _repository;

        public MicrowaveServiceTests()
        {
            _autoMoqer = new AutoMoqer();
            _service = _autoMoqer.Create<MicrowaveService>();
            _repository = _autoMoqer.GetMock<IMicrowaveRepository>();
        }


        [Fact]
        public void Cancel_WhenCalled_ShouldCancelMicrowaveTimer()
        {
            var options = MicrowaveOptionsBuilder.New().Build();
            _repository.Setup(x => x.GetCurrent()).Returns(new Microwave(options));

            _service.Cancel();

            _repository.Verify(x => x.ClearCurrent(), Times.Once);
        }

        [Fact]
        public void Cancel_WhenCurrentMicrowaveDoesNotExists_ThrowsException()
        {
            Action act = () => _service.Cancel();
            act.Should().Throw<InvalidOperationException>()
                .WithMessage(Errors.MICROWAVE_OFF);
        }
    }
}