using AutoMapper;

public class AccountingMap : Profile
{
    public AccountingMap()
    {
        CreateMap<Accounting, GetAccountingResponse>();
    }
}