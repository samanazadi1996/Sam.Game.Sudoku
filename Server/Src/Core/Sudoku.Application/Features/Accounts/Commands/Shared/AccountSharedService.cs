using Microsoft.IdentityModel.Tokens;
using Sudoku.Application.DTOs.DomanDtos;
using Sudoku.Domain.Entities;
using Sudoku.Domain.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Sudoku.Application.Features.Accounts.Commands.Shared
{
    public class AccountSharedService(JwtSettings jwtSettings)
    {

        internal AuthenticationResponse GetAuthenticationResponse(User user)
        {
            var jwToken = GenerateJwtToken();
            var rolesList = new List<string>();

            if (user.UserRoles is not null)
                rolesList = user.UserRoles.Select(p => p.Role.Name).ToList();

            return new AuthenticationResponse()
            {
                Id = user.Id,
                JwToken = new JwtSecurityTokenHandler().WriteToken(jwToken),
                UserName = user.UserName,
                Roles = rolesList,
                ProfileImage = user.ProfileImage
            };

            JwtSecurityToken GenerateJwtToken()
            {
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
                return new JwtSecurityToken(
                    issuer: jwtSettings.Issuer,
                    audience: jwtSettings.Audience,
                    claims: GetClaims(),
                    expires: DateTime.MaxValue,
                    signingCredentials: signingCredentials);
            }
            List<Claim> GetClaims()
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                    new Claim("SecurityStamp", user.SecurityStamp),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                if (user.UserRoles is not null)
                    claims.AddRange(user.UserRoles.Select(r => new Claim(ClaimTypes.Role, r.Role.Name)));

                return claims;
            }

        }


    }
}
