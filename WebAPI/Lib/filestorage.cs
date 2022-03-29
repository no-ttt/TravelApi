using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebAPI.Lib
{
    public class FileManager
    {
        public class FileMetadata
        {
            public string FullFileName { get; set; }
            public string FileName { get; set; }
            public string FileExtension { get; set; }
            public string ContentType { get; set; }
            public long ContentLen { get; set; }

            public FileMetadata(IFormFile file)
            {
                FullFileName = file.FileName;
                FileName = Path.GetFileNameWithoutExtension(file.FileName);
                FileExtension = Path.GetExtension(file.FileName).Replace(".", "");
                ContentType = file.ContentType;
                ContentLen = file.Length;
            }
        }
        public bool UploadFile(IFormFile file, int AID)
        {
            string FileStorage_Root = AppConfig.Config["Filestorage:Default"];

            // 取App config
            
            // 取檔案的extension
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext))
            {
                return false;
            }

            string HexStr = Convert.ToString(Convert.ToInt32(AID), 16).PadLeft(8, '0');
            string SubFilePath = string.Join("\\", System.Text.RegularExpressions.Regex.Split(HexStr, "(?<=\\G.{2})(?!$)"));
            string FilePath = Path.Combine(FileStorage_Root, SubFilePath);

            FileInfo fi = new FileInfo(FilePath);
            fi.Directory.Create();
            using (FileStream fs = fi.Create())
            {
                file.CopyTo(fs);
            }

            return true;
        }

        public static async Task<MemoryStream> DownloadFile(int? AID)
        {
            string FileStorage_Root = AppConfig.Config["Filestorage:Default"];

            

            string HexStr = Convert.ToString(Convert.ToInt32(AID), 16).PadLeft(8, '0');
            string SubFilePath = string.Join("\\", System.Text.RegularExpressions.Regex.Split(HexStr, "(?<=\\G.{2})(?!$)"));
            string FilePath = Path.Combine(FileStorage_Root, SubFilePath);

            if (!System.IO.File.Exists(FilePath))
            {
                throw new Exception("找不到該檔案！");
            }

            MemoryStream Result = new MemoryStream();

            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                await fs.CopyToAsync(Result);
            }

            Result.Seek(0, SeekOrigin.Begin);
            return Result;
        }
    }
    
}
