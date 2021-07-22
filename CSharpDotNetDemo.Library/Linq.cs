using CSharpDotNetDemo.Data.Models;
using CSharpDotNetDemo.Data.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpDotNetDemo.Library
{
    public class Linq
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int[] numbers_array1 = { 1, 2, 3, 4, 5 };
        int[] numbers_array2 = { 1, 3, 6, 7, 8 };

        ArrayList numbers_string = new ArrayList() { 1, "2", 3, "4", 5, "6" };
        string[] countries = { "India", "USA", "Japan", "China", "France" };
        string[] countries_dup = { "India", "uk", "USA", "Japan", "China", "usa", "France", "UK" };

        List<String> countries_List = new List<string> { "India", "USA", "Japan", "China", "France" };
        List<Department> departments = EmployeeRepository.GetAllDepartments();
        List<Employee> employees = EmployeeRepository.GetAllEmployees();

        List<Employee> employees_list1 = new List<Employee>()
                                            {
                                                new Employee { ID = 101, Name = "Mike"},
                                                new Employee { ID = 102, Name = "Susy"},
                                                new Employee { ID = 103, Name = "Mary"}
                                            };

        List<Employee> employees_list2 = new List<Employee>()
                                            {
                                                new Employee { ID = 101, Name = "Mike"},
                                                new Employee { ID = 104, Name = "John"}
                                            };
        public void Examples()
        {
            var repository = new SampleCustomerRepository();
            var customers = repository.GetCustomers();
            var customersString = JsonConvert.SerializeObject(customers, Formatting.Indented);
            Console.WriteLine(customersString);



            //A. Aggregate Operators
            //1.Min
            DateTime min_datetime = customers.Min(x => x.RegistrationDate);

            int min_number = numbers.Min();
            int min_even_number = numbers.Where(x => x % 2 == 0).Min();
            int min_country_name_length = countries.Min(x => x.Length);


            //2.Max
            int max_number = numbers.Max();
            int max_even_number = numbers.Where(x => x % 2 == 0).Max();
            int max_country_name_length = countries.Max(x => x.Length);


            //3.Sum
            int sum_of_all_numbers = numbers.Sum();
            int sum_of_all_even_numbers = numbers.Where(x => x % 2 == 0).Sum();


            //4.Count
            int count_of_numbers_in_array = numbers.Count();
            int count_of_even_numbers_in_array = numbers.Where(x => x % 2 == 0).Count();


            //5.Average
            double average_of_all_numbers = numbers.Average();
            double Average_of_all_even_numbers = numbers.Where(x => x % 2 == 0).Average();


            //6.Aggregate
            // Initially Takes First and Second elemnt in a nd b , 
            // Then Performs the oreration and stores in a
            // Then take next value in b
            // Again Perform the operation in a ......... Untill it reached to last element  of array
            string Commma_seprated_countries_list_string = countries.Aggregate((a, b) => $"{a}, {b}");
            int multiplication_of_all_numbers_in_array = numbers.Aggregate((a, b) => a * b);

            //Aggregate with Seed Parametrs 
            // It takes Seed param in a and 1st element in b
            // after that its same as above
            int sum_of_all_numbers_in_array_with_seed_Parameter = numbers.Aggregate(10, (a, b) => a + b);




            //B. Restriction /Filter Operators
            //1. Where => Pass a predicate
            var FiveStarCustomers = customers.Where(c => c.Level == 5);
            var ActiveMaleCustomer = customers.Where(c => c.IsActive && c.Gender == Gender.Male);

            IEnumerable<int> evenNubers = numbers.Where(x => x % 2 == 0);

            //Using Predicate 
            Func<int, bool> is_even_predicate = x => x % 2 == 0;
            IEnumerable<int> evenNumbers_using_Predicate = numbers.Where(is_even_predicate);

            //Using Function
            IEnumerable<int> evenNumbers_using_function = numbers.Where(x => IsEven(x));

            //Where with Index
            var OrderMorethan5_and_indexEven = customers.Where((c, index) => c.Orders.Count() > 5 && index % 2 == 0);



            var number_and_index = numbers
                                    .Select((num, index) => new { Number = num, Index = index })
                                    .Where(x => x.Number % 2 == 0)
                                    .Select(x => x.Index);

            //C. Projection Operators
            //1.Select

            IEnumerable<string> all_customer_emailIds = customers.Select(c => c.Email);

            var all_customer_name_and_gender = customers.Select(x => new { x.FirstName, x.Gender });


            //Using Extension Method
            var customer_email_lower = customers.Select
                                        (c => new
                                        {
                                            //Using Extension Method
                                            //Name = $"{ExtensionMethods.ChangeFirstLetterCase(c.FirstName)} {ExtensionMethods.ChangeFirstLetterCase(c.LastName)}",
                                            FullName = $"{c.FirstName.ChangeFirstLetterCase()} {c.LastName.ChangeFirstLetterCase()}",
                                            Email = c.Email.ToLower(),
                                            OrderCount = c.Orders.Count(),
                                            TotalOrderAmount = c.Orders.Sum(x => x.OrderValue)
                                        });
            var customer_reg_in_last_one_year = customers.Where(x => x.RegistrationDate > DateTime.Now.AddYears(-1))
                                                          .Select(x => new
                                                          {
                                                              FullName = $"{ x.FirstName} {x.LastName}",
                                                              x.Level,
                                                              x.RegistrationDate
                                                          });

            //2.SelectMany
            IEnumerable<string> all_ordered_items = customers.SelectMany(x => x.Orders.SelectMany(y => y.OrderItems.Select(x => x.Name)));
            IEnumerable<string> all_distinct_ordered_items = customers.SelectMany(x => x.Orders.SelectMany(y => y.OrderItems.Select(x => x.Name))).Distinct();


            //D.Ordering Operators
            //1.OrderBy
            var customers_orderedBy_Name_asce = customers.OrderBy(x => x.FirstName);

            //2.OrderByDescending
            var customers_orderedBy_Name_desc = customers.OrderByDescending(x => x.FirstName);

            //3.ThenBy
            var custimers_orderBy_Level_ThenBy_Name = customers.OrderBy(x => x.Level).ThenBy(y => y.UserName);

            //4.ThenByDescending
            var custimers_orderBy_Level_ThenDescendingBy_RegistartionDate = customers.OrderBy(x => x.Level).ThenByDescending(y => y.RegistrationDate);

            //5.Reverse
            var customer_list_reversed = customers.Reverse();

            //E.Partioning Operators

            //1.Take
            var first_five_customers = customers.Take(5);

            //2.Skip
            var all_customers_except_first_five = customers.Skip(3);

            //3.TakeWhile
            var all_customers_whose_name_starts_with_A = customers.TakeWhile(x => x.FirstName.StartsWith("A"));

            //4.SkipWhile
            var all_customers__except_whose_name_starts_with_A = customers.SkipWhile(x => x.FirstName.StartsWith("A"));

            //Implmenting Pagging with Skip and Take
            int PageSize = 5;
            int PageNumber = 1
                var cuustomers_for_page = customers.Skip((PageSize - 1) * PageSize).Take(PageSize);

            int BatchSize = 10;

            for (int i = 0; i < customers.Count(); i += BatchSize)
            {
                var batch = customers.Skip(i).Take(BatchSize);
            }

            //F.Conversion Operators
            //1.ToList
            List<int> num_list = numbers.ToList();

            //2.ToArray
            string[] countries_array_sorted = countries_List.OrderBy(x => x).ToArray();
            //3.ToDictionary =>Key must be unique within a dict, else it will throw an exception
            //Both Key and Value
            Dictionary<Guid, String> Cust_userName_dict = customers.ToDictionary(x => x.Id, x => x.UserName);

            //Only Key , Value will be enitre customer object
            Dictionary<Guid, Customer> cust_dict = customers.ToDictionary(x => x.Id);

            //4.ToLookup
            ILookup<int, Customer> customer_lookup = customers.ToLookup(x => x.Level);

            //5.Cast
            IEnumerable<int> int_num_list_cast = numbers_string.Cast<int>();
            // Output 1,2,3,4,5,6

            //6.OffType
            IEnumerable<int> int_num_list_typeoff = numbers_string.OfType<int>();
            //Output: 1,3,5

            //7.AsEnumerable
            // Generates the SQL Query with Where, OrderBy & Take, So feches only the required data, everthing is executed in SQL 
            var customer_without_AsEnumerable = customers
                                                    .Where(x => x.Gender == Gender.Male)
                                                    .OrderBy(x => x.Orders.Count())
                                                    .Take(5);

            // Generates the SQL Query without Where, OrderBy & Take, so fetched all data, after that everthing is executed in memory objects 
            var customer_with_AsEnumerable = customers
                                                    .AsEnumerable()
                                                    .Where(x => x.Gender == Gender.Male)
                                                    .OrderBy(x => x.Orders.Count())
                                                    .Take(5);


            //8.AsQuerable
            //TODO: to be discussed later

            //G.Grouping Operators
            //1.GroupBy
            //
            var customers_grouped_by_level = customers.GroupBy(x => x.Level);

            foreach (var group in customers_grouped_by_level)
            {
                Console.WriteLine($"Level: {group.Key} ");
                Console.WriteLine($"Group Count: {group.Count()}");
                Console.WriteLine($"Total male in each group: {group.Count(x => x.Gender == Gender.Male)}");
                Console.WriteLine($"Group Total Order Value {group.Sum(x => x.Orders.Sum(y => y.OrderValue))}");

            }
            //Group and Select Anonymous Object for each group
            var Customers_group_by_level_and_sorted = customers.GroupBy(x => x.Level)
                                                              .OrderBy(group => group.Key)
                                                              .Select(x => new
                                                              {
                                                                  Level = x.Key,
                                                                  Customers = x.OrderBy(c => c.FirstName),
                                                                  Count = x.Count()
                                                              });

            //GroupBy Multiple Keys
            var customers_groupby_level_and_then_gender = customers.GroupBy(x => new { x.Level, x.Gender })
                                                                   .OrderBy(group => group.Key.Level).ThenBy(group => group.Key.Gender)
                                                                   .Select(x => new
                                                                   {
                                                                       Level = x.Key.Level,
                                                                       Gender = x.Key.Gender,
                                                                       Customers = x.OrderBy(c => c.FirstName),
                                                                       Count = x.Count()
                                                                   });

            //H.Element Operators

            //1.First
            Customer first_cust = customers.First();
            Customer first_male_cust = customers.First(x => x.Gender == Gender.Male);

            //2.FirstOrDefault
            Customer firstOrDefault_cust = customers.FirstOrDefault();
            Customer firstOrDefault_male_cust = customers.FirstOrDefault(x => x.Gender == Gender.Male);

            //3.Last
            Customer last_cust = customers.Last();
            Customer last_male_cust = customers.Last(x => x.Gender == Gender.Male);

            //4.LastOrDefault
            Customer lastOrDefault_cust = customers.LastOrDefault();
            Customer lastOrDefault_male_cust = customers.LastOrDefault(x => x.Gender == Gender.Male);

            //5.ElementAt
            Customer elemntAt_five_cust = customers.ElementAt(5);

            //6.ElementAtOrDefault
            Customer elemntAtOrDefault_five_cust = customers.ElementAtOrDefault(5000);

            //7.Single
            Customer single_cust = customers.Single();
            Customer single_male_cust = customers.Single(x => x.Gender == Gender.Male);
            //8.SingleOrDefault
            Customer singleOrDefault_cust = customers.SingleOrDefault();
            Customer singleOrDefault_male_cust = customers.SingleOrDefault(x => x.Gender == Gender.Male);

            //9.DefaultIfEmpty
            IEnumerable<Customer> defaultOrEmpty_customers = customers.DefaultIfEmpty();

            IEnumerable<Customer> defaultOrEmpty_with_default_customers = customers.DefaultIfEmpty(
                                                                                        new Customer()
                                                                                        {
                                                                                            FirstName = "default Customer",
                                                                                            LastName = "default Customer"
                                                                                        });


            //I.Join Operators
            //1.Group Join
            var employeesByDepartment = departments.GroupJoin(employees,
                                                     d => d.ID,
                                                     e => e.DepartmentID,
                                                     (department, employees) => new
                                                     {
                                                         Department = department,
                                                         Employees = employees
                                                     });

            foreach (var department in employeesByDepartment)
            {
                Console.WriteLine(department.Department.Name);
                foreach (var employee in department.Employees)
                {
                    Console.WriteLine(" " + employee.Name);
                }
                Console.WriteLine();
            }

            //2.Inner Join
            var emp_join_dep = employees.Join(departments,
                e => e.DepartmentID,
                d => d.ID,
                (emp, dep) => new
                {
                    EmployeeName = emp.Name,
                    DepartmentName = dep.Name
                });

            foreach (var employee in emp_join_dep)
            {
                Console.WriteLine(employee.EmployeeName + "\t" + employee.DepartmentName);
            }

            //3.Left Outer Join
            var emp_leftOuterJoin_dep = employees
                        .GroupJoin(departments,
                                e => e.DepartmentID,
                                d => d.ID,
                                (emp, depts) => new { emp, depts })
                        .SelectMany(z => z.depts.DefaultIfEmpty(),
                                (a, b) => new
                                {
                                    EmployeeName = a.emp.Name,
                                    DepartmentName = b == null ? "No Department" : b.Name
                                });

            foreach (var v in emp_leftOuterJoin_dep)
            {
                Console.WriteLine(" " + v.EmployeeName + "\t" + v.DepartmentName);
            }

            //4.Cross Join
            var epm_cossJoin_dep_SelectMany = employees
                        .SelectMany(e => departments, (e, d) => new { e, d });

            foreach (var v in epm_cossJoin_dep_SelectMany)
            {
                Console.WriteLine(v.e.Name + "\t" + v.d.Name);
            }


            var epm_cossJoin_dep_Join = employees
                                     .Join(departments,
                                               e => true,
                                               d => true,
                                               (e, d) => new { e, d });
            foreach (var v in epm_cossJoin_dep_Join)
            {
                Console.WriteLine(v.e.Name + "\t" + v.d.Name);
            }

            //J.Set Operators
            //1.Distinct
            var distinct_countries_CaseSensetive = countries_dup.Distinct();
            var distinct_countries_IgnoreCase = countries_dup.Distinct(StringComparer.OrdinalIgnoreCase);

            var distinct_employee = employees.Distinct();
            var distinct_employy_comparer = employees.Distinct(new EmployeeComparer());
            var distinct_employee_Select = employees.Select(x => new { x.ID, x.Name }).Distinct();




            //2.Union
            var countries_union = countries.Union(countries_dup);
            var employee_union = employees_list1.Union(employees_list2);
            var emploee_union_with_projection = employees_list1.Select(e1 => new { e1.ID, e1.Name })
                                                               .Union(employees_list2.Select(e2 => new { e2.ID, e2.Name }));


            //3.Intersect
            var num1_Intercept_num2 = numbers_array1.Intersect(numbers_array2);

            //4.Except
            var num1_Except_num2 = numbers_array1.Except(numbers_array2);

            //K.Generation Operators
            //1.Range 
            IEnumerable<int> range_int_1to10 = Enumerable.Range(1, 10);
            IEnumerable<int> range_even_int_1to10 = Enumerable.Range(1, 10).Where(is_even_predicate);

            //2.Repeat
            IEnumerable<string> repeat_hello_5times = Enumerable.Repeat("Hello", 10);

            //3.Empty
            IEnumerable<int> empty_int = Enumerable.Empty<int>();
            IEnumerable<string> empty_string = Enumerable.Empty<string>();




            //L. Concat Operator
            IEnumerable<int> concat_num = numbers_array1.Concat(numbers_array2);

            //M. SequenceEqual Operator
            bool is_numarray_equal = numbers_array1.SequenceEqual(numbers_array2,);
            bool is_countriesString_Equal = countries.SequenceEqual(countries_dup);
            bool is_countriesString_Equa_Ignorecasel = countries.SequenceEqual(countries_dup, StringComparer.OrdinalIgnoreCase);

            //N. Quantifier Operators
            //1.All
            bool is_all_element_lessthan_10 = numbers_array1.All(x => x < 10);
            //2.Any
            bool is_any_elemnt_in_list = numbers_array1.Any();
            bool is_any_element_greaterthan100 = numbers_array1.Any(x => x > 100);

            //3.Contains
            bool conatins_India = countries.Contains("India");
            bool contains_India_IgnoreCase = countries.Contains("India", StringComparer.OrdinalIgnoreCase);





        }

        private bool IsEven(int x)
        {
            //if (x % 2 == 0)
            //    return true;
            //else return false;

            return x % 2 == 0;
        }
    }
}
