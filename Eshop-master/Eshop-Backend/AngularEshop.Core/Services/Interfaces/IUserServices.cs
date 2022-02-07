using AngularEshop.Core.DTOs.Account;
using AngularEshop.DataLayer.Entities.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Interfaces
{
    public interface IUserServices : IDisposable
    {
        Task<List<User>> GetAllUsers();

        Task<RegisterUserResult> RegisterUser(RegisterUserDTO register);

        bool IsUserExistByEmail(string email);

        Task<LoginUserResult> LoginUser(LoginUserDTO login , bool checkAdminRole = false);

        Task<User> GetUserByEmail(string email);

        Task<User> GetUserById(long userId);

        Task<User> GetUserByEmailActiveCode(string emailActiveCode);

        void ActiveUser(User user);

        #region Edit User
        Task EditUserInfo(EditUserDTO user, long userId);
        #endregion
    }
}
