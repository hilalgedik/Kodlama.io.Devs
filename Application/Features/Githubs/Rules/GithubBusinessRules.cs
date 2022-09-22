using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Security.Entities;

namespace Application.Features.Githubs.Rules
{
    public class GithubBusinessRules
    {

        private readonly IGithubRepository _githubRepository;


        public GithubBusinessRules(IGithubRepository githubRepository)
        {
            _githubRepository = githubRepository;
        }

        public async Task GithubUrlCanNotBeDuplicatedWhenInserted(string githubUrl)
        {
            IPaginate<Github> result =
                await _githubRepository.GetListAsync(b => b.GithubUrl == githubUrl);
            if (result.Items.Any()) throw new BusinessException("Github Url exists.");
        }

        public void GithubShouldExistWhenRequested(Github github)
        {
            
            if (github == null) throw new BusinessException("Github does not exists.");
        }

    }
}
