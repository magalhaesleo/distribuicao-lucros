using System;

namespace distribuicao_lucros_domain.Features.Employees
{
    public class Employee
    {
        public const string CollectionName = "employee";
        public int Registration { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Role { get; set; }
        public double GrossSalary { get; set; }
        public DateTime AdmissionDate { get; set; }
    }
}
