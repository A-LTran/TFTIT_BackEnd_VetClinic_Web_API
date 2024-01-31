using System.ComponentModel.DataAnnotations;

namespace BLL.Tools
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
            DateTime endDate = DateTime.Now.Date.AddYears(2);
            if (dateTime.Value.Date < startDate)
            {
                ErrorMessage = $"La date ne peut pas être plus petite que {startDate:dd/MM/yyyy}";
                return false;
            }

            if (dateTime.Value.Date > endDate)
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

            DateTime startDate = DateTime.Now.Date.AddYears(-100);
            DateTime endDate = DateTime.Now.Date;
            if (dateTime.Value.Date < startDate)
            {
                ErrorMessage = $"La date ne peut pas être plus petite que {startDate:dd/MM/yyyy}";
                return false;
            }

            if (dateTime.Value.Date > endDate)
            {
                ErrorMessage = $"La date ne peut pas être plus grande que {endDate:dd/MM/yyyy}";
                return false;
            }

            return true;
        }
    }
}
