using FlashLog.LogisticsService.Shared.Results;
using MediatR;

namespace FlashLog.LogisticsService.Application.Commands.Order;

public record CreateOrderCommand() : IRequest<Result<Guid>>;