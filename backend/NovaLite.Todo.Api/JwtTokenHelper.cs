using System.IdentityModel.Tokens.Jwt;

namespace Novalite.Todo.Api
{
    public static class JwtTokenHelper
    {
        public static string ExtractUsernameFromToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;

            // Extract the username claim (preferred_username in this case)
            string username = jsonToken?.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;

            return username;
        }
    }
}
