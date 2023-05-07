using AutoMapper;

public class EmployeeMap : Profile
{
    public EmployeeMap()
    {
        CreateMap<Employee, RegistrationResponse>();
        CreateMap<Employee, UpdateEmployeeResponse>();
    }
}