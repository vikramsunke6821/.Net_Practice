﻿using System.ComponentModel.DataAnnotations;

namespace EFDemo.Models
{
    public class AddEmployeeViewModel
    {
        [Key]
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public string? Number { get; set; }
    }
}

