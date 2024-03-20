using CleanArchitecture.Application.Common.Email;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Domain.Products.Events;

namespace CleanArchitecture.Application.Features.V1.Products.Events;

public sealed class ProductUpdateDomainEventHandler : IDomainEventHandler<ProductUpdatedDomainEvent>
{
    private readonly IMailService _mailService;
    public ProductUpdateDomainEventHandler(IMailService mailService)
    {
        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
    }

    public async Task Handle(ProductUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var mail = new MailRequest(new List<string> {
            "nguyenminhdat0802@gmail.com" },
            "Update Product",
            $"Product with id = {notification.productId} updated!");

        await _mailService.SendAsync(mail, cancellationToken);
    }
}
