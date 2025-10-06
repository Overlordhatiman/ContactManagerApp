using ContactManager.Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Core.Validators
{
    public class ContactDtoValidator : AbstractValidator<ContactDto>
    {
        public ContactDtoValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(c => c.DateOfBirth)
                .NotEmpty().WithMessage("Date of Birth is required.")
                .LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.");

            RuleFor(c => c.Married)
                .NotNull().WithMessage("Married field must be specified (true/false).");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9\s\-]{7,20}$").WithMessage("Phone number format is invalid.");

            RuleFor(c => c.Salary)
                .GreaterThanOrEqualTo(0).WithMessage("Salary must be non-negative.");
        }
    }
}