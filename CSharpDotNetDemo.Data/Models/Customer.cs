using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpDotNetDemo.Data.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public int Level { get; set; }
        public IEnumerable<Order> Orders { get; set; }



    }
}
