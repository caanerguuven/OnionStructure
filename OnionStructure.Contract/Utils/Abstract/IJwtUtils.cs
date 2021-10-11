namespace OnionStructure.Contract.Utils.Abstract
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(string userId);
        public string ValidateJwtToken(string token);
    }
}
