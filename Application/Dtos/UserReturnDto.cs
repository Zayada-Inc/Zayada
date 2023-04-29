using Domain.Entities;

namespace Application.Dtos
{
    public class UserReturnDto
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public PersonalTrainersAdminToReturnDto PersonalTrainer { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }
}
