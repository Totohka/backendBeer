namespace Model.Entities;

public class WhiteIp : BaseEntity
{
    public string Ip { get; set; }

    /// <summary>
    /// Идентификатор api-ключа 
    /// </summary>
    public Guid ApiKeyUid { get; set; }

    /// <summary>
    /// API-ключ, которым можно воспользоваться с данного IP адреса
    /// </summary>
    public ApiKey ApiKey { get; set; }
    public bool IsActive { get; set; }
}
