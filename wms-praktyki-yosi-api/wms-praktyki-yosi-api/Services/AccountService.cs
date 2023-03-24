using AutoMapper;
using AutoMapper.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wms_praktyki_yosi_api.Enitities;
using wms_praktyki_yosi_api.Exceptions;
using wms_praktyki_yosi_api.Models;
using wms_praktyki_yosi_api.Results;

namespace wms_praktyki_yosi_api.Services
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterUserDto dto);
        Task<List<UserDto>> GetAll();
        Task<LoginResult> GetToken(UserLoginDto dto);
        Task ModifyUserRole(string id, string newrole);
        Task<User> Get(string id);
        Task DeleteUser(string id);
        Task<UserDto> GetUserInfo(string email);
    }

    public class AccountService : IAccountService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly MagazinesDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            AuthenticationSettings authenticationSetings,
            MagazinesDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authenticationSettings = authenticationSetings;
            _context = context;
        }
        
        public async Task<List<UserDto>> GetAll()
        {
            var users = _userManager.Users.ToList();

            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                userDtos.Add(new UserDto()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        PasswordHash = user.PasswordHash,
                        Role = (await _userManager.GetRolesAsync(user))[0]
                    });
            }

            return userDtos;
        }

        public async Task<User> Get(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user ?? throw new NotFoundException("154");
        }

        public async Task RegisterUser(RegisterUserDto dto)
        {
             var seeder = new MagazinesSeeder(_context, _roleManager);
             await seeder.SeedRoles();

            var userExists = await _userManager.FindByNameAsync(dto.Email);
            if (userExists != null)
                throw new BadRequestException("4");

            var user = new User()
            {
                Email = dto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = dto.Email,
                CustomName = dto.Email[..5]
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new Exception();

            result = await _userManager.AddToRoleAsync(user, UserRoles.User);

            if (!result.Succeeded)
                throw new Exception();
        }

        public async Task<LoginResult> GetToken(UserLoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new BadRequestException("3");

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }


            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var token = new JwtSecurityToken(
                issuer: _authenticationSettings.JwtIssuer,
                audience: _authenticationSettings.JwtIssuer,
                expires: DateTime.Now.AddDays(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            var res = new LoginResult()
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                role = (await _userManager.GetRolesAsync(user))[0]
            };

            return res;
            
        }

        public async Task ModifyUserRole(string id, string newrole)
        {

            var user = await _userManager
                .FindByIdAsync(id)
                ?? throw new BadRequestException("154");

            var oldRole = (await _userManager.GetRolesAsync(user))[0];
            var result = await _userManager.RemoveFromRoleAsync(user, oldRole);

            if (!result.Succeeded)
            {
                throw new Exception();
            }

            result = await _userManager.AddToRoleAsync(user, newrole);
             
            if (!result.Succeeded)
            {
                throw new Exception();
            }

        }

        public async Task DeleteUser(string id)
        {
            var user = await _userManager
                .FindByIdAsync(id)
                ?? throw new BadRequestException("154");
            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
                throw new Exception();
        }

        public async Task<UserDto> GetUserInfo(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user is null)
                throw new BadRequestException("9");
            var userDto = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = (await _userManager.GetRolesAsync(user))[0]
            };

            return userDto;
        }
    }
}
