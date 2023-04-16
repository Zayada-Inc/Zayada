using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using MediatR;

namespace Application.CommandsQueries.Gyms
{
    public class GymAddEmployee
    {
        public class Command : IRequest<EmployeeToPostDto>
        {
            public EmployeeToPostDto Employee { get; set;}
        }

        public class Handler : IRequestHandler<Command,EmployeeToPostDto>
        {
            private readonly IGymService _gymService;
            private readonly IUserAccesor _userAccesor;
           public Handler(IGymService gymService,IUserAccesor userAccesor)
            {
                _gymService = gymService;
                _userAccesor = userAccesor;
           }
            
            public async Task<EmployeeToPostDto> Handle(Command request, CancellationToken cancellationToken)
            {

                    var employeeDto = request.Employee;
                    var employee = new EmployeeToPostDto
                    {
                        GymId = employeeDto.GymId,
                        UserId = employeeDto.UserId
                    };
                    await _gymService.AddEmployeeToGymAsync(employee, _userAccesor.GetCurrentUsername());

                    return employee;
            }
        }

    }
}
