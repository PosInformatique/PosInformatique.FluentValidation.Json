//-----------------------------------------------------------------------
// <copyright file="ProductValidator.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.FluentValidation.Json.AspNetCore.Tests
{
    using global::FluentValidation;

    public class ProductValidator : AbstractValidator<Product>
    {
        private readonly IValidator<ProductCategory> productCategoryValidator;

        public ProductValidator(IValidator<ProductCategory> productCategoryValidator)
        {
            this.productCategoryValidator = productCategoryValidator;

            this.RuleLevelCascadeMode = CascadeMode.Stop;

            this.RuleFor(p => p.Description).NotNull().NotEmpty();
            this.RuleFor(p => p.Price).GreaterThan(0);
            this.RuleFor(p => p.Category).NotNull().SetValidator(this.productCategoryValidator!);

            this.RuleForEach(p => p.Items).NotNull().ChildRules(item =>
            {
                item.RuleFor(i => i.Color).NotNull().NotEmpty();
            });
        }
    }
}
