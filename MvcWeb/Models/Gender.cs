using Bogus.DataSets;

namespace MvcWeb.Models
{
    public enum Gender
    {
        Male,
        Female,
    }

    public static class GenderExtensions
    {
        public static Name.Gender MapToLibGender(this Gender gender)
        {
            return gender == Gender.Male ? Name.Gender.Male : Name.Gender.Female;
        }
    }
}
