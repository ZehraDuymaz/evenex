namespace Evenex.Domain.Common;

/// <summary>
/// Tüm entity'lerin miras aldığı ortak temel sınıf. Oluşturulma/güncellenme
/// bilgisi ve soft delete alanlarını içerir.
/// </summary>

public abstract class BaseEntity
{
    // _eid encrpyt it hash it, secure... 
    
    public int Id { get; set; }
    public string CreatedUser { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public string CreatedIP { get; set; } = default!;
    public string? ModifiedUser { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedIP { get; set; }
    public bool IsDeleted { get; set; }
}
