using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaaAahwanam.Models;

namespace MaaAahwanam.Repository.db
{
    public class ReviewRepository
    {
        ApiContext _Dbcontext = new ApiContext();
        public Review InsertReview(Review review)
        {
            _Dbcontext.Review.Add(review);
            _Dbcontext.SaveChanges();
            return review;
        }
        public List<Review> GetReview(int serviceId)
        {
            var a= _Dbcontext.Review.Where(i=>i.ServiceId==serviceId).ToList();
            return a;
        }
        //public int Reviewscount(int serviceId)
        //{
        //    int Rcount =0;
        //    Rcount= _Dbcontext.Review.Where(i => i.ServiceId == serviceId).Count;
        //    return Rcount;
        //}
    }
}
