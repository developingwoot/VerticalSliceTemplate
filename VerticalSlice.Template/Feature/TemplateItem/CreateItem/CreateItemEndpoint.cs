
public record CreateItemRequest(string ItemName) {
    public CreateItemCommand ConvertToComamnd() {
        return new CreateItemCommand(ItemName);
    }
}

public record CreateItemResponse(string ItemName);

public class CreateItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/item", async (CreateItemRequest item, ISender sender) => {
            
            var result = await sender.Send(item.ConvertToComamnd());

            var response = new CreateItemResponse(result.ItemName);

            return Results.Created($"/item/{response.ItemName}", response);

        })
        .Produces(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Creates an Item")
        .MapToApiVersion(1, 0);
    }
}