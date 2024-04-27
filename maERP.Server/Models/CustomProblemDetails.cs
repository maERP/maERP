using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Models;

public class CustomProblemDetails : ProblemDetails
{
    public List<string> Errors { get; set; } = new();
}