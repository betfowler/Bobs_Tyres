using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Bobs_Tyres.Models
{
    [Validator(typeof(ContactValidator))]
    public class Contact
    {
        [Display(Name = "Name")]
        public string customerName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string emailAddress { get; set; }
        [Display(Name = "Contact Number")]
        public string contactNumber { get; set; }
        [Display(Name = "Enquiry")]
        [DataType(DataType.MultilineText)]
        public string message { get; set; }
    }
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(c => c.customerName).NotNull().WithMessage("Your name is required");
            RuleFor(c => c.contactNumber).NotNull().Unless(c => !string.IsNullOrEmpty(c.emailAddress)).WithMessage("Please leave a contact number or email address");
            RuleFor(c => c.emailAddress).NotNull().Unless(c => !string.IsNullOrEmpty(c.contactNumber)).WithMessage("Please leave a contact number or email address");
            RuleFor(c => c.message).NotNull().WithMessage("Please enter a message");
        }

    }
}