using AutoMapper;

public class ShiftMap : Profile
{
    public ShiftMap()
    {
        CreateMap<Employee, EmployeeDTO>()
            .ForMember(dest => dest.FullName,
                       conf => conf.MapFrom(src => src.Name + " " + src.Surname + " " + src.Patronymic));
    }
}