using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NotesMarketPlace.Models
{
    public class UserProfileData
    {
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        public string EmailID { get; set; }

        public string Gender { get; set; }
        public string SecondaryEmailAddress { get; set; }
        [Required(ErrorMessage = "Please Select Country  Code")]
        public string CountryCode { get; set; }
        [Required(ErrorMessage = "Please Select Country ")]
        public int CountryID { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        public string PhoneNumber { get; set; }
        public HttpPostedFileBase ProfilePicture { get; set; }
        [Required(ErrorMessage = "Please enter your address")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public Nullable<System.DateTime> DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter your city")]

        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your state")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter your zip code")]
        public string ZipCode { get; set; }
     
        public string University { get; set; }
        public string College { get; set; }
    }
}