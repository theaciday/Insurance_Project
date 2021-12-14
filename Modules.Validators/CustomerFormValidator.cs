using BusLay;
using BusLay.Forms;
using BusLay.Services;
using FluentValidation;

namespace Modules.Validators
{
    public class CustomerFormValidator : AbstractValidator<CustomerForm>
    {
        public CustomerFormValidator(CustomerService service)
        {
            Transform(ex => ex.UserName, x => x.Trim());
            RuleFor(x => x.FirstName).MaximumLength(15).WithMessage("To long Name!!!");
            RuleFor(x => x.UserName)
                .Must(x => !service.Any(y => y.Username == x)).WithMessage("UserName already exist!!!");
        }
    }
}
