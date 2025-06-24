namespace Model.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid ApplicationUid { get; set; }
    public Application Application { get; set; }
    public List<UserRole> UserRoles { get; set; }
}
