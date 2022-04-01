using Dapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Lib;


namespace WebAPI.model
{
    public class CommentRepository
    {
       // private readonly Lib.FileManager fileManager;
        public CommentCombine GetList(int? id, int page, int fetch)
        {
            string sqlstr = $@"SELECT O2.OID as OID,N.Rank AS Rank, O3.CName as CName, O2.CDes as CDes, O2.Since as Since
                            FROM Object O1,ORel,Note N,OREL R,Archive A, Object O2,Object O3
                            WHERE (O1.OID=@OID) AND ORel.OID1 = O1.OID AND  ORel.OID2 = N.NID AND O2.OID = N.NID AND R.OID1 =N.NID AND A.AID = R.OID2 AND O3.OID = O2.OwnerMID
                            order by oid
                            offset (@page - 1) * @fetch rows
                            fetch next @fetch rows only";
            string s_pic = $@"select r.OID2 as Pic_id
                            from Object o, ORel r
                            where o.OID = @NID and r.OID1 = o.OID";

            string sqlstrcount = $@"select COUNT(nt.OID) AS Total, SUM(N.Rank)/COUNT(NT.OID) as Rank
                                    from object nt, object o, Note n, ORel r
                                    where (o.OID = @OID) and o.OID = r.OID1 and r.OID2 = nt.OID and nt.OID = n.NID";
            

            var p = new DynamicParameters();

            p.Add("@OID", id);
            p.Add("@page", page);
            p.Add("@fetch", fetch);
            
            using (var db = new AppDb())
            {
                var result = db.Connection.Query<Comment>(sqlstr, p).ToList();

                for(int i = 0; i < result.Count; i++)
                {
                    //int M_Pic = result[i].M_Pic_id;

                    //string FileStorage_Root = AppConfig.Config["Filestorage:Default"];
                    //string HexStr = Convert.ToString(Convert.ToInt32(M_Pic), 16).PadLeft(8, '0');
                    //string SubFilePath = string.Join("\\", System.Text.RegularExpressions.Regex.Split(HexStr, "(?<=\\G.{2})(?!$)"));
                    //string FilePath = Path.Combine(FileStorage_Root, SubFilePath);
                    //result[i].M_Pic_place = FilePath;

                    int NID = result[i].OID;
                    var k = new DynamicParameters();
                    FileManager fileManager = new FileManager();
                    k.Add("@NID", NID);
                    List<Spot_pic> pic_result = db.Connection.Query<Spot_pic>(s_pic, k).ToList();
                    for (int j = 0; j < pic_result.Count; j++)
                    {
                        int S_Pic = pic_result[j].Pic_id;
                        string FileStorage_Root2 = AppConfig.Config["Filestorage:Default"];
                        string HexStr2 = Convert.ToString(Convert.ToInt32(S_Pic), 16).PadLeft(8, '0');
                        string SubFilePath2 = string.Join("\\", System.Text.RegularExpressions.Regex.Split(HexStr2, "(?<=\\G.{2})(?!$)"));
                        string FilePath2 = Path.Combine(FileStorage_Root2, SubFilePath2);
                        pic_result[j].Pic_place = FilePath2;
                    }

                    result[i].pic = pic_result;
                }
                var s = new DynamicParameters();

                s.Add("@OID", id);
                var count = db.Connection.QueryFirstOrDefault<CommentTotal>(sqlstrcount, s);
                CommentCombine a = new CommentCombine
                {
                    Notes = result,
                    count = count
                };
                return a;
            }
        }

        public int insert_comment(int id, int Rank, string Des, int AID)
        {
            string sqlstr = $@"Insert_Comment";
            string sql = $@"Insert into Orel(OID1, OID2) VALUES(@NewOID, @AID)";
            var p = new DynamicParameters();
            p.Add("@id", id);
            p.Add("@Rank", Rank);
            p.Add("@Des", Des);
            p.Add("@NewOID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var db = new AppDb())
            {
                var result = db.Connection.Execute(sqlstr, p, commandType: CommandType.StoredProcedure);
                int NewID = p.Get<int>("@NewOID");
                var k = new DynamicParameters();
                k.Add("@NewOID", NewID);
                k.Add("@AID", AID);
                var a = db.Connection.Execute(sql, k);
                //var j = new DynamicParameters();
                //j.Add("@NewOID", SID);
                //j.Add("@AID", NewID);
                //var b = db.Connection.Execute(sql, j);
                return a;
            }
        }
        public int insert_pic(int Id, IFormFile File)
        {
            string sqlstr = $@"xp_insertArchive";
            var p = new DynamicParameters();
            p.Add("@FileName", Path.GetFileNameWithoutExtension(File.FileName));
            p.Add("@FileExtension", Path.GetExtension(File.FileName).Replace(".", ""));
            p.Add("@ContentType", File.ContentType);
            p.Add("@ContentLen", File.Length);
            p.Add("@MID", Id);
            p.Add("@NewOID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@NewUUID", dbType: DbType.String, direction: ParameterDirection.Output, size:100) ;


            FileManager fileManager = new FileManager();
            using (var db = new AppDb())
            {
                var result = db.Connection.Execute(sqlstr, p, commandType: CommandType.StoredProcedure);
                int NewID = p.Get<int>("@NewOID");

                string FileStorage_Root = AppConfig.Config["Filestorage:Default"];

                // 取App config

                // 取檔案的extension
                var ext = Path.GetExtension(File.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(ext))
                {
                    return NewID;
                }

                string HexStr = Convert.ToString(Convert.ToInt32(NewID), 16).PadLeft(8, '0');
                string SubFilePath = string.Join("\\", System.Text.RegularExpressions.Regex.Split(HexStr, "(?<=\\G.{2})(?!$)"));
                string FilePath = Path.Combine(FileStorage_Root, SubFilePath);

                FileInfo fi = new FileInfo(FilePath);
                fi.Directory.Create();
                using (FileStream fs = fi.Create())
                {
                    File.CopyTo(fs);
                }

                return NewID;
            

        }

        }
        
    }
}






