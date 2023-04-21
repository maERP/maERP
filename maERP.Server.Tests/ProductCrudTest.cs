using System.Net;
using System.Net.Http.Json;
using maERP.Shared.Dtos;
using maERP.Shared.Dtos.Product;
using maERP.Shared.Models;
using Xunit;
using Xunit.Abstractions;
using Xunit.Priority;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Assert = NUnit.Framework.Assert;

#pragma warning disable CS8602
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace maERP.Server.Tests;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class ProductCrudTest : IClassFixture<maERPWebApplicationFactory<Program>>
{
    private readonly maERPWebApplicationFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    public ProductCrudTest(maERPWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
    }

    [Theory]
    [InlineData("/api/Product")]
    public async Task ProductCreate(string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.InitializeDbForTests();
        var createCompany = new ProductCreateDto
        {
            Name = "Testprodukt 1 created"
        };

        // Act
        var result = await client.PostAsJsonAsync(url, createCompany);
        var resultContent = await result.Content.ReadFromJsonAsync<ProductDetailDto>();

        // Assert
        Assert.IsTrue(result.IsSuccessStatusCode);
        Assert.IsNotNull(resultContent);
        Assert.IsNotNull(resultContent);
        Assert.IsTrue(resultContent.Id != default);
    }

    [Theory]
    [InlineData("/api/Product/GetAll")]
    public async Task ProductGetAll(string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.InitializeDbForTests(new List<Product> { new() { Id = 1, Name = "Testprodukt 1" } });

        // Act
        var result = await client.GetFromJsonAsync<List<ProductListDto>>(url);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotEmpty(result);
    }

    [Theory]
    [InlineData("/api/Product/1")]
    public async Task ProductDetail(string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.InitializeDbForTests(new List<Product> { new() { Id = 1, Name = "Testprodukt 1" } });

        // Act
        var result = await client.GetFromJsonAsync<ProductDetailDto>(url);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.Name);
    }

    [Theory]
    [InlineData("/api/Product/999999")]
    public async Task ProductNotExist(string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.InitializeDbForTests();

        // Act
        var result = await client.GetAsync(url);

        // Assert
        Assert.IsTrue(result.StatusCode == HttpStatusCode.NotFound);
    }

    [Theory]
    [InlineData("/api/Product/1")]
    public async Task ProductUpdate(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        await _factory.InitializeDbForTests(new List<Product> { new() { Id = 1, Name = "Testprodukt 1" } });

        var updateProduct = new ProductUpdateDto
        {
            Name = "Testprodukt 1 updated",
        };

        // Act
        var result = await client.PutAsJsonAsync(url, updateProduct);
        var resultContent = await result.Content.ReadFromJsonAsync<ProductDetailDto>();

        // Assert
        Assert.IsTrue(result.IsSuccessStatusCode);
        Assert.IsNotNull(resultContent);
        Assert.IsTrue(resultContent.Name == updateProduct.Name);
    }

    [Theory]
    [InlineData("/api/Product/1")]
    public async Task ProductDelete(string url)
    {
        // Arrange
        var client = _factory.CreateClient();
        await _factory.InitializeDbForTests(new List<Product> { new() { Id = 1, Name = "Testprodukt 1" } });

        // Act
        var result = await client.DeleteAsync(url);

        // Assert
        Assert.IsTrue(result.IsSuccessStatusCode);
    }
}