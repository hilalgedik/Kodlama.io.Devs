using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Technologies.Commands.UpdateTechnology
{
    public class UpdateTechnologyCommandValidator:AbstractValidator<UpdateTechnologyCommand>
    {
        public UpdateTechnologyCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
               ;

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
               ;

            RuleFor(p => p.ProgrammingLanguageId)
                .NotEmpty()
                .NotNull()
                ;

            RuleFor(d => d.ProgrammingLanguageId)
                .GreaterThan(0)
               ;
        }

    }
}
