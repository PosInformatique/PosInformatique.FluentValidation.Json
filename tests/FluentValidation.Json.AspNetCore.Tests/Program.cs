//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.FluentValidation.Json.AspNetCore.Tests
{
    using global::FluentValidation;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddTransient<IValidator<Product>, ProductValidator>();
            builder.Services.AddTransient<IValidator<ProductCategory>, ProductCategoryValidator>();

            ValidatorOptions.Global.UseJsonProperties();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
