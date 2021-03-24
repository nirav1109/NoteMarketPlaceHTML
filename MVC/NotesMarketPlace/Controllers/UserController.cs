using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NotesMarketPlace.Models;
using NotesMarketPlace.EmailTemplates;
using System.IO;
using System.IO.Compression;
using PagedList;
using PagedList.Mvc;

namespace NoteMarketPlace.Controllers
{
    public class UserController : Controller
    {


        //Get Sign-Up
        public ActionResult signup()
        {
            return View();
        }


        [HttpPost]
        public ActionResult signup(SignUp model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    using (NMEntities nm = new NMEntities())
                    {
                        Users u = new Users();

                        u.FirstName = model.FirstName;
                        u.LastName = model.LastName;
                        u.EmailID = model.EmailID;
                        u.UserRoleID = 3;
                        u.IsActive = true;
                        u.CreatedDate = DateTime.Now;
                        u.Password = model.Password;
                        u.IsEmailVerified = false;

                        nm.Users.Add(u);
                        nm.SaveChanges();

                        if (u.UserID > 0)
                        {

                            ModelState.Clear();
                            ViewBag.IsSuccess = "<p><span><i class='fas fa-check-circle'></i></span> Your account has been successfully created </p>";
                            TempData["name"] = model.FirstName;


                            //  Email Verification Link
                            var activationCode = model.Password;
                            var verifyUrl = "/User/VerifyAccount/" + activationCode;
                            var activationlink = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);


                            // Sending Email
                            emailVerification.SendVerifyLinkEmail(u, activationlink);
                            ViewBag.Title = "NotesMarketPlace";


                            return RedirectToAction("emailverification", "User");
                        }
                    }

                }

                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }



        //Verify User Account 
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            using (NMEntities nm = new NMEntities())
            {
                nm.Configuration.ValidateOnSaveEnabled = false;                                                                
                var em = nm.Users.Where(x => x.Password == id).FirstOrDefault();
                if (em != null)
                {
                    em.IsEmailVerified = true;
                    nm.SaveChanges();
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            TempData["Message"] = "Your Email Is Verified You Can Login Here";
            return RedirectToAction("Login", "User");

        }


        public ActionResult login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult login(Users m)
        {
            using (NMEntities nm = new NMEntities())
            {

                Users u = new Users();
                bool isValid = nm.Users.Any(x => x.EmailID == m.EmailID && x.Password == m.Password);


                if (isValid)
                {



                    if (nm.Users.Any(x => x.EmailID == m.EmailID && x.IsEmailVerified == true && x.IsActive == true))
                    {

                        FormsAuthentication.SetAuthCookie(m.EmailID, false);
                        Session["EmailID"] = m.EmailID;

                        var user = nm.Users.Where(x => x.EmailID == m.EmailID).FirstOrDefault();
                        Session["FullName"] = user.FirstName + " " + user.LastName;
                        Session["Userid"] = user.UserID;
                        if (user.UserRoleID != 3) //check user complete his/her profile or note and redirect regarding it.
                        {
                            return RedirectToAction("dashboard", "Admin");
                        }
                        else
                        {
                            int upCheck = nm.UserProfile.Where(x => x.UserID == user.UserID).Count();
                            if (upCheck > 0)
                            {
                                return RedirectToAction("dashboard", "User");
                            }
                            else
                            {
                                return RedirectToAction("userProfile", "User");
                            }
                        }

                    }
                    else
                    {
                        @TempData["message"] = "Your Email Address is not Verified, Please Verified it Once...!";
                    }


                }
                else
                {
                    ViewBag.log = "<p> The password that you've entered is incorrect </p>";
                    return View();
                }
            }
            return View();
        }



        //logout (destory session)
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");

        }



        public ActionResult forgot()
        {
            return View();
        }



        //generate new password and send it to the mail
        [HttpPost]
        public ActionResult forgot(Users user)
        {
            using (NMEntities nm = new NMEntities())
            {
                string allowedChars = "";
                string passwordString = "";
                string temp = "";


                //generate the rnadom password with password rules if email id is exists
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

                    //  Save new Password  to the db
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



        public ActionResult contactus()
        {
            return View();
        }



        
        [HttpPost]
        public ActionResult contactus(ContactUs c)
        {
            try
            {
                //get the user data if user is already registered.
                using (NMEntities nm = new NMEntities())
                {
                    ContactUs cu = new ContactUs();

                    cu.FullName = c.FullName;
                    cu.EmailID = c.EmailID;


                    cu.Subject = c.Subject;
                    cu.Comments = c.Comments;
                    cu.IsActive = true;
                    cu.CreatedDate = DateTime.Now;

                    string subject = cu.Subject;
                    string name = cu.FullName;
                    string comment = cu.Comments;



                    nm.ContactUs.Add(cu);
                    nm.SaveChanges();

                    if (cu.ContactUsID > 0)
                    {
                        //sending mail 
                        ModelState.Clear();
                        contactusemail.ContactUs(subject, name, comment);




                        return View();
                    }
                }


                return View();
            }
            catch (Exception e)
            {
                return View(e);
            }
        }


        [HttpGet]
        public ActionResult addnote()
        {
            using (NMEntities nm = new NMEntities())
            {                
                    ViewBag.notecategoies = new SelectList(nm.NoteCategories.ToList(), "NoteCategoryID", "CategoryName");
                    ViewBag.notetypes = new SelectList(nm.NoteType.ToList(), "NoteTypeID", "TypeName");
                    ViewBag.countries = new SelectList(nm.Countries.ToList(), "CountryID", "CountryName");
            }
            return View();
        }


        [HttpPost]
        public ActionResult addnote(NoteDetailsDummy notedetails,string submit)
        {

            if (ModelState.IsValid)
            {
                if (notedetails.SellType == "Paid" && notedetails.PreviewUpload == null)
                {
                    using(NMEntities nm = new NMEntities())
                    {
                        ViewBag.previewmessage = "Note Preview Is Required For Paid Note...";
                        ViewBag.notecategoies = new SelectList(nm.NoteCategories.ToList(), "NoteCategoryID", "CategoryName");
                        ViewBag.notetypes = new SelectList(nm.NoteType.ToList(), "NoteTypeID", "TypeName");
                        ViewBag.countries = new SelectList(nm.Countries.ToList(), "CountryID", "CountryName");
                        return View();
                    }
                }
               

                using (NMEntities nm = new NMEntities())
                {
                    NoteDetails nd = new NoteDetails();
                    SellerNoteAttachment sna = new SellerNoteAttachment();
                    nd.SellerID = (int)Session["UserID"];
                    if (submit == "Save")
                    {
                        nd.Status = 1;
                    }
                    if (submit == "Publish")
                    {
                        nd.Status = 4;
                        nd.ModifiedDate = DateTime.Now;
                    }
                    nd.NoteTitle = notedetails.NoteTitle;
                    nd.NoteCategoryID = notedetails.NoteCategoryID;
                    nd.NoteTypeID = notedetails.NoteTypeID;
                    nd.NumberOfPages = notedetails.NumberOfPages;
                    nd.NoteDescription = notedetails.NoteDescription;
                    nd.UniversityInformation = notedetails.UniversityInformation;
                    nd.CountryID = notedetails.CountryID;
                    nd.Course = notedetails.Course;
                    nd.CourseCode = notedetails.CourseCode;
                    nd.ProfessorName = notedetails.ProfessorName;
                    nd.SellType = notedetails.SellType;
                    nd.SellPrice = notedetails.SellPrice;
                    nd.IsActive = true;
                    nd.CreatedDate = DateTime.Now;
                    nd.ModifiedDate = DateTime.Now;
                    nd.ModifiedBy = (int)Session["Userid"];
                    nd.CreatedBy = (int)Session["Userid"];

                    nm.NoteDetails.Add(nd);
                    nm.SaveChanges();


                    //file or folder path
                    string path = Path.Combine(Server.MapPath("~/Member/" + Session["UserID"].ToString()), nd.NoteID.ToString());


                    //check path is exists or not
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    if (notedetails.DisplayPicture != null && notedetails.DisplayPicture.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(notedetails.DisplayPicture.FileName);
                        string extension = Path.GetExtension(notedetails.DisplayPicture.FileName);
                        fileName = "DP_" + DateTime.Now.ToString("ddMMyyyy") + extension;
                        string finalpath = Path.Combine(path, fileName);
                        notedetails.DisplayPicture.SaveAs(finalpath);

                        nd.DisplayPicture = Path.Combine(("~/Member/" + Session["UserID"].ToString() + "/" + nd.NoteID.ToString() + "/"), fileName);
                        nm.SaveChanges();
                    }
                    if (notedetails.PreviewUpload != null && notedetails.PreviewUpload.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(notedetails.PreviewUpload.FileName);
                        string extension = Path.GetExtension(notedetails.PreviewUpload.FileName);
                        fileName = "PREVIEW_" + DateTime.Now.ToString("ddMMyyyy") + extension;
                        string finalpath = Path.Combine(path, fileName);
                        notedetails.PreviewUpload.SaveAs(finalpath);

                        nd.PreviewUpload = Path.Combine(("~/Member/" + Session["UserID"].ToString() + "/" + nd.NoteID.ToString() + "/"), fileName);
                        nm.SaveChanges();
                    }

                    string attachmentpath = Path.Combine(Server.MapPath("~/Member/" + Session["UserID"].ToString() + "/" + nd.NoteID.ToString()), "attachement");

                    if (!Directory.Exists(attachmentpath))
                    {
                        Directory.CreateDirectory(attachmentpath);
                    }
                    if (notedetails.NoteAttachment != null && notedetails.NoteAttachment[0].ContentLength > 0)
                    {
                        var count = 1;
                        var FilePath = "";
                        var FileName = "";
                        foreach (var file in notedetails.NoteAttachment)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            string extension = Path.GetExtension(file.FileName);
                            fileName = "Attachment_" + count + "_" + DateTime.Now.ToString("ddMMyyyy") + extension;
                            string finalpath = Path.Combine(attachmentpath, fileName);
                            file.SaveAs(finalpath);
                            FileName += fileName + ";";
                            FilePath += Path.Combine("/Member/" + Session["UserID"].ToString() + "/" + nd.NoteID.ToString() + "/attachement/", fileName) + ";";
                            count++;
                        }
                        sna.FileName = FileName;
                        sna.FilePath = FilePath;

                        sna.NoteID = nd.NoteID;
                        sna.IsActive = true;
                        sna.CreatedDate = DateTime.Now;
                        sna.CreatedBy = (int)Session["UserID"];
                        nm.SaveChanges();

                    }
                    nm.SellerNoteAttachment.Add(sna);
                    nm.SaveChanges();

                    //Sending mail to admin
                    if (submit == "Publish")
                    {
                        informAdminForPublishNote.sellerRequestToAdmin(Session["FullName"].ToString(), nd.NoteTitle);
                    }

                }
                ModelState.Clear();
                return RedirectToAction("dashboard", "User");
            }
            using(NMEntities nm=new NMEntities())
            {
                ViewBag.notecategoies = new SelectList(nm.NoteCategories.ToList(), "NoteCategoryID", "CategoryName");
                ViewBag.notetypes = new SelectList(nm.NoteType.ToList(), "NoteTypeID", "TypeName");
                ViewBag.countries = new SelectList(nm.Countries.ToList(), "CountryID", "CountryName");
                return View();
            }
            
        }


        [Route("noteReview/id")]
        //GET: NoteReview
        public ActionResult noteReview(NoteReviews model, int noteID)
        {
            if (ModelState.IsValid)
            {
                using (NMEntities DBobj = new NMEntities())
                {
                    int id = (int)Session["Userid"];

                    NoteReviews nr = new NoteReviews();
                    var downloaddata = DBobj.DownloadNotes.Where(x => x.NoteID == noteID && x.BuyerID == id).FirstOrDefault();
                    nr.AgainstDownloadID = downloaddata.DownloadNoteID;
                    nr.ReviewByID = id;
                    nr.Ratings = model.Ratings;
                    nr.Comments = model.Comments;
                    nr.NoteID = noteID;
                    nr.IsActive = true;
                    nr.CreatedDate = DateTime.Now;
                    nr.CreatedBy = id;
                    nr.ModifiedBy = id;
                    nr.ModifiedDate = DateTime.Now;

                    DBobj.NoteReviews.Add(nr);
                    DBobj.SaveChanges();

                }
            }
            return RedirectToAction("mydownloads", "User");
        }


        // GET
        public ActionResult dashboard(string submit,string searchnotes)
        {
            using (NMEntities DBobj = new NMEntities())
            {
                int userid = (int)Session["UserID"];

                ViewBag.mySoldNotes = DBobj.DownloadNotes.Where(x => x.SellerID == userid && x.IsSellerHasAllowedDownload == true).Count();
                var earning = DBobj.DownloadNotes.Where(x => x.SellerID == userid && x.IsSellerHasAllowedDownload == true).Select(x => x.PurchasePrice).Sum();
                if (earning > 0)
                {
                    ViewBag.totalEarning = earning;
                }
                else
                {
                    ViewBag.totalEarning = 0;
                }
                ViewBag.myDownloadNotes = DBobj.DownloadNotes.Where(x => x.BuyerID == userid && x.IsSellerHasAllowedDownload == true).Count();
                ViewBag.myRejectedNotes = DBobj.NoteDetails.Where(x => x.SellerID == userid && x.Status == 3).Count();
                ViewBag.buyerRequests = DBobj.DownloadNotes.Where(x => x.SellerID == userid).Count();

                List<NoteDetails> SellerNotes = null;
                if (submit == "search1")
                {
                    SellerNotes = DBobj.NoteDetails.Where(x => x.SellerID == userid && (x.Status == 4 || x.Status == 1) &&
                                                        (x.NoteTitle.StartsWith(searchnotes) || searchnotes == null)).ToList();
                }
                else
                {
                    SellerNotes = DBobj.NoteDetails.Where(x => x.SellerID == userid && (x.Status == 4 || x.Status == 1)).ToList();
                }
                  
                var ProgressNotes = (from sell in SellerNotes
                                     join cate in DBobj.NoteCategories.ToList() on sell.NoteCategoryID equals cate.NoteCategoryID                                   
                                     orderby sell.CreatedDate descending
                                     join stu in DBobj.NoteStatus.ToList() on sell.Status equals stu.NoteStatusID                                     
                                     select new AllProgressNotes
                                     {
                                         SellerNotes = sell,
                                         Category = cate,
                                         status = stu
                                     }).ToList();


                ViewBag.inProgressNotes = ProgressNotes;
                ViewBag.inProgressNotesCount = ProgressNotes.Count();

                List<NoteDetails> PublishedNote = null;
                if (submit == "search2")
                {
                    PublishedNote = DBobj.NoteDetails.Where(x => x.SellerID == userid && x.Status == 2 &&
                                                           (x.NoteTitle.StartsWith(searchnotes) || searchnotes == null)).ToList();
                }
                else
                {
                    PublishedNote = DBobj.NoteDetails.Where(x => x.SellerID == userid && x.Status == 2).ToList();
                }



                

                var PublishedNoted = (from sell in PublishedNote
                                      join cate in DBobj.NoteCategories.ToList() on sell.NoteCategoryID equals cate.NoteCategoryID                                      
                                      orderby sell.PublishedDate descending
                                      select new AllPublishNotes
                                      {
                                          SellerNotes = sell,
                                          Category = cate,

                                      }).ToList();

                ViewBag.publishedNote = PublishedNoted;
                ViewBag.publishedNoteCount = PublishedNoted.Count();



                return View();
            }
           
        }


        //GET: Notedetails
        [Route("notedetail /{id}")]
        public ActionResult notedetail(int? id)
        {

            using (NMEntities DBobj = new NMEntities())
            {

                var ni = DBobj.NoteDetails.Where(x => x.NoteID == id).FirstOrDefault();
                NoteCategories noteCategory = DBobj.NoteCategories.Find(ni.NoteCategoryID);
                ViewBag.Category = noteCategory.CategoryName;
                Countries country = DBobj.Countries.Find(ni.CountryID);
                ViewBag.Country = country.CountryName;

                var reviewdetail = (from nr in DBobj.NoteReviews
                                    join n in DBobj.NoteDetails on nr.NoteID equals n.NoteID
                                    join us in DBobj.Users on nr.ReviewByID equals us.UserID
                                    join up in DBobj.UserProfile on nr.ReviewByID equals up.UserID
                                    orderby nr.CreatedDate descending
                                    select new AllProgressNotes
                                    {
                                        SellerNotes = n,
                                        notereview = nr,
                                        user = us,
                                        uprofiledata = up
                                    }).Take(3).ToList();

                ViewBag.reviewdetailbag = reviewdetail;
                ViewBag.reviewcount = reviewdetail.Count();
                ViewBag.ratingCount = DBobj.NoteReviews.Where(x => x.NoteID == id).Select(x => x.Ratings).Count();
                if (ViewBag.ratingcount > 0)
                {
                    ViewBag.ratingSum = DBobj.NoteReviews.Where(x => x.NoteID == id).Select(x => x.Ratings).Sum();
                }
                else
                {
                    ViewBag.ratingSum = "No Review Found !";
                }
                

               
                ViewBag.spamtotalcount = DBobj.SpamReports.Where(x => x.NoteID == id).Count();

                return View(ni);
            }
        }



        //get
        public ActionResult userprofile()
        {
            using (NMEntities nm = new NMEntities())
            {
                int id = (int)Session["Userid"];
                var user = nm.Users.Where(x => x.UserID == id).FirstOrDefault();
                UserProfile userprofiles = nm.UserProfile.Where(x => x.UserID == id).FirstOrDefault();
                UserProfileData upd = new UserProfileData();
                ViewBag.firstName = user.FirstName;
                ViewBag.lastName = user.LastName;
                ViewBag.email = user.EmailID;

                var countryname = nm.Countries.ToList();
                ViewBag.countries = new SelectList(countryname, "CountryID", "CountryName");
                var countrycode = nm.Countries.ToList();
                ViewBag.countryCode = new SelectList(countrycode, "CountryID", "CountryCode");

                if (userprofiles != null)
                {
                    upd.DateOfBirth = userprofiles.DateOfBirth;
                    upd.AddressLine1 = userprofiles.AddressLine1;
                    upd.AddressLine2 = userprofiles.AddressLine2;
                    upd.City = userprofiles.City;
                    upd.State = userprofiles.State;
                    upd.ZipCode = userprofiles.ZipCode;
                    upd.University = userprofiles.University;
                    upd.College = userprofiles.College;
                    upd.PhoneNumber = userprofiles.PhoneNumber;
                   

                    return View(upd);
                }

                return View();
            }


        }

        [HttpPost]
        public ActionResult userprofile(UserProfileData upd)
        {
            
                using (NMEntities nm = new NMEntities())
                {
                    int id = (int)Session["Userid"];
                    var countryname = nm.Countries.ToList();
                    ViewBag.countries = new SelectList(countryname, "CountryID", "CountryName");
                    var countrycode = nm.Countries.ToList();
                    ViewBag.countryCode = new SelectList(countrycode, "CountryID", "CountryCode");
                    Users u = nm.Users.Where(x => x.UserID == id).FirstOrDefault();
                    u.FirstName = upd.FirstName;
                    u.LastName = upd.LastName;


                    int p = nm.UserProfile.Where(x => x.UserID == id).Count();
                    if (p > 0)
                    {

                        UserProfile profile = nm.UserProfile.Where(x => x.UserID == id).FirstOrDefault();

                        profile.UserID = (int)Session["Userid"];
                        profile.State = upd.State;
                        profile.CountryCode = upd.CountryCode;
                        profile.CountryID = upd.CountryID;
                        profile.AddressLine1 = upd.AddressLine1;
                        profile.AddressLine2 = upd.AddressLine2;
                        profile.DateOfBirth = upd.DateOfBirth;
                        profile.Gender = upd.Gender;
                        profile.IsActive = true;
                        profile.ModifiedDate = DateTime.Now;
                        profile.University = upd.University;
                        profile.ZipCode = upd.ZipCode;
                        profile.College = upd.College;
                        profile.City = upd.City;
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
                        profile.State = upd.State;
                        profile.CountryCode = upd.CountryCode;
                        profile.CountryID = upd.CountryID;
                        profile.AddressLine1 = upd.AddressLine1;
                        profile.AddressLine2 = upd.AddressLine2;
                        profile.DateOfBirth = upd.DateOfBirth;
                        profile.Gender = upd.Gender;
                        profile.IsActive = true;
                        profile.ModifiedDate = DateTime.Now;
                        profile.University = upd.University;
                        profile.ZipCode = upd.ZipCode;
                        profile.College = upd.College;
                        profile.City = upd.City;
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
            

            return RedirectToAction("dashboard", "User");
        }


        //Get
        public ActionResult buyersrequests(int? page)
        {
            using (NMEntities nm = new NMEntities())
            {
                int id = (int)Session["Userid"];     
                
                var by = (from n in nm.NoteDetails
                         join dn in nm.DownloadNotes on n.NoteID equals dn.NoteID
                          where dn.SellerID == id orderby dn.AttachmentDownloadDate descending
                          join cat in nm.NoteCategories on n.NoteCategoryID equals cat.NoteCategoryID
                         join u in nm.Users on dn.BuyerID equals u.UserID
                         join up in nm.UserProfile on dn.BuyerID equals up.UserID
                         join cc in nm.Countries on up.CountryID equals cc.CountryID
                         select new AllProgressNotes                
                          {
                              downloadNote = dn,
                              user = u,
                              SellerNotes = n,
                              Category =cat,
                              uprofiledata = up,
                              country = cc
                              
                          }).ToList().ToPagedList(page ?? 1,5);
                ViewBag.BuyerRequests = by;
                ViewBag.BuyerRequestsCount = by.Count();

            }

                return View();
        }


        [Route("acceptDownloadRequest /{id}")]
        public ActionResult acceptDownloadRequest(int? id)
        {
            using (NMEntities nm = new NMEntities())
            {

                var accepted = nm.DownloadNotes.Where(x => x.NoteID == id && x.IsSellerHasAllowedDownload == false).FirstOrDefault();
                accepted.IsSellerHasAllowedDownload = true;
                accepted.ModifiedBy = (int)Session["Userid"];
                accepted.ModifiedDate = DateTime.Now;
                var path = nm.SellerNoteAttachment.Where(x => x.NoteID == id && x.FilePath != null).FirstOrDefault();
                accepted.AttachmentPath = path.FilePath;
                nm.SaveChanges();

                int buyerId = accepted.BuyerID;
                var buyerInfo = nm.Users.Where(x=>x.UserID == buyerId);
                foreach (var i in buyerInfo)
                {
                    ViewBag.buyerName = i.FirstName + " " + i.LastName;
                    ViewBag.emailId = i.EmailID;
                }

                ViewBag.sellerName = Session["FullName"];
                informBuyer.mailToBuyer(ViewBag.buyerName, ViewBag.emailId, ViewBag.sellerName);
            }
                return RedirectToAction("buyersrequests", "User");
        }


        public ActionResult myrejectednotes()
        {
            using (NMEntities nm = new NMEntities())
            {
                int id = (int)Session["Userid"];
                var rejected = (from n in nm.NoteDetails
                                join cat in nm.NoteCategories on n.NoteCategoryID equals cat.NoteCategoryID
                                where n.Status == 3 && n.SellerID == id
                                select new AllProgressNotes
                                {
                                    SellerNotes = n,
                                    Category = cat
                                }).ToList();
                ViewBag.rejectedNote = rejected;
            }
            return View();
        }
        public ActionResult mysoldnotes()
        {
            using (NMEntities nm = new NMEntities())
            {
                int id = (int)Session["Userid"];
                var soldnotes = (from n in nm.NoteDetails
                                 join dn in nm.DownloadNotes on n.NoteID equals dn.NoteID
                                 where dn.IsSellerHasAllowedDownload == true && dn.SellerID == id
                                 join cat in nm.NoteCategories on n.NoteCategoryID equals cat.NoteCategoryID
                                 join u in nm.Users on dn.BuyerID equals u.UserID
                                 select new AllProgressNotes
                                 {
                                     SellerNotes = n,
                                     downloadNote = dn,
                                     Category = cat,
                                     user = u
                                 }).ToList();

                ViewBag.mysoldnotes = soldnotes;
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
                int id = (int)Session["Userid"];
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

        public ActionResult emailverification()
        {
            return View();
        }
        public ActionResult faq()
        {
            return View();
        }
        
        public ActionResult home()
        {
            return View();
        }

        [Route("downloadflow /{ id }")]
        public ActionResult downloadflow(int? id)
        {
             if (Session["Userid"] != null)
             {

                 using (NMEntities nm = new NMEntities())
                 {
                    int sellerId=0;



                    //Free Note
                    int isFree = nm.NoteDetails.Where(x => x.NoteID == id && x.SellType == "Free").Count();
                    if (isFree > 0)
                    {
                        DownloadNotes dn = new DownloadNotes();
                        var notetitle = nm.NoteDetails.Where(x => x.NoteID == id);
                        var sellerAttachement = nm.SellerNoteAttachment.Where(x => x.NoteID == id).FirstOrDefault();

                        dn.BuyerID = (int)Session["Userid"];
                        dn.IsSellerHasAllowedDownload = false;
                        dn.IsPaid = true;
                        dn.IsAttachmentDownloaded = false;
                        dn.CreatedDate = DateTime.Now;
                        dn.CreatedBy = (int)Session["Userid"];
                        dn.IsActive = true;
                        foreach (var iv in notetitle)
                        {
                            dn.NoteID = iv.NoteID;
                            dn.SellerID = iv.SellerID;
                            dn.PurchasePrice = iv.SellPrice;
                            dn.NoteTitle = iv.NoteTitle;
                            dn.NoteCategory = (iv.NoteCategoryID).ToString();
                            sellerId = iv.SellerID;
                        }

                        nm.DownloadNotes.Add(dn);
                        nm.SaveChanges();

                        //return files

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
                                    if (FileName == "User")
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        z.CreateEntryFromFile(FullPath, FileName);
                                    }
                                }
                            }
                            return File(ms.ToArray(),"application/zip","Attachement.zip");
                        }
                    }
                      

                        


                    //Paid Note 

                    var i = nm.NoteDetails.Where(x => x.NoteID == id && x.SellType == "Paid").Count();
                     if (i > 0)
                     {
                         DownloadNotes dn = new DownloadNotes();
                         var notetitle = nm.NoteDetails.Where(x => x.NoteID == id);
                        
                         dn.BuyerID = (int)Session["Userid"];
                         dn.IsSellerHasAllowedDownload = false;
                         dn.IsPaid = true;
                         dn.IsAttachmentDownloaded = false;
                         dn.CreatedDate = DateTime.Now;
                         dn.CreatedBy = (int)Session["Userid"];
                         dn.IsActive = true;
                         foreach (var iv in notetitle)
                         {
                            dn.NoteID = iv.NoteID;
                             dn.SellerID = iv.SellerID;
                             dn.PurchasePrice = iv.SellPrice;
                             dn.NoteTitle = iv.NoteTitle;
                             dn.NoteCategory = (iv.NoteCategoryID).ToString();
                            sellerId = iv.SellerID;
                         }

                         nm.DownloadNotes.Add(dn);
                         nm.SaveChanges();
                                                                                                              
                        var sellerInfo = nm.Users.Where(x => x.UserID == sellerId).ToList();
                        foreach (var s in sellerInfo)
                        {
                            ViewBag.sellerName = s.FirstName +" "+ s.LastName;
                            ViewBag.sellerEmailId = s.EmailID;
                        }                     
                        

                       
                        string buyerName= (string)Session["FullName"];



                        //Send mail to owner
                        informSeller.SellerPublishNote(ViewBag.sellerName, ViewBag.sellerEmailId, buyerName);
                    }
                 }
                 return RedirectToAction("notedetail","User",new { id = id});
             }
             else
             {
                 return RedirectToAction("login", "User");
             }
            
        }       


        public ActionResult mydownloads()
        {
            try
            {
                using (NMEntities nm = new NMEntities())
                {
                    nm.Configuration.LazyLoadingEnabled = false;
                    int id = (int)Session["Userid"];

                   
                    var downloadNotes = (from dn in nm.DownloadNotes
                                        join n in nm.NoteDetails on dn.NoteID equals n.NoteID
                                        where dn.BuyerID == id && dn.IsSellerHasAllowedDownload == true && dn.IsActive == true
                                        join nc in nm.NoteCategories on n.NoteCategoryID equals nc.NoteCategoryID
                                        join u in nm.Users on dn.SellerID equals u.UserID
                                      
                                        select new AllProgressNotes
                                        {
                                            user = u,
                                            SellerNotes = n,
                                            downloadNote = dn,
                                            Category = nc
                                        }).ToList();
                    ViewBag.DownloadNotes = downloadNotes;
                    ViewBag.DownloadNotesCount = downloadNotes.Count();

                }
            }
            catch (Exception e)
            {
                return View(e);
            }
            
                return View();
            
        }
        //Get 
        public ActionResult searchnotes(string search,string NoteTypeID,string NoteCategoryID,string UniversityInformation, string CountryID,string Course)
        {
            using (NMEntities nm = new NMEntities())
            {

                var notecategory = nm.NoteCategories.Distinct().ToList();
                var notetype = nm.NoteType.Distinct().ToList();
                var country = nm.Countries.Distinct().ToList();
                var university_course = nm.NoteDetails.Distinct().ToList();
                ViewBag.notecategoies = new SelectList(notecategory.Distinct(), "NoteCategoryID", "CategoryName");
                ViewBag.notetypes = new SelectList(notetype.Distinct(), "NoteTypeID", "TypeName");
                ViewBag.countries = new SelectList(country.Distinct(), "CountryID", "CountryName");
                ViewBag.universities = new SelectList(university_course.Distinct(), "UniversityInformation", "UniversityInformation");
                ViewBag.courses = new SelectList(university_course.Distinct(), "Course", "Course");






                var notes = nm.NoteDetails.Where(x => x.NoteTitle.StartsWith(search) || search == null).ToList();
                var cr = nm.Countries.ToList();

               

                var seachedNotes = (from n in notes
                                    join c in cr on n.CountryID equals c.CountryID into table1
                                    from c in table1.ToList() where (n.Status == 2 && (n.NoteTypeID.ToString() == NoteTypeID || String.IsNullOrEmpty(NoteTypeID)) 
                                    && (n.NoteCategoryID.ToString() == NoteCategoryID || String.IsNullOrEmpty(NoteCategoryID)) 
                                    && (n.UniversityInformation.ToString() == UniversityInformation || String.IsNullOrEmpty(UniversityInformation))
                                    && (n.CountryID.ToString() == CountryID || String.IsNullOrEmpty(CountryID))
                                    && (n.Course.ToString() == Course || String.IsNullOrEmpty(Course)))
                                    select new nd
                                    {
                                        note = n,
                                        countryname = c
                                    }).ToList();

                ViewBag.filterNotes = seachedNotes;

                ViewBag.nd = seachedNotes.Count();

               

                return View();
            }

        }


        [Route("spamReport/id")]
        public ActionResult spamReport(NoteReviews model,int id)
        {
            if (model.Comments!=null)
            {
                using(NMEntities nm=new NMEntities())
                {
                    int count = nm.SpamReports.Where(x => x.NoteID == id && x.ReportByID == (int)Session["Userid"]).Count();
                    if (count > 0)
                    {
                        ViewBag.spammsg = "You have already reported for this note";
                    }
                    else
                    {
                        SpamReports sr = new SpamReports();
                        sr.NoteID = id;
                        sr.ReportByID = (int)Session["Userid"];
                        sr.Remark = model.Comments;
                        sr.IsActive = true;
                        sr.CreatedBy = (int)Session["Userid"];
                        sr.ModifiedBy = (int)Session["Userid"];
                        sr.CreatedDate = DateTime.Now;                   
                        return RedirectToAction("mydownloads", "User");
                    }
                        
                }
                
            }

            return RedirectToAction("mydownloads","User");
        }

    }

   
    
}