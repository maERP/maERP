using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.Customer.Commands.CustomerCreate;
using maERP.Application.Features.Customer.Commands.CustomerUpdate;
using maERP.Domain.Dtos.Customer;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class CustomerCrudTest : IClassFixture<MaErpWebApplicationFactory<Program>>
{
    private readonly MaErpWebApplicationFactory<Program> _webApplicationFactory;

    public CustomerCrudTest(MaErpWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/Customers")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var customer = new CustomerCreateCommand()
        {
            Firstname = "Customer Firstname",
            Lastname = "Customer Lastname",
        };

        HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, customer);
        var result = await response.Content.ReadFromJsonAsync<Result<int>>();

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Created, result.StatusCode);
        Assert.IsType<int>(result.Data);
    }

    [Theory]
    [InlineData("/api/v1/Customers/")]
    public async Task GetAll(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Customer> {
                new() {
                    Id = 1,
                    Firstname = "Customer 2 Firstname",
                    Lastname = "Customer 2 Lastname"
                }
        });

        var result = await httpClient.GetFromJsonAsync<PaginatedResult<CustomerListDto>>(url);

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.Equal(1, result.TotalCount);
        Assert.NotNull(result.Data);
    }

    [Theory]
    [InlineData("/api/v1/Customers/3")]
    public async Task GetDetail(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Customer> {
                new() {
                    Id = 3,
                    Firstname = "Customer 3 Firstname",
                    Lastname = "Customer 3 Lastname"
                }
        });

        var response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<Result<CustomerDetailDto>>();

        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.Firstname.Length > 0);
    }

    [Theory]
    [InlineData("/api/v1/Customers/4")]
    public async Task Update(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests(
            new List<Customer> {
                new() {
                    Id = 4,
                    Firstname = "Customer 4 Firstname",
                    Lastname = "Customer 4 Lastname"
                }
        });

        var customer = new CustomerUpdateCommand
        {
            Firstname = "Customer 4 Firstname updated",
            Lastname = "Customer 4 Lastname updated",
        };

        var httpResponseMessage = await httpClient.PutAsJsonAsync(url, customer);
        var result = await httpResponseMessage.Content.ReadFromJsonAsync<Result<int>>();

        Assert.Equal(HttpStatusCode.OK, httpResponseMessage.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.IsType<int>(result.Data);
    }

    [Theory]
    [InlineData("/api/v1/Customers/5")]
    public async Task Delete(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests(
            new List<Customer> {
                new() {
                    Id = 5,
                    Firstname = "Customer 5 Firstname",
                    Lastname = "Customer 5 Lastname"
                }
        });

        var response = await httpClient.DeleteAsync(url);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Theory]
    [InlineData("/api/v1/Customers/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage response = await httpClient.GetAsync(url);

        // We should still get OK status code from the API
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        // But the Result wrapper should indicate NotFound status
        var result = await response.Content.ReadFromJsonAsync<Result>();
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
        Assert.Equal(ResultStatusCode.NotFound, result.StatusCode);
        Assert.NotEmpty(result.Messages);
    }
}