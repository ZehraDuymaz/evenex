using Evenex.Domain.Common;

namespace Evenex.Domain.Entities;

public class Ticket : BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; } = default!;

    public string QrCode { get; set; } = default!;
    public string Status { get; set; } = "Valid";
}
