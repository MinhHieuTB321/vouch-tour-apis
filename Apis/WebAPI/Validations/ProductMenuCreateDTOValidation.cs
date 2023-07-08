using Application.ViewModels.MenuDTOs;
using Application.ViewModels.ProductInMenuDTOs;
using FluentValidation;

namespace WebAPI.Validations
{
    public class ProductMenuCreateDTOValidation: AbstractValidator<ProductMenuCreateDTO>
    {
        public ProductMenuCreateDTOValidation()
        {
            RuleFor(x => x.ActualPrice).NotNull().NotEmpty().GreaterThan(x => x.SupplierPrice).WithMessage("ActualPrice must greater than SupplierPrice").WithErrorCode("400");
            RuleFor(x => x.SupplierPrice).NotNull().NotEmpty().GreaterThan(0).WithMessage("SupplierPrice must greater than 0").WithErrorCode("400");
        }
    }

    public class MenuCreateDTOValidation : AbstractValidator<MenuCreateDTO>
    {
        public MenuCreateDTOValidation()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Title can not be empty!").WithErrorCode("400");
        }
    }
}
