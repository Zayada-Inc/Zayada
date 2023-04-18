using Domain.Entities.IdentityEntities;

namespace Domain.Specifications.Users
{
    public class UsersWithFilterForCountSpecification : BaseSpecification<AppUser>
    {
        public UsersWithFilterForCountSpecification(UsersParam usersParam)
            : base(x =>
                                      (string.IsNullOrEmpty(usersParam.Search) || x.DisplayName.ToLower().Contains(usersParam.Search))
                                  )
        {
        }
    }
}
