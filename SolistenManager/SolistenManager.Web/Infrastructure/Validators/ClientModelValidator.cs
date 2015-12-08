using FluentValidation;
using SolistenManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolistenManager.Web.Infrastructure.Validators
{
    public class ClientModelValidator : AbstractValidator<ClientModel>
    {
        public ClientModelValidator()
        {
            RuleFor(client => client.FirstName).NotEmpty()
                .Length(1, 100).WithMessage("Please enter a first name between 1 - 100 characters long.");

            RuleFor(client => client.LastName).NotEmpty()
                .Length(1, 100).WithMessage("Please enter a last name between 1 - 100 characters long.");

            RuleFor(client => client.Mobile).NotEmpty().Matches(@"^\d{10}$")
                .Length(10).WithMessage("Please enter a mobile number with 10 digits.");

            RuleFor(client => client.Email).NotEmpty().EmailAddress()
                .WithMessage("Please enter a valid email address.");
        }
    }
}