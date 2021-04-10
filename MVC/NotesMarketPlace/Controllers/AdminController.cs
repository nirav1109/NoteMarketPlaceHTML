using NotesMarketPlace.EmailTemplates;
using NotesMarketPlace.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO.Compression;

namespace NoteMarketPlace.Controllers
{
    public class AdminController : Controller
    {
      
        [Authorize]
        public ActionResult addadministrator()
        {
            using (NMEntities DBobj = new NMEntities())
            {
                var countrycode = DBobj.Countries.ToList();
                ViewBag.countryCode = new SelectList(countrycode, "CountryCode", "CountryCode");
                return View();
            }

        }


        [Authorize]
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
                    u.Password = "admin";
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


        [Authorize]
        [Route("addcategory/id")]
        //get
        public ActionResult addcategory(int?id)
        {
            NMEntities nm = new NMEntities();
            NoteCategories catdata = nm.NoteCategories.Where(x => x.NoteCategoryID == id).FirstOrDefault();
            return View(catdata);
        }


        [Authorize]
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



        [Authorize]
        [Route("addcountry/id")]
        //get
        public ActionResult addcountry(int?id)
        {
            NMEntities nm = new NMEntities();
            Countries cdata = nm.Countries.Where(x => x.CountryID == id).FirstOrDefault();
           
                return View(cdata);
           
        }


        [Authorize]
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


        [Authorize]
        [Route("addType/id")]
        public ActionResult addtype(int? id)
        {
            NMEntities nm = new NMEntities();
            NoteType typedata = nm.NoteType.Where(x => x.NoteTypeID == id).FirstOrDefault();
            return View(typedata);
            

        }


        [Authorize]
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

        [Authorize]
        //get
        public ActionResult changepassword()
        {
            return View();
        }

        [Authorize]
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

        [Authorize]
        public ActionResult forgot()
        {
            return View();
        }

        [Authorize]
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


        [Authorize]
        // GET: ManageType
        public ActionResult managetype(string typesearch, int? page)
        {
            NMEntities nm = new NMEntities();
            
               
                var typeuser = (from n in nm.NoteType.Where(x => x.TypeName.StartsWith(typesearch) || typesearch == null).ToList()
                                 join u in nm.Users.ToList() on n.CreatedBy equals u.UserID 
                                select new typeuser
                                {
                                    types = n,
                                    user = u
                                });
                ViewBag.tulist = typeuser.ToPagedList(page??1,5);
            ViewBag.tulistCount = typeuser.Count();
                return View();
            

        }

        [Authorize]
        // POST: ManageType
        [HttpPost]
        public ActionResult managetype(NoteType model)
        {
            return View();
        }


        [Authorize]
        // GET: ManageCategory
        public ActionResult managecategory(string catsearch, int? page)
        {
            NMEntities nm = new NMEntities();
            
                var catusr = (from c in nm.NoteCategories.Where(x=>x.CategoryName.StartsWith(catsearch) || catsearch == null ).ToList()
                             join u in nm.Users.ToList() on c.CreatedBy equals u.UserID                               
                              select new typeuser
                              {
                                  categorydata = c,
                                  user = u
                              });
                ViewBag.culist = catusr.ToPagedList(page ?? 1, 5);
            ViewBag.culistCount = catusr.Count();

            return View();
            
        }

        [Authorize]
        // POST: ManageCategory
        [HttpPost]
        public ActionResult managecategory(NoteCategories model)
        {
            return View();
        }



        [Authorize]
        // GET: ManageCountry
        public ActionResult managecountry(string countysearch, int? page)
        {
            using (NMEntities nm = new NMEntities())
            {
                var countrylist = nm.Countries.Where(x=>x.CountryName.StartsWith(countysearch) || countysearch == null).ToList();
                var usr = nm.Users.ToList();
                var cousr = (from co in countrylist                            
                             join u in usr on co.CreatedBy equals u.UserID into table1
                             from u in table1.ToList()
                             select new typeuser
                             {
                                 countrydata = co,
                                 user = u
                             });
                ViewBag.colist = cousr.ToPagedList(page ?? 1, 5);
                ViewBag.colistCount = cousr.Count();
                return View();
            }
        }


