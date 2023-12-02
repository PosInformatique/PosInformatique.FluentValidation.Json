//-----------------------------------------------------------------------
// <copyright file="ReflectionHelper.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.FluentValidation.Json
{
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text.Json.Serialization;

    internal static class ReflectionHelper
    {
        public static string GetJsonPropertyPath(LambdaExpression expression)
        {
            var body = expression.Body;

            if (body is UnaryExpression unaryExpression)
            {
                body = unaryExpression.Operand;
            }

            if (body is not MemberExpression memberExpression)
            {
                throw new ArgumentException("The argument is not expression to access to a property.", nameof(expression));
            }

            var path = new List<string>();

            while (true)
            {
                if (memberExpression.Member is not PropertyInfo property)
                {
                    throw new ArgumentException("The argument is not expression to access to a property.", nameof(expression));
                }

                path.Add(GetJsonPropertyName(property));

                if (memberExpression.Expression is ParameterExpression)
                {
                    break;
                }

                var nextMemberExpression = memberExpression.Expression as MemberExpression;

                if (nextMemberExpression is null)
                {
                    throw new ArgumentException("The argument is not expression to access to a property.", nameof(expression));
                }

                memberExpression = nextMemberExpression;
            }

            path.Reverse();

            return string.Join('.', path);
        }

        private static string GetJsonPropertyName(PropertyInfo property)
        {
            var attribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();

            if (attribute is not null)
            {
                return attribute.Name;
            }

            return property.Name;
        }
    }
}
