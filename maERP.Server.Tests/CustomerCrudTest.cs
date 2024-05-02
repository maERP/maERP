using maERP.Application.Dtos.Customer;
using maERP.Domain.Models;
using System.Net;
using System.Net.Http.Json;

namespace maERP.Server.Tests;
public class CustomerCrudTest : IClassFixture<maERPWebApplicationFactory<Program>>
{
    private readonly maERPWebApplicationFactory<Program> _webApplicationFactory;

    public CustomerCrudTest(maERPWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/Customers")]
    public async Task Create(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();

        await _webApplicationFactory.InitializeDbForTests();
        var customer = new CustomerCreateDto
        {
            Firstname = "Customer Firstname",
            Lastname = "Customer Lastname",
        };

        HttpResponseMessage result = await httpClient.PostAsJsonAsync(url, customer);
        CustomerDetailDto? resultContent = await result.Content.ReadFromJsonAsync<CustomerDetailDto>();

        Assert.NotNull(resultContent);
        Assert.True(result.IsSuccessStatusCode);
        Assert.True(resultContent != null && resultContent.Id != default);
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

        ICollection<CustomerListDto>? result = await httpClient.GetFromJsonAsync<ICollection<CustomerListDto>>(url);

        Assert.NotNull(result);
        Assert.Equal(result?.Count, 1);
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

        CustomerDetailDto? result = await httpClient.GetFromJsonAsync<CustomerDetailDto>(url);

        Assert.NotNull(result);
        Assert.True(result.Firstname.Length > 0);
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

        var customer = new CustomerUpdateDto
        {
            Firstname = "Customer 4 Firstname updated",
            Lastname = "Customer 4 Lastname updated",
        };

        HttpResponseMessage result = await httpClient.PutAsJsonAsync(url, customer);
        
        Assert.True(result.StatusCode == HttpStatusCode.NoContent);
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

        HttpResponseMessage result = await httpClient.DeleteAsync(url);

        Assert.Equal(result?.StatusCode, HttpStatusCode.NoContent);
    }

    [Theory]
    [InlineData("/api/v1/Customers/999999")]
    public async Task NotExist(string url)
    {
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        await _webApplicationFactory.InitializeDbForTests();

        HttpResponseMessage result = await httpClient.GetAsync(url);

        Assert.Equal(result?.StatusCode, HttpStatusCode.NotFound);
    }
}