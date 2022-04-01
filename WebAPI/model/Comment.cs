using System;
using System.Collections.Generic;

namespace WebAPI.model
{
    public class Comment
    {
        public int OID { get; set; }

        //public int M_Pic_id { get; set; }

        //public string M_Pic_place { get; set; } = string.Empty;

        public int Rank { get; set; }

        //public int Cost { get; set; }

        public string CName { get; set; }

        public string CDes { get; set; }

        public string Since { get; set; }

        public List<Spot_pic> pic { get; set; }
    }
    public class CommentTotal
    {
        public int Total { get; set; }
        public int Rank { get; set; }
    }
    public class Spot_pic
    {
        public int Pic_id { get; set; }

        public string Pic_place { get; set; } = string.Empty;

    }
    public class CommentCombine
    {
        public CommentTotal count { get; set; }
        public List<Comment> Notes { get; set; }



    }

    
}
