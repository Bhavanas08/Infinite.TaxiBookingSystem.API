using Infinite.TaxiBookingSystem.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Infinite.TaxiBookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;

        public AccountsController(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginModel login)
        {
            var currentUser=_dbContext.Users.FirstOrDefault(x=>x.LoginID==login.LoginID && x.Password==login.Password);
            if(currentUser==null)
            {
                return NotFound("Invalid LoginID  or  Password");
            }
            var token = GenerateToken(currentUser);
            if(token==null)
            {
                return NotFound("Invalid credentials");
            }
            return Ok(token);
        }
        [NonAction]
        public string GenerateToken(User user)
        {
            var securitykey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha512);
            var myclaims = new List<Claim>
            {
                new Claim(ClaimTypes.Role,user.Role),
            };
            var token = new JwtSecurityToken(issuer: _configuration["JWT:issuer"],
                                            audience: _configuration["JWT:audience"],
                                            claims: myclaims, expires: DateTime.Now.AddDays(1),
                                            signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token); 
            
        }
       
    }
}
