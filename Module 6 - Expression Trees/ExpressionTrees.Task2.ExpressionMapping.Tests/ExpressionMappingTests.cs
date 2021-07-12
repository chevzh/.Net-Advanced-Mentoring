using ExpressionTrees.Task2.ExpressionMapping.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionTrees.Task2.ExpressionMapping.Tests
{
    [TestClass]
    public class ExpressionMappingTests
    {
        MappingGenerator mapGenerator;
        Mapper<Foo, Bar> mapper;

        [TestInitialize]
        public void Init()
        {
            mapGenerator = new MappingGenerator();
            mapper = mapGenerator.Generate<Foo, Bar>();
        }

        [TestMethod]
        public void MappingGenerator_MapsEmptyValues()
        {
            // Act
            Bar actual = mapper.Map(new Foo());

            // Assert
            Assert.AreEqual(new Bar().A, actual.A);
            Assert.AreEqual(new Bar().B, actual.B);
        }

        [TestMethod]
        public void MappingGenerator_MapsIntegerValue()
        {
            // Arrange
            Foo foo = new Foo() { A = 1 };

            // Act
            Bar actual = mapper.Map(foo);

            // Assert
            Assert.AreEqual(1, actual.A);
        }

        [TestMethod]
        public void MappingGenerator_MapsStringValues()
        {
            // Arrange
            Foo foo = new Foo() { B = "B" };

            // Act
            Bar actual = mapper.Map(foo);

            // Assert
            Assert.AreEqual("B", actual.B);
        }

        [TestMethod]
        public void MappingGenerator_MapsObjectValues()
        {
            // Arrange
            object expected = new object();
            Foo foo = new Foo() { C = expected };

            // Act
            Bar actual = mapper.Map(foo);

            // Assert
            Assert.AreEqual(expected, actual.C);
        }

        [TestMethod]
        public void MappingGenerator_NotMapsReadonlyProperties()
        {
            // Arrange
            Foo foo = new Foo();

            // Act
            Bar actual = mapper.Map(foo);

            // Assert
            Assert.AreEqual(true, foo.D, "D property has value of 'true' in Foo class.");
            Assert.AreEqual(new Bar().D, actual.D, "D property has value of 'false' in mapped Bar class.");
        }
    }
}
