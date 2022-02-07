using AngularEshop.Core.DTOs.Account;
using AngularEshop.Core.Security;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Convertors;
using AngularEshop.DataLayer.Entities.Access;
using AngularEshop.DataLayer.Entities.Account;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Implementasion
{
    public class UserServices : IUserServices
    {
        #region Constructor
        private IGenericRepository<User> _userRepository;
        private IGenericRepository<UserRole> _userRoleRepository;
        private IPasswordHelper _passwordHelper;
        private IMailSender _mailSender;
        private IViewRenderService _renderView;

        public UserServices(IGenericRepository<User> userRepository, IPasswordHelper passwordHelper, IMailSender mailSender, IViewRenderService renderView, IGenericRepository<UserRole> userRoleRepository)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _mailSender = mailSender;
            _renderView = renderView;
            _userRoleRepository = userRoleRepository;
        }
        #endregion

        #region User Section
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<RegisterUserResult> RegisterUser(RegisterUserDTO register)
        {
            if (IsUserExistByEmail(register.Email))
            {
                return RegisterUserResult.EmailExist;
            }

            var user = new User
            {
                Email = register.Email.SanitizeText(),
                Address = register.Address.SanitizeText(),
                FirstName = register.FirstName.SanitizeText(),
                LastName = register.LastName.SanitizeText(),
                EmailActiveCode = Guid.NewGuid().ToString(),
                Password = _passwordHelper.EncodePasswordMd5(register.Password)
            };

            await _userRepository.AddEntity(user);
            await _userRepository.SaveChanges();


            #region Sending Activated Email
            var body = await _renderView.RenderToStringAsync("Email/_ActivateAccount", user);

            _mailSender.Send("kafaayoub278@gmail.com", "تست فعالسازی", body);
            #endregion

            return RegisterUserResult.Success;
        }

        public bool IsUserExistByEmail(string email)
        {
            return _userRepository.GetEntitiesQuery().Any(u => u.Email == email.ToLower().Trim());
        }

        public async Task<LoginUserResult> LoginUser(LoginUserDTO login, bool checkAdminRole = false)
        {
            var password = _passwordHelper.EncodePasswordMd5(login.Password);

            var user = await _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(u => u.Email == login.Email.ToLower().Trim() && u.Password == password);

            if (user == null)
            {
                return LoginUserResult.IncorrectData;
            }
            if (!user.IsActivated)
            {
                return LoginUserResult.NotActivated;
            }

            if (checkAdminRole)
            {
                var isUserAdmin = _userRoleRepository.GetEntitiesQuery().Include(c => c.Role).AsQueryable().Any(s => s.UserId == user.Id && s.Role.RoleName == "Admin");

                if (!isUserAdmin)
                {
                    return LoginUserResult.NotAdmin;
                }
            }

            return LoginUserResult.Success;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(u => u.Email == email.ToLower().Trim());
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _userRepository.GetEntitByID(userId);
        }

        public async Task<User> GetUserByEmailActiveCode(string emailActiveCode)
        {
            return await _userRepository.GetEntitiesQuery().SingleOrDefaultAsync(u => u.EmailActiveCode == emailActiveCode);
        }

        public void ActiveUser(User user)
        {
            user.IsActivated = true;
            user.EmailActiveCode = Guid.NewGuid().ToString();

            _userRepository.UpdateEntity(user);
            _userRepository.SaveChanges();
        }
        #endregion

        #region Edit User

        public async Task EditUserInfo(EditUserDTO user, long userId)
        {
            var mainUser = await GetUserById(userId);

            if (mainUser != null)
            {
                mainUser.FirstName = user.FirstName;
                mainUser.LastName = user.LastName;
                mainUser.Address = user.Address;
                _userRepository.UpdateEntity(mainUser);
                await _userRepository.SaveChanges();
            }
        }

        #endregion

        #region Dispose
        public void Dispose()
        {
            _userRepository?.Dispose();
            _userRoleRepository?.Dispose();
        }
        #endregion
    }
}
