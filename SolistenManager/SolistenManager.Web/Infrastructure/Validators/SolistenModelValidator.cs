using FluentValidation;
using SolistenManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolistenManager.Web.Infrastructure.Validators
{
    public class SolistenModelValidator : AbstractValidator<SolistenModel>
    {
        public SolistenModelValidator()
        {
            RuleFor(solisten => solisten.Description).NotEmpty().Length(1, 250)
                .WithMessage("Please enter a description.");

            RuleFor(solisten => solisten.SerialNumber).NotEmpty().Length(1, 150)
                .WithMessage("Please enter a serial number.");

            RuleFor(solisten => solisten.Owner).NotEmpty().Length(1, 100)
                .WithMessage("Please select a owner.");

            RuleFor(solisten => solisten.PurchaseDate).NotEmpty()
                .WithMessage("Please enter a date.");
        }

        //private bool ValidTrailerURI(string trailerURI)
        //{
        //    return (!string.IsNullOrEmpty(trailerURI) && trailerURI.ToLower().StartsWith("https://www.youtube.com/watch?"));
        //}
    }
}