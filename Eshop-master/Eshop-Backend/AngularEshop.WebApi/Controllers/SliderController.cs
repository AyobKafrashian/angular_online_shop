using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AngularEshop.WebApi.Controllers
{
    public class SliderController : SiteBaseController
    {
        #region Constructor
        private ISliderServices _sliderServices;

        public SliderController(ISliderServices sliderServices)
        {
            _sliderServices = sliderServices;
        }
        #endregion

        [HttpGet("GetActiveSliders")]
        public async Task<IActionResult> GetActiveSliders()
        {
            var sliders = await _sliderServices.GetActiveSlliders();
            //از دو کد زیر میشه استفاده کرد اما این کد تمیز تره
            //return new JsonResult(sliders);
            return JsonResponseStatus.Success(sliders);
        }

    }
}
