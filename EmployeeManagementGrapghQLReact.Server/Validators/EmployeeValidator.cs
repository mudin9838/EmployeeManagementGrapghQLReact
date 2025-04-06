using EmployeeManagementGrapghQLReact.Server.Data;
using FluentValidation;

namespace EmployeeManagementGrapghQLReact.Server.Validators;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
    }
}

