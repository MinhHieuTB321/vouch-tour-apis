using Application.ViewModels.TourGuideDTO;
using FluentValidation;

namespace WebAPI.Validations
{
    public class TourGuideCreateDTOValidation:AbstractValidator<TourGuideCreateDTO>
    {
        public TourGuideCreateDTOValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Email is incorrect format!").WithErrorCode("400");
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name can not be null!").WithErrorCode("400");
            RuleFor(x => x.Address).NotEmpty().NotNull().WithMessage("Address is incorrect format!").WithErrorCode("400");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Matches("^(84|0[3|5|7|8|9])+([0-9]{8})\b$").WithMessage("Phone is incorrect format!").WithErrorCode("400");
            RuleFor(x => x.DateOfBirth).NotEmpty().NotNull().LessThan(DateTime.Now).WithMessage("Email is incorrect format!").WithErrorCode("400");
        }
    }

    public class TourGuideUpdateDTOValidation : AbstractValidator<TourGuideUpdateDTO>
    {
        public TourGuideUpdateDTOValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name can not be null!").WithErrorCode("400");
            RuleFor(x => x.Address).NotEmpty().NotNull().WithMessage("Address is incorrect format!").WithErrorCode("400");
            RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Matches("^(84|0[3|5|7|8|9])+([0-9]{8})\b$").WithMessage("Phone is incorrect format!").WithErrorCode("400");
        }
    }
}
