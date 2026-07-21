using Evenex.Domain.Common;
using Evenex.Domain.Enums;

namespace Evenex.Domain.Entities;

/// <summary>Bir mekânın bölümü — oturmalı ya da ayakta.</summary>
public class Section : BaseEntity
{
    public int VenueId { get; set; }
    public Venue Venue { get; set; } = default!;

    public string Name { get; set; } = default!;
    public SectionType Type { get; set; }

    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
}
