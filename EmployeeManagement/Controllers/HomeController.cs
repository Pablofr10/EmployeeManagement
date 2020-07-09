using System;
using System.IO;
using EmployeeManagement.Models;
using EmployeeManagement.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository,
            IHostingEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index()
        {
            var modal = _employeeRepository.GetAllEmployee();
            return View(modal);
        }
        public ViewResult Details(int? id)
        {
            Employee employee = _employeeRepository.GetEmployee(id.Value);

            if (employee == null)
            {
                Response.StatusCode = 400;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDto homeDto = new HomeDto()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };
            
            return View(homeDto);
        }
        
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }


        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditDto employeeEditDto = new EmployeeEditDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Departament = employee.Departament,
                ExistingPhotoPath = employee.PhotoPath
            };

            return View(employeeEditDto);
        }


        [HttpPost]
        public IActionResult Create(EmployeeCreateDto model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photos != null && model.Photos.Count > 0)
                {
                    foreach (IFormFile photo in model.Photos)
                    {
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    
                }

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Departament = model.Departament,
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.Add(newEmployee);

                return RedirectToAction("details", new {id = newEmployee.Id});
            }

            return View();
        }
    }
    
}