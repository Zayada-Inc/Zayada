using Domain.Entities;
using System.Collections.ObjectModel;

namespace Application.Dtos
{
    public class UserDto
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }
}
