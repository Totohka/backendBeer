﻿namespace BL.RequestDTOs;

public class CreateUserDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
}
