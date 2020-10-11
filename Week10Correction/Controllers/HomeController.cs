using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Week10Correction.DTOs;
using Week10Correction.Models;

namespace Week10Correction.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly IConfiguration _configuration;

        public HomeController(UserManager<Users> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpGet("allUsers/{page}")]
        public IActionResult GetAllUsers(int page = 1)
        {
            var userList = new List<LoginDetailsDTO>();

            int perpage = 5;
            var users = _userManager.Users.Skip(perpage * (page - 1)).Take(perpage);

            if (users.Count() < 1)
            {
                return StatusCode(204, "No content found");
            }
            else
            {
                foreach (var user in users)
                {
                    var userToReturn = new LoginDetailsDTO
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Department = user.Department,
                        DateOfEmployment = user.DateOfEmployment,
                        Gender = user.Gender
                    };
                    userList.Add(userToReturn);
                }
                return Ok(userList);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO userToRegister)
        {
            if (ModelState.IsValid)
            {
                var newUser = new Users
                {
                    UserName = userToRegister.Email,
                    Email = userToRegister.Email,
                    FirstName = userToRegister.FirstName,
                    LastName = userToRegister.LastName,
                    Department = userToRegister.Department,
                    Gender = userToRegister.Gender
                };

                var result = await _userManager.CreateAsync(newUser, userToRegister.Password);

                if (result.Succeeded)
                {
                    return Ok("Registration was successful");
                }
            }
            return BadRequest(ModelState);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserToLoginDTO userToLogin)
        {
            if (ModelState.IsValid)
            {
                var getUserByEmail = _userManager.Users.FirstOrDefault(user => user.Email == userToLogin.Email);
                var userPwd = await _userManager.CheckPasswordAsync(getUserByEmail, userToLogin.Password);

                if (userPwd)
                {
                    //Establish the claims of the unique user
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, getUserByEmail.Id),
                        new Claim(ClaimTypes.Name, getUserByEmail.FirstName)
                    };

                    //Generate the token key
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecuredToken").Value));

                    //Generate the sign-in credentials
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                    //Create the security token descriptor (token descriptor outlines the shape of your token)
                    var securityTokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.Now.AddDays(3),
                        SigningCredentials = creds
                    };

                    //Next, we create the token handler
                    var tokenHandler = new JwtSecurityTokenHandler();

                    //Then, we create the token
                    var token = tokenHandler.CreateToken(securityTokenDescriptor);

                    //...and generate it
                    var generatedToken = tokenHandler.WriteToken(token);

                    var userToReturn = new LoggedInUserDetailsDTO
                    {
                        FirstName = getUserByEmail.FirstName,
                        LastName = getUserByEmail.LastName,
                        Email = getUserByEmail.Email,
                        Department = getUserByEmail.Department,
                        Token = generatedToken
                    };
                    return Ok(userToReturn);
                }
            }
            return BadRequest();
        }

        [HttpPost("getUserDetails")]
        public async Task<IActionResult> LoginDetails([FromBody] FetchDetailsDTO userToFetch)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(userToFetch.Email);

                var userDetailsToReturn = new LoginDetailsDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Department = user.Department,
                    DateOfEmployment = user.DateOfEmployment,
                    Gender = user.Gender
                };
                return Ok(userDetailsToReturn);
            }
            else return Unauthorized();
        }
    }
}