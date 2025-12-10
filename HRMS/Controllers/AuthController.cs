using HRMS.DbContexts;
using HRMS.Dto.Auth;
using HRMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HRMSContext _dbContext;
        public AuthController(HRMSContext dbContext) 
        {
            _dbContext = dbContext;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(x=>x.Username.ToUpper() == loginDto.Username.ToUpper());
                if (user == null)
                {
                    return BadRequest("Invalid Username Or Password");
                }

             //   if (loginDto.Password == user.HashedPassword)
                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password,user.HashedPassword))
                {
                    return BadRequest("Invalid Username Or Password");
                }
                var token = GenrateJwtToken(user);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        private string GenrateJwtToken(User user)
        {
            var claims = new List<Claim>();// user info

            //key -> value
            //id -> 1
            //name -> admin
            claims.Add(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name,user.Username));

            //role -> hr , manager, developer , admin
            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role,"Admin"));
            }
            else
            {
                var employee = _dbContext.Employees.Include(x=>x.Lookup).FirstOrDefault(x=>x.UserId==user.Id);// use include with navgtion
               claims.Add(new Claim(ClaimTypes.Role, employee.Lookup.Name));
                // lookup null object refreance only work with projection (select) or with include.

            }

            //Secert Key = "EWEED#2!@r#dwef$##@@$$$TRTERF"

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("EWEED#2!@r#dwef$##@@$$243tge$TRTERF"));//to array asci code
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);//signing the token

            var tokenSettings = new JwtSecurityToken(
                claims: claims,//user ifo
                signingCredentials: creds,
                expires: DateTime.Now.AddHours(1)
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenSettings);

            return token;

        }



    }
}
