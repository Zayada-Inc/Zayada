using Application.Interfaces;
using Application.Services.Photos.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandsQueries.Photos
{
    public class SetMainPhoto
    {
        
        public class Command : IRequest<Photo>
        {
            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Command,Photo>
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
            public async Task<Photo> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users.Include(p => p.Photos)
                    .SingleOrDefaultAsync(x => x.Id == _userAccesor.GetCurrentUsername());

                if (user == null)
                {
                    throw new Exception("User not found!");
                }

                var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

                if (photo == null)
                {
                    throw new Exception("Photo not found");
                }

                var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
                if (currentMain != null) currentMain.IsMain = false;
                photo.IsMain = true;

                var success = await _context.SaveChangesAsync() > 0;

                if (success)  return photo;

                throw new Exception("Problem saving changes");

            }
        }
    }
}
