//-----------------------------------------------------------------------
// <copyright file="ValidatorConfigurationExtensionsTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace FluentValidation.Tests
{
    using System.Text.Json.Serialization;

    public class ValidatorConfigurationExtensionsTest
    {
        [Fact]
        public void UseJsonProperties()
        {
            var configuration = new ValidatorConfiguration();

            configuration.UseJsonProperties();

            configuration.DisplayNameResolver(null, typeof(JsonObjectToValidate).GetProperty("FirstName"), null).Should().Be("FirstName");
            configuration.DisplayNameResolver(null, typeof(JsonObjectToValidate).GetProperty("LastName"), null).Should().Be("lastName");

            configuration.PropertyNameResolver(null, typeof(JsonObjectToValidate).GetProperty("FirstName"), null).Should().Be("FirstName");
            configuration.PropertyNameResolver(null, typeof(JsonObjectToValidate).GetProperty("LastName"), null).Should().Be("lastName");
        }

        [Fact]
        public void UseJsonProperties_WithConfigurationNull()
        {
            var act = () =>
            {
                ValidatorConfigurationExtensions.UseJsonProperties(null);
            };

            act.Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("configuration");
        }

        private class JsonObjectToValidate
        {
            public string FirstName { get; set; }

            [JsonPropertyName("lastName")]
            public string LastName { get; set; }
        }
    }
}