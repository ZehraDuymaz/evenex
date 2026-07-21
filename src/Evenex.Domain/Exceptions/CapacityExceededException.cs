namespace Evenex.Domain.Exceptions;

public class CapacityExceededException : DomainException
{
    public CapacityExceededException(int eventSectionId)
        : base($"No remaining capacity for event section {eventSectionId}.") { }
}
