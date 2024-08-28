namespace FinalProject.BusinessLayer.Infrastructure.Handlers
{
    public class Validation
    {
        public static bool Validate(string value, string salt, string hash)
           => Hash.Create(value, salt) == hash;
    }
}
