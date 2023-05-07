using System.Security.Claims;

public interface IShiftService
{
    Task<Guid> OpenShift(ClaimsPrincipal user, OpenShiftRequest request);
    Task CloseShift(CloseShiftRequest request);
    Task<GetCurrentShiftResponse> GetCurrentShift(int stockId);
    Task UpdateCurrentShift(Guid shiftId, UpdateShiftRequest request);
    Task UpdateShiftInfo(Guid shiftInfoId, UpdateShiftInfoRequest request);
}