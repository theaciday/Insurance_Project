using BusLay.Forms;
using BusLay.Services;
using FluentValidation;

namespace Modules.Validators
{
    public class RegisterFormValidator : AbstractValidator<RegisterForm>
    {
        public RegisterFormValidator(CustomerService service)
        {
            RuleFor(form => form.UserName.Trim().ToLower());
            RuleFor(form => form.Password).NotNull();

        }
    }
}
