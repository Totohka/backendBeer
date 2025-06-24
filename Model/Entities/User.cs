using AuthSSO.Common.Constants;
using AuthSSO.Common.Enums;

namespace Model.Entities;

public class User : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string Login { get; set; }
    public EnumLanguages Language { get; set; } = EnumLanguages.ru;
    public string Ip { get; set; } = Constants.DefaultIp;
    public string? HashPassword { get; set; } = null;
    public byte[]? Salt { get; set; } = null;
    public bool IsUserPass { get; set; } = true;
    public bool IsApiKey { get; set; } = false;
    public bool IsPass { get; set; } = false;
    public string? Email { get; set; }
    public string Code { get; set; } = Constants.DefaultAuthCode;
    public bool IsActiveCode { get; set; } = false;
    public DateTime DateCreate { get; set; } = DateTime.UtcNow;
    public DateTime? DateUpdate { get; set; } = null;
    public List<UserRole> UserRoles { get; set; }
    public Guid? ApiKeyUid { get; set; }
    public ApiKey? ApiKey { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid? RefreshTokenUid { get; set; }
    public RefreshToken RefreshToken { get; set; }
}
