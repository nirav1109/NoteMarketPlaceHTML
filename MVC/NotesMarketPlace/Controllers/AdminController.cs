using NotesMarketPlace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoteMarketPlace.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult addadministrator()
        {
            using (NMEntities DBobj = new NMEntities())
            {
                var countrycode = DBobj.Countries.ToList();
                ViewBag.countryCode = new SelectList(countrycode, "CountryCode", "CountryCode");
                return View();
            }

        }
        // POST: Admin
        [HttpPost]
        public ActionResult addadministrator(AdminSignUp model)
        {
            if (ModelState.IsValid)
            {

                using (NMEntities DBobj = new NMEntities())

                {
                    Users u = new Users();
                    u.FirstName = model.FirstName;
                    u.LastName = model.LastName;
                    u.EmailID = model.EmailID;
                    u.UserRoleID = 2;
                    u.IsActive = true;
                    u.CreatedDate = DateTime.Now;
                    u.Password = "hdhd";
                    u.IsEmailVerified = true;

                    DBobj.Users.Add(u);
                    DBobj.SaveChanges();

                    if (u.UserID > 0)
                    {
                        UserProfile up = new UserProfile();
                        up.UserID = u.UserID;
                        up.CountryCode = model.CountryCode;
                        up.PhoneNumber = model.PhoneNumber;
                        up.AddressLine1 = "null";
                        up.City = "null";
                        up.State = "null";
                        up.ZipCode = "null";
                        up.CountryID = 1;
                        up.CreatedDate = DateTime.Now;
                        up.IsActive = true;

                        DBobj.UserProfile.Add(up);
                        DBobj.SaveChanges();
                        ModelState.Clear();
                        var countrycode = DBobj.Countries.ToList();
                        ViewBag.countryCode = new SelectList(countrycode, "CountryCode", "CountryCode");
                        ViewBag.IsSuccess = "<p><span><i class='fas fa-check-circle'></i></span> Admin added successfully.</p>";
                    }
                }

            }

            return View();
        }

        public ActionResult addcategory()
        {
            return View();
        }
        public ActionResult addcountry()
        {
            return View();
        }
        public ActionResult addtype()
        {
            return View();
        }
        public ActionResult changepassword()
        {
            return View();
        }
        public ActionResult dashboard()
        {
            return View();
        }
        public ActionResult downloadednotes()
        {
            return View();
        }
        public ActionResult forgot()
        {
            return View();
        }
        public ActionResult login()
        {
            return View();
        }
        public ActionResult managetype()
        {
            return View();
        }
        public ActionResult manageadministrator()
        {
            return View();
        }
        public ActionResult managecategory()
        {
            return View();
        }
        public ActionResult managecountry()
        {
            return View();
        }
        public ActionResult managesystemconfiguration()
        {
            return View();
        }
        public ActionResult memberdetails()
        {
            return View();
        }
        public ActionResult members()
        {
            return View();
        }
        public ActionResult myprofile()
        {
            return View();
        }
        public ActionResult notedetails()
        {
            return View();
        }
        public ActionResult notesunderreview()
        {
            return View();
        }
        public ActionResult publishednotes()
        {
            return View();
        }
        public ActionResult rejectednotes()
        {
            return View();
        }
        public ActionResult spamreports()
        {
            return View();
        }

    }
}