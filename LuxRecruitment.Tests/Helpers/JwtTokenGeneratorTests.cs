using LuxRecruitment.Web.Helpers;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace LuxRecruitment.Tests.Helpers
{
    public class JwtTokenGeneratorTests
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public JwtTokenGeneratorTests()
        {
            var inMemorySettings = new Dictionary<string, string>
            {
                { "JwtSettings:Key", "BardzoTajnyKluczDoTestow123456789012345678901234" },
                { "JwtSettings:Issuer", "LuxRecruitment" },
                { "JwtSettings:Audience", "LuxRecruitmentUsers" },
                { "JwtSettings:ExpiresInMinutes", "60" }
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _jwtTokenGenerator = new JwtTokenGenerator(configuration);
        }

        [Fact]
        public void GenerateToken_ShouldReturnValidJwt()
        {
            // Arrange
            string username = "testuser";

            // Act
            string token = _jwtTokenGenerator.GenerateToken(username);

            // Assert
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            Assert.NotNull(jwt);
            Assert.Equal("LuxRecruitment", jwt.Issuer);
            Assert.Contains("LuxRecruitmentUsers", jwt.Audiences);
        }
    }
}
