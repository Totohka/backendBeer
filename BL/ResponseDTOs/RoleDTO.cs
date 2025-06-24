namespace BL.ResponseDTOs;
/// <summary>
/// DTO роли
/// </summary>
public class RoleDTO
{
    /// <summary>
    /// Уникальный идентификатор роли
    /// </summary>
    public Guid Uid { get; set; }

    /// <summary>
    /// Название роли
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Описание роли
    /// </summary>
    public string? Description { get; set; }
}
