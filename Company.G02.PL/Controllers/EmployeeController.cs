using Company.G02.BLL.interfaces;
using Company.G02.BLL.Repositories;
using Company.G02.DAL.Models;
using Company.G02.PL.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Depts = _unitOfWork.DepartmentRepository.GetAll();
           
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Phone = model.Phone,
                    Name = model.Name,
                    Address = model.Address,
                    Age = model.Age,

                    CreateAt = model.CreateAt,
                    Email = model.Email,
                    HiringDate = model.HiringDate,
                    Salary = model.Salary,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    DepartmentId=model.DepartmentId,

                };
                _unitOfWork.EmployeeRepository.Add(employee);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public IActionResult Detailes(int id)
        {
           var employee= _unitOfWork.EmployeeRepository.Get(id);
            return View(employee);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(id);
            return View(employee);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int id,Employee model)
        {
            if (ModelState.IsValid)
            {
                if(id == model.Id)
                {
                   _unitOfWork.EmployeeRepository.Update(model);
                    return RedirectToAction("Index");
                }
                
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Get(id);
            _unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction("Index");
        }
    }
}
