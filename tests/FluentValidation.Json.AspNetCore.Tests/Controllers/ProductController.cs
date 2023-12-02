//-----------------------------------------------------------------------
// <copyright file="ProductController.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.FluentValidation.Json.AspNetCore.Tests
{
    using global::FluentValidation;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IValidator<Product> validator;

        public ProductController(IValidator<Product> validator)
        {
            this.validator = validator;
        }

        [HttpPost]
        public IResult Post(Product product)
        {
            var result = this.validator.Validate(product);

            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.ToDictionary());
            }

            return Results.Ok();
        }
    }
}
