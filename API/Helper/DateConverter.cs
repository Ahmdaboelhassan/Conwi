using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Helper
{
    public class DateConverter : ValueConverter<DateOnly,DateTime>
    {
        public DateConverter() 
        : base(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), // this will store in data base 
            dateTime => DateOnly.FromDateTime(dateTime)         // this will retreive from database
        ){}
     
    }
}

