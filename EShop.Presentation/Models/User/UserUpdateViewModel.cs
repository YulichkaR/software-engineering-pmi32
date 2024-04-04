﻿using System.ComponentModel.DataAnnotations;

namespace EShop.Presentation.Models.User;

public record UserUpdateViewModel
{
    public Guid Id { get; set; }
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
};