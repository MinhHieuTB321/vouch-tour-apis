using Application.ViewModels.GroupDTOs;
using FluentValidation;

namespace WebAPI.Validations
{
    public class GroupCreateDTOValidation:AbstractValidator<GroupCreateDTO>
    {
        public GroupCreateDTOValidation()
        {
            RuleFor(x=>x.GroupName).NotEmpty().NotNull().WithMessage("Name can not be null").WithErrorCode("400");
            RuleFor(x=>x.Description).NotEmpty().NotNull().WithMessage("Name can not be null").WithErrorCode("400");
            RuleFor(x => x.Quantity).NotNull().GreaterThan(0).WithMessage("Quantity must higher than 0").WithErrorCode("400");
            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(DateTime.Now).NotNull().WithMessage("Start Date must higher than now!").WithErrorCode("400");
            RuleFor(x=>x.EndDate).GreaterThan(x=>x.StartDate).NotNull().WithMessage("Start Date must higher than start Date!").WithErrorCode("400");
        }
    }

    public class GroupUpdateDTOValidation : AbstractValidator<GroupUpdateDTO>
    {
        public GroupUpdateDTOValidation()
        {
            RuleFor(x => x.GroupName).NotEmpty().NotNull().WithMessage("Name can not be null").WithErrorCode("400");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Name can not be null").WithErrorCode("400");
            RuleFor(x => x.Quantity).NotNull().GreaterThan(0).WithMessage("Quantity must higher than 0").WithErrorCode("400");
            RuleFor(x => x.StartDate).GreaterThanOrEqualTo(DateTime.Now).NotNull().WithMessage("Start Date must higher than now!").WithErrorCode("400");
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).NotNull().WithMessage("Start Date must higher than start Date!").WithErrorCode("400");
        }
    }
}
