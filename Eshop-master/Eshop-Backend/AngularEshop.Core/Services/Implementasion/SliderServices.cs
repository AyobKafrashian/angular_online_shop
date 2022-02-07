using AngularEshop.Core.Services.Interfaces;
using AngularEshop.DataLayer.Entities.Site;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Services.Implementasion
{
    public class SliderServices : ISliderServices
    {
        #region Constructor
        private IGenericRepository<Slider> _sliderRepository;

        public SliderServices(IGenericRepository<Slider> sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        #endregion

        #region Sliders

        public async Task AddSlider(Slider slider)
        {
            await _sliderRepository.AddEntity(slider);
            await _sliderRepository.SaveChanges();

        }

        public async Task<List<Slider>> GetActiveSlliders()
        {
            return await _sliderRepository.GetEntitiesQuery().Where(u => !u.IsDelete).ToListAsync();
        }

        public async Task<List<Slider>> GetAllSliders()
        {
            return await _sliderRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<Slider> GetSliderById(long sliderId)
        {
            return await _sliderRepository.GetEntitByID(sliderId);
        }

        public async Task UpdateSlider(Slider slider)
        {
            _sliderRepository.UpdateEntity(slider);
            await _sliderRepository.SaveChanges();
        }
        #endregion


        #region Dispose
        public void Dispose()
        {
            _sliderRepository?.Dispose();
        }

        #endregion
    }
}
