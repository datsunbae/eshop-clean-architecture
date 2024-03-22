using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Domain.Colors;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Colors.Commands.CreateColor;

public sealed class CreateColorHandler : ICommandHandler<CreateColorCommand, Guid>
{
    private readonly IColorRepository _colorRepository;
    public CreateColorHandler(IColorRepository clolorRepository)
    {
        _colorRepository = clolorRepository ?? throw new ArgumentNullException(nameof(clolorRepository));
    }

    public async Task<Result<Guid>> Handle(CreateColorCommand request, CancellationToken cancellationToken)
    {
        var color = new Color(Guid.NewGuid(), request.Name);
        var result = await _colorRepository.AddAsync(color);
        return result.Id;
    }
}
