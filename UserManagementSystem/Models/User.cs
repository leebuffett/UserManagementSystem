//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assignment.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    public partial class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please fill-in username.")]
        [RegularExpression("^[a-zA-Z0-9]{8,15}$", ErrorMessage = "Username must be 8-15 characters, and include letters and numbers.")] //length and alphanumeric
        [Remote("IsUserNameExists", "User", ErrorMessage = "This username has already been registered. Please enter a different username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please fill-in employee ID.")]
        [Range(1, 10000000000, ErrorMessage = "Employee Id must be 1-10 numbers.")]
        // [MaxLength(10, ErrorMessage = "Employee Id must be 1-10 numbers.")]
        [Display(Name = "Employee Id")]
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Employee ID must be numbers.")]
        [Remote("IsEmployeeIdExist", "User", ErrorMessage = "This employee ID has already been registered. Please enter a different employee ID.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please fill-in valid email address.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "This email address is not valid")]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [MaxLength(20, ErrorMessage = "Full name must not exceed 20 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please fill-in password.")]
        [RegularExpression("^[a-zA-Z0-9]{8,15}$", ErrorMessage = "Password  must be 8-15 characters, and include letters and numbers.")] //length and alphanumeric
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please fill-in confirm password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password fields did not match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please fill-in join date.")]
        [Display(Name = "Join Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime JoinDate { get; set; }

        [Required(ErrorMessage = "Please select a position.")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "Please select a team.")]
        public int TeamId { get; set; }

        [Required(ErrorMessage = "Please fill-in Security Phrase.")]
        [MaxLength(40, ErrorMessage = "Security phrase must not exceed 40 characters")]
        [Display(Name = "Security Phrase")]
        public string Security { get; set; }
        public int StatusId { get; set; }
        public string LoginStatus { get; set; }
        public string SessionId { get; set; }
        public int FailedLogin { get; set; }
        public virtual Position Position { get; set; }
        public virtual Status Status { get; set; }
        public virtual Team Team { get; set; }
    }
}