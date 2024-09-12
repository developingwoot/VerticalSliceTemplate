
public record CreateItemCommand(string ItemName): IRequest<CreateItemCommandResult>;

public record CreateItemCommandResult(string ItemName);

public class CreateItemValidator: AbstractValidator<CreateItemCommand> {
    public CreateItemValidator()
    {
        RuleFor(x => x.ItemName).NotEmpty().WithMessage("An item name is required.");
    }
}


internal class CreateItemHandler : IRequestHandler<CreateItemCommand, CreateItemCommandResult>
{
    public async Task<CreateItemCommandResult> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        /// create the item
        /// 

        return new CreateItemCommandResult("success");
    }
}