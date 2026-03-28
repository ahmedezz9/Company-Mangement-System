using Company.G02.BLL.interfaces;
using Company.G02.BLL.Repositories;
using Company.G02.DAL.Models;
using Company.G02.PL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace Company.G02.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentController(IDepartmentRepository _departmentRepository)
        {
            departmentRepository = _departmentRepository;
        }
        public async Task<IActionResult> Index()
        {
           var departments= await departmentRepository.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto model)
        {

            if (ModelState.IsValid)
            {
                var dept = new Department()
                {
                    Name = model.Name,
                    CreateAt = model.CreateAt,
                    Code = model.Code
                };
                await departmentRepository.AddAsync(dept);
                return RedirectToAction("Index");
            }
            return View(model);

        }

        public async Task<IActionResult> Detailes(int id)
        {
           var department= await departmentRepository.GetAsync(id);

            return View(department);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await departmentRepository.GetAsync(id);


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
        public async Task<IActionResult> Delete(int id)
        {
            var department =await  departmentRepository.GetAsync(id);
            departmentRepository.Delete(department);
            return RedirectToAction("Index");
        }
        
    }
}
