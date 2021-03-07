using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NotesMarketPlace.Models;
using NotesMarketPlace.EmailTemplates;
using System.IO;


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



        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            using (NMEntities nm = new NMEntities())
            {
                nm.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                                                                // Confirm password does not match issue on save changes
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
                        if (user.UserRoleID != 3)
                        {
                            return RedirectToAction("dashboard", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("dashboard", "User");
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




        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");

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



        public ActionResult contactus()
        {
            return View();
        }

        [HttpPost]
        public ActionResult contactus(ContactUs c)
        {
            try
            {

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
                var notecategory = nm.NoteCategories.ToList();
                var notetype = nm.NoteType.ToList();
                var country = nm.Countries.ToList();
                ViewBag.notecategoies = new SelectList(notecategory, "NoteCategoryID", "CategoryName");
                ViewBag.notetypes = new SelectList(notetype, "NoteTypeID", "TypeName");
                ViewBag.countries = new SelectList(country, "CountryID", "CountryName");
            }
            return View();
        }


        [HttpPost]
        public ActionResult addnote(NoteDetailsDummy notedetails)
        {
           
                if (ModelState.IsValid)
                {
                    if (notedetails.SellType == "Paid" && notedetails.PreviewUpload == null)
                    {
                         NMEntities nm = new NMEntities();
                        ViewBag.previewmessage = "Note Preview Is Required For Paid Note...";
                        var notecategory = nm.NoteCategories.ToList();
                        var notetype = nm.NoteType.ToList();
                        var country = nm.Countries.ToList();
                        ViewBag.notecategoies = new SelectList(notecategory, "NoteCategoryID", "CategoryName");
                        ViewBag.notetypes = new SelectList(notetype, "NoteTypeID", "TypeName");
                        ViewBag.countries = new SelectList(country, "CountryID", "CountryName");
                        return View();               

                    }

                    using (NMEntities nm = new NMEntities())
                    {
                        NoteDetails nd = new NoteDetails();
                        SellerNoteAttachment sna = new SellerNoteAttachment();
                        nd.SellerID = (int)Session["Userid"];
                        nd.Status = 4;
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

                        nm.NoteDetails.Add(nd);
                    nm.SaveChanges();

                    string path = Path.Combine(Server.MapPath("~/Member/" + Session["Userid"].ToString()), nd.NoteID.ToString());

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

                            nd.DisplayPicture = Path.Combine(("~/Member/" + Session["Userid"].ToString() + "/" + nd.NoteID.ToString() + "/"), fileName);
                            nm.SaveChanges();
                        }
                        if (notedetails.PreviewUpload != null && notedetails.PreviewUpload.ContentLength > 0)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(notedetails.PreviewUpload.FileName);
                            string extension = Path.GetExtension(notedetails.PreviewUpload.FileName);
                            fileName = "PREVIEW_" + DateTime.Now.ToString("ddMMyyyy") + extension;
                            string finalpath = Path.Combine(path, fileName);
                            notedetails.PreviewUpload.SaveAs(finalpath);

                            nd.PreviewUpload = Path.Combine(("~/Member/" + Session["Userid"].ToString() + "/" + nd.NoteID.ToString() + "/"), fileName);
                            nm.SaveChanges();
                        }

                        string attachmentpath = Path.Combine(Server.MapPath("~/Member/" + Session["Userid"].ToString() + "/" + nd.NoteID.ToString()), "attachment");

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
                                    fileName = "Attachment_" +count +"_"+ DateTime.Now.ToString("ddMMyyyy") + extension;
                                    string finalpath = Path.Combine(attachmentpath, fileName);
                                     file.SaveAs(finalpath);
                                    FileName += fileName + ";";
                                    FilePath += Path.Combine(("~/Member/" + Session["Userid"].ToString() + "/" + nd.NoteID.ToString() + "/"), fileName)+";";
                            count++;
                            }
                                sna.FileName = FileName;
                            sna.FilePath = FilePath;

                            sna.NoteID = nd.NoteID;
                                sna.IsActive = true;
                                sna.CreatedDate = DateTime.Now;
                                sna.CreatedBy = (int)Session["Userid"];
                                nm.SaveChanges();



                        }
                        nm.SellerNoteAttachment.Add(sna);
                        nm.SaveChanges();


                    }
                
                ModelState.Clear();
                return RedirectToAction("addnote","User");
            }


            return View();
        }

        //Get 
        public ActionResult searchnotes()
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
                ViewBag.universities = new SelectList(university_course.Distinct(), "NoteID", "UniversityInformation");
                ViewBag.courses = new SelectList(university_course.Distinct(), "NoteID", "Course");

                





            }
            return View();
        }



        // GET
        public ActionResult dashboard()
        {
            using (NMEntities nm = new NMEntities())
            {

                int userid = (int)Session["Userid"];
                ViewBag.mySoldNotes = nm.DownloadNotes.Where(x => x.SellerID == userid).Count();
                ViewBag.totalEarning = nm.DownloadNotes.Where(x=>x.SellerID == userid).Select(x=>x.PurchasePrice).Sum();
                ViewBag.myDownloadNotes = nm.DownloadNotes.Where(x => x.BuyerID == userid  && x.IsSellerHasAllowedDownload == true).Count();
                ViewBag.myRejectedNotes = nm.NoteDetails.Where(x => x.SellerID == userid && x.Status == 3).Count();
                ViewBag.buyerRequests = nm.DownloadNotes.Where(x => x.SellerID == userid && x.IsSellerHasAllowedDownload == false && x.AttachmentPath == null).Count();

                var SellerNotes = nm.NoteDetails.Where(x=>x.SellerID == userid && x.Status == 4 || x.Status==1).ToList();
                var Category = nm.NoteCategories.ToList();
                var Status = nm.NoteStatus.ToList();

                var ProgressNotes = (from sell in SellerNotes
                                     join cate in Category on sell.NoteCategoryID equals cate.NoteCategoryID into table1
                                     from cate in table1.ToList()
                                     join stu in Status on sell.Status equals stu.NoteStatusID into table2
                                     from stu in table2.ToList()
                                     select new AllProgressNotes
                                     {
                                         SellerNotes = sell,
                                         Category = cate,
                                         Status = stu
                                     }).ToList();


                ViewBag.inProgressNotes = ProgressNotes;




                var PublishedNote = nm.NoteDetails.Where(x => x.SellerID == userid && x.Status == 2).ToList();

                var PublishedNoted = (from sell in SellerNotes
                                      join cate in Category on sell.NoteCategoryID equals cate.NoteCategoryID into table1
                                      from cate in table1.ToList()
                                      select new AllPublishNotes
                                      {
                                          SellerNotes = sell,
                                          Category = cate,
                                          
                                      }).ToList();

                ViewBag.publishedNote = PublishedNoted;




            }
            return View();
        }     


        //GET: Notedetails
        public ActionResult notedetail()
        {
            int id = 1;
            using (NMEntities DBobj = new NMEntities())
            {
                var note = DBobj.NoteDetails.Where(x => x.NoteID == id).ToList();
                var category = DBobj.NoteCategories.ToList();
                var country = DBobj.Countries.ToList();
                var notedetail = (from n in note
                                  join c in category on n.NoteCategoryID equals c.NoteCategoryID into table1
                                  from c in table1.ToList()
                                  join cr in country on n.CountryID equals cr.CountryID into table2
                                  from cr in table2.ToList()
                                  select new nd
                                  {
                                      note = n,
                                      Category = c,
                                      countryname = cr
                                  }).ToList();
                ViewBag.notedetailbag = notedetail;

                var reviews = DBobj.NoteReviews.ToList();
                var users = DBobj.Users.ToList();
                var reviewdetail = (from nr in reviews
                                    join n in note on nr.NoteID equals n.NoteID into table3
                                    from n in table3.ToList()
                                    join us in users on nr.ReviewByID equals us.UserID into table4
                                    from us in table4.ToList()
                                    select new reviewtable
                                    {
                                        note = n,
                                        notereview = nr,
                                        usr = us
                                    }).ToList();

                ViewBag.reviewdetailbag = reviewdetail;
                ViewBag.reviewcount = reviewdetail.Count();

                var spam = DBobj.SpamReports.ToList();
                var spamtotal = (from sp in spam
                                 join n in note on sp.NoteID equals id into table5
                                 from n in table5.ToList()
                                 select new spamtable
                                 {
                                     note = n,
                                     spamrpt = sp
                                 }).ToList();

                ViewBag.spamtotalcount = spamtotal.Count();



                //var u = DBobj.Users.ToList();
                //var uprofile = DBobj.UserProfile.ToList();
                //var profiledata = (from n in note
                //                   join user in u on n.SellerID equals user.UserID into table2
                //                   from user in table2.ToList()
                //                   join up in uprofile on user.UserID equals up.UserID into table3
                //                   from up in table3.ToList()
                //                   select new userprofiledata
                //                   {
                //                       u = user,
                //                       upd = up 
                //                   }).ToList();
                //ViewBag.profiledatabag = profiledata;
            }
            return View();
        }

        //Get
        public ActionResult buyersrequests()
        {
            
                return View();
        }

        public ActionResult changepassword()
        {
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
        
        public ActionResult mydownloads()
        {
            return View();
        }
        public ActionResult myrejectednotes()
        {
            return View();
        }
        public ActionResult mysoldnotes()
        {
            return View();
        }
       
        
        
        public ActionResult userprofile()
        {
            return View();
        }

    }
}