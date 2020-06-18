using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Dtos
{
    public class EmployeeCreateDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email inválido")]
        public string Email { get; set; }
        [Required]
        public Dept? Departament { get; set; }

        public List<IFormFile> Photos { get; set; }    
    }
}