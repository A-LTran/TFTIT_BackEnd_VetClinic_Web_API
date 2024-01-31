using System.ComponentModel.DataAnnotations;

namespace BLL.Entities
{

    public class DateRangeFromTodayToTwoYearsAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime? dateTime = value as DateTime?;
            if (dateTime is null)
            {
                ErrorMessage = "Ce n'est pas une date";
                return false;
            }

            DateTime startDate = DateTime.Now.Date;
            DateTime endDate = DateTime.Now.AddYears(2);
            if (dateTime.Value < startDate)
            {
                ErrorMessage = $"La date ne peut pas être plus petite que {startDate:dd/MM/yyyy}";
                return false;
            }

            if (dateTime.Value > endDate)
            {
                ErrorMessage = $"La date ne peut pas être plus grande que {endDate:dd/MM/yyyy}";
                return false;
            }

            return true;
        }
    }

    public class DateRangeBeforeTodayAndAfter100YAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime? dateTime = value as DateTime?;
            if (dateTime is null)
            {
                ErrorMessage = "Ce n'est pas une date";
                return false;
            }

            DateTime startDate = DateTime.Now.AddYears(-100);
            DateTime endDate = DateTime.Now.Date;
            if (dateTime.Value < startDate)
            {
                ErrorMessage = $"La date ne peut pas être plus petite que {startDate:dd/MM/yyyy}";
                return false;
            }

            if (dateTime.Value > endDate)
            {
                ErrorMessage = $"La date ne peut pas être plus grande que {endDate:dd/MM/yyyy}";
                return false;
            }

            return true;
        }
    }
}
