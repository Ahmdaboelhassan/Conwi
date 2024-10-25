using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Helper
{
    public class DateConverter : ValueConverter<DateOnly,DateTime>
    {
        public DateConverter() 
        : base(
            dateOnly =>  DateTime.SpecifyKind(dateOnly.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc), // this will store in data base 
            dateTime => DateOnly.FromDateTime(dateTime)         // this will retreive from database
        ){}
     
    }
}

