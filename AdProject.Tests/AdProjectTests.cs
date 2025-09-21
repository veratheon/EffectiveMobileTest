using NUnit.Framework;
using AdProject.Collections;
using System.Collections.Generic;

namespace AdProject.Tests
{
    [TestFixture]
    public class TreeTests
    {
        private Tree<string> _tree;

        [SetUp]
        public void Setup()
        {
            _tree = new Tree<string>();
        }

        [Test]
        public void Add_And_Find_SimplePath_ReturnsPlatform()
        {
            // arrange
            _tree.Add("/ru", "Яндекс.Директ");

            // act
            var result = _tree.Find("/ru");

            // assert
            Assert.That(result, Is.EqualTo(new List<string> { "Яндекс.Директ" }));
        }

        [Test]
        public void Add_And_Find_NestedPath_ReturnsCorrectPlatforms()
        {
            // arrange
            _tree.Add("/ru/svrd/revda", "Ревдинский рабочий");
            _tree.Add("/ru", "Яндекс.Директ");

            // act
            var result = _tree.Find("/ru/svrd/revda");

            // assert
            Assert.That(result, Does.Contain("Ревдинский рабочий"));
            Assert.That(result, Does.Contain("Яндекс.Директ"));
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public void Find_UnknownPath_ReturnsEmptyList()
        {
            // arrange
            _tree.Add("/ru", "Яндекс.Директ");

            // act
            var result = _tree.Find("/us");

            // assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Clear_RemovesAllPlatforms()
        {
            // arrange
            _tree.Add("/ru", "Яндекс.Директ");

            // act
            _tree.Clear();
            var result = _tree.Find("/ru");

            // assert
            Assert.That(result, Is.Empty);
        }
    }
}
