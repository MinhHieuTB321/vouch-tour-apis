using Application.ViewModels.Product;
using FluentValidation;

namespace WebAPI.Validations
{
    public class ProductCreateDTOValidation:AbstractValidator<CreateProductDTO>
    {
        public ProductCreateDTOValidation()
        {
            RuleFor(x=>x.ProductName).NotEmpty().NotNull().WithMessage("Name can not be null").WithErrorCode("400");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Name can not be null").WithErrorCode("400");
            RuleFor(x => x.ResellPrice).NotEmpty().NotNull().GreaterThan(x=>x.RetailPrice).WithMessage("Resell Price must higher than 0").WithErrorCode("400");
            RuleFor(x => x.RetailPrice).NotEmpty().NotNull().GreaterThan(0).WithMessage("Resell Price must higher than resell price!").WithErrorCode("400");
        }
    }

    public class ProductUpdateDTOValidation : AbstractValidator<UpdateProductDTO>
    {
        public ProductUpdateDTOValidation()
        {
            RuleFor(x => x.ProductName).NotEmpty().NotNull().WithMessage("Name can not be null").WithErrorCode("400");
            RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Name can not be null").WithErrorCode("400");
            RuleFor(x => x.ResellPrice).NotEmpty().NotNull().GreaterThan(x=>x.RetailPrice).WithMessage("Resell Price must higher than 0").WithErrorCode("400");
            RuleFor(x => x.RetailPrice).NotEmpty().NotNull().GreaterThan(0).WithMessage("Resell Price must higher than resell price!").WithErrorCode("400");
        }
    }
}
