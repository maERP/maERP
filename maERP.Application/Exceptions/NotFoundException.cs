﻿namespace maERP.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
        Console.WriteLine($"Entity \"{name}\" ({key}) was not found.");
    }
}
