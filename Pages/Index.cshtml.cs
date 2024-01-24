using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DemoWeb33.Models;
using DemoWeb33.Services;

namespace DemoWeb33.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeService _employeeService;

        // 'Employees' is now initialized in the constructor.
        public List<Employee> Employees { get; private set; }

        public IndexModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            Employees = new List<Employee>(); // Initialize the list
        }

        public void OnGet()
        {
            Employees = _employeeService.GetEmployees();
        }
    }
}