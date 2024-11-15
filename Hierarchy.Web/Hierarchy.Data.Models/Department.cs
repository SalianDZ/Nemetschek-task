﻿using System.ComponentModel.DataAnnotations;
using static Hierarchy.Common.EntityValidations;

namespace Hierarchy.Data.Models
{
    public class Department
    {
        public Department()
        {
            Id = Guid.NewGuid();
            Employees = new HashSet<Employee>();
        }


        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public ICollection<Employee> Employees { get; set; }
    }
}