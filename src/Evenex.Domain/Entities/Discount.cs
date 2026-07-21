using Evenex.Domain.Common;

namespace Evenex.Domain.Entities;

/// <summary>
/// RuleType kasıtlı olarak enum değil string — yeni indirim türleri
/// migration gerektirmeden eklenebilsin diye.
/// </summary>
public class Discount : BaseEntity
{
    public int EventSectionId { get; set; }
    public EventSection EventSection { get; set; } = default!;

    public string Name { get; set; } = default!;
    public string RuleType { get; set; } = default!;
    public decimal Value { get; set; }
}
