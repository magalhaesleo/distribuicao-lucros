using AutoMapper;

using distribuicao_lucros_application.Features.Employees.Dto;

using distribuicao_lucros_domain.Features.Employees;

using System;

namespace distribuicao_lucros_application.Features.Employees.Mappers
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTO, Employee>()
                .ForMember(e => e.AdmissionDate, mo => mo.MapFrom(ed => Convert.ToDateTime(ed.Data_De_Admissao)))
                .ForMember(e => e.Department, mo => mo.MapFrom(ed => ed.Area))
                .ForMember(e => e.GrossSalary, mo => mo.MapFrom(ed => Convert.ToDouble(ed.Salario_Bruto.Substring(3))))
                .ForMember(e => e.Name, mo => mo.MapFrom(ed => ed.Nome))
                .ForMember(e => e.Registration, mo => mo.MapFrom(ed => Convert.ToInt32(ed.Matricula)))
                .ForMember(e => e.Role, mo => mo.MapFrom(ed => ed.Cargo));
        }
    }
}
