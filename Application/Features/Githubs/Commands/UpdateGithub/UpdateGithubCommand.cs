using Application.Features.Technologies.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Githubs.Dtos;
using Application.Features.Githubs.Rules;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Githubs.Commands.UpdateGithub
{
    public class UpdateGithubCommand : IRequest<UpdatedGithubDto>
    {
        public int Id { get; set; }
       
        public int UserId { get; set; }
        public string GithubUrl { get; set; }


        public class UpdateGithubCommandHandler : IRequestHandler<UpdateGithubCommand, UpdatedGithubDto>
        {
            private readonly IGithubRepository _githubRepository;
            private readonly IMapper _mapper;
            private readonly GithubBusinessRules _githubBusinessRules;


            public UpdateGithubCommandHandler(IGithubRepository githubRepository, IMapper mapper, GithubBusinessRules githubBusinessRules)
            {
                _githubRepository = githubRepository;
                _mapper = mapper;
                _githubBusinessRules = githubBusinessRules;
            }





            public async Task<UpdatedGithubDto> Handle(UpdateGithubCommand request, CancellationToken cancellationToken)
            {
                //await _technologyBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);
                Github mappedGithub = _mapper.Map<Github>(request);

                Github updatedGithub = await _githubRepository.UpdateAsync(mappedGithub);

                UpdatedGithubDto updatedGithubDto =
                    _mapper.Map<UpdatedGithubDto>(updatedGithub);
                return updatedGithubDto;

            }
        }



    }
}
