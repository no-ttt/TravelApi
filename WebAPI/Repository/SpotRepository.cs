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
        public Spotcombine GetList(int? type, string city, int page, int fetch)
        {
            string sqlstr = $@"SELECT * FROM vd_Spot
                            where city like @City
                            and(@Type is null or Type = @Type)
                            order by OId
                            offset (@page - 1) * @fetch rows
                            fetch next @fetch rows only";
            string sqlstrcount  = $@"SELECT count(*) as Total FROM vd_Spot
                            where city like @City
                            and(@Type is null or Type = @Type)";

            var p = new DynamicParameters();           

            p.Add("@City", '%' + city + '%');
            p.Add("@Type", type);
            p.Add("@page", page);
            p.Add("@fetch", fetch);

            using (var db = new AppDb())
            {
                var result = db.Connection.Query<Spot>(sqlstr, p).ToList();
                var count = db.Connection.QueryFirstOrDefault<SpotTotal>(sqlstrcount, p);
                Spotcombine spotdaa = new Spotcombine
                {
                    spots = result,
                    count = count
                };
                return spotdaa;
            }  
        }


        public Spot_detail Get(int id)
        {
            var sqlstr =
                @"SELECT * FROM vd_Spot_Detail where OID = @id";
            
            
            
            using (var db = new AppDb())
            {
                var result = db.Connection.QueryFirstOrDefault<Spot_detail>(sqlstr, new {id});
                return result;
            }
        }
    }
}
