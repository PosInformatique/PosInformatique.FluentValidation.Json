//-----------------------------------------------------------------------
// <copyright file="ValidatorConfigurationExtensions.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace FluentValidation
{
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Allows to configure the <see cref="ValidatorConfiguration"/> for the <see cref="ValidatorOptions.Global"/>.
    /// </summary>
    public static class ValidatorConfigurationExtensions
    {
        /// <summary>
        /// Configure FluentValidation to use JSON property name (specified by the <see cref="JsonPropertyNameAttribute"/>) instead of the display and properties name.
        /// </summary>
        /// <param name="configuration"><see cref="ValidatorConfiguration"/> instance to configure.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="configuration"/> argument is null.</exception>
        public static void UseJsonProperties(this ValidatorConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            configuration.DisplayNameResolver = GetJsonPropertyName;
            configuration.PropertyNameResolver = GetJsonPropertyName;
        }

        private static string GetJsonPropertyName(Type type, MemberInfo member, LambdaExpression expression)
        {
            var attribute = member.GetCustomAttribute<JsonPropertyNameAttribute>();

            if (attribute is not null)
            {
                return attribute.Name;
            }

            return member.Name;
        }
    }
}
