using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaaAahwanam.Repository.db;
using MaaAahwanam.Models;
using MaaAahwanam.Repository;

namespace MaaAahwanam.Service
{
    public class EnquiryService
    {
        EnquiryRepository enquiryRepository = new EnquiryRepository();
        public string SaveEnquiries(Enquiry enquiry)
        {
            string response = string.Empty;

            try
            {
                
                enquiryRepository.SaveEnquiries(enquiry);
                response = "Success";
            }
            catch (Exception ex)
            {
                response = "failure";
            }
            return response;
        }
        public Enquiry SaveallEnquiries(Enquiry enquiry)
        {
            return enquiryRepository.SaveEnquiries(enquiry);
        }

        public List<Enquiry> getallenquires()
        {
            return enquiryRepository.getallenquires();
        }

        public List<getallwishlistdetailsofusers_Result> Getwishlistdataforadmin()
        {
            return enquiryRepository.Getwishlistdataforadmin();
        }

        public List<getuserdetailsforadmin_Result> Getuserdataforadmin()
        {
            return enquiryRepository.Getuserdataforadmin();
        }
        public Enquiry getenquiry(long id)
        {
            return enquiryRepository.getenquiry(id);
        }

        public Enquirycomment saveenquirycomment(Enquirycomment cmnt)
        {
            return enquiryRepository.saveenquirycomment(cmnt);
        }

        public string UpdateEnquriystatus(long id,string status)
        {
            return enquiryRepository.UpdateEnquriyStatus(id, status);
        }
        public List<Enquirycomment> Getcomment(long enquiryid)
        {
            return enquiryRepository.Getcomment(enquiryid);
        }
        public Googlelead addgooglelead(Googlelead goglead)
        {
            return enquiryRepository.addgooglelead(goglead);
        }

        public Facebooklead addfblead(Facebooklead fblead)
        {
            return enquiryRepository.addfblead(fblead);
        }

        public List<Googlelead> getgoogleleads()
        {
            return enquiryRepository.getgoogleleads();
        }
        public List<Facebooklead> getfblead()
        {
            return enquiryRepository.getfblead();
        }
        public string updateGnrlLead(Enquiry gnenqry, long id)
        {
            return enquiryRepository.updateGnrlLead(gnenqry, id);
        }

        public string updatecomment(long commentid, string comment)
        {
            return enquiryRepository.updatecomment(commentid, comment);
        }

        public int Removecomment(long commentid)
        {
            return enquiryRepository.Removecomment(commentid);
        }
    }
}
