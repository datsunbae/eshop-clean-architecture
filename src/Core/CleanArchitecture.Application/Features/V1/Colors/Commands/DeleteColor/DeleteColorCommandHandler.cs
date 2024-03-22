using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Persistence.Repositories;
using CleanArchitecture.Domain.Colors;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Colors.Commands.DeleteColor;

public sealed class DeleteColorCommandHandler : ICommandHandler<DeleteColorCommand, Guid>
{
    private readonly IColorRepository _clolorRepository;
    public DeleteColorCommandHandler(IColorRepository clolorRepository)
    {
        _clolorRepository = clolorRepository ?? throw new ArgumentNullException(nameof(_clolorRepository));
    }

    public async Task<Result<Guid>> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
    {
        Color color = await _clolorRepository.GetByIdAsync(request.Id);
        if (color is null)
            return Result.Failure<Guid>(ColorErrors.NotFound);

        await _clolorRepository.SoftDeleteAsync(color);

        return color.Id;
    }
}
