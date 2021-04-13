using FluentValidation;
using ShopMate.Application.UnitSizes;

namespace ShopMate.Application.Validations
{
    public class CreateCommandValidation : AbstractValidator<Create.Command>
    {
        public CreateCommandValidation()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(64);

            // TODO
            // RuleFor(p => p.Name).IsUnique(nameof(Create.Command.Name));
        }
    }
}
