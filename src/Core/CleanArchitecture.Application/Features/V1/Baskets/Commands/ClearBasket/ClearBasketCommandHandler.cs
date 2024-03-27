using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Common.Persistence;
using CleanArchitecture.Application.Features.V1.Baskets.Specs;
using CleanArchitecture.Domain.AggregatesModels.Baskets;
using CleanArchitecture.Domain.AggregatesModels.Baskets.Repository;
using CleanArchitecture.Domain.Common;

namespace CleanArchitecture.Application.Features.V1.Baskets.Commands.ClearBasket;

public sealed class ClearBasketCommandHandler : ICommandHandler<ClearBasketCommand, Guid>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unitOfWork;
    public ClearBasketCommandHandler(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
    {
        _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Result<Guid>> Handle(ClearBasketCommand request, CancellationToken cancellationToken)
    {
        Basket basket = await _basketRepository.FirstOrDefaultAsync(new BasketByUserIdSpec(request.UserId));

        if(basket is null)
            return Result.Failure<Guid>(BasketErrors.NotFound);

        basket.Clear();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return basket.Id;
    }
}
    