using Evenex.Domain.Common;

namespace Evenex.Domain.Entities;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; } = default!;

    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Confirmed";

    public Ticket? Ticket { get; set; }
}
