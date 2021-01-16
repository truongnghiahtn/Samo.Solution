using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using samo.Aplication.ViewModel.Common;
using samo.Aplication.ViewModel.User;
using samo.Data.Entities;
using samo.Data.FE;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace samo.Aplication.ServiceSamo.ServiceUser
{
    public class ServiceUser : IServiceUser
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly IConfiguration _config;
        private readonly samoDbContext _context;

        public ServiceUser(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config, samoDbContext context)
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _config = config;
            _context = context;

        }

        public async Task<ApiResult<UserVm>> GetUserById(Guid id)
        {
            //lay du lieu
            var registerMakeMoney = from rmm in _context.RegisterMakeMoneys
                                    where rmm.IdUser == id
                                    select new { rmm };

            var registerSpend = from rs in _context.RegisterSpends
                                where rs.IdUser == id
                                select new { rs };
            var totalSpend = registerSpend.Sum(x => x.rs.Money);
            var totalMakeMoney = registerMakeMoney.Sum(x => x.rmm.Money);
            //lay du lieu
            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user==null)
            {
                return new ApiErrorResult<UserVm>("Khong tim thay nguoi dung");
            }
            var data = new UserVm()
            {
                id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TotalSpend=totalSpend,
                TotalMakeMoney=totalMakeMoney,
                LimitMoney = user.LimitMoney,
                AccountBalance = user.AccountBalance,
            };

            return new ApiSuccessResult<UserVm>(data);

        }

        public async Task<ApiResult<ResultLogin>> LoginUser(LoginUser request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return new ApiErrorResult<ResultLogin>("Tài Khoản của bạn không tồn tại");
            }
            var result = await _singInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

            if (!result.Succeeded)
            {
                return new ApiErrorResult<ResultLogin>("mật Khẩu không đúng");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.Username)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            // lay du lieu
            var registerMakeMoney = from rmm in _context.RegisterMakeMoneys
                                    where rmm.IdUser == user.Id
                                    select new { rmm };

            var registerSpend = from rs in _context.RegisterSpends
                                where rs.IdUser == user.Id
                                select new { rs };
            var totalSpend = registerSpend.Sum(x => x.rs.Money);
            var totalMakeMoney = registerMakeMoney.Sum(x => x.rmm.Money);



            //lay du lieu

            var data = new ResultLogin()
            {
                id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                TotalSpend=totalSpend,
                TotalMakeMoney=totalMakeMoney,
                LimitMoney = user.LimitMoney,
                AccountBalance = user.AccountBalance,
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return new ApiSuccessResult<ResultLogin>(data);
        }
        public async Task<ApiResult<bool>> RegisterUser(RequestUser request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                return new ApiErrorResult<bool>("Tài khoản bạn đã tồn tại");
            }
            user = new AppUser()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new ApiErrorResult<bool>("Đăng ký không thành công");
            }
            return new ApiSuccessResult<bool>();

        }
    }
}
