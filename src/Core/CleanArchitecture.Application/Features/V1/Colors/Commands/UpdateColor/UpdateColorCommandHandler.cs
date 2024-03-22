using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Domain.Colors;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Colors.Commands.UpdateColor;

public sealed class UpdateColorCommandHandler : ICommandHandler<UpdateColorCommand, Guid>
{
    private readonly IColorRepository _colorRepository;
    public UpdateColorCommandHandler(IColorRepository clolorRepository)
    {
        _colorRepository = clolorRepository ?? throw new ArgumentNullException(nameof(clolorRepository));
    }

    public async Task<Result<Guid>> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
    {
        Color color = await _colorRepository.GetByIdAsync(request.Id);
        if (color == null)
            return Result.Failure<Guid>(ColorErrors.NotFound);

        color.Update(request.Name);

        await _colorRepository.UpdateAsync(color);

        return color.Id;
    }
}
