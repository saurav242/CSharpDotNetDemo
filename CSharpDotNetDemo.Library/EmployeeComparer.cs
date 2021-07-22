using CSharpDotNetDemo.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CSharpDotNetDemo.Library
{
    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.ID == y.ID && x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] Employee obj)
        {
            return obj.ID.GetHashCode() ^ obj.Name.GetHashCode();
        }
    }
}
