using BusLay.Forms;
using BusLay.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Validators
{
    internal class InsuranceFormValidator : AbstractValidator<InsuranceForm>
    {
        public InsuranceFormValidator(InsuranceService service)
        {
            Transform(ex => ex.InsurType, x => x.Trim());
            Transform(ex => ex.InsurName, x => x.Trim());
            RuleFor(x => x.InsurName).MaximumLength(15).WithMessage("To long Name!!!");
            RuleFor(x => x.InsurName)
                .Must(x => !service.Any(y => y.InsurName == x)).WithMessage("Insurance name already exist!!!");
        }
    }
}
