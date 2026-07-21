namespace Evenex.Domain.Exceptions;

public class SeatUnavailableException : DomainException
{
    public SeatUnavailableException(int seatId)
        : base($"Seat {seatId} is not available to hold.") { }
}
