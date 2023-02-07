namespace Paytools.Common.Model;

public record EmailAddress
{
    public string? Name { get; init; }
    public string Address { get; init; }

    public EmailAddress(string address, string? name = null)
    {
        Address = address;
        Name = name;
    }

    public override string ToString() =>
        Name != null ? $"{Name} <{Address}>" : Address;
}
