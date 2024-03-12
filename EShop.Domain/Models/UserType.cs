using System.Text.Json.Serialization;

namespace EShop.Domain.Models;

public class UserType
{
    public Guid Id { get; set; }
    public string Type { get; set; }

    [JsonIgnore]
    public ICollection<User> Users { get; set; }
}