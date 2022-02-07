using AngularEshop.Core.DTOs.Account;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using AngularEshop.Core.Utilities.Extensions.Identity;
using AngularEshop.DataLayer.Entities.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.WebApi.Controllers
{
    public class AccountController : SiteBaseController
    {
        #region Constructor
        private IUserServices _userServices;

        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        #region Register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO register)
        {
            if (ModelState.IsValid)
            {
                var res = await _userServices.RegisterUser(register);

                //سووییچ کیس برای اینه که اگه آیتم های بیشتری مصه شماره همراهشو خواستی تست کنی راحت باشی
                switch (res)
                {
                    case RegisterUserResult.EmailExist:
                        return JsonResponseStatus.Error(new { info = "EmailExist" });
                }
            }

            return JsonResponseStatus.Success();
        }
        #endregion

        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO login)
        {
            if (ModelState.IsValid)
            {
                var res = await _userServices.LoginUser(login);

                switch (res)
                {
                    case LoginUserResult.IncorrectData:
                        return JsonResponseStatus.NotFound(new { message = "کاربری با مشخصات وارد شده یافت نشد" });

                    case LoginUserResult.NotActivated:
                        return JsonResponseStatus.Error(new { message = "حساب کاربری شما فعال نشده است" });

                    case LoginUserResult.Success:
                        var user = await _userServices.GetUserByEmail(login.Email);
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("AngularEShopJwtBearer"));
                        var signinCredential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokenOptions = new JwtSecurityToken(
                            issuer: "https://localhost:44302",
                            claims: new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, user.Email),
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                            },
                            expires: DateTime.Now.AddDays(30),
                            signingCredentials: signinCredential
                            );

                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                        return JsonResponseStatus.Success(new { token = tokenString, expireTime = 30, firstName = user.FirstName, lastName = user.LastName, userId = user.Id, address = user.Address, message = $"{user.FirstName + " " + user.LastName} عزیز خوش آمدید" });
                }
            }
            return JsonResponseStatus.Error();
        }
        #endregion

        #region Check User Authentication
        [HttpPost("check-auth")]
        public async Task<IActionResult> CheckUserAuth()
        {
            if (User.Identity.IsAuthenticated)
            {

                var user = await _userServices.GetUserById(User.GetUserId());

                return JsonResponseStatus.Success(new
                {
                    userId = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    address = user.Address,
                    email = user.Email
                });
            }

            return JsonResponseStatus.Error();
        }

        #endregion

        #region activate-user
        [HttpGet("activate-account/{id}")]
        public async Task<IActionResult> ActivateAccount(string id)
        {
            var user = await _userServices.GetUserByEmailActiveCode(id);

            if (user != null)
            {
                _userServices.ActiveUser(user);
                return JsonResponseStatus.Success(new { message = $"{user.FirstName} عزیز خوش آمدید!" });
            }

            return JsonResponseStatus.NotFound();
        }
        #endregion

        #region SignOut
        [HttpGet("sign-out")]
        public async Task<IActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
                return JsonResponseStatus.Success();
            }
            return JsonResponseStatus.Error();
        }
        #endregion



        #region User Account

        #region Edit User Account
        [HttpPost("edit-user")]
        public async Task<IActionResult> EditUser([FromBody] EditUserDTO editUser)
        {
            if (User.Identity.IsAuthenticated)
            {
                await _userServices.EditUserInfo(editUser, User.GetUserId());
                return JsonResponseStatus.Success(new { message = "اطلاعات کاربر با موفقیت ویرایش شد" });

            }
            return JsonResponseStatus.UnAuthorized();
        }
        #endregion

        #endregion
    }
}