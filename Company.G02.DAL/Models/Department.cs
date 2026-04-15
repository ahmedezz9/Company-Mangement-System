

namespace Company.G02.DAL.Models
{
    public class Department : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
