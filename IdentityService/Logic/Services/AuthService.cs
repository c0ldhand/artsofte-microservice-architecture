﻿using CoreLib.DTO;
using CoreLib.Entities;
using CoreLib.Interfaces;
using CoreLib.Interfaces.Repositories;
using CoreLib.Interfaces.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;


namespace IdentityService.Logic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<RefreshToken> _refreshTokenRepository;
        public AuthService(IUserRepository userRepository, IRepository<RefreshToken> refreshTokenRepository)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<UserDTO> RegisterAsync(RegisterRequest request)
        {
            _ = await _userRepository.GetByEmailAsync(request.Email) ?? throw new Exception("User already exists");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Name = request.Username,
                PasswordHash = HashPassword(request.Password),
                RoleId = Guid.Empty,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = "USER"
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = Guid.NewGuid().ToString(),
                ExpiresOn = DateTime.UtcNow.AddDays(2)
            };

            await _refreshTokenRepository.AddAsync(refreshToken);

            return new AuthResponse
            {
                AccessToken = "JWT-TOKEN",
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
        {
            var stored = (await _refreshTokenRepository.GetAllAsync())
            .FirstOrDefault(r => r.Token == refreshToken && !r.IsRevoked && r.ExpiresOn > DateTime.UtcNow) ?? throw new Exception("Invalid refresh token");
            return new AuthResponse
            {
                AccessToken = "JWT-TOKEN",
                RefreshToken = stored.Token
            };
        }

        private string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                10000,
                32));
            return $"{Convert.ToBase64String(salt)}.{hash}";
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split('.');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                10000,
                32));

            return hash == parts[1];
        }
    }
}
