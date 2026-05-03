namespace Task_Management.Auth
{
    public class TokenService
    {
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
