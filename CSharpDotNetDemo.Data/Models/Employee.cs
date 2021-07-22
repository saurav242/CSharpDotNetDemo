using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpDotNetDemo.Data.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int DepartmentID { get; set; }

        public override bool Equals(object obj)
        {
            return this.ID == ((Employee)obj).ID && this.Name == ((Employee)obj).Name;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode() ^ this.Name.GetHashCode();
        }
    }
}
