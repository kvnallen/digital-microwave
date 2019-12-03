using System;
using AutoMoqCore;
using Benner.DigitalMicrowave.Core;
using Benner.DigitalMicrowave.Core.Commands;
using Benner.DigitalMicrowave.Core.Models;
using Benner.DigitalMicrowave.Core.Services;
using Benner.DigitalMicrowave.Tests.Builders;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Xunit;

namespace Benner.DigitalMicrowave.Tests
{
    public class ProgramServiceTests
    {
        private ProgramService _service;
        private Mock<IProgramRepository> _repository;
        private CreateProgramCommand _command;

        public ProgramServiceTests()
        {
            var autoMoqer = new AutoMoqer();
            _service = autoMoqer.Create<ProgramService>();
            _repository = autoMoqer.GetMock<IProgramRepository>();
            _command = new CreateProgramCommand
            {
                Time = 1,
                HeatingCharacter = "-",
                Instructions = "...",
                Power = 1,
                Name = "New"
            };

        }

        [Fact]
        public void Add_WhenValid_CreatesNewProgram()
        {
            _service.Add(_command);

            _repository.Verify(x => x.Add(It.IsAny<MicrowaveProgram>()), Times.Once);
        }

        [Fact]
        public void Add_WhenExistsAnotherProgramWithSameName_ThrowsException()
        {
           

            _repository
                .Setup(x => x.GetByName(It.IsAny<string>()))
                .Returns(MicrowaveProgramBuilder.New().Build());

            Action act = () => _service.Add(_command);
            
            act.Should()
                .ThrowExactly<InvalidOperationException>()
                .WithMessage(Errors.PROGRAM_WITH_SAME_NAME);
        }
    }
}