using AngularEshop.DataLayer.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularEshop.DataLayer.Entities.Access
{
   public class Role:BaseEntity
    {
        #region Properties

        [Display(Name = "نام سیستمی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string RoleName { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "تعداد کاراکتر های {0} نمیتواند بیشتر از {1} باشد")]
        public string RoleTitle { get; set; }

        #endregion

        #region Relations
        public ICollection<UserRole> UserRoles { get; set; }
        #endregion
    }
}
