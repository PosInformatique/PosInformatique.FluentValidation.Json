//-----------------------------------------------------------------------
// <copyright file="ProductCategoryValidator.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.FluentValidation.Json.AspNetCore.Tests
{
    using global::FluentValidation;

    public class ProductCategoryValidator : JsonAbstractValidator<ProductCategory>
    {
        public ProductCategoryValidator()
        {
            this.RuleFor(p => p.Name).NotEmpty();
        }
    }
}
