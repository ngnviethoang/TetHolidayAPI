using System;
using System.ComponentModel.DataAnnotations;

namespace TetHolidayAPI;

public class User
{
    public Guid Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; }

    [Required, MaxLength(255)]
    public string AccountNumber { get; set; }

    [Required, MaxLength(255)]
    public string Wish { get; set; }

    [Required, MaxLength(255)]
    public string ImageName { get; set; }

    [Required, MaxLength(255)]
    public string LuckyMoney { get; set; }
}