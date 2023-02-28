using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications.PersonalTrainers
{
    public class PersonalTrainersSpecification : BaseSpecification<PersonalTrainer>
    {
        public PersonalTrainersSpecification() { 
        AddInclude(x => x.Gym);
        }
        public PersonalTrainersSpecification(int id): base(x => x.Id == id)
        {
        }

        public PersonalTrainersSpecification(PersonalTrainersParam personalTrainersParam): base(x =>
                                  (string.IsNullOrEmpty(personalTrainersParam.Search) || x.Name.ToLower().Contains(personalTrainersParam.Search)) &&
                                  (!personalTrainersParam.GymId.HasValue || x.GymId == personalTrainersParam.GymId)
                              )
        {
            AddInclude(x => x.Gym);
            ApplyPaging(personalTrainersParam.PageSize * (personalTrainersParam.PageIndex - 1), personalTrainersParam.PageSize);
            AddOrderBy(n => n.Name.ToLower());
            if (!string.IsNullOrEmpty(personalTrainersParam.Sort))
            {
                switch (personalTrainersParam.Sort)
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
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

    }
}
