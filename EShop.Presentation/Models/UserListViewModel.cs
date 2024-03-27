using EShop.Domain.Models;

namespace EShop.Presentation.Models;

public class UserListViewModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string UserType { get; set; }
}