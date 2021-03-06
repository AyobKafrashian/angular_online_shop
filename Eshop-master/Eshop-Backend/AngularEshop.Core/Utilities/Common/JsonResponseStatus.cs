using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AngularEshop.Core.Utilities.Common
{
    public static class JsonResponseStatus
    {
        #region Success
        public static JsonResult Success()
        {
            return new JsonResult(new { status = "Success" });
        }

        public static JsonResult Success(object returnData)
        {
            return new JsonResult(new { status = "Success", data = returnData });
        }
        #endregion

        #region NotFound
        public static JsonResult NotFound()
        {
            return new JsonResult(new { status = "NotFound" });
        }

        public static JsonResult NotFound(object returnData)
        {
            return new JsonResult(new { status = "NotFound", data = returnData });
        }
        #endregion

        #region Error
        public static JsonResult Error()
        {
            return new JsonResult(new { status = "Error" });
        }

        public static JsonResult Error(object returnData)
        {
            return new JsonResult(new { status = "Error", data = returnData });
        }
        #endregion

        #region UnAuthorized
        public static JsonResult UnAuthorized()
        {
            return new JsonResult(new { status = "UnAuthorized" });
        }

        public static JsonResult UnAuthorized(object returnData)
        {
            return new JsonResult(new { status = "UnAuthorized", data = returnData });
        }
        #endregion
    }
}
