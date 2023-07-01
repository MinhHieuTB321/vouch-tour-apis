using Application.ViewModels.CategoryDTO;
using FluentValidation;

namespace WebAPI.Validations
{
    public class CategoryCreateDTOValidation: AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidation()
        {
            //RuleFor(x => x.CategoryName).NotEmpty().NotEmpty().Matches("^[a-zA-Z]+((\\w\\  *)[a-zA-Z0-9]+)+$").WithMessage("Name can not be empty").WithErrorCode("400");
        }
    }
}
