using AutoMapper;
using Company.G02.BLL.interfaces;
using Company.G02.BLL.Repositories;
using Company.G02.DAL.Models;
using Company.G02.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.G02.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
            return View(employees);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Depts = await _unitOfWork.DepartmentRepository.GetAllAsync();
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);
                //var employee = new Employee()
                //{
                //    Phone = model.Phone,
                //    Name = model.Name,
                //    Address = model.Address,
                //    Age = model.Age,

                //    CreateAt = model.CreateAt,
                //    Email = model.Email,
                //    HiringDate = model.HiringDate,
                //    Salary = model.Salary,
                //    IsActive = model.IsActive,
                //    IsDeleted = model.IsDeleted,
                //    DepartmentId=model.DepartmentId,

                //};
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Detailes(int id)
        {
           var employee= await _unitOfWork.EmployeeRepository.GetAsync(id);
            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee =await _unitOfWork.EmployeeRepository.GetAsync(id);
            ViewBag.Depts = await _unitOfWork.DepartmentRepository.GetAllAsync();
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
        public async Task<IActionResult> Delete(int id)
        {
            var employee =await _unitOfWork.EmployeeRepository.GetAsync(id);
            _unitOfWork.EmployeeRepository.Delete(employee);
            return RedirectToAction("Index");
        }
    }
}
