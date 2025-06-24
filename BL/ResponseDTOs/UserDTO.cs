namespace BL.ResponseDTOs;

/// <summary>
/// DTO пользователя
/// </summary>
public class UserDTO
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    public Guid Uid { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Логин
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string? Email { get; set; }
}
