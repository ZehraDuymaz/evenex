using Evenex.Domain.Common;

namespace Evenex.Domain.Entities;

/// <summary>Fiziksel mekân. Bir veya daha fazla Section'a sahip olabilir.</summary>
public class Venue : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;

    public ICollection<Section> Sections { get; set; } = new List<Section>();
}
