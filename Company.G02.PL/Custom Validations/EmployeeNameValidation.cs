using Company.G02.DAL.Data.Contexts;
using System.ComponentModel.DataAnnotations;

namespace Company.G02.PL.Custom_Validations
{
    public class EmployeeNameValidation : ValidationAttribute
    {
       
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = (CompanyDbContext)validationContext.GetService(typeof(CompanyDbContext))!;

            var name = value as string;

            bool flag = context.Employees.Any(x => x.Name == name);
            if (flag) 
            {
                return new ValidationResult("Name Should Be Unique");
            }
           
                return ValidationResult.Success;
            

        }
    }
}
