using distribuicao_lucros_domain.Features.Employees;

using Firebase.Database;
using Firebase.Database.Query;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace distribuicao_lucros_infra_data.Features.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly FirebaseClient firebaseClient;

        public EmployeeRepository(FirebaseClient firebaseClient)
        {
            this.firebaseClient = firebaseClient;
        }

        public async Task Add(IEnumerable<Employee> employees)
        {
            foreach (Employee employee in employees)
            {
                await firebaseClient.Child(Employee.CollectionName).PostAsync(employee);
            } 
        }
    }
}
