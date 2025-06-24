namespace Model.Entities;

public class ApiKey : BaseEntity
{
    public string Name { get; set; }
    public string Value { get; set; }
    public DateTime DateCreate { get; set; }
    public DateTime? DateExpire { get; set; }
    public bool IsActive { get; set; }
}
