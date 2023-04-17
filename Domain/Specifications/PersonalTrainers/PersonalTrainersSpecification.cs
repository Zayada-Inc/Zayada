using Domain.Entities;

namespace Domain.Specifications.PersonalTrainers
{
    public class PersonalTrainersSpecification : BaseSpecification<PersonalTrainer>
    {
        public PersonalTrainersSpecification() { 
        AddInclude(x => x.Gym);
        AddInclude(x => x.User);
        AddInclude(x => x.User.Photos);
        }
        public PersonalTrainersSpecification(int id): base(x => x.Id == id)
        {
            AddInclude(x => x.Gym);
            AddInclude(x => x.User);
            AddInclude(x => x.User.Photos);
        }

        public PersonalTrainersSpecification(PersonalTrainersParam personalTrainersParam): base(x =>
                                  (string.IsNullOrEmpty(personalTrainersParam.Search) || x.User.DisplayName.ToLower().Contains(personalTrainersParam.Search)) &&
                                  (!personalTrainersParam.GymId.HasValue || x.GymId == personalTrainersParam.GymId)
                              )
        {
            AddInclude(x => x.Gym);
            AddInclude(x => x.User);
            AddInclude(x => x.User.Photos);
            ApplyPaging(personalTrainersParam.PageSize * (personalTrainersParam.PageIndex - 1), personalTrainersParam.PageSize);
            AddOrderBy(n => n.User.DisplayName);
            if (!string.IsNullOrEmpty(personalTrainersParam.Sort))
            {
                switch (personalTrainersParam.Sort)
                {
                    case "emailAsc":
                        AddOrderBy(p => p.User.Email);
                        break;
                    case "emailDesc":
                        AddOrderByDescending(p => p.User.Email);
                        break;
                    case "idAsc":
                        AddOrderBy(p => p.Id);
                        break;
                    
                    default:
                        AddOrderBy(n => n.User.DisplayName);
                        break;
                }
            }
        }

    }
}
