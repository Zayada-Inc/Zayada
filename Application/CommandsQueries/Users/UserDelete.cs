using Domain.Interfaces;
using Infrastructure.Services.Photos.Interfaces;
using MediatR;
using Persistence;

namespace Application.CommandsQueries.Users
{
    public class UserDelete
    {
        public class Command : IRequest<bool>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command,bool>
        {
            private readonly IUserRepository _repo;
            private readonly IPhotoAccessor _photoAccessor;
            public Handler(IUserRepository repo, IPhotoAccessor photoAccessor)
            {
                _photoAccessor = photoAccessor;
                _repo = repo;
            }
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _repo.GetByIdAsync(request.Id);
                if (user == null)
                    throw new Exception("Could not find user");
                var result = await _repo.DeleteUser(user.Id);
                if (result.Id == null)
                    throw new Exception("Problem saving changes");

                var photosId = user.Photos.AsEnumerable();

                if (photosId != null)
                foreach( var photo in photosId)
                {
                   await _photoAccessor.DeletePhoto(photo.Id);
                }

                if(result != null)
                return true;

                return false;
            }
        }
    }
}
