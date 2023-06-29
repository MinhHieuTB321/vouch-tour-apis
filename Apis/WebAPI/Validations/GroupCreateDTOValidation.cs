using Application.ViewModels.GroupDTOs;
using FluentValidation;

namespace WebAPI.Validations
{
    public class GroupCreateDTOValidation:AbstractValidator<GroupCreateDTO>
    {
        public GroupCreateDTOValidation()
        {
            //RuleFor(x=>x.GroupName)
        }
    }
}
