# PosInformatique.FluentValidation.Json
[PosInformatique.FluentValidation.Json](https://www.nuget.org/packages/PosInformatique.FluentValidation.Json/)
is a library based on FluentValidation to validate JSON objects for the Web API.

By default, when using the [FluentValidation](https://www.nuget.org/packages/FluentValidation)
library to validate an object, the property name (or related display name) are used in the error message.
This can be useful for functional validation to display to users on the views of the application.

But when you perform some validations in a Web API context, on JSON DTO objects,
using C# property name does not help developers to indicate which properties are invalid.
Specially if the C# property name is differents of the JSON property name associated.

For example, imagine you have the following JSON object that represents a product:

```json
{
    "description": "Chicken adobo",
    "price": 10
}
```

This JSON object is mapped to the following C# class, using `[JsonPropertyName]` attributes
to define the JSON property names.

```csharp
public class Product
{
    public Product()
    {
    }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}
```

If you want to validate the C# `Product` class, you have to create a validator
which inherit from the `AbstractValidator<T>` class.

```csharp
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        this.RuleFor(p => p.Description).NotNull().NotEmpty();
        this.RuleFor(p => p.Price).GreaterThan(0);
        this.RuleFor(p => p.Category).NotNull().SetValidator(new ProductCategoryValidator());
    }
}

public class ProductCategoryValidator : AbstractValidator<ProductCategory>
{
    public ProductCategoryValidator()
    {
        this.RuleFor(p => p.Name).NotEmpty();
    }
}
```

When performing the validation of inside a ASP .NET MVC API application
the following JSON problem is returned by default:

```json
{
  "value": {
    "title": "One or more validation errors occurred.",
    "errors": {
      "Description": [
        "'Description' must not be empty."
      ],
      "Price": [
        "'Price' must be greater than '0'."
      ],
      "Category.Name": [
        "'Name' must not be empty."
      ]
    }
  },
  "statusCode": 400,
  "contentType": "application/problem+json"
}
```

Here, because we expose this JSON content to developers, we prefered to
have the JSON property name path in the errors messages.

This the main goal of this library to return the following JSON result instead:

```json
{
  "value": {
    "title": "One or more validation errors occurred.",
    "errors": {
      "description": [
        "'description' must not be empty."
      ],
      "price": [
        "'price' must be greater than '0'."
      ],
      "category.name": [
        "'name' must not be empty."
      ]
    }
  },
  "statusCode": 400,
  "contentType": "application/problem+json"
}
```

## Installing from NuGet
The [PosInformatique.FluentValidation.Json](https://www.nuget.org/packages/PosInformatique.FluentValidation.Json/)
library is available directly on the
[![Nuget](https://img.shields.io/nuget/v/PosInformatique.FluentValidation.Json)](https://www.nuget.org/packages/PosInformatique.FluentValidation.Json/)
official website.

To download and install the library to your Visual Studio unit test projects use the following NuGet command line 

```
Install-Package PosInformatique.FluentValidation.Json
```

## How it is work?

This library is really easy to use and do not need lot of changes if you already implemented
`AbstractValidator<T>` validators.

To use JSON property names when validating a DTO class, just inherit from the
`JsonAbstractValidator<T>` instead of `AbstractValidator<T>`.

For example, to validate the `Product` or `ProductCategory` classes of the previous example,
use the following `JsonAbstractValidator<T>` implementations:

```csharp
public class ProductValidator : JsonAbstractValidator<Product>
{
    public ProductValidator()
    {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        this.RuleFor(p => p.Description).NotNull().NotEmpty();
        this.RuleFor(p => p.Price).GreaterThan(0);
        this.RuleFor(p => p.Category).NotNull().SetValidator(new ProductCategoryValidator());
    }
}

public class ProductCategoryValidator : JsonAbstractValidator<ProductCategory>
{
    public ProductCategoryValidator()
    {
        this.RuleFor(p => p.Name).NotEmpty();
    }
}
```

And **THAT ALL !!**.

Next, you use your own validation strategy depending of the context usage.
For example, if you ASP .NET Core to create an Web API, you can use the following code
and returns an error as JSON problem format:

```json
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
```

Do not hesitate to read the
[FluentValidation ASP .NET Integration](https://docs.fluentvalidation.net/en/latest/aspnet.html)
documentation for more information.

## JSON serialization library
This library use the JSON property names specified by the `[JsonPropertyName]` attributes
with the Microsoft `System.Text.Json`.

This library **DO NOT** use the property names specified by the `[JsonProperty]` attributes
of the `Newtonsoft.Json` library.

## Library dependencies
- The [PosInformatique.FluentValidation.Json](https://www.nuget.org/packages/PosInformatique.FluentValidation.Json/) library
target the .NET Standard 2.0 and can be used with various of .NET architecture (.NET Core, .NET Framework,...).

- The [PosInformatique.FluentValidation.Json](https://www.nuget.org/packages/PosInformatique.FluentValidation.Json/) library
use the 4.6.0 version of the [System.Text.Json](https://www.nuget.org/packages/System.Text.Json/) NuGet package
and can be used with old projects that target this library version and earlier.