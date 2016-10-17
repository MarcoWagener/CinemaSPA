using FluentValidation;
using SolistenManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolistenManager.Web.Infrastructure.Validators
{
    public class StockModelValidator : AbstractValidator<StockModel>
    {
        public StockModelValidator()
        {
            RuleFor(s => s.ID).GreaterThan(0)
                .WithMessage("Invalid stock item");

            RuleFor(s => s.UniqueKey).NotEqual(Guid.Empty)
                .WithMessage("Invalid stock item");
        }
    }
}