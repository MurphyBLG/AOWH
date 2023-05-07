using AutoMapper;

public class WorkPlanMap : Profile
{
    public WorkPlanMap()
    {
        CreateMap<WorkPlan, GetWorkPlanResponse>();
    }
}