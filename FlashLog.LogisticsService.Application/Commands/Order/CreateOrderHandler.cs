using FlashLog.LogisticsService.Shared.Results;
using MediatR;

namespace FlashLog.LogisticsService.Application.Commands.Order;

internal sealed class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}