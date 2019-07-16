using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaaAahwanam.Models;
using MaaAahwanam.Repository.db;

namespace MaaAahwanam.Service
{
    public class ReviewService
    {
        ReviewRepository reviewRepository = new ReviewRepository();
        public List<Review> GetReview(int serviceId)
        {
            var a = reviewRepository.GetReview(serviceId);
            return a;
        }
        public Review InsertReview(Review review)
        {
            return reviewRepository.InsertReview(review);
        }
    }
}
