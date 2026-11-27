namespace MicroMob.Core.Models.Exceptions;

public class ParkingSpaceException : DomainException
{
    public ParkingSpaceException()
    {
    }

    public ParkingSpaceException(string? message) : base(message)
    {
    }

    public ParkingSpaceException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
