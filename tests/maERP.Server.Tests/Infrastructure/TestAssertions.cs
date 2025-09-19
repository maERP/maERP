using System.Net;
using Xunit;

namespace maERP.Server.Tests.Infrastructure;

public static class TestAssertions
{
    public static void AssertHttpSuccess(HttpResponseMessage response)
    {
        Assert.True(response.IsSuccessStatusCode,
            $"Expected successful status code, but got {response.StatusCode}. Content: {response.Content.ReadAsStringAsync().Result}");
    }

    public static void AssertHttpStatusCode(HttpResponseMessage response, HttpStatusCode expectedStatusCode)
    {
        Assert.Equal(expectedStatusCode, response.StatusCode);
    }

    public static void AssertNotNull<T>(T? value, string? message = null)
    {
        Assert.NotNull(value);
    }

    public static void AssertNull<T>(T? value, string? message = null)
    {
        Assert.Null(value);
    }

    public static void AssertEqual<T>(T expected, T actual)
    {
        Assert.Equal(expected, actual);
    }

    public static void AssertNotEqual<T>(T expected, T actual)
    {
        Assert.NotEqual(expected, actual);
    }

    public static void AssertNotEmpty<T>(IEnumerable<T> collection)
    {
        Assert.NotEmpty(collection);
    }

    public static void AssertEmpty<T>(IEnumerable<T> collection)
    {
        Assert.Empty(collection);
    }

    public static void AssertTrue(bool condition, string? message = null)
    {
        Assert.True(condition, message);
    }

    public static void AssertFalse(bool condition, string? message = null)
    {
        Assert.False(condition, message);
    }

    public static void AssertContains<T>(T expected, IEnumerable<T> collection)
    {
        Assert.Contains(expected, collection);
    }

    public static void AssertDoesNotContain<T>(T expected, IEnumerable<T> collection)
    {
        Assert.DoesNotContain(expected, collection);
    }
}
