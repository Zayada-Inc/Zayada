using Application.Dtos;
using AutoMapper;
using Domain.Entities.IdentityEntities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CommandsQueries.Users
{
    public class UserEdit
    {
        public class Command : IRequest<UserToEditDto>
        {
            public UserToEditDto UserDto { get; set; }
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, UserToEditDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            public Handler(IUserRepository userRepository,IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<UserToEditDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user == null)
                {
                    // Changed exception to ArgumentException to preserve user details and stack trace.
                    throw new ArgumentException("User not found");
                }

                // Changed the name of the variable for readability purposes
                var updatedUser = _mapper.Map<AppUser>(request.UserDto);

                // Modified to keep user properties that are null or not being updated
                updatedUser.Id = user.Id;
                updatedUser.UserName ??= user.UserName;
                updatedUser.Email ??= user.Email;
                updatedUser.Bio ??= user.Bio;
                updatedUser.DisplayName ??= user.DisplayName;

                var result = await _userRepository.UpdateUserAsync(updatedUser);
                var mappedResult = _mapper.Map<UserToEditDto>(result);

                return mappedResult;
            }
        }
    }
}
