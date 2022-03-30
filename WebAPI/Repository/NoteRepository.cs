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
        public NoteCombine GetList(int? id, int page, int fetch)
        {
            string sqlstr = $@"select nt.OID as OID, a1.AID as M_Pic, a2.AID as S_Pic, n.Rank, n.Cost, mb.CName as CName, nt.CDes, nt.Since
                            from Object nt, Member m, Object mb, Note n, Archive a1, Archive a2, Object o3, ORel r, ORel r1, ORel r2
                            where (o3.OID = @id) and o3.OID = r.OID1 and r.OID2 = nt.OID and nt.OwnerMID = mb.OID and nt.OID = n.NID and nt.OID = r1.OID1 and r1.OID2 = a2.AID and mb.OID = r2.OID2 and r2.OID2 = a1.AID
                            order by oid
                            offset (@page - 1) * @fetch rows
                            fetch next @fetch rows only";
            string sqlstrcount = $@"select COUNT(nt.OID) AS Total, SUM(N.Rank)/COUNT(NT.OID) as Rank
                                    from object nt, object o, Note n, ORel r
                                    where (o.OID = @id) and o.OID = r.OID1 and r.OID2 = nt.OID and nt.OID = n.NID";

            var p = new DynamicParameters();

            p.Add("@id", id);
            p.Add("@page", page);
            p.Add("@fetch", fetch);

            using (var db = new AppDb())
            {
                var result = db.Connection.Query<Note>(sqlstr, p).ToList();
                var count = db.Connection.QueryFirstOrDefault<NoteTotal>(sqlstrcount, p);
                NoteCombine a = new NoteCombine
                {
                    Notes = result,
                    count = count
                };
                return a;
            }
        }
        }





        
    }

