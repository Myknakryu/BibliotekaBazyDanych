using BibliotekaProject.Entities.Base;
using BibliotekaProject.Entities.Enums;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace BibliotekaProject.Entities;

public class Book : BaseEntity
{
    public int IdPosition {get; set;}
    public Position Position {get; set;}
    public WearoutStatusEnum WearoutStatus {get; set;}
    public required StatusEnum CurrentStatus {get; set;}
    public List<StatusHistory> StatusHistories { get; set; }
    public static class Factory
    {
        public static Book Create(int idPosition, WearoutStatusEnum wearoutStatus = WearoutStatusEnum.New)
        {
            Book book = new()
            {
                WearoutStatus = wearoutStatus,
                IdPosition = idPosition,
                CurrentStatus = StatusEnum.InLibrary
            };
            return book;
        }
    }
    public void Update(WearoutStatusEnum? wearoutStatus, StatusEnum? currentStatus)
    {
        if(wearoutStatus is not null)
        {
            WearoutStatus = (WearoutStatusEnum)wearoutStatus;
        }
        if(currentStatus is not null)
        {
            CurrentStatus = (StatusEnum) currentStatus;
        }
    }
}