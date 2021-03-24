using NotesMarketPlace.EmailTemplates;
using NotesMarketPlace.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        //get
        public ActionResult addcategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addcategory(NoteCategories catergory)
        {
            using (NMEntities nm = new NMEntities())
            {
                NoteCategories ng = new NoteCategories();
                ng.CategoryName = catergory.CategoryName;
                ng.Description = catergory.Description;
                ng.IsActive = true;
                ng.CreatedDate = DateTime.Now;
                nm.NoteCategories.Add(ng);
                nm.SaveChanges();
                ModelState.Clear();

            }
                return View();
        }


        //get
        public ActionResult addcountry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addcountry(Countries c)
        {
            if (ModelState.IsValid)
            {
                using (NMEntities nm = new NMEntities())
                {
                    Countries cr = new Countries();
                    cr.CountryCode = c.CountryCode;
                    cr.CountryName = c.CountryName;
                    cr.IsActive = true;
                    cr.CreatedDate = DateTime.Now;
                    nm.Countries.Add(cr);
                    nm.SaveChanges();
                }
            }
            
                return View();
        }

        //get
        public ActionResult addtype()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addtype(NoteType type)
        {
            if (ModelState.IsValid)
            {
                using (NMEntities nm = new NMEntities())
                {
                    NoteType  note= new NoteType();
                    note.TypeName = type.TypeName;
                    note.Description = type.Description;
                    note.IsActive = true;
                    note.CreatedDate = DateTime.Now;
                    nm.NoteType.Add(note);
                    nm.SaveChanges();
                    ModelState.Clear();
                }
               
            }
            return View();
        }
        //get
        public ActionResult changepassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changepassword(ChangePassword cp)
        {

            using (NMEntities nm = new NMEntities())
            {
                int id= (int)Session["Userid"];
                Users u = nm.Users.Where(x => x.UserID == id).FirstOrDefault();
                if (u.Password == cp.Password)
                {
                    u.Password = cp.NewPassword;
                    nm.SaveChanges();
                    ViewBag.PassMessage = "<p><span><i class='fas fa-check-circle'></i></span> Your Password has been Changed successfully</p>";
                  

                }

            }
                return View();
        }

        public ActionResult forgot()
        {
            return View();
        }
        [HttpPost]
        public ActionResult forgot(Users user)
        {
            using (NMEntities nm = new NMEntities())
            {
                string allowedChars = "";
                string passwordString = "";
                string temp = "";

                bool isValid = nm.Users.Any(x => x.EmailID == user.EmailID);
                if (isValid)
                {
                    Users u = nm.Users.Where(x => x.EmailID == user.EmailID).FirstOrDefault();

                    allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

                    allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";

                    allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";

                    char[] sep = { ',' };

                    string[] arr = allowedChars.Split(sep);





                    Random rand = new Random();

                    for (int i = 0; i < 6; i++)

                    {

                        temp = arr[rand.Next(0, arr.Length)];

                        passwordString += temp;

                    }

                    //  Save Password 

                    u.Password = passwordString;
                    nm.SaveChanges();

                    //Sending new password on mail
                    forgotemail.SendOtpToEmail(u, passwordString);

                    TempData["Message"] = "Otp Sent To Your Registered EmailAddress use it for login";
                    return RedirectToAction("Login", "User");
                }
                TempData["Message"] = "Invalid EmailAddress";
                return View();


            }
        }



        // GET: ManageType
        public ActionResult managetype(string model)
        {
            using (NMEntities nm = new NMEntities())
            {
                List<NoteType> notetypes = nm.NoteType.ToList();
                List<Users> users = nm.Users.ToList();
                var typeuser = (from n in notetypes
                                join u in users on n.CreatedBy equals u.UserID into table1
                                from u in table1.ToList()
                                select new typeuser
                                {
                                    types = n,
                                    user = u
                                });
                ViewBag.tulist = typeuser;
                return View();
            }

        }
        // POST: ManageType
        [HttpPost]
        public ActionResult managetype(NoteType model)
        {
            return View();
        }



        // GET: ManageCategory
        public ActionResult managecategory(string model)
        {
            using (NMEntities nm = new NMEntities())
            {
                var categorylist = nm.NoteCategories.ToList();
                var usr = nm.Users.ToList();
                var catusr = (from n in categorylist
                              join u in usr on n.CreatedBy equals u.UserID into table1
                              from u in table1.ToList()
                              select new typeuser
                              {
                                  categorydata = n,
                                  user = u
                              });
                ViewBag.culist = catusr;
                return View();
            }
        }
        // POST: ManageCategory
        [HttpPost]
        public ActionResult managecategory(NoteCategories model)
        {
            return View();
        }



        // GET: ManageCountry
        public ActionResult managecountry(string model)
        {
            using (NMEntities nm = new NMEntities())
            {
                var countrylist = nm.Countries.ToList();
                var usr = nm.Users.ToList();
                var cousr = (from co in countrylist
                             join u in usr on co.CreatedBy equals u.UserID into table1
                             from u in table1.ToList()
                             select new typeuser
                             {
                                 countrydata = co,
                                 user = u
                             });
                ViewBag.colist = cousr;
                return View();
            }
        }

        // POST: ManageCountry
        [HttpPost]
        public ActionResult managecountry(Countries model)
        {
            return View();
        }





        public ActionResult dashboard()
        {
            using (NMEntities nm = new NMEntities())
            {
                var day = DateTime.Now.AddDays(-7);
                ViewBag.NoteUnderReview = nm.NoteDetails.Where(x=>x.Status==4 && x.IsActive==true).Count();
                ViewBag.LastDownLoadedNote = nm.DownloadNotes.Where(x => x.IsSellerHasAllowedDownload == true && x.CreatedDate > day).Count();
                ViewBag.LastRegisteredUsers = nm.Users.Where(x=>x.UserRoleID == 3 && x.CreatedDate > day).Count();

                var categories = nm.NoteCategories.ToList();
                var notes = nm.NoteDetails.Where(x => x.Status == 2 ).ToList();
                
                var users = nm.Users.ToList();
                var dnotes = nm.DownloadNotes.Where(x => x.IsSellerHasAllowedDownload == true ).ToList();
                var attachmentNote = nm.SellerNoteAttachment.ToList();
                ViewBag.publishednotes = (from n in notes
                                          join ct in categories on n.NoteCategoryID equals ct.NoteCategoryID into table1
                                          from ct in table1.ToList()
                                          join usr in users on n.SellerID equals usr.UserID into table2
                                          from usr in table2.ToList()
                                          join anote in attachmentNote on n.NoteID equals anote.NoteID into table3
                                          from anote in table3.ToList()
                                          select new AllProgressNotes
                                          {
                                              Category = ct,
                                              user = usr,
                                              Attachmnet = anote,
                                              SellerNotes = n
                                          });

               

                
                              
            }
                return View();
        }

        public ActionResult members()
        {
            using (NMEntities nm = new NMEntities())
            {
                var u = nm.Users.ToList();
                var s = nm.NoteDetails.ToList();
                 
            }
            return View();
        }


        public ActionResult memberdetails()
        {
           int  id = 1;

            using (NMEntities nm = new NMEntities())
            {
                var user = nm.Users.Where(x => x.UserID == id).ToList();
                var up = nm.UserProfile.Where(x => x.UserID == id).ToList();
                var notes = nm.NoteDetails.ToList();
                var category = nm.NoteCategories.ToList();
                var status = nm.NoteStatus.ToList();
                var userProfileDate = (from usr in user
                                       join updata in up on usr.UserID equals updata.UserID
                                       join n in notes on usr.UserID equals n.SellerID
                                       join cat in category on n.NoteCategoryID equals cat.NoteCategoryID                                       
                                       join cn in nm.Countries on n.CountryID equals cn.CountryID
                                       join ns in nm.NoteStatus on n.Status equals ns.NoteStatusID

                                       select new AllProgressNotes
                                       {
                                           user = usr,
                                           uprofiledata = updata,
                                           SellerNotes = n,
                                           Category = cat,
                                           country = cn,
                                           status=ns

                                       }).ToList();
                ViewBag.profile = userProfileDate;

            }
                return View();
        }


        public ActionResult myprofile()
        {

            using (NMEntities nm = new NMEntities())
            {
                int id = (int)Session["Userid"];
                var user = nm.Users.Where(x => x.UserID == id).FirstOrDefault();
                UserProfile userprofiles = nm.UserProfile.Where(x => x.UserID == id).FirstOrDefault();
                ViewBag.firstName = user.FirstName;
                ViewBag.lastName = user.LastName;
                ViewBag.email = user.EmailID;

                var countrycode = nm.Countries.ToList();
                ViewBag.countryCode = new SelectList(countrycode, "CountryCode", "CountryCode");


                return View();


            }
        }

        [HttpPost]

        public ActionResult myprofile(UserProfileData upd)
        {
            
                using (NMEntities nm = new NMEntities())
                {
                    int id = (int)Session["Userid"];
                    
                    var countrycode = nm.Countries.ToList();
                    ViewBag.countryCode = new SelectList(countrycode, "CountryCode", "CountryCode");



                    Users u = nm.Users.Where(x => x.UserID == id).FirstOrDefault();
                    u.FirstName = upd.FirstName;
                    u.LastName = upd.LastName;


                    int p = nm.UserProfile.Where(x => x.UserID == id).Count();
                    if (p > 0)
                    {

                        UserProfile profile = nm.UserProfile.Where(x => x.UserID == id).FirstOrDefault();

                        profile.UserID = (int)Session["Userid"];
                        profile.State = "NULL";
                        profile.CountryCode = upd.CountryCode;
                        profile.CountryID = 1;
                        profile.AddressLine1 = "NULL";
                      
                   
                        
                        profile.IsActive = true;
                        profile.ModifiedDate = DateTime.Now;
                        profile.University = "NULL";
                        profile.ZipCode = "NULL";
                        profile.College = "NULL";
                        profile.City = "NULL";
                        profile.SecondaryEmailAddress = upd.SecondaryEmailAddress;
                        profile.PhoneNumber = upd.PhoneNumber;

                        nm.SaveChanges();
                        string path = Path.Combine(Server.MapPath("~/Member/" + Session["Userid"].ToString()));

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        if (upd.ProfilePicture != null && upd.ProfilePicture.ContentLength > 0)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(upd.ProfilePicture.FileName);
                            string extension = Path.GetExtension(upd.ProfilePicture.FileName);
                            fileName = "DP_" + DateTime.Now.ToString("ddMMyyyy") + extension;
                            string finalpath = Path.Combine(path, fileName);
                            upd.ProfilePicture.SaveAs(finalpath);

                            profile.ProfilePicture = Path.Combine(("~/Member/" + Session["Userid"].ToString()), fileName);
                            nm.SaveChanges();
                        }

                    }
                    else
                    {
                        UserProfile profile = new UserProfile();

                        profile.UserID = (int)Session["Userid"];
                        profile.State = null;
                        profile.CountryCode = upd.CountryCode;
                        profile.CountryID = 1;
                        profile.AddressLine1 = "NULL";
                        
                     
                        
                        profile.IsActive = true;
                        profile.ModifiedDate = DateTime.Now;
                       
                        profile.ZipCode = "NULL";
                        
                        profile.City = "NULL";
                        profile.SecondaryEmailAddress = upd.SecondaryEmailAddress;
                        profile.PhoneNumber = upd.PhoneNumber;
                        nm.UserProfile.Add(profile);
                        nm.SaveChanges();
                        string path = Path.Combine(Server.MapPath("~/Member/" + Session["Userid"].ToString()));

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        if (upd.ProfilePicture != null && upd.ProfilePicture.ContentLength > 0)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(upd.ProfilePicture.FileName);
                            string extension = Path.GetExtension(upd.ProfilePicture.FileName);
                            fileName = "DP_" + DateTime.Now.ToString("ddMMyyyy") + extension;
                            string finalpath = Path.Combine(path, fileName);
                            upd.ProfilePicture.SaveAs(finalpath);

                            profile.ProfilePicture = Path.Combine(("~/Member/" + Session["Userid"].ToString()), fileName);
                            nm.SaveChanges();
                        }


                    }

                
            }




            return View();
        }


        public ActionResult manageadministrator()
        {
            return View();
        }

        public ActionResult downloadednotes()
        {
            return View();
        }


        public ActionResult managesystemconfiguration()
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