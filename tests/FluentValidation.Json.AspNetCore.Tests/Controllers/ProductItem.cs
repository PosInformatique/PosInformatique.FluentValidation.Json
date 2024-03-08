//-----------------------------------------------------------------------
// <copyright file="ProductItem.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.FluentValidation.Json.AspNetCore.Tests
{
    using System.Text.Json.Serialization;

    public class ProductItem
    {
        public ProductItem()
        {
        }

        [JsonPropertyName("color")]
        public string? Color { get; set; }
    }
}
