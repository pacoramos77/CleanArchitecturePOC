using FluentValidation;

namespace Core.ToDoListAggregate.Commands;

public class CreateToDoListValidator : AbstractValidator<CreateToDoListRequest>
{
    public CreateToDoListValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}
