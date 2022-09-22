using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Commands.UserRegister
{
    public class UserRegisterCommand : IRequest<AccessToken>
    {

        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

       
        public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, AccessToken>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _userBusinessRules;
            private readonly ITokenHelper _tokenHelper;
            private readonly IUserOperationClaimRepository _userOperationClaimRepository;

            public UserRegisterCommandHandler(IUserRepository userRepository, IMapper mapper,
                UserBusinessRules userBusinessRules, ITokenHelper tokenHelper,
                IUserOperationClaimRepository userOperationClaimRepository)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _userBusinessRules = userBusinessRules;
                _tokenHelper = tokenHelper;
                _userOperationClaimRepository = userOperationClaimRepository;
            }

            public async Task<AccessToken> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserEmailAddressCanNotBeDuplicated(request.Email);

                HashingHelper.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

                var mappedUser = _mapper.Map<User>(request);
                mappedUser.PasswordHash = passwordHash;
                mappedUser.PasswordSalt = passwordSalt;
                mappedUser.Status = true;

                var registeredUser = await _userRepository.AddAsync(mappedUser);
                await _userOperationClaimRepository.AddAsync(new UserOperationClaim
                {
                    UserId = registeredUser.Id,
                    OperationClaimId = 2
                });

                var userClaims = await _userOperationClaimRepository.GetListAsync(x =>
                        x.UserId == registeredUser.Id,
                    include: x => x.Include(c => c.OperationClaim),
                    cancellationToken: cancellationToken);

                var accessToken = _tokenHelper.CreateToken(registeredUser,
                    userClaims.Items.Select(x => x.OperationClaim).ToList());

                return accessToken;



            }
        }
    }
}
