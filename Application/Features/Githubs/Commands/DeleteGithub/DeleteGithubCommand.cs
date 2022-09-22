using Application.Features.Technologies.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Githubs.Dtos;
using Application.Features.Githubs.Rules;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Githubs.Commands.DeleteGithub
{
    public class DeleteGithubCommand : IRequest<DeletedGithubDto>
    {
        public int Id { get; set; }
       
        public class DeleteGithubCommandHandler : IRequestHandler<DeleteGithubCommand, DeletedGithubDto>
        {
            private readonly IGithubRepository _githubRepository;
            private readonly IMapper _mapper;
            private readonly GithubBusinessRules _githubBusinessRules;


            public DeleteGithubCommandHandler(IGithubRepository githubRepository,
                IMapper mapper, GithubBusinessRules githubBusinessRules)
            {
                _githubRepository = githubRepository;
                _mapper = mapper;
                _githubBusinessRules = githubBusinessRules;
            }



            public async Task<DeletedGithubDto> Handle(DeleteGithubCommand request, CancellationToken cancellationToken)
            {
                Github mappedGithub = _mapper.Map<Github>(request);
                Github deletedGithub =
                    await _githubRepository.DeleteAsync(mappedGithub);
                DeletedGithubDto deletedGithubDto =
                    _mapper.Map<DeletedGithubDto>(deletedGithub);
                return deletedGithubDto;
            }
        }

    }
}
