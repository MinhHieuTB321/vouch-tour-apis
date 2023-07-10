using Application.ViewModels;
using FluentValidation;

namespace WebAPI.Validations
{
    public class OrderCreateDTOValidation:AbstractValidator<OrderCreateDTO>
    {
        public OrderCreateDTOValidation()
        {
            RuleFor(x => x.CustomerName).NotEmpty().NotNull().WithMessage("Name can not be null!").WithErrorCode("400");
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().Matches("^(84|0[3|5|7|8|9])([0-9]{8})$").WithMessage("Phone number is incorrect format!").WithErrorCode("400");
        }
    }

    public class OrderUpdateDTOValidation : AbstractValidator<OrderUpdateDTO>
    {
        public OrderUpdateDTOValidation()
        {
            RuleFor(x => x.Status).NotNull().NotEmpty().Matches("^([Completed]{9})$|^([Canceled]{8})$").WithMessage("Status is incorrect format!").WithErrorCode("400");
        }
    }
}
