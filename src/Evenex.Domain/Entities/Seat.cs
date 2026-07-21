using Evenex.Domain.Common;
using Evenex.Domain.Enums;

namespace Evenex.Domain.Entities;

/// <summary>
/// Sadece oturmalı (Seated) bölümler için. RowVersion, çift satışı
/// önlemek için kullanılan optimistic concurrency alanıdır.
/// </summary>
public class Seat : BaseEntity
{
    public int SectionId { get; set; }
    public Section Section { get; set; } = default!;

    public string RowLabel { get; set; } = default!;
    public string SeatNumber { get; set; } = default!;

    public SeatStatus Status { get; set; } = SeatStatus.Available;
    public DateTime? HeldUntil { get; set; }

    public byte[] RowVersion { get; set; } = default!;
}
