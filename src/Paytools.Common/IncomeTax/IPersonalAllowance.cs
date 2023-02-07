namespace Paytools.Common;

public interface IPersonalAllowance
{
    PayFrequency PayFrequency { get; }
    decimal Value { get; }
}
