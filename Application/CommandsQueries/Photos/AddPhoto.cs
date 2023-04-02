using Application.Interfaces;
using Application.Services.Photos.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.CommandsQueries.Photos
{
    public class AddPhoto
    {
        public class Command : IRequest<Photo>
        {
            public IFormFile File { get; set; }
        }

        public class Handler : IRequestHandler<Command, Photo>
        {
            private readonly DataContext _context;
            private readonly IPhotoAccessor _photoAccessor;
            private readonly IUserAccesor _userAccesor;
            public Handler(DataContext context, IPhotoAccessor photoAccessor,IUserAccesor userAccesor)
            {
                _photoAccessor = photoAccessor;
                _context = context;
                _userAccesor = userAccesor;
            }

            public async Task<Photo> Handle(Command request, CancellationToken cancellationToken)
            {
               var user = await _context.Users.Include(p => p.Photos)
                    .SingleOrDefaultAsync(x => x.Id == _userAccesor.GetCurrentUsername());
                var tst = _userAccesor.GetCurrentUsername();
                if (user == null)
                {
                    return null;
                }

                var photoUploadResult = await _photoAccessor.AddPhoto(request.File);
                var photo = new Photo
                {
                    Url = photoUploadResult.Url,
                    Id = photoUploadResult.PublicId
                };

                if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;

                user.Photos.Add(photo);

                var result = await _context.SaveChangesAsync() > 0;
                if (result) return photo;

                return null;
            }
        }
    }
}
