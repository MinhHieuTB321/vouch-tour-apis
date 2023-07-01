using Application.ViewModels.SupplierDTO;
using FluentValidation;

namespace WebAPI.Validations
{
    public class SupplierCreateDTOValidation:AbstractValidator<SupplierCreateDTO>
    {
        public SupplierCreateDTOValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Email is incorrect format!").WithErrorCode("400");
            RuleFor(x => x.SupplierName).NotEmpty().NotNull().WithMessage("Name can not be null!").WithErrorCode("400");
            RuleFor(x => x.Address).NotEmpty().NotNull().WithMessage("Address is incorrect format!").WithErrorCode("400");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Matches("^(84|0[3|5|7|8|9])+([0-9]{8})\b$").WithMessage("Phone is incorrect format!").WithErrorCode("400");
        }
    }
    public class SupplierUpdateDTOValidation : AbstractValidator<SupplierUpdateDTO>
    {
        public SupplierUpdateDTOValidation()
        {
            RuleFor(x => x.SupplierName).NotEmpty().NotNull().WithMessage("Name can not be null!").WithErrorCode("400");
            RuleFor(x => x.Address).NotEmpty().NotNull().WithMessage("Address is incorrect format!").WithErrorCode("400");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Matches("^(84|0[3|5|7|8|9])+([0-9]{8})\b$").WithMessage("Phone is incorrect format!").WithErrorCode("400");
        }
    }

}
