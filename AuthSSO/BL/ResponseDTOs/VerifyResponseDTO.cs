namespace BL.ResponseDTOs;

/// <summary>
/// DTO верификации
/// </summary>
public class VerifyResponseDTO
{
    /// <summary>
    /// Успешность запроса
    /// </summary>
    public bool Success { get; set; } 

    /// <summary>
    /// Были ли ошибки в ходе выполнения запроса
    /// </summary>
    public bool Failure { get; set; }

    /// <summary>
    /// Список DTO ролей
    /// </summary>
    public List<RoleDTO> Roles { get; set; }= new List<RoleDTO>();
}
