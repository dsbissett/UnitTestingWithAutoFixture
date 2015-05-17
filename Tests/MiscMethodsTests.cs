using System;
using System.Net.Mail;
using AutoFixtureDemo;
using FluentAssertions;
using Ploeh.AutoFixture;
using Xunit;

namespace Tests
{
    public class MiscMethodsTests
    {
        [Fact]
        public void ValidEmailAddressReturnsTrue()
        {
            // Arrange
            //-----------
            var fixture = new Fixture();

            var validEmailAddress = fixture.Create<MailAddress>().Address;

            var module = new MiscMethods();

            // Act
            //-----------
            var result = module.IsEmailAddressValid(validEmailAddress);

            // Assert
            //-----------
            result.Should().BeTrue();
        }

        [Fact]
        public void InvalidEmailAddressReturnsFalse()
        {
            // Arrange
            //-----------
            var fixture = new Fixture();

            var invalidEmailAddress = fixture.Create<string>();

            var module = new MiscMethods();

            // Act
            //-----------
            var result = module.IsEmailAddressValid(invalidEmailAddress);

            // Assert
            //-----------
            result.Should().BeFalse();
        }

        [Fact]
        public void MethodThrowsWhenPassedNullValue()
        {
            // Arrange
            //-----------
            var result = false;
            
            var module = new MiscMethods();

            // Act
            //-----------
            try
            {
                var response = module.IsEmailAddressValid(null);
            }
            catch (ArgumentNullException)
            {
                result = true;
            }
            
            // Assert
            //-----------
            result.Should().BeTrue();
        }
    }
}
