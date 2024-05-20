using CleanArchitecture.Application.Common.ApplicationServices.Email;
using CleanArchitecture.Application.Common.Messaging;
using CleanArchitecture.Application.Features.Identities.Users;
using CleanArchitecture.Domain.AggregatesModels.Orders.Events;

namespace CleanArchitecture.Application.Features.V1.Orders.EventHandlers;

public sealed class OrderCreatedDomainEventHandler : IDomainEventHandler<OrderCreatedDomainEvent>
{
    private readonly IMailService _mailService;
    private readonly IUserService _userService;
    public OrderCreatedDomainEventHandler(IMailService mailService, IUserService userService)
    {
        _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userService.GetAsync(notification.UserId, cancellationToken);
        if (user is null)
            return;

        var mail = new MailRequest(new List<string> {
            user.Email! },
            "Thank you for your purchase",
            "Thank you for ordering. We received your order and will begin processing it soon!");

        await _mailService.SendAsync(mail, cancellationToken);
    }
}
