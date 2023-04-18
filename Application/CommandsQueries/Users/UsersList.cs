using Application.Dtos;
using AutoMapper;
using Domain.Entities.IdentityEntities;
using Domain.Helpers;
using Domain.Interfaces;
using Domain.Specifications.Users;
using MediatR;

namespace Application.CommandsQueries.Users
{
    public class UsersList
    {
        public class Query : IRequest<Pagination<UserReturnDto>>
        {
            public UsersParam UsersParams { get; set; }

        }

        public class Handler : IRequestHandler<Query, Pagination<UserReturnDto>>
        {
            private readonly IUserRepository _userRepositry;
            private readonly IMapper _mapper;

            public Handler(IUserRepository userRepository, IMapper mapper)
            {
                _userRepositry = userRepository;
                _mapper = mapper;
            }

            public async Task<Pagination<UserReturnDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var spec = new UsersSpecification(request.UsersParams);
                var countSpec = new UsersWithFilterForCountSpecification(request.UsersParams);
                var totalItems = await _userRepositry.CountAsync(countSpec);
                var users = await _userRepositry.ListAsync(spec);
                var data = _mapper.Map<IReadOnlyList<AppUser>, IReadOnlyList<UserReturnDto>>(users);

                return new Pagination<UserReturnDto>(request.UsersParams.PageIndex, request.UsersParams.PageSize, totalItems, data);
            }
        }
    }
}
