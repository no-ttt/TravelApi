using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.model;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly model.CityRepository _CityRepository;

        public CityController()
        {
            this._CityRepository = new CityRepository();
        }

        /// <summary>
        /// 取得縣市
        /// </summary>
        [HttpGet]
        public List<City> GetList()
        {
            return this._CityRepository.GetList();
        }
    }
}
