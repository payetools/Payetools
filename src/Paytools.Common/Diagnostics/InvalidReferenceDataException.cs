namespace Paytools.Common.Diagnostics;

public class InvalidReferenceDataException : Exception
{
    public InvalidReferenceDataException(string message)
        : base(message) { }
}
