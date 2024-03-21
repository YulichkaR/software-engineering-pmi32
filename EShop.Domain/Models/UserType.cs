using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class UserType : BaseEntity<Guid>
{
    public string Type { get; set; }

    [JsonIgnore]
    public ICollection<User> Users { get; set; }
}