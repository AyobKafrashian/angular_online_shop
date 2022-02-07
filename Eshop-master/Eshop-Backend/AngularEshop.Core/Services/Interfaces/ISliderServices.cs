using AngularEshop.DataLayer.Entities.Site;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Interfaces
{
    public interface ISliderServices : IDisposable
    {
        Task<List<Slider>> GetAllSliders();

        Task<List<Slider>> GetActiveSlliders();

        Task AddSlider(Slider slider);

        Task UpdateSlider(Slider slider);

        Task<Slider> GetSliderById(long sliderId);
    }
}
