using Company.G02.BLL.interfaces;
using Company.G02.DAL.Data.Contexts;
using Company.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G02.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _context;

        public DepartmentRepository(CompanyDbContext context)
        {
            _context = context;
        }
       
        

        public IEnumerable<Department> GetAll()
        {
          return  _context.Departments.ToList();
        }
        public Department? Get(int id)
        {
          return  _context.Departments.Find(id);
        }

        public int Add(Department department)
        {
            _context.Departments.Add(department);
           int count = _context.SaveChanges();
            return count;
        }

        public int Update(Department department)
        {
           
            _context.Departments.Update(department);
           return _context.SaveChanges();
        }
        public int Delete(Department department)
        {
            _context.Departments.Remove(department);
           return _context.SaveChanges();
        }
    }
}
