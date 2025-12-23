using FlashLog.LogisticsService.Shared.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlashLog.LogisticsService.Application.Behaviors;

public class ExceptionBehavior<TRequest, TResponse>(ILogger<ExceptionBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Erro ao processar {Request}", typeof(TRequest).Name);

            var errors = new List<string> { ex.Message };

            if (typeof(TResponse) == typeof(Result))
                return (TResponse)(object)Result.Fail("Erro interno.", errors);

            if (typeof(TResponse).IsGenericType &&
                typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var failMethod = typeof(Result<>)
                    .MakeGenericType(typeof(TResponse).GetGenericArguments()[0])
                    .GetMethod(nameof(Result<object>.Fail), [typeof(string), typeof(List<string>)]);

                return (TResponse)failMethod!.Invoke(null, ["Erro interno.", errors])!;
            }

            throw;
        }
    }
}