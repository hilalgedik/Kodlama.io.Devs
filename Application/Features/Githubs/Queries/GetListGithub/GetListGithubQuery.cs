using Application.Features.Technologies.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Githubs.Models;
using Application.Features.Technologies.Queries.GetListTechnology;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Githubs.Queries.GetListGithub
{
    public class GetListGithubQuery : IRequest<GithubListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListGithubQueryHandler : IRequestHandler<GetListGithubQuery, GithubListModel>
        {
            private readonly IMapper _mapper;
            private readonly IGithubRepository _githubRepository;

            public GetListGithubQueryHandler(IMapper mapper, IGithubRepository githubRepository)
            {
                _mapper = mapper;
                _githubRepository = githubRepository;
            }

            public async Task<GithubListModel> Handle(GetListGithubQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Github> githubs = await _githubRepository.GetListAsync(include:
                    m => m.Include(c => c.User),
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize
                );

                GithubListModel mappedGithubs = _mapper.Map<GithubListModel>(githubs);
                return mappedGithubs;
            }
        }

    }
}
