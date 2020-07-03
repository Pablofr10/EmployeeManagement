using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Dtos
{
    public class EmployeeEditDto : EmployeeCreateDto
    {
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
