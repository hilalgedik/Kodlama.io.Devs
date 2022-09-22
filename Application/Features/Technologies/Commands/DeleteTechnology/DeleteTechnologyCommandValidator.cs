using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Technologies.Commands.DeleteTechnology
{
    public class DeleteTechnologyCommandValidator:AbstractValidator<DeleteTechnologyCommand>
    {

        public DeleteTechnologyCommandValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty()
                .NotNull()
               ;

            RuleFor(d => d.Id)
                .GreaterThan(0)
                ;
        }

    }
}