        [Authorize]
        // POST: ManageCountry
        [HttpPost]
        public ActionResult managecountry(Countries model)
        {
            return View();
        }




        [Authorize]
        public ActionResult dashboard(int? page, string dashsearch, int? Month)
        {
            NMEntities nm = new NMEntities();
            
                var day = DateTime.Now.AddDays(-7);
                ViewBag.NoteUnderReview = nm.NoteDetails.Where(x=>x.Status==4 && x.IsActive==true).Count();
                ViewBag.LastDownLoadedNote = nm.DownloadNotes.Where(x => x.IsSellerHasAllowedDownload == true && x.CreatedDate > day).Count();
                ViewBag.LastRegisteredUsers = nm.Users.Where(x=>x.UserRoleID == 3 && x.CreatedDate > day).Count();

                var categories = nm.NoteCategories.ToList();
                var notes = nm.NoteDetails.Where(x => x.Status == 2 &&
            (x.NoteTitle.StartsWith(dashsearch) || dashsearch == null) && (x.PublishedDate.Value.Month == Month || String.IsNullOrEmpty(Month.ToString()))).ToList();
                
                var users = nm.Users.ToList();
                var dnotes = nm.DownloadNotes.Where(x => x.IsSellerHasAllowedDownload == true ).ToList();
                var attachmentNote = nm.SellerNoteAttachment.ToList();
                var pn = (from n in notes
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

            ViewBag.publishednotes = pn.ToPagedList(page??1,5);
            ViewBag.publishednotesCount = pn.Count();





            return View();
        }

        [Authorize]
        public ActionResult members(int? page, string membersearch)
        {
            NMEntities nm = new NMEntities();

            
            var members = nm.Users.Where(x => x.UserRoleID == 3 && ((x.FirstName + " " + x.LastName).StartsWith(membersearch) || membersearch == null)).ToList();

            ViewBag.members = members.ToPagedList(page ?? 1, 5);
            ViewBag.membersCount = members.Count();

            return View();

        }



        [Authorize]
        [Route("memberdetails/id")]
        public ActionResult memberdetails(int? id)
        {
           

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

        [Authorize]
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


        [Authorize]
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


        [Authorize]
        //GET: NotesUnderReview
        public ActionResult notesunderreview(string FirstName, string nursearch, int? page)
        {
            using (NMEntities DBobj = new NMEntities())
            {
                var adminnotesunderreview = (from n in DBobj.NoteDetails.Where(x => (x.Status == 4 || x.Status == 5) && (x.NoteTitle.StartsWith(nursearch) || nursearch == null)).ToList()
                                             join cat in DBobj.NoteCategories.ToList() on n.NoteCategoryID equals cat.NoteCategoryID
                                             join usr in DBobj.Users.ToList() on n.SellerID equals usr.UserID
                                             where (usr.FirstName == FirstName || String.IsNullOrEmpty(FirstName))
                                             join stu in DBobj.NoteStatus.ToList() on n.Status equals stu.NoteStatusID
                                             select new AllProgressNotes
                                             {
                                                 user = usr,
                                                 SellerNotes = n,
                                                 Category = cat,
                                                 status = stu
                                             }).ToList();

                ViewBag.notesUnderReview = adminnotesunderreview.ToPagedList(page ?? 1, 5);
                ViewBag.notesUnderReviewCount = adminnotesunderreview.Count();

                ViewBag.Sellers = new SelectList(DBobj.Users.Where(x=>x.UserRoleID == 3).ToList(), "FirstName", "FirstName");

                return View();
            }

        }


        [Authorize]

        [Route("approveNotes/id")]
        //GET: ApproveNote
        public ActionResult approveNotes(int id)
        {
            using (NMEntities DBobj = new NMEntities())
            {
                NoteDetails note = DBobj.NoteDetails.FirstOrDefault(x => x.NoteID == id);
                if (note != null)
                {
                    note.Status = 2;
                    note.PublishedDate = DateTime.Now;
                    note.ActionBy = (int)Session["UserID"];
                    note.ModifiedBy = (int)Session["UserID"];
                    note.ModifiedDate = DateTime.Now;
                    DBobj.SaveChanges();
                }
                return RedirectToAction("notesUnderReview", "Admin");
            }
        }

        [Authorize]
        [Route("rejectNotes/id")]


        [Authorize]
        //GET: RejectNotes
        public ActionResult rejectNotes(int id, string adminRemarks)
        {
            using (NMEntities DBobj = new NMEntities())
            {
                NoteDetails note = DBobj.NoteDetails.FirstOrDefault(x => x.NoteID == id);
                if (note != null)
                {
                    note.Status = 3;
                    note.AdminRemarks = adminRemarks;
                    note.PublishedDate = DateTime.Now;
                    note.ActionBy = (int)Session["UserID"];
                    note.ModifiedBy = (int)Session["UserID"];
                    note.ModifiedDate = DateTime.Now;
                    DBobj.SaveChanges();
                }
                return RedirectToAction("notesUnderReview", "Admin");
            }
        }


        [Authorize]
        [Route("inReviewNotes/id")]
        //GET: InReviewNotes
        public ActionResult inReviewNotes(int id)
        {
            using (NMEntities DBobj = new NMEntities())
            {
                NoteDetails note = DBobj.NoteDetails.FirstOrDefault(x => x.NoteID == id);
                if (note != null)
                {
                    note.Status = 5;
                    note.PublishedDate = DateTime.Now;
                    note.ActionBy = (int)Session["UserID"];
                    note.ModifiedBy = (int)Session["UserID"];
                    note.ModifiedDate = DateTime.Now;
                    DBobj.SaveChanges();
                }
                return RedirectToAction("notesUnderReview", "Admin");
            }
        }


        [Authorize]
        public ActionResult downloadednotes(string Note, string Seller,string Buyer,string dnsearch, int? page)
        {
            using (NMEntities nm = new NMEntities())
            {
                var admindownloadnotes = (from dn in nm.DownloadNotes
                                          join n in nm.NoteDetails.Where(x=>x.NoteTitle.StartsWith(dnsearch) || dnsearch==null) on dn.NoteID equals n.NoteID
                                          where (dn.IsSellerHasAllowedDownload == true && dn.AttachmentPath != null && dn.AttachmentDownloadDate != null
                                          && (n.NoteTitle == Note || String.IsNullOrEmpty(Note))) 
                                          join nc in nm.NoteCategories on n.NoteCategoryID equals nc.NoteCategoryID
                                          join u in nm.Users on dn.SellerID equals u.UserID
                                          where (u.FirstName == Seller || String.IsNullOrEmpty(Seller))
                                          join s in nm.Users on dn.BuyerID equals s.UserID
                                          where (s.FirstName == Buyer || String.IsNullOrEmpty(Buyer))
                                          select new AllProgressNotes
                                             {
                                                 user = u,
                                                 SellerNotes = n,
                                                 Category = nc,
                                                 buyer = s,
                                                 downloadNote = dn
                                               
                                          }).ToList();

                ViewBag.downloadNote = admindownloadnotes.ToPagedList(page ?? 1, 5);
                ViewBag.downloadNoteCount = admindownloadnotes.Count();

                ViewBag.dnSellers = new SelectList(nm.Users.Where(x => x.UserRoleID == 3).ToList(), "FirstName", "FirstName");
                ViewBag.dnBuyers = new SelectList(nm.Users.Where(x => x.UserRoleID == 3).ToList(), "FirstName", "FirstName");
                ViewBag.dnNote = new SelectList(nm.NoteDetails.ToList(), "NoteTitle", "NoteTitle");
                return View();
            }

        }




        [Authorize]
        //GET: PublishedNotes
        public ActionResult publishednotes(string pnsearch, string FirstName,int?page)
        {
            NMEntities nm = new NMEntities();

            List<AllProgressNotes> adminpublishednotes = (from n in nm.NoteDetails.Where(x => x.Status == 2 && (x.NoteTitle.StartsWith(pnsearch) || pnsearch == null)).ToList()
                                                         join cat in nm.NoteCategories.ToList() on n.NoteCategoryID equals cat.NoteCategoryID
                                                         join usr in nm.Users.ToList() on n.SellerID equals usr.UserID
                                                         where usr.FirstName == FirstName || String.IsNullOrEmpty(FirstName)
                                                         join u in nm.Users.ToList() on n.ActionBy equals u.UserID
                                                         select new AllProgressNotes
                                                         {
                                                             user = usr,
                                                             SellerNotes = n,
                                                             Category = cat,
                                                             buyer = u
                                                         }).ToList();

            ViewBag.publishedNotes = adminpublishednotes.ToPagedList(page ?? 1,5);
            ViewBag.publishedNotesCount = adminpublishednotes.Count();

            ViewBag.pnSellers = new SelectList(nm.Users.Where(x => x.UserRoleID == 3).ToList(), "FirstName", "FirstName");

            return View();

        }


        [Authorize]
        public ActionResult rejectednotes(string rnsearch, string Seller,int?page)
        {
            NMEntities nm = new NMEntities();

            var adminRejectedNote = (from n in nm.NoteDetails.Where(x => x.Status == 3 && (x.NoteTitle.StartsWith(rnsearch) || rnsearch == null)).ToList()
                                     join cat in nm.NoteCategories on n.NoteCategoryID equals cat.NoteCategoryID
                                     join u in nm.Users on n.SellerID equals u.UserID
                                     where (u.FirstName == Seller || String.IsNullOrEmpty(Seller))
                                     join a in nm.Users on n.ActionBy equals a.UserID
                                     select new AllProgressNotes
                                     {
                                         user = u,
                                         SellerNotes = n,
                                         Category = cat,
                                         buyer = a,
                                     }).ToList();

            ViewBag.rejectedNote = adminRejectedNote.ToPagedList(page ?? 1,5);
            ViewBag.rejectedNoteCount = adminRejectedNote.Count();

            ViewBag.dnSellers = new SelectList(nm.Users.Where(x => x.UserRoleID == 3).ToList(), "FirstName", "FirstName");
            return View();
        }


        [Authorize]
        public ActionResult manageadministrator(string adminsearch , int?page)
        {
            NMEntities nm = new NMEntities();
            
                var admins = (from u in nm.Users
                              where u.UserRoleID == 2 && (u.FirstName.StartsWith(adminsearch) || adminsearch == null)
                              join up in nm.UserProfile on u.UserID equals up.UserID                               
                              select new AllProgressNotes
                              {
                                  uprofiledata =up,
                                  user = u
                              }).ToList();
                ViewBag.administrator = admins.ToPagedList(page ?? 1, 5);
                ViewBag.administratorCount = admins.Count();

            return View();
            
           
        }



        [Authorize]
        public ActionResult managesystemconfiguration()
        {
            return View();
        }

        [Authorize]
        public ActionResult notedetails()
        {
            return View();
        }


        [Authorize]
        public ActionResult spamreports(string spamsearch,int? page)
        {
            NMEntities nm = new NMEntities();
            var spamReports = (from sr in nm.SpamReports
                          join u in nm.Users on sr.ReportByID equals u.UserID
                          join n in nm.NoteDetails on sr.NoteID equals n.NoteID
                          where(n.NoteTitle.StartsWith(spamsearch) || spamsearch == null)
                          join c in nm.NoteCategories on n.NoteCategoryID equals c.NoteCategoryID
                          select new AllProgressNotes
                          {
                              Category =c,
                              user = u,
                              spam =sr,
                              SellerNotes=n
                          }).ToList();
            ViewBag.spams = spamReports.ToPagedList(page ?? 1,5);
            ViewBag.spamsCount = spamReports.Count();

            return View();
        }



        [Authorize]
        [Route("adminDownloadNote/id")]
        //GET: AdminDownloadNote
        public ActionResult adminDownloadNote(int id)
        {
            using (NMEntities DBobj = new NMEntities())
            {
                SellerNoteAttachment sellerAttachement = DBobj.SellerNoteAttachment.Where(x => x.NoteID == id).FirstOrDefault();

                //Return files

                var filesPath = sellerAttachement.FilePath.Split(';');
                var filesName = sellerAttachement.FileName.Split(';');
                using (var ms = new MemoryStream())
                {
                    using (var z = new ZipArchive(ms, ZipArchiveMode.Create, true))
                    {
                        foreach (var FilePath in filesPath)
                        {
                            string FullPath = Path.Combine(Server.MapPath(FilePath));
                            string FileName = Path.GetFileName(FullPath);
                            if (FileName == "adminDownloadNote")
                            {
                                continue;
                            }
                            else
                            {
                                z.CreateEntryFromFile(FullPath, FileName);
                            }
                        }
                    }
                    return File(ms.ToArray(), "application/zip", "Attachement.zip");
                }
            }
        }

        [Authorize]
        [Route("deleteType/id")]


        [Authorize]
        //GET: DeleteType
        public ActionResult deleteType(int id)
        {
            NMEntities DBobj = new NMEntities();
            var typedetail = DBobj.NoteType.Where(x => x.NoteTypeID == id).FirstOrDefault();
            typedetail.IsActive = false;
            DBobj.SaveChanges();
            return RedirectToAction("managetype", "Admin");
        }

        [Authorize]
        [Route("deleteCategory/id")]
        //GET: DeleteCategory
        public ActionResult deleteCategory(int id)
        {
            NMEntities DBobj = new NMEntities();
            var categorydetail = DBobj.NoteCategories.Where(x => x.NoteCategoryID == id).FirstOrDefault();
            categorydetail.IsActive = false;
            DBobj.SaveChanges();
            return RedirectToAction("managecategory", "Admin");
        }


        [Authorize]
        [Route("deleteCountry/id")]
        //GET: DeleteCountry
        public ActionResult deleteCountry(int id)
        {
            NMEntities DBobj = new NMEntities();
            var countrydetail = DBobj.Countries.Where(x => x.CountryID == id).FirstOrDefault();
            countrydetail.IsActive = false;
            DBobj.SaveChanges();
            return RedirectToAction("managecountry", "Admin");
        }

        [Authorize]
        [Route("deleteAdmin/id")]
        //GET: DeleteAdmin
        public ActionResult deleteAdmin(int id)
        {
            NMEntities DBobj = new NMEntities();
            var admindetail = DBobj.Users.Where(x => x.UserID == id).FirstOrDefault();
            admindetail.IsActive = false;
            DBobj.SaveChanges();
            return RedirectToAction("manageadministrator", "Admin");
        }

        [Authorize]
        [Route("unpublishNote/id")]
        //GET: DeleteAdmin
        public ActionResult unpublishNote(int id,string adminRemarks)
        {
            using (NMEntities DBobj = new NMEntities())
            {
                NoteDetails note = DBobj.NoteDetails.FirstOrDefault(x => x.NoteID == id);
                Users u = DBobj.Users.Where(x => x.UserID == note.SellerID).FirstOrDefault();
                if (note != null)
                {
                    note.Status = 1005;
                    note.AdminRemarks = adminRemarks;
                    note.PublishedDate = DateTime.Now;
                    note.ActionBy = (int)Session["UserID"];
                    note.ModifiedBy = (int)Session["UserID"];
                    note.ModifiedDate = DateTime.Now;
                    DBobj.SaveChanges();
                    unpublishSellerNote.unpublishNote(u.FirstName,u.EmailID, adminRemarks);
                }
                return RedirectToAction("dashboard", "Admin");
            }
        }


        //GET: DeactivateUser
        [Authorize]
        public ActionResult deactivateUser(int uid)
        {
            using (NMEntities nm = new NMEntities())
            {
                Users udetail = nm.Users.Where(x => x.UserID == uid).FirstOrDefault();
                IQueryable<NoteDetails> ndetail = nm.NoteDetails.Where(x => x.SellerID == uid);
                foreach (var i in ndetail)
                {
                    i.IsActive = false;
                    SellerNoteAttachment nddetails = nm.SellerNoteAttachment.Where(x => x.NoteID == i.NoteID).FirstOrDefault();
                    nddetails.IsActive = false;
                }
                udetail.IsActive = false;

                nm.SaveChanges();
                return View("members", "Admin");
            }
        }

    }
}