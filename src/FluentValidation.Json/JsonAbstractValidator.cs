//-----------------------------------------------------------------------
// <copyright file="JsonAbstractValidator.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.FluentValidation.Json
{
    using System.Text.Json.Serialization;
    using global::FluentValidation;

    /// <summary>
    /// Base class for object validators which will use the JSON property name (based on <see cref="JsonPropertyNameAttribute"/>)
    /// for the errors messages.
    /// </summary>
    /// <typeparam name="T">The type of the object being validated.</typeparam>
    public abstract class JsonAbstractValidator<T> : AbstractValidator<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonAbstractValidator{T}"/> class.
        /// </summary>
        protected JsonAbstractValidator()
        {
        }

        /// <inheritdoc />
        protected override void OnRuleAdded(IValidationRule<T> rule)
        {
            var jsonPropertyPath = ReflectionHelper.GetJsonPropertyPath(rule.Expression);

            rule.PropertyName = jsonPropertyPath;
            SetDisplayName(rule, jsonPropertyPath);

            base.OnRuleAdded(rule);
        }

        private static void SetDisplayName(IValidationRule<T> rule, string name)
        {
            // The SetDisplayName() method is not available in the IValidationRule<T>.
            // A feature request has been submitted on the GitHub of FluentValidation to avoid using reflection:
            // https://github.com/FluentValidation/FluentValidation/issues/2179
            var setDisplayNameMethod = rule.GetType().GetMethod("SetDisplayName", [typeof(string)])!;

            setDisplayNameMethod.Invoke(rule, [name]);
        }
    }
}
