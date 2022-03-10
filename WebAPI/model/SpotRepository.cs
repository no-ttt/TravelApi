using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Lib;

namespace WebAPI.model
{
    public class SpotRepository
    {
        public IEnumerable<Spot>GetList(int? type, string city)
        {
            string sql =
                "SELECT top 30 * FROM V_Spot";
            var p = new DynamicParameters();
            if(type != null && !String.IsNullOrEmpty(city))
            {
                sql += " where CName like @city and Type = @type";
                p.Add("city", "%" + city + "%");
                p.Add("type", type);
            }
            else if(type != null)
            {
                sql += " where Type = @type";
                p.Add("type", type);
            }
            else if (!String.IsNullOrEmpty(city))
            {
                sql += " where CName like @city";
                p.Add("city", "%" + city + "%");
            }

            using (var db = new AppDb())
            {
                var result = db.Connection.Query<Spot>(sql, p);
                return result;
            }  
        }
        
        
        public Spot_detail Get(int id)
        {
            var sql =
                @"SELECT top 30 * FROM V_Spot_Detail where OID = @id";
            
            using (var db = new AppDb())
            {
                var result = db.Connection.QueryFirstOrDefault<Spot_detail>(sql, new {id});
                return result;
            }
        }
    }
}
