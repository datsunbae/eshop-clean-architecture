namespace CleanArchitecture.Domain.AggregatesModels.Shared;

public record UserInformation
{
    public string Name { get; private set; }
    public Phone Phone { get; private set; }
    public Address Address { get; private set; }

    protected UserInformation() { }

    public UserInformation(string name, Phone phone, Address address)
    {
        Name = name;
        Phone = phone;
        Address = address;
    }
}
