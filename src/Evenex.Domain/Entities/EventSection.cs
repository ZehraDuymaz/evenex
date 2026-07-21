using Evenex.Domain.Common;

namespace Evenex.Domain.Entities;

/// <summary>
/// Bir etkinliğin bir Section için fiyat/kontenjan konfigürasyonu.
/// RemainingCapacity ve RowVersion, ayakta bölümler için çift satış
/// önleme mekanizmasının parçasıdır.
/// </summary>
public class EventSection : BaseEntity
{
    public int EventId { get; set; }
    public Event Event { get; set; } = default!;

    public int SectionId { get; set; }
    public Section Section { get; set; } = default!;

    public decimal BasePrice { get; set; }
    public int Capacity { get; set; }
    public int RemainingCapacity { get; set; }

    public byte[] RowVersion { get; set; } = default!;

    public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
