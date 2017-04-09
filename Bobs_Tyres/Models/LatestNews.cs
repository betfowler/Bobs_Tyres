using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bobs_Tyres.Models
{
    [Validator(typeof(LatestNewsValidator))]
    public class LatestNews
    {
        [Key]
        public int LatestNewsID { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string ButtonLink { get; set; }
    }

    public class LatestNewsValidator : AbstractValidator<LatestNews>
    {
        public LatestNewsValidator()
        {
            RuleFor(ln => ln.Title).NotNull().WithMessage("News item title is required");
            RuleFor(ln => ln.Description).NotNull().WithMessage("News item description is required");
            RuleFor(ln => ln.Description).Length(1, 120).WithMessage("Description too long, it must be less than 120 characters");
        }

    }
}