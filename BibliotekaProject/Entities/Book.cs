using BibliotekaProject.Entities.Base;
using BibliotekaProject.Entities.Enums;

namespace BibliotekaProject.Entities;

public class Book : BaseEntity
{
    public int IdPosition {get; set;}
    public Position Position {get; set;}
    public WearoutStatusEnum WearoutStatus {get; set;}
    public static class Factory
    {
        public static Book Create(int idPosition, WearoutStatusEnum wearoutStatus = WearoutStatusEnum.New)
        {
            Book book = new()
            {
                WearoutStatus = wearoutStatus,
                IdPosition = idPosition
            };
            return book;
        }
    }
    public void Update(WearoutStatusEnum? wearoutStatus)
    {
        if(wearoutStatus is not null)
        {
            WearoutStatus = (WearoutStatusEnum)wearoutStatus;
        }
    }
}