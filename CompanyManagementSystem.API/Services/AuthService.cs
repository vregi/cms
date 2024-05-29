using CompanyManagementSystem.API.DTO;
    using CompanyManagementSystem.API.Repositories;

    namespace CompanyManagementSystem.API.Services;

    public class AuthService : IAuthService
    {
        private readonly UserRepository _userRepository;
        private readonly JwtProvider _jwt;

        public AuthService(UserRepository userRepository, JwtProvider jwt)
        {
            _userRepository = userRepository;
            _jwt = jwt;
        }
        
        public async Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto loginRequest)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginRequest.Username);
            if (user == null || !VerifyPassword(user.PasswordHash, loginRequest.Password))
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var token = _jwt.GenerateToken(user);

            return new LoginResponseDto()
            {
                Token = token,
                Username = user.Username,
                Role = user.Role.ToString()
            };
        }

        private bool VerifyPassword(string storedHash, string providedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, storedHash);
        }
    }