﻿using System.ComponentModel.DataAnnotations;

namespace maERP.Application.Features.User.Queries.GetUsers;

public class GetUsersResponse
{
    public string? Id { get; set; }

    [Required]
    public string Firstname { get; set; } = string.Empty;

    [Required]
    public string Lastname { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;
}