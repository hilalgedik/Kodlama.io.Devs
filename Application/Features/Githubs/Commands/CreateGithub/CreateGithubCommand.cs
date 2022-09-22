using Application.Features.Technologies.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Githubs.Dtos;
using Application.Features.Githubs.Rules;
using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Githubs.Commands.CreateGithub
{
    public class CreateGithubCommand : IRequest<CreatedGithubDto>
    {
        public int UserId { get; set; }
        public string GithubUrl { get; set; }


        public class CreateGithubCommandHandler : IRequestHandler<CreateGithubCommand, CreatedGithubDto>
        {
            private readonly IGithubRepository _githubRepository;
            private readonly IMapper _mapper;
            private readonly GithubBusinessRules _githubBusinessRules;


            public CreateGithubCommandHandler(IGithubRepository githubRepository, IMapper mapper, GithubBusinessRules githubBusinessRules)
            {
                _githubRepository = githubRepository;
                _mapper = mapper;
                _githubBusinessRules = githubBusinessRules;
            }



            public async Task<CreatedGithubDto> Handle(CreateGithubCommand request, CancellationToken cancellationToken)
            {
                //await _githubBusinessRules.TechnologyNameCanNotBeDuplicatedWhenInserted(request.Name);

                Github mappedGithub = _mapper.Map<Github>(request);
                Github createdGithub = await _githubRepository.AddAsync(mappedGithub);
                CreatedGithubDto createdGithubDto = _mapper.Map<CreatedGithubDto>(createdGithub);

                return createdGithubDto;
            }
        }



    }
}
