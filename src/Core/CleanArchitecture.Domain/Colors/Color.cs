using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Domain.Colors;

public sealed class Color : BaseEntity
{
    public Color(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; private set; }

    public Color Update(string name)
    {
        if(name is not null && Name.Equals(name) is not true)
            Name = name;

        return this;
    }
}
