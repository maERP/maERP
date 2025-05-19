using System.Net;
using System.Net.Http.Json;
using maERP.Application.Features.Statistic.Queries.StatisticOrderCustomerChart;
using maERP.Domain.Dtos.Statistic;
using maERP.Domain.Entities;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Server.Tests;

[Collection("Sequential")]
public class StatisticCrudTest : IClassFixture<MaErpWebApplicationFactory<Program>>
{
    private readonly MaErpWebApplicationFactory<Program> _webApplicationFactory;

    public StatisticCrudTest(MaErpWebApplicationFactory<Program> webApplicationFactory)
    {
        _webApplicationFactory = webApplicationFactory;
    }

    [Theory]
    [InlineData("/api/v1/Statistics/OrderStatistic")]
    public async Task OrderStatistic(string url)
    {
        // Arrange
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        var now = DateTime.UtcNow;
        var thirtyDaysAgo = now.AddDays(-30);
        
        // Seed test data
        await _webApplicationFactory.InitializeDbForTests(
            new List<Customer>
            {
                new() { Id = 1, Firstname = "John", Lastname = "Doe", DateCreated = now.AddDays(-5) },
                new() { Id = 2, Firstname = "Jane", Lastname = "Smith", DateCreated = now.AddDays(-15) },
                new() { Id = 3, Firstname = "Bob", Lastname = "Johnson", DateCreated = now.AddDays(-40) }
            });
            
        await _webApplicationFactory.InitializeDbForTests(
            new List<Order>
            {
                new() { Id = 1, CustomerId = 1, DateOrdered = now.AddDays(-5) },
                new() { Id = 2, CustomerId = 2, DateOrdered = now.AddDays(-15) },
                new() { Id = 3, CustomerId = 3, DateOrdered = now.AddDays(-25) },
                new() { Id = 4, CustomerId = 1, DateOrdered = now.AddDays(-35) }
            });

        // Act
        var response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<Result<StatisticOrderDto>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.NotNull(result.Data);
        
        // Verify counts
        Assert.Equal(4, result.Data.OrderTotal);
        Assert.Equal(3, result.Data.Order30Days);
        //Assert.Equal(3, result.Data.CustomerTotal);
        
        // Verify daily statistics
        Assert.Equal(31, result.Data.DailyStatistics.Count); // Last 30 days + today
        //Assert.Equal(1, result.Data.DailyStatistics.Count(d => d.Date.Date == now.AddDays(-5).Date && d.OrderCount == 1 && d.NewCustomerCount == 1));
        //Assert.Equal(1, result.Data.DailyStatistics.Count(d => d.Date.Date == now.AddDays(-15).Date && d.OrderCount == 1 && d.NewCustomerCount == 1));
        //Assert.Equal(1, result.Data.DailyStatistics.Count(d => d.Date.Date == now.AddDays(-25).Date && d.OrderCount == 1 && d.NewCustomerCount == 0));
    }
    
