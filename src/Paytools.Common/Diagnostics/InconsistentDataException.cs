namespace Paytools.Common.Diagnostics;

public class InconsistentDataException : Exception
{
    public InconsistentDataException(string message)
        : base(message) { }
}
