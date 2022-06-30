using Dapper;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Lib;

namespace WebAPI.model
{
    public class CityRepository
    {
        public List<City> GetList()
        {
            string sqlstr = @"select * from vd_City";

            using (var db = new AppDb())
            {
                var result = db.Connection.Query<City>(sqlstr).ToList();

                return result;
            }
        }
    }
}