    [Theory]
    [InlineData("/api/v1/Statistics/ProductStatistic")]
    public async Task ProductStatistic(string url)
    {
        // Arrange
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        
        // Seed test data - warehouses
        await _webApplicationFactory.InitializeDbForTests(
            new List<Warehouse>
            {
                new() { Id = 11, Name = "Warehouse 11" },
                new() { Id = 12, Name = "Warehouse 12" }
            });
        
        // Seed test data - tax classes
        await _webApplicationFactory.InitializeDbForTests(
            new List<TaxClass>
            {
                new() { Id = 11, TaxRate = 39 }
            });
        
        // Seed test data - products
        await _webApplicationFactory.InitializeDbForTests(
            new List<Product>
            {
                new() { Id = 1, Sku = "SKU001", Name = "Product 1", Price = 10.99m, TaxClassId = 1 },
                new() { Id = 2, Sku = "SKU002", Name = "Product 2", Price = 20.50m, TaxClassId = 1 },
                new() { Id = 3, Sku = "SKU003", Name = "Product 3", Price = 15.75m, TaxClassId = 1 },
                new() { Id = 4, Sku = "SKU004", Name = "Product 4", Price = 99.99m, TaxClassId = 1 }
            });
        
        // Seed test data - product stocks
        await _webApplicationFactory.InitializeDbForTests(
            new List<ProductStock>
            {
                new() { Id = 1, ProductId = 1, WarehouseId = 1, Stock = 10 },
                new() { Id = 2, ProductId = 2, WarehouseId = 1, Stock = 5 },
                new() { Id = 3, ProductId = 2, WarehouseId = 2, Stock = 15 },
                new() { Id = 4, ProductId = 3, WarehouseId = 1, Stock = 0 } // Out of stock
                // Product 4 has no stock entries
            });

        // Act
        var response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<Result<StatisticProductDto>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.NotNull(result.Data);
        
        // Verify counts
        //Assert.Equal(4, result.Data.ProductTotal); // Total products
        //Assert.Equal(2, result.Data.ProductInStock); // Products with stock > 0 (Products 1 and 2)
        //Assert.Equal(30, result.Data.ProductInWarehouse); // Sum of all stock (10 + 5 + 15)
    }
    
    [Theory]
    [InlineData("/api/v1/Statistics/OrderCustomerChart")]
    public async Task OrderCustomerChart(string url)
    {
        // Arrange
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        var now = DateTime.UtcNow;
        var thirtyDaysAgo = now.AddDays(-30);
        
        // Create customers
        await _webApplicationFactory.InitializeDbForTests(
            new List<Customer>
            {
                new() { Id = 1, Firstname = "John", Lastname = "Doe", DateCreated = now.AddDays(-5) },
                new() { Id = 2, Firstname = "Jane", Lastname = "Smith", DateCreated = now.AddDays(-15) },
                new() { Id = 3, Firstname = "Bob", Lastname = "Johnson", DateCreated = now.AddDays(-15) },
                new() { Id = 4, Firstname = "Alice", Lastname = "Brown", DateCreated = now.AddDays(-35) }
            });
            
        // Create orders with specific dates
        await _webApplicationFactory.InitializeDbForTests(
            new List<Order>
            {
                // Today - 5 days: 1 order from customer 1
                new() { Id = 1, CustomerId = 1, DateOrdered = now.AddDays(-5) },
                
                // Today - 15 days: 2 orders from different customers
                new() { Id = 2, CustomerId = 2, DateOrdered = now.AddDays(-15) },
                new() { Id = 3, CustomerId = 3, DateOrdered = now.AddDays(-15) },
                
                // Today - 25 days: 1 order from customer 1 again
                new() { Id = 4, CustomerId = 1, DateOrdered = now.AddDays(-25) },
                
                // Outside 30-day window - should not be included
                new() { Id = 5, CustomerId = 4, DateOrdered = now.AddDays(-35) }
            });

        // Act
        var response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<Result<StatisticOrderCustomerChartResponse>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.chartData);
        
        // Verify chart data
        // Only 3 dates with orders in the last 30 days
        Assert.Equal(3, result.Data.chartData.Count);
        
        // Check specific days
        var day5 = result.Data.chartData.SingleOrDefault(d => d.Date.Date == now.AddDays(-5).Date);
        Assert.NotNull(day5);
        Assert.Equal(1, day5.OrdersNew); // 1 order
        Assert.Equal(1, day5.CustomersNew); // 1 unique customer
        
        var day15 = result.Data.chartData.SingleOrDefault(d => d.Date.Date == now.AddDays(-15).Date);
        Assert.NotNull(day15);
        Assert.Equal(2, day15.OrdersNew); // 2 orders
        Assert.Equal(2, day15.CustomersNew); // 2 unique customers
        
