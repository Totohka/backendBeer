namespace BL.RequestDTOs;

public class FilterUserDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Login { get; set; }
    public string? Email { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
}
