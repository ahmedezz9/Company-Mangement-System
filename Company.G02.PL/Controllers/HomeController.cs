using Company.G02.BLL.interfaces;
using Company.G02.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Company.G02.PL.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();



            ViewBag.EmployeeCount = employees.Count();
            ViewBag.DepartmentCount = departments.Count();
            ViewBag.RecentEmployees = employees.TakeLast(4);// آخر 4 موظفين
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
