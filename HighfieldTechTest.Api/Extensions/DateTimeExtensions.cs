namespace HighfieldTechTest.Api.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetAge(DateTime dateOfBirth)
        {
            DateTime date = DateTime.Today;

            if (dateOfBirth.Date > date)
                throw new ArgumentException("Birth date is in the future.", nameof(dateOfBirth));

            int age = date.Year - dateOfBirth.Year;

            // If birthday hasn't happened yet this year, minus one
            if (dateOfBirth.Date > date.AddYears(-age))
                age--;

            return age;
        }
    }
}
