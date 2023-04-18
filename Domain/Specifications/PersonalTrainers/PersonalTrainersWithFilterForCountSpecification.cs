using Domain.Entities;

namespace Domain.Specifications.PersonalTrainers
{
    public class PersonalTrainersWithFilterForCountSpecification: BaseSpecification<PersonalTrainer>
    {
        public PersonalTrainersWithFilterForCountSpecification(PersonalTrainersParam personalTrainersParam)
            : base(x =>
                           (string.IsNullOrEmpty(personalTrainersParam.Search) || x.User.DisplayName.ToLower().Contains(personalTrainersParam.Search)) &&
                           (!personalTrainersParam.GymId.HasValue || x.GymId == personalTrainersParam.GymId)
                       )
        {
        }
    }
}
