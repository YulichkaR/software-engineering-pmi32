using EShop.Domain.Models;

namespace EShop.Application.User;

public record UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserType UserType { get; set; }
};