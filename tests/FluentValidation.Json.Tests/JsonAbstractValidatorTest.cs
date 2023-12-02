//-----------------------------------------------------------------------
// <copyright file="JsonAbstractValidatorTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.FluentValidation.Json.Tests
{
    using System.Text.Json.Serialization;
    using global::FluentValidation;

    public class JsonAbstractValidatorTest
    {
        [Fact]
        public void Validate()
        {
            var obj = new JsonObjectToValidate();

            var validator = new JsonObjectValidator();

            var result = validator.Validate(obj);

            result.Errors.Should().HaveCount(6);

            result.Errors[0].ErrorMessage.Should().Be("'inner_explicit.ImplicitProperty' must not be empty.");
            result.Errors[0].PropertyName.Should().Be("inner_explicit.ImplicitProperty");

            result.Errors[1].ErrorMessage.Should().Be("'inner_explicit.explicit_property' must not be empty.");
            result.Errors[1].PropertyName.Should().Be("inner_explicit.explicit_property");

            result.Errors[2].ErrorMessage.Should().Be("'InnerImplicit.ImplicitProperty' must not be empty.");
            result.Errors[2].PropertyName.Should().Be("InnerImplicit.ImplicitProperty");

            result.Errors[3].ErrorMessage.Should().Be("'InnerImplicit.explicit_property' must not be empty.");
            result.Errors[3].PropertyName.Should().Be("InnerImplicit.explicit_property");

            result.Errors[4].ErrorMessage.Should().Be("'FirstName' must not be empty.");
            result.Errors[4].PropertyName.Should().Be("FirstName");

            result.Errors[5].ErrorMessage.Should().Be("'lastName' must not be empty.");
            result.Errors[5].PropertyName.Should().Be("lastName");
        }

        private class JsonObjectValidator : JsonAbstractValidator<JsonObjectToValidate>
        {
            public JsonObjectValidator()
            {
                this.RuleFor(o => o.InnerExplicit.ImplicitProperty).NotEmpty();
                this.RuleFor(o => o.InnerExplicit.ExplicitProperty).NotEmpty();
                this.RuleFor(o => o.InnerImplicit.ImplicitProperty).NotEmpty();
                this.RuleFor(o => o.InnerImplicit.ExplicitProperty).NotEmpty();
                this.RuleFor(o => o.FirstName).NotEmpty();
                this.RuleFor(o => o.LastName).NotEmpty();
            }
        }

        private class JsonObjectToValidate
        {
            public string FirstName { get; set; }

            [JsonPropertyName("lastName")]
            public string LastName { get; set; }

            [JsonPropertyName("inner_explicit")]
            public InnerJsonObject InnerExplicit => new InnerJsonObject();

            public InnerJsonObject InnerImplicit => new InnerJsonObject();
        }

        private class InnerJsonObject
        {
            public string ImplicitProperty { get; set; }

            [JsonPropertyName("explicit_property")]
            public string ExplicitProperty { get; set; }
        }
    }
}