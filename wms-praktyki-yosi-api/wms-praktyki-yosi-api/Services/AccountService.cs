using AutoMapper;
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

namespace wms_praktyki_yosi_api.Services
{
    public interface IAccountService
    {
        Task<bool> RegisterUser(RegisterUserDto dto);
        List<UserDto> GetAll();
        Task<string> GetToken(UserLoginDto dto);
        bool ModifyUserPermission(int id, int newPermissionLevel);
        UserDto Get(int id);
    }

    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly MagazinesDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper,
            AuthenticationSettings authenticationSetings,
            MagazinesDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _authenticationSettings = authenticationSetings;
            _context = context;
        }
        
        public List<UserDto> GetAll()
        {
            /*var users = _context
                .Users
                .Include(u => u.Role)
                .ToList();

            var userDtos = _mapper.Map<List<UserDto>>(users);
            return userDtos;*/
            return new List<UserDto>();
        }

        public UserDto Get(int id)
        {
            /*var user = _context
                .Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == id);
            if (user == null) return null;
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;*/
            return null;
        }

        public async Task<bool> RegisterUser(RegisterUserDto dto)
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
                CustomName = dto.Email.Substring(0, 5)
            };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                throw new Exception();


            return true;

        }

        public async Task<string> GetToken(UserLoginDto dto)
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
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }

        public bool ModifyUserPermission(int id, int newPermissionLevel)
        {

            /*if (newPermissionLevel > 3 || newPermissionLevel < 0)
            {
                throw new
            }

            var user = _context.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return false;
            }*/
            return true;


        }
    }
}
