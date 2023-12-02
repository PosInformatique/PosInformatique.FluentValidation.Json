//-----------------------------------------------------------------------
// <copyright file="ProductCategory.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.FluentValidation.Json.AspNetCore.Tests
{
    using System.Text.Json.Serialization;

    public class ProductCategory
    {
        public ProductCategory()
        {
        }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
