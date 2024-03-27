namespace CleanArchitecture.Domain.AggregatesModels.Shared;

public record UserInformation
{
    public string UserName { get; private set; }
    public Phone Phone { get; private set; }
    public Address Address { get; private set; }

    protected UserInformation() { }

    public UserInformation(string userName, Phone phone, Address address)
    {
        UserName = userName;
        Phone = phone;
        Address = address;
    }
}
