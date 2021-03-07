using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class AllProgressNotes
    {
        public NoteDetails SellerNotes { get; set; }
        public NoteCategories Category { get; set; }
        public NoteStatus Status { get; set; }
    }

    public class AllPublishNotes
    {
        public NoteDetails SellerNotes { get; set; }
        public NoteCategories Category { get; set; }
    }
    public class nd
    {
        public NoteDetails note { get; set; }
        public NoteCategories Category { get; set; }
        public Countries countryname { get; set; }
    }



    public class userprofiledata
    {
        public NoteDetails note { get; set; }
        public Users u { get; set; }
        public UserProfile upd { get; set; }
    }

    public class reviewtable
    {
        public NoteDetails note { get; set; }
        public NoteReviews notereview { get; set; }
        public Users usr { get; set; }
    }
    public class spamtable
    {
        public NoteDetails note { get; set; }
        public SpamReports spamrpt { get; set; }
    }
}