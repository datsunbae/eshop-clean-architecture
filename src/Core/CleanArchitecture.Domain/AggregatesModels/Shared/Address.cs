namespace CleanArchitecture.Domain.AggregatesModels.Shared;

public record Address
{
    public string Street { get; private set; }
    public string City { get; private set; }

    public Address(string street, string city)
    {
        Street = street;
        City = city;
    }
}
