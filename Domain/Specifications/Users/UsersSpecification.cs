using Domain.Entities.IdentityEntities;

namespace Domain.Specifications.Users
{
    public class UsersSpecification : BaseSpecification<AppUser>
    {
        public UsersSpecification() {
            AddInclude(x => x.GymMemberships);
            AddInclude(x => x.PersonalTrainer);
            AddInclude(x => x.PersonalTrainer.Gym);
            AddInclude(x => x.Photos);
        }

        public UsersSpecification(string id): base(x => x.Id == id)
        {
            AddInclude(x => x.GymMemberships);
            AddInclude(x => x.PersonalTrainer);
            AddInclude(x => x.PersonalTrainer.Gym);
            AddInclude(x => x.Photos);
        }

        public UsersSpecification(UsersParam usersParam): base(x =>
                                         (string.IsNullOrEmpty(usersParam.Search) || x.DisplayName.ToLower().Contains(usersParam.Search)) 
                                     )
        {
            AddInclude(x => x.GymMemberships);
            AddInclude(x => x.PersonalTrainer);
            AddInclude(x => x.PersonalTrainer.Gym);
            AddInclude(x => x.Photos);
            ApplyPaging(usersParam.PageSize * (usersParam.PageIndex - 1), usersParam.PageSize);
            AddOrderBy(n => n.DisplayName);
            if (!string.IsNullOrEmpty(usersParam.Sort))
            {
                switch (usersParam.Sort)
                {
                    case "emailAsc":
                        AddOrderBy(p => p.Email);
                        break;
                    case "emailDesc":
                        AddOrderByDescending(p => p.Email);
                        break;
                    case "idAsc":
                        AddOrderBy(p => p.Id);
                        break;
                    
                    default:
                        AddOrderBy(n => n.DisplayName);
                        break;
                }
            }
        }
    }
}
