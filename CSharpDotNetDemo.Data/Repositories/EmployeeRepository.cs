using CSharpDotNetDemo.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpDotNetDemo.Data.Repositories
{
    public class EmployeeRepository
    {
        public static List<Department> GetAllDepartments()
        {
            return new List<Department>()
            {
                new Department { ID = 1, Name = "IT"},
                new Department { ID = 2, Name = "HR"},
                new Department { ID = 3, Name = "Payroll"},
            };
        }
        public static List<Employee> GetAllEmployees()
        {
            return new List<Employee>()
            {
                new Employee { ID = 1, Name = "Mark", DepartmentID = 1 },
                new Employee { ID = 2, Name = "Steve", DepartmentID = 2 },
                new Employee { ID = 3, Name = "Ben", DepartmentID = 1 },
                new Employee { ID = 4, Name = "Philip", DepartmentID = 1 },
                new Employee { ID = 5, Name = "Mary", DepartmentID = 2 },
                new Employee { ID = 6, Name = "Valarie", DepartmentID = 2 },
                new Employee { ID = 7, Name = "John", DepartmentID = 1 },
                new Employee { ID = 8, Name = "Pam", DepartmentID = 1 },
                new Employee { ID = 9, Name = "Stacey", DepartmentID = 2 },
                new Employee { ID = 10, Name = "Andy", DepartmentID = 1}
            };
        }
    }
}
