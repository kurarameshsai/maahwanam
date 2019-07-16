using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaaAahwanam.Models;

namespace MaaAahwanam.Repository.db
{
    public class EnquiryRepository
    {
        readonly ApiContext _dbContext = new ApiContext();
        MaaAahwanamEntities maaAahwanamEntities = new MaaAahwanamEntities();
        public Enquiry SaveEnquiries(Enquiry enquiry)
        {
            _dbContext.Enquiry.Add(enquiry);
            _dbContext.SaveChanges();
            return enquiry;
        }

        public List<Enquiry> getallenquires()
        {
            return _dbContext.Enquiry.OrderByDescending(e => e.EnquiryId).ToList();
        }

        public List<getallwishlistdetailsofusers_Result> Getwishlistdataforadmin()
        {
            return maaAahwanamEntities.getallwishlistdetailsofusers().OrderByDescending(w => w.WishlistdetailId).ToList();

        }

        public List<getuserdetailsforadmin_Result> Getuserdataforadmin()
        {
            return maaAahwanamEntities.getuserdetailsforadmin().OrderByDescending(u => u.UserDetailId).ToList();
        }

        public Enquiry getenquiry(long id)
        {
            return _dbContext.Enquiry.Where(e => e.EnquiryId == id).FirstOrDefault();
        }

        public Enquirycomment saveenquirycomment(Enquirycomment cmnt)
        {
            _dbContext.Enquirycomment.Add(cmnt);
            _dbContext.SaveChanges();
            return cmnt;
        }

        public List<Enquirycomment> Getcomment(long enquiryid)
        {
            return _dbContext.Enquirycomment.Where(c => c.enquiryid == enquiryid).ToList();
        }


        public string UpdateEnquriyStatus(long id,string status)
        {
            var query = from enq in _dbContext.Enquiry where enq.EnquiryId == id select enq;
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            foreach (Enquiry enq in query)
            {
                enq.Status = status;
                enq.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                // Insert any additional changes to column values.
            }
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return "failed";
            }
            return "success";
        }

        public Googlelead addgooglelead(Googlelead goglead)
        {
            _dbContext.Googlelead.Add(goglead);
            _dbContext.SaveChanges();
            return goglead;
        }

        public Facebooklead addfblead(Facebooklead fblead)
        {
            _dbContext.Facebooklead.Add(fblead);
            _dbContext.SaveChanges();
            return fblead;
        }

        public string updatecomment(long commentid, string comment)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var data = from ecomment in _dbContext.Enquirycomment where ecomment.commenId == commentid select ecomment;
            Enquirycommentlog ecommentlog = new Enquirycommentlog();
           
            foreach(Enquirycomment acomment in data)
            {
                ecommentlog.commentId = acomment.commenId;
                ecommentlog.comment = acomment.comment;
                ecommentlog.commentedDate = acomment.commentedDate;
                ecommentlog.enquiryid = acomment.enquiryid;
                ecommentlog.IpAddress = acomment.IpAddress;
                ecommentlog.userid = acomment.userid;
                ecommentlog.UpdatedDate = acomment.UpdatedDate;
                acomment.comment = comment;
                acomment.UpdatedDate= TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            }
            try
            {
                if(ecommentlog!=null)
                {
                    _dbContext.Enquirycommentlog.Add(ecommentlog);
                    _dbContext.SaveChanges();
                }
               
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return "failed";
            }
            return "success";
        }

        public int Removecomment(long commentid)
        {
            var getdata = _dbContext.Enquirycomment.Where(c => c.commenId == commentid).FirstOrDefault();
            _dbContext.Enquirycomment.Remove(getdata);
            return _dbContext.SaveChanges();
        }

        public List<Googlelead> getgoogleleads()
        {
            return _dbContext.Googlelead.OrderByDescending(g => g.GoogleLeadId).ToList();
        }

        public List<Facebooklead> getfblead()
        {
            return _dbContext.Facebooklead.OrderByDescending(f => f.facebookLeadId).ToList();
        }
        public string updateGnrlLead(Enquiry gnenqry,long id)
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var data = from ord in _dbContext.Enquiry
                       where ord.EnquiryId == id
                       select ord;
            foreach (Enquiry ord in data)
            {
                ord.PersonName = gnenqry.PersonName;
                ord.SenderEmailId = gnenqry.SenderEmailId;
                ord.SenderPhone = gnenqry.SenderPhone;
                ord.EnquiryRegarding = gnenqry.EnquiryRegarding;
                ord.EnquiryDate = gnenqry.EnquiryDate;
                ord.city = gnenqry.city;
                ord.EnquiryDetails = gnenqry.EnquiryDetails;
                ord.Services = gnenqry.Services;
                ord.UpdatedDate = gnenqry.UpdatedDate;
            }
            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return "failed";
            }
            return "success";
        }
    }
}
