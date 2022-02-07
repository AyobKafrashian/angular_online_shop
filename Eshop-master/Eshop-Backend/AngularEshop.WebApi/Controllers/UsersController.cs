using AngularEshop.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularEshop.WebApi.Controllers
{
    public class UsersController : SiteBaseController
    {
        #region Constructor
        private IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        #endregion

        #region users list
        [HttpGet]
        public async Task<IActionResult> Users()
        {
            return new ObjectResult(await _userServices.GetAllUsers());
        }
        #endregion
    }
}
