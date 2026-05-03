using Microsoft.EntityFrameworkCore;


namespace Task_Management.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly PasswordService _passwordService;
        private readonly JwtTokenService _jwtService;

        public AuthService(
            AppDbContext context,
            IConfiguration config,
            PasswordService passwordService,
            JwtTokenService jwtService)
        {
            _context = context;
            _config = config;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }

        public async Task<bool> Register(RegisterDto dto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
                return false;

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Role = "User",
                
            };
            user.PasswordHash = _passwordService.HashPassword(user, dto.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AuthResponseDto?> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
                return null;

            var isValid = _passwordService.VerifyPassword(user, user.PasswordHash, dto.Password);

            if (!isValid)
                return null;

            var accessToken = _jwtService.GenerateToken(user, _config);

            var refreshToken = new RefreshToken
            {
                Token = GenerateRefreshToken(),
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                UserId = user.Id
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(15)
            };
        }

        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
        public async Task<AuthResponseDto?> RefreshToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            var storedToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == token);

            if (storedToken == null ||
                storedToken.ExpiryDate < DateTime.UtcNow)
                return null;

            var user = await _context.Users.FindAsync(storedToken.UserId);
            if (user == null)
                return null;

            var newAccessToken = _jwtService.GenerateToken(user, _config);

            return new AuthResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = storedToken.Token,
                AccessTokenExpiry = DateTime.UtcNow.AddMinutes(15)
            };
        }
    }
}