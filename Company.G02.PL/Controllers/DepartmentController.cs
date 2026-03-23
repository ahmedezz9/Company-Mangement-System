using Company.G02.BLL.interfaces;
using Company.G02.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Company.G02.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentController(IDepartmentRepository _departmentRepository)
        {
            departmentRepository = _departmentRepository;
        }
        public IActionResult Index()
        {
           var departments= departmentRepository.GetAll();
            return View(departments);
        }
    }
}
