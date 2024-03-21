namespace EShop.Application.User;

public interface IUserService
{
    Task<UserDto> GetUserById(Guid id);
    Task<List<UserDto>> GetAllUsers();
    Task DeleteUser(Guid id);
}