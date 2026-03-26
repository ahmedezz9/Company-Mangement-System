using Company.G02.BLL.interfaces;
using Company.G02.BLL.Repositories;
using Company.G02.DAL.Models;
using Company.G02.PL.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDto model)
        {

            if (ModelState.IsValid)
            {
                var dept = new Department()
                {
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                    Code = model.Code
                };
                departmentRepository.Add(dept);
                return RedirectToAction("Index");
            }
            return View(model);

        }

        public IActionResult Detailes(int id)
        {
           var department= departmentRepository.Get(id);

            return View(department);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = departmentRepository.Get(id);


            var updatedeptdto = new UpdateDepartementDto()
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };
                 
            return View(updatedeptdto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id,UpdateDepartementDto model)
        {
            if (ModelState.IsValid)
            {
                var department = new Department() { Id = id,Code=model.Code,Name=model.Name,CreateAt=model.CreateAt };
                    var result = departmentRepository.Update(department);


                    return RedirectToAction("Index");
                
            }
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var department = departmentRepository.Get(id);
            departmentRepository.Delete(department);
            return RedirectToAction("Index");
        }
        
    }
}
