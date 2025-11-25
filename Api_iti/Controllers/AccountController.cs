using Api_iti.DTO;
using Api_iti.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api_iti.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<ApplicationUser> userManager , IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }


        [HttpPost("Register")] // api/Account/Register
        public async Task<IActionResult> Register(RegisterDto userFromRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = userFromRequest.UserName;
                user.Email = userFromRequest.Email;
                
                IdentityResult result = await _userManager.CreateAsync(user, userFromRequest.Password);
                if (result.Succeeded)
                {
                    return Ok("User registered successfully");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
            }
            return BadRequest(ModelState);
        }



        [HttpPost("Login")] // api/Account/Login
        public async Task<IActionResult> Login(LoginDto userFromRequest)
        {
            if (ModelState.IsValid)
            {
                // check
                ApplicationUser userFromDb =
                   await _userManager.FindByNameAsync(userFromRequest.UserName);

                if (userFromDb != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(userFromDb, userFromRequest.Password);
                    if (found)
                    {
                        //**generate token
                        List<Claim> userClaims = new List<Claim>();

                        //token generated id change (JWT Predefined claims)
                        userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        userClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDb.Id));
                        userClaims.Add(new Claim(ClaimTypes.Name, userFromDb.UserName));

                        var userRoles = await _userManager.GetRolesAsync(userFromDb);

                        foreach (var role in userRoles)
                        {
                            userClaims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));

                        //Create signing credentials (use key + header + payload)
                        SigningCredentials signCred = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256); //take key & algorithem which make encode to token

                        // design payload
                        JwtSecurityToken mySecToken = new JwtSecurityToken(
                            issuer: _config["JWT:Issuer"], // who generate the token
                            audience: _config["JWT:Audience"], // who will use the token
                            claims: userClaims,
                            expires: DateTime.UtcNow.AddHours(1),
                            signingCredentials: signCred  // to make the token verify & trusted
                        );

                        //generate signature
                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(mySecToken),
                            expiration = DateTime.Now.AddHours(1) //my token.ValidTo

                        });
                    }
                    //if password not correct
                    ModelState.AddModelError("Login", "Invalid username or password");
                    return BadRequest(ModelState);
                }
                // if user not found
                ModelState.AddModelError("Login", "User not found");
                return BadRequest(ModelState);
            }
            return BadRequest(ModelState);
        }
        
    }
}
