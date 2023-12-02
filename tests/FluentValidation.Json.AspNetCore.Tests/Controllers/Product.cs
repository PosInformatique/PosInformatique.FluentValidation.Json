//-----------------------------------------------------------------------
// <copyright file="Product.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.FluentValidation.Json.AspNetCore.Tests
{
    using System.Text.Json.Serialization;

    public class Product
    {
        public Product()
        {
        }

        [JsonPropertyName("category")]
        public ProductCategory? Category { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }
}
