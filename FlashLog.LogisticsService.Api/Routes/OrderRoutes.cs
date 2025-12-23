namespace FlashLog.LogisticsService.Api.Routes;

public static class OrderRoutes
{
    public static void MapOrderRoutes(this IEndpointRouteBuilder app)
    {
        var order = app.MapGroup("/orders").WithTags("Rotas para Pedidos.");

        app.MapPost("/", async () =>
        {
            return Results.Ok("Order created");
        })
        .WithSummary("Endpoint para criação de novo pedido.")
        .WithOpenApi()
        .Produces(204);
    }
}