using NUnit.Framework;
using AdProject.Collections;
using System.Collections.Generic;
using AdProject.Repositories;
using Moq;
using AdProject.Entities;
using System.Linq.Expressions;
using AdProject.Services;

namespace AdProject.Tests
{
    [TestFixture]
    public class AdPlatformServiceTest
    {
        private Mock<IAdPlatformRepository> _adPlatformRepository;

        [SetUp]
        public void Setup()
        {
            _adPlatformRepository = new Mock<IAdPlatformRepository>(MockBehavior.Strict);
            _adPlatformRepository.Setup(r => r.AddPlatforms(It.IsAny<IEnumerable<AdPlatform>>())).Returns(Task.CompletedTask);
        }

        [Test]
        public void AdPlatforms_Parse_SimplePath_Correctly()
        {
            // arrange
            var service = new AdPlatformService(_adPlatformRepository.Object);
            var fileContent =
@"Яндекс.Директ:/ru
Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
Крутая реклама:/ru/svrd";

            var expectedPlatforms = new List<AdPlatform>()
            {
                new AdPlatform("Яндекс.Директ", new List<string>(){"/ru" }),
                new AdPlatform("Ревдинский рабочий", new List<string>(){"/ru/svrd/revda", "/ru/svrd/pervik"}),
                new AdPlatform("Газета уральских москвичей", new List<string>{"/ru/msk", "/ru/permobl", "/ru/chelobl"}),
                new AdPlatform("Крутая реклама", new List<string>(){"/ru/svrd"})

            };

            // act assert
            Assert.DoesNotThrowAsync(async () => await service.AddPlatforms(fileContent));
            _adPlatformRepository.Verify(r => r.AddPlatforms(expectedPlatforms), Times.Once);
        }

        [Test]
        public void AdPlatforms_Throws_IfEmpty()
        {
            // arrange
            var service = new AdPlatformService(_adPlatformRepository.Object);
            var fileContent = @"";

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await service.AddPlatforms(fileContent)
            );

            Assert.That(ex.Message, Is.EqualTo("file is empty"));
        }


        [TestCase(
                @"


",
                "file does not contain any platforms",
                TestName = "Throws_IfOnlyEmptyStrings")]
        [TestCase(
                @"Яндекс.Директ::/ru",
                "invalid format of platform or invalid location",
                TestName = "Throws_IfContainMultipleColonsInARow")]
        [TestCase(
                @"Яндекс.Директ:/?ru",
                "invalid format of platform or invalid location",
                TestName = "Throws_IfLocationContainIncorrectSymbols")]
        [TestCase(
                @"Яндекс.Директ:",
                "invalid format of platform or invalid location",
                TestName = "Throws_IfWithoutLocations")]
        public void AddPlatforms_ShouldThrow_OnInvalidInput(string fileContent, string expectedMessage)
        {
            // arrange
            var service = new AdPlatformService(_adPlatformRepository.Object);

            // act & assert
            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await service.AddPlatforms(fileContent)
            );

            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }
    }


}

