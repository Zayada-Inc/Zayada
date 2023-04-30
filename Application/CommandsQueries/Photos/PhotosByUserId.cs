using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Services.Photos.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CommandsQueries.Photos
{
    public class PhotosByUserId
    {
        public class Query : IRequest<IEnumerable<Photo>>
        {
            public string UserId { get; set; }
        }
        public class Handler : IRequestHandler<Query, IEnumerable<Photo>>
        {
            private readonly DataContext _context;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUserAccesor _userAccesor;
            public Handler(DataContext context, IPhotoAccessor photoAccessor, IUserAccesor userAccesor)
            {
                _photoAccessor = photoAccessor;
                _context = context;
                _userAccesor = userAccesor;
            }
            public async Task<IEnumerable<Photo>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Include(u => u.Photos)
                                   .FirstOrDefaultAsync(u => u.Id == request.UserId);
                if (user == null)
                {
                    return Enumerable.Empty<Photo>();
                }
                return user.Photos;
            }
        }   
    }
}
