using FluentValidation;
namespace Application.CreateNewOrder
{
    public class CreateNewOrderCommandValidator : AbstractValidator<CreateNewOrderCommand>
    {
        public CreateNewOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0)
                .WithMessage("order identifier should b greater than zero.");
        }
    }
}