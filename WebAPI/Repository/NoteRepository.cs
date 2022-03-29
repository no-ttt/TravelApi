using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Lib;

namespace WebAPI.model
{
    public class NotrRepository
    {
        //public IEnumerable<Note>GetList(int id)
        //{
        //    string sqlstr = $@"SELECT * FROM V_Spot
        //                    where CName like @City
        //                    and(@Type is null or Type = @Type)
        //                    order by OId
        //                    offset (@page - 1) * @fetch rows
        //                    fetch next @fetch rows only";
            
        //    var p = new DynamicParameters();

        //    p.Add("id", id);

        //    using (var db = new AppDb())
        //    {
        //        var result = db.Connection.Query<Spot>(sqlstr, p);
        //        return result;
        //    }
        //}

        



        public Spot_detail Get(int id)
        {
            var sqlstr =
                @"SELECT * FROM V_Spot_Detail where OID = @id";



            using (var db = new AppDb())
            {
                var result = db.Connection.QueryFirstOrDefault<Spot_detail>(sqlstr, new { id });
                return result;
            }
        }
    }
}
