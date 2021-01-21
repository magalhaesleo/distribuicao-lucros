using System;

namespace distribuicao_lucros_domain.Features.Employees
{
    public class InvalidDepartmentException : Exception
    {
        public InvalidDepartmentException() : base("O departamento é inválido para calculo de distribuição dos lucros")
        {

        }
    }
}
