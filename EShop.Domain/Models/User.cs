using Microsoft.AspNetCore.Identity;

namespace EShop.Domain.Models;

public class User : IdentityUser<Guid>, IBaseEntity<Guid>
{
   // public Guid UserTypeId { get; set; }
   // public UserType UserType { get; set; }
}