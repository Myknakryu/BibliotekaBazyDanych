namespace BibliotekaProject.Entities;
using BibliotekaProject.Entities.Base;

public class Bookshelf : BaseEntity
{
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
    public int ShelfCount { get; set; }

    public static class Factory
    {
        public static Bookshelf Create(int rowNumber, 
                                    int columnNumber, 
                                    int shelfCount)
        {
            Bookshelf bookshelf = new()
            {
                RowNumber = rowNumber,
                ColumnNumber = columnNumber,
                ShelfCount = shelfCount
            };
            return bookshelf;
        }
    }
    public void Update(int? rowNumber,
                        int? columnNumber,
                        int? shelfCount)
    {
        if(rowNumber is not null and > 0)
        {
            RowNumber = (int)rowNumber;
        }
        if(columnNumber is not null and > 0)
        {
            ColumnNumber = (int)columnNumber;
        }
        if(shelfCount is not null and > 0)
        {
            ShelfCount = (int)shelfCount;
        }
    }
}