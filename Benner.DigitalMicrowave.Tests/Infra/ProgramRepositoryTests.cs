using Benner.DigitalMicrowave.Infra.Repositories;
using FluentAssertions;
using Xunit;

namespace Benner.DigitalMicrowave.Tests.Infra
{
    public class ProgramRepositoryTests
    {
        private ProgramRepository _repository = new ProgramRepository();

        [Theory]
        [InlineData("ovo")]
        [InlineData("OvO")]
        [InlineData("Ovo")]
        [InlineData("Ovo ")]
        [InlineData(" Ovo")]
        [InlineData(" Ovo ")]
        public void GetByName_WhenSearchByPartOfWord_ReturnItem(string word)
        {
            _repository.GetByName(word).Should().NotBeNull();
        }

        [Fact]
        public void GetByName_WhenNotExists_ReturnsNull()
        {
            _repository.GetByName("not_exists").Should().BeNull();
        }
    }
}