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
        public IEnumerable<Spot>GetList(int? type, string city, int page, int fetch)
        {
            string sqlstr = $@"SELECT * FROM V_Spot
                            where CName like @City
                            and(@Type is null or Type = @Type)
                            order by OId
                            offset (@page - 1) * @fetch rows
                            fetch next @fetch rows only";
                //"SELECT * FROM V_Spot" +
                //"where CName like @City" +
                //"and (@Type is null or Type = @Type)" +
                //"order by OID" +
                //"offset (@page - 1) * @fetch rows" +
                //"fetch next @fetch rows only";
            var p = new DynamicParameters();
            
            p.Add("@City", '%' + city + '%');
            p.Add("@Type", type);
            p.Add("@page", page);
            p.Add("@fetch", fetch);

            using (var db = new AppDb())
            {
                var result = db.Connection.Query<Spot>(sqlstr, p);
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
