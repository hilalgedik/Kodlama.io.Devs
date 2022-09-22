using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Persistence.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.UserLogin
{
    public class UserLoginCommand:IRequest<AccessToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public class UserLoginCommandHandler:IRequestHandler<UserLoginCommand,AccessToken>
        {

            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;


            public UserLoginCommandHandler(IUserRepository userRepository, UserBusinessRules userBusinessRules, IMapper mapper, ITokenHelper tokenHelper, IUserOperationClaimRepository userOperationClaimRepository)
            {
                _userRepository = userRepository;
                _userBusinessRules = userBusinessRules;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<AccessToken> Handle(UserLoginCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(x => x.Email == request.Email);

                _userBusinessRules.CheckIfUserExists(user);
                _userBusinessRules.CheckIfPasswordIsCorrect(request.Password, user.PasswordHash, user.PasswordSalt);

                var userClaims = await _userOperationClaimRepository.GetListAsync(x =>
                        x.UserId == user.Id,
                    include: x => x.Include(c => c.OperationClaim),
                    cancellationToken: cancellationToken);

                var accessToken = _tokenHelper.CreateToken(user, userClaims.Items.Select(x => x.OperationClaim).ToList());

                return accessToken;

            }
        }
    }
}
