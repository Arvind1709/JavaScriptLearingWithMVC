﻿using System.ComponentModel.DataAnnotations;

namespace JavaScriptLearingWithMVC.Models
{
    public class UserModel
    {
        //public int Id { get; set; } // Primary Key
        //public string Username { get; set; } = string.Empty;
        //public string Email { get; set; } = string.Empty;
        //public string PasswordHash { get; set; } = string.Empty;
        //public string Role { get; set; } = "Customer"; // Default role
        //public string FirstName { get; set; } = string.Empty;
        //public string LastName { get; set; } = string.Empty;
        //public string PhoneNumber { get; set; } = string.Empty;
        //public DateTime DateCreated { get; set; } = DateTime.Now;
        //public bool IsActive { get; set; } = true; // Active by default

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = "Customer"; // Default role

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true; // Active by default
    }



}