        var day25 = result.Data.chartData.SingleOrDefault(d => d.Date.Date == now.AddDays(-25).Date);
        Assert.NotNull(day25);
        Assert.Equal(1, day25.OrdersNew); // 1 order
        Assert.Equal(1, day25.CustomersNew); // 1 unique customer
    }
    
    [Theory]
    [InlineData("/api/v1/Statistics/SalesStatistic")]
    public async Task SalesStatistic(string url)
    {
        // Arrange
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        var now = DateTime.UtcNow;
        
        // Create customers
        await _webApplicationFactory.InitializeDbForTests(
            new List<Customer>
            {
                new() { Id = 1, Firstname = "John", Lastname = "Doe" },
                new() { Id = 2, Firstname = "Jane", Lastname = "Smith" }
            });
            
        // Create orders with specific dates and values
        await _webApplicationFactory.InitializeDbForTests(
            new List<Order>
            {
                // Today (last 24 hours)
                new() { 
                    Id = 1, 
                    CustomerId = 1, 
                    DateOrdered = now.AddHours(-12), 
                    Status = OrderStatus.Pending,
                    Total = 100.50m
                },
                
                // Last 7 days
                new() { 
                    Id = 2, 
                    CustomerId = 2, 
                    DateOrdered = now.AddDays(-3), 
                    Status = OrderStatus.Completed,
                    Total = 200.75m
                },
                
                // Last 30 days
                new() { 
                    Id = 3, 
                    CustomerId = 1, 
                    DateOrdered = now.AddDays(-20), 
                    Status = OrderStatus.Completed,
                    Total = 300.25m
                },
                
                // Last 365 days
                new() { 
                    Id = 4, 
                    CustomerId = 2, 
                    DateOrdered = now.AddDays(-100), 
                    Status = OrderStatus.Completed,
                    Total = 400.00m
                },
                
                // Outside 365-day window - should not be included in any calculation
                new() { 
                    Id = 5, 
                    CustomerId = 1, 
                    DateOrdered = now.AddDays(-366), 
                    Status = OrderStatus.Cancelled,
                    Total = 1000.00m
                }
            });

        // Act
        var response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<Result<StatisticSalesDto>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.NotNull(result.Data);
        
        // Verify sales totals for different time periods
        Assert.Equal(100.50m, result.Data.Sales24Hours);
        Assert.Equal(301.25m, result.Data.Sales7Days); // 100.50 + 200.75
        Assert.Equal(601.50m, result.Data.Sales30Days); // 100.50 + 200.75 + 300.25
        Assert.Equal(1001.50m, result.Data.Sales365Days); // 100.50 + 200.75 + 300.25 + 400.00
    }
    
    [Theory]
    [InlineData("/api/v1/Statistics/MostSellingProducts")]
    public async Task MostSellingProducts(string url)
    {
        // Arrange
        HttpClient httpClient = _webApplicationFactory.CreateClient();
        var now = DateTime.UtcNow;
        var today = now.Date;
        
        // Seed products
        await _webApplicationFactory.InitializeDbForTests(
            new List<Product>
            {
                new() { Id = 1, Sku = "PROD1", Name = "Product 1", Price = 19.99m },
                new() { Id = 2, Sku = "PROD2", Name = "Product 2", Price = 29.99m },
                new() { Id = 3, Sku = "PROD3", Name = "Product 3", Price = 39.99m },
                new() { Id = 4, Sku = "PROD4", Name = "Product 4", Price = 49.99m }
            });
        
        // Seed customers
        await _webApplicationFactory.InitializeDbForTests(
            new List<Customer>
            {
                new() { Id = 1, Firstname = "Customer", Lastname = "One" }
            });
        
        // Seed orders for different time periods
        
        // Orders from today
        var todayOrders = new List<Order>
        {
            new() 
            { 
                Id = 1, 
                CustomerId = 1, 
                DateOrdered = today.AddHours(10), // Today at 10am
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = 1, Quantity = 2 },
                    new() { ProductId = 2, Quantity = 1 }
                }
            },
            new() 
            { 
                Id = 2, 
                CustomerId = 1, 
                DateOrdered = today.AddHours(14), // Today at 2pm
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = 1, Quantity = 1 },
                    new() { ProductId = 3, Quantity = 3 }
                }
            }
        };
        
        // Orders from last week
        var lastWeekOrders = new List<Order>
        {
            new() 
            { 
                Id = 3, 
                CustomerId = 1, 
                DateOrdered = today.AddDays(-5), 
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = 2, Quantity = 5 },
                    new() { ProductId = 4, Quantity = 1 }
                }
            }
        };
        
        // Orders from last month (but not this week)
        var lastMonthOrders = new List<Order>
        {
            new() 
            { 
                Id = 4, 
                CustomerId = 1, 
                DateOrdered = today.AddDays(-20), 
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = 3, Quantity = 2 },
                    new() { ProductId = 4, Quantity = 4 }
                }
            }
        };
        
        // Orders from this year (but not this month)
        var thisYearOrders = new List<Order>
        {
            new() 
            { 
                Id = 5, 
                CustomerId = 1, 
                DateOrdered = today.AddDays(-60), 
                OrderItems = new List<OrderItem>
                {
                    new() { ProductId = 1, Quantity = 10 }
                }
            }
        };
        
        // Seed all orders
        var allOrders = new List<Order>();
        allOrders.AddRange(todayOrders);
        allOrders.AddRange(lastWeekOrders);
        allOrders.AddRange(lastMonthOrders);
        allOrders.AddRange(thisYearOrders);
        
        await _webApplicationFactory.InitializeDbForTests(allOrders);

        // Act
        var response = await httpClient.GetAsync(url);
        var result = await response.Content.ReadFromJsonAsync<Result<StatisticMostSellingProductsDto>>();

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
        Assert.Equal(ResultStatusCode.Ok, result.StatusCode);
        Assert.NotNull(result.Data);
        
        // Verify top products based on timeframes
        
        // Today
        // Assert.NotNull(result.Data.TopProductsToday);
        // Assert.Equal(3, result.Data.TopProductsToday.Count); // Products 1, 2, and 3
        
        //var topTodayProduct = result.Data.TopProductsToday.First(); // Should be Product 3
        // Assert.Equal(3, topTodayProduct.ProductId);
        // Assert.Equal(3, topTodayProduct.TotalQuantity);
        
        // Last 7 days
        //Assert.NotNull(result.Data.TopProductsLastSevenDays);
        //Assert.Equal(4, result.Data.TopProductsLastSevenDays.Count); // All products
        
        //var topWeekProduct = result.Data.TopProductsLastSevenDays.First(); // Should be Product 2
        //Assert.Equal(2, topWeekProduct.ProductId);
        //Assert.Equal(6, topWeekProduct.TotalQuantity); // 1 from today + 5 from last week
        
        // This month
        //Assert.NotNull(result.Data.TopProductsThisMonth);
        //Assert.Equal(4, result.Data.TopProductsThisMonth.Count);
        
        //var topMonthProduct = result.Data.TopProductsThisMonth.First(); // Product 4
        //Assert.Equal(4, topMonthProduct.ProductId);
        //Assert.Equal(5, topMonthProduct.TotalQuantity); // 1 from last week + 4 from last month
        
        // This year
        //Assert.NotNull(result.Data.TopProductsThisYear);
        //Assert.Equal(4, result.Data.TopProductsThisYear.Count);
        
        //var topYearProduct = result.Data.TopProductsThisYear.First(); // Product 1
        //Assert.Equal(1, topYearProduct.ProductId);
        //Assert.Equal(13, topYearProduct.TotalQuantity); // 2+1 from today + 10 from earlier this year
        
        // All time
        //Assert.NotNull(result.Data.TopProductsAllTime);
        //Assert.Equal(4, result.Data.TopProductsAllTime.Count); // All products
        
        // Verify it's the same as this year since we don't have older data
        //Assert.Equal(13, result.Data.TopProductsAllTime.First().TotalQuantity);
    }
}