using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NotesMarketPlace.Models
{
    public class NoteDetailsDummy
    {
        public int NoteID { get; set; }
        public int SellerID { get; set; }
        public int Status { get; set; }
        public Nullable<int> ActionBy { get; set; }
        public string AdminRemarks { get; set; }
        public Nullable<System.DateTime> PublishedDate { get; set; }
        [Required(ErrorMessage ="Please Enter Note Title")]
        public string NoteTitle { get; set; }

        [Required(ErrorMessage = "Please Select Note Category")]
        public int NoteCategoryID { get; set; }

        public HttpPostedFileBase DisplayPicture { get; set; }


        public List<HttpPostedFileBase> NoteAttachment { get; set; }

        [Required(ErrorMessage = "Please select Note Type")]
        public int NoteTypeID { get; set; }

        public Nullable<int> NumberOfPages { get; set; }

        [Required(ErrorMessage = "Please Enter Note Description")]
        public string NoteDescription { get; set; }        
        public string UniversityInformation { get; set; }

        [Required(ErrorMessage = "Please Select Country")]
        public Nullable<int> CountryID { get; set; }
        public string Course { get; set; }
        public string CourseCode { get; set; }
        public string ProfessorName { get; set; }

        [Required(ErrorMessage = "Please Select Selling Type")]
        public string SellType { get; set; }

        [Required(ErrorMessage = "Please Enter Selling Price")]
        public decimal SellPrice { get; set; }

        
        public HttpPostedFileBase PreviewUpload { get; set; }      

       

    }
}