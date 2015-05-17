using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoFixtureDemo;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using Xunit;

namespace Tests
{
    public class EfSampleTests
    {
        [Fact]
        public void CanGetEmployeeById()
        {
            // Arrange
            //----------
            var fixture = new Fixture();
            var fakeEmployee = fixture.Create<Employees>();

            var dbSet = new Mock<DbSet<Employees>>();            
            dbSet.Setup(x => x.Find(It.IsAny<object>())).Returns(fakeEmployee);
            
            var mock = new Mock<NORTHWNDEntities>();
            mock.Setup(x => x.Employees).Returns(dbSet.Object);
            
            var sut = new EfSample(mock.Object);
            
            // Act
            //----------
            var result = sut.GetEmployeeById(fixture.Create<int>());

            // Assert
            //----------
            result.Should().NotBeNull();
            result.Should().BeOfType<Employees>();
        }

        [Fact]
        public void CanGetAllEmployees()
        {
            // Arrange
            //-------------------------------------------------------------------
            // Get list of 100 fake employees..
            var fixture = new Fixture();
            var fakeEmployees = fixture.CreateMany<Employees>(100).ToList();

            // Create mock DbSet..
            // Always returns 'fakeEmployees' object..
            var employees = new Mock<DbSet<Employees>>();
            employees.SetupData(fakeEmployees);
            
            // Create mock DbContext..
            // Calling 'Employees' returns our mocked DbSet..
            var mock = new Mock<NORTHWNDEntities>();
            mock.Setup(x => x.Employees).Returns(employees.Object);

            // Instantiate SampleClass,
            // passing in mocked DbContext..
            var sut = new EfSample(mock.Object);
            
            // Act
            //-------------------------------------------------------------------
            // Call 'GetAllEmployees' method.
            var result = sut.GetAllEmployees();

            // Assert
            //-------------------------------------------------------------------
            // Expect result to not be null, count to equal 100, to be instance of type 'Employees'..
            result.Should().NotBeNull();
            result.Count().Should().Be(100);
            result.Should().BeOfType<List<Employees>>();
        }

        [Fact]
        public void ModelBuilderTest()
        {
            var result = false;
            var fixture = new Fixture();
            var modelBuilder = fixture.Create<DbModelBuilder>();

            try
            {
                var response = new TestDb(modelBuilder);
            }
            catch (UnintentionalCodeFirstException)
            {
                result = true;
            }

            result.Should().BeTrue();
        }
    }

    public class TestDb : NORTHWNDEntities
    {
        public TestDb(DbModelBuilder modelBuilder)
        {
            OnModelCreating(modelBuilder);
        }
    }
}
