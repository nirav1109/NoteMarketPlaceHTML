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
        public NoteStatus status { get; set; }
        public Users user { get; set; }
        public DownloadNotes downloadNote { get; set; }
        public SellerNoteAttachment Attachmnet { get; set; }
        public UserProfile uprofiledata { get; set; }
        public Countries country { get; set;}
        public Users buyer { get; set; }
        public SpamReports spam { get; set; }

       
        public NoteReviews notereview { get; set; }
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
        public NoteReviews nr { get; set; }
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
    public class typeuser
    {
        public NoteType types { get; set; }
        public NoteCategories categorydata { get; set; }
        public Countries countrydata { get; set; }
        public Users user { get; set; }
    }
}