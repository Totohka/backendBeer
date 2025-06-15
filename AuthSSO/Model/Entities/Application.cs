using AuthSSO.Common.Enums;

namespace Model.Entities;
public class Application : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Version { get; set; }
    public string Ip { get; set; }
    public int Port { get; set; }
    public bool IsWork { get; set; }
    public EnumTypeApplication TypeApplication { get; set; }
    public DateTime? DateLastCheck { get; set; }
    public DateTime? DateBreak { get; set; }
    public List<Role> Roles { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public bool IsActive { get; set; }
}
