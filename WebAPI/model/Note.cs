using System;
using System.Collections.Generic;

namespace WebAPI.model
{
    public class Note
    {
        public int OID { get; set; }

        public string M_Pic { get; set; }

        public string S_Pic { get; set; }

        public int Rank { get; set; }

        public int Cost { get; set; }

        public string CName { get; set; }

        public string CDes { get; set; }

        public string Since { get; set; }
    }
    public class NoteTotal
    {
        public int Total { get; set; }
        public int Rank { get; set; }
    }
    public class NoteCombine
    {
        public NoteTotal count { get; set; }
        public List<Note> Notes { get; set; }

    }

    public class NoteParameter
    {

    }
}
