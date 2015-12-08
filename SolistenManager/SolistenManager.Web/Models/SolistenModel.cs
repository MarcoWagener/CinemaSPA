using SolistenManager.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SolistenManager.Web.Models
{
    [Bind(Exclude = "Image")]
    public class SolistenModel : IValidatableObject
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string SerialNumber { get; set; }
        public string Owner { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime PurchaseDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new SolistenModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}