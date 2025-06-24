using BL.Helpers;
using BL.RequestDTOs;
using BL.ResponseDTOs;
using DAL.Entities;
using DAL.Repositories;
using Model.Entities;
using System.Linq.Expressions;

namespace BL.Services.Users;

public interface IUserService
{
    public Task<Guid> CreateUserAsync(CreateUserDTO dto);
    public Task<UserDTO> GetUserByUidAsync(Guid uid);
    public Task<UserDTO> GetUserByLoginAsync(string login);
    public Task<UserDTO> GetUserByEmailAsync(string email);
    public Task<List<UserDTO>> GetAllUsersAsync();
    public Task<ResponsePagination<UserDTO>> GetUsersAsync(FilterUserDTO dto);
    public Task UpdateUserAsync();
    public Task<bool> ChangePasswordUserAsync(Guid uid, string oldPassword, string password);
    public Task<bool> ChangeLoginUserAsync(Guid uid, string login);
    public Task<bool> ChangeEmailUserAsync(Guid uid, string email);
    public Task<bool> ChangeFIOUserAsync(Guid uid, ChangeFIOUserDTO dto);
    public Task ChangeActiveUserAsync(Guid uid);
    public Task DeleteUserAsync(Guid uid);
}

public class UserService : IUserService
{
    private readonly IBaseRepository<User> _userRepository;
    public UserService(IBaseRepository<User> userRepository) 
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> CreateUserAsync(CreateUserDTO dto)
    {
        var salt = HashHelper.GenerateSalt();
        var hash = HashHelper.GenerateSha256Hash(dto.Password, salt);
        var user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            MiddleName = dto.MiddleName,
            Email = dto.Email,
            Login = dto.Login,
            HashPassword = hash,
            Salt = salt,
            IsActive = dto.IsActive
        };
        return await _userRepository.CreateAsync(user);
    }

    public async Task<UserDTO> GetUserByUidAsync(Guid uid)
    {
        var user = await _userRepository.GetAsync(uid);
        var userDTO = new UserDTO()
        {
            Uid = user.Uid,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
            Email = user.Email,
            Login = user.Login
        };
        return userDTO;
    }

    public async Task<UserDTO> GetUserByLoginAsync(string login)
    {
        var user = await _userRepository.GetAsync(u => u.Login == login);
        var userDTO = new UserDTO()
        {
            Uid = user.Uid,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
            Email = user.Email,
            Login = user.Login
        };
        return userDTO;
    }

    public async Task<UserDTO> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetAsync(u => u.Email == email);
        var userDTO = new UserDTO()
        {
            Uid = user.Uid,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
            Email = user.Email,
            Login = user.Login
        };
        return userDTO;
    }

    public async Task<List<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        var userDTOs = new List<UserDTO>();
        foreach (var user in users) 
        {
            userDTOs.Add(new UserDTO() {
                Uid = user.Uid,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Email = user.Email,
                Login = user.Login
            });
        }
        return userDTOs;
    }

    public async Task<ResponsePagination<UserDTO>> GetUsersAsync(FilterUserDTO dto)
    {
        Expression<Func<User, bool>> lambda;

        var argument = Expression.Parameter(typeof(User), "user");

        var conditions = new List<Expression>();

        if (dto.FirstName is not null)
        {
            var property = Expression.Property(argument, nameof(User.FirstName));
            var value = Expression.Constant(dto.FirstName);
            var condition = Expression.Equal(property, value);
            conditions.Add(condition);
        }
        if (dto.LastName is not null)
        {
            var property = Expression.Property(argument, nameof(User.LastName));
            var value = Expression.Constant(dto.LastName);
            var condition = Expression.Equal(property, value);
            conditions.Add(condition);
        }
        if (dto.MiddleName is not null)
        {
            var property = Expression.Property(argument, nameof(User.MiddleName));
            var value = Expression.Constant(dto.MiddleName);
            var condition = Expression.Equal(property, value);
            conditions.Add(condition);
        }
        if (dto.Login is not null)
        {
            var property = Expression.Property(argument, nameof(User.Login));
            var value = Expression.Constant(dto.Login);
            var condition = Expression.Equal(property, value);
            conditions.Add(condition);
        }
        if (dto.Email is not null)
        {
            var property = Expression.Property(argument, nameof(User.Email));
            var value = Expression.Constant(dto.Email);
            var condition = Expression.Equal(property, value);
            conditions.Add(condition);
        }
        var body = conditions.Aggregate(Expression.AndAlso);
        lambda = Expression.Lambda<Func<User, bool>>(body, argument);
        var paginationUsers = await _userRepository.GetPartAsync(lambda, dto.Skip, dto.Take);
        var paginationUserDTOs = new ResponsePagination<UserDTO>();
        paginationUserDTOs.CurrentPage = paginationUsers.CurrentPage;
        paginationUserDTOs.PageCount = paginationUsers.PageCount;
        paginationUserDTOs.TotalEntity = paginationUsers.TotalEntity;
        var userDTOs = new List<UserDTO>();
        foreach (var user in paginationUsers.Data)
        {
            userDTOs.Add(new UserDTO()
            {
                Uid = user.Uid,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Email = user.Email,
                Login = user.Login
            });
        }
        paginationUserDTOs.Data = userDTOs;
        return paginationUserDTOs;
    }

    public Task UpdateUserAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangePasswordUserAsync(Guid uid, string oldPassword, string password)
    {
        var user = await _userRepository.GetAsync(uid);
        var oldHash = HashHelper.GenerateSha256Hash(oldPassword, user.Salt);
        if (user.HashPassword == oldHash)
        {
            user.DateUpdate = DateTime.UtcNow;
            user.HashPassword = HashHelper.GenerateSha256Hash(password, user.Salt);
            await _userRepository.UpdateAsync(user);
            return true;
        }
        else return false;
    }

    public async Task<bool> ChangeFIOUserAsync(Guid uid, ChangeFIOUserDTO dto)
    {
        try
        {
            var user = await _userRepository.GetAsync(uid);
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.MiddleName = dto.MiddleName;
            await _userRepository.UpdateAsync(user);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> ChangeLoginUserAsync(Guid uid, string login)
    {
        var userWithLogin = await _userRepository.GetAsync(u => u.Login == login);
        if (userWithLogin is null) 
        {
            var user = await _userRepository.GetAsync(uid);
            user.DateUpdate = DateTime.UtcNow;
            user.Login = login;
            await _userRepository.UpdateAsync(user);
            return true;
        }
        return false;
    }

    public async Task<bool> ChangeEmailUserAsync(Guid uid, string email)
    {
        var userWithEmail = await _userRepository.GetAsync(u => u.Email == email);
        if (userWithEmail is null)
        {
            var user = await _userRepository.GetAsync(uid);
            user.DateUpdate = DateTime.UtcNow;
            user.Email = email;
            await _userRepository.UpdateAsync(user);
            return true;
        }
        return false;
    }

    public async Task ChangeActiveUserAsync(Guid uid)
    {
        var user = await _userRepository.GetAsync(uid);
        user.DateUpdate = DateTime.UtcNow;
        user.IsActive = !user.IsActive;
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(Guid uid)
    {
        await _userRepository.DeleteAsync(uid);
    }
}
