using AutoMapper;

namespace EShop.Application.User;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await _userRepository.GetById(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _userRepository.GetAll();
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task DeleteUser(Guid id)
    {
        var isExists = await _userRepository.AnyAsync(id);
        if (!isExists)
        {
            throw new Exception("User not found");
        }
        
        await _userRepository.Delete(id);
    }
}