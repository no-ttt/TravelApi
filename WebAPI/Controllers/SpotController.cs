using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.model;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class SpotController : ControllerBase
    {
        private readonly model.SpotRepository _SpotRepository;

        public SpotController()
        {
            this._SpotRepository = new SpotRepository();
        }

        /// <summary>
        /// 取得景點
        /// </summary>
        [HttpGet]
        public IEnumerable<Spot> GetList(int? type ,string city = "")
        {
            return this._SpotRepository.GetList(type, city);
        }
        

        /// <summary>
        /// 取得詳細景點
        /// </summary>
        /// <param name="id">景點OID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Spot_detail Get([FromRoute] int id)
        {
            var result = this._SpotRepository.Get(id);
            if (result is null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return result;
        }


    }
}
