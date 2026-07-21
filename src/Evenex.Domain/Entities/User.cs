using Evenex.Domain.Common;
using Evenex.Domain.Enums;

namespace Evenex.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public UserRole Role { get; set; }
}
