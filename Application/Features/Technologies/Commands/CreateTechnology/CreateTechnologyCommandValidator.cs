using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyCommandValidator:AbstractValidator<CreateTechnologyCommand>
    {

        public  CreateTechnologyCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.ProgrammingLanguageId)
                .NotEmpty()
                .NotNull();

            RuleFor(d => d.ProgrammingLanguageId)
                .GreaterThan(0);
        }
    }
}
