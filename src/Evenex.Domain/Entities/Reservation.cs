using Evenex.Domain.Common;
using Evenex.Domain.Enums;

namespace Evenex.Domain.Entities;

/// <summary>
/// Projenin en kritik entity'si — 5 dakikalık geçici tutmayı temsil eder.
/// SeatId null ise ayakta bölüm rezervasyonudur.
/// </summary>
public class Reservation : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public int EventSectionId { get; set; }
    public EventSection EventSection { get; set; } = default!;

    public int? SeatId { get; set; }
    public Seat? Seat { get; set; }

    public ReservationStatus Status { get; set; }
    public DateTime HeldAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}
