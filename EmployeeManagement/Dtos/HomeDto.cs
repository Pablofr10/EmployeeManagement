using EmployeeManagement.Models;

namespace EmployeeManagement.Dtos
{
    public class HomeDto
    {
        public Employee Employee { get; set; }
        public string PageTitle { get; set; }
    }
}