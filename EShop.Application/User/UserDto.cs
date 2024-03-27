using EShop.Domain.Models;

namespace EShop.Application.User;

public record UserDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string UserType { get; set; }
};