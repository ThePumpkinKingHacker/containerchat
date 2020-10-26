using System;
using System.ComponentModel.DataAnnotations;

public class UserModel
{
    [Required]
    [StringLength(50)]
    public string UserName { get; set; }

    [Required]
    public byte RoleEnum { get; set; }
}