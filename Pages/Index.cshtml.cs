using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DemoWeb33.Models;
using DemoWeb33.Services;

namespace DemoWeb33.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeService _employeeService;

        public IndexModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public List<Employee> Employees;
        public void OnGet()
        {
            Employees = _employeeService.GetEmployees();
        }
    }
}