//-----------------------------------------------------------------------
// <copyright file="ReflectionHelperTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.FluentValidation.Json.Tests
{
    using System.Linq.Expressions;
    using System.Text.Json.Serialization;

    public class ReflectionHelperTest
    {
        [Fact]
        public void GetJsonPropertyPath()
        {
            CallGetJsonPropertyPath<JsonRootClass>(c => c.Inner1.Property1).Should().Be("inner_explicit_name.property_1");
            CallGetJsonPropertyPath<JsonRootClass>(c => c.Inner1.Property2).Should().Be("inner_explicit_name.Property2");
            CallGetJsonPropertyPath<JsonRootClass>(c => c.Inner2.Property1).Should().Be("Inner2.property_1");
            CallGetJsonPropertyPath<JsonRootClass>(c => c.Inner2.Property2).Should().Be("Inner2.Property2");
            CallGetJsonPropertyPath<JsonRootClass>(c => c.Inner2.Property3).Should().Be("Inner2.Property3");
        }

        [Fact]
        public void GetJsonPropertyPath_WithFieldMember()
        {
            var act = () =>
            {
                CallGetJsonPropertyPath<JsonRootClass>(c => c.Inner1.Field);
            };

            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage("The argument is not expression to access to a property. (Parameter 'expression')")
                .And.ParamName.Should().Be("expression");
        }

        [Fact]
        public void GetJsonPropertyPath_NotMemberAccess()
        {
            var act = () =>
            {
                CallGetJsonPropertyPath<JsonRootClass>(c => c.ToString());
            };

            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage("The argument is not expression to access to a property. (Parameter 'expression')")
                .And.ParamName.Should().Be("expression");
        }

        [Fact]
        public void GetJsonPropertyPath_NotMemberAccessBefore()
        {
            var act = () =>
            {
                CallGetJsonPropertyPath<JsonRootClass>(c => c.Get().Inner1);
            };

            act.Should().ThrowExactly<ArgumentException>()
                .WithMessage("The argument is not expression to access to a property. (Parameter 'expression')")
                .And.ParamName.Should().Be("expression");
        }

        private static string CallGetJsonPropertyPath<T>(Expression<Func<T, object>> expression)
        {
            return ReflectionHelper.GetJsonPropertyPath(expression);
        }

        private class JsonRootClass
        {
            [JsonPropertyName("property_explicit_name")]
            public string PropertyExplicitName { get; set; }

            public string PropertyImplicitName { get; set; }

            [JsonPropertyName("inner_explicit_name")]
            public InnerClass Inner1 { get; set; }

            public InnerClass Inner2 { get; set; }

            public JsonRootClass Get()
            {
                throw new NotImplementedException();
            }
        }

        private class InnerClass
        {
#pragma warning disable SA1401 // Fields should be private
            public int Field = 10;
#pragma warning restore SA1401 // Fields should be private

            [JsonPropertyName("property_1")]
            public string Property1 { get; set; }

            public string Property2 { get; set; }

            public int Property3 { get; set; }
        }
    }
}