using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaaAahwanam.Models;
using MaaAahwanam.Repository;

namespace MaaAahwanam.Repository.db
{
    public class AdminTestimonialPathRepository
    {
        readonly ApiContext _dbContext = new ApiContext();
        public List<AdminTestimonialPath> AdminTestimonialPath()
        {
            return _dbContext.AdminTestimonialPath.ToList();
        }
        public string SaveAdminTestimonial(AdminTestimonialPath adminTestimonialPath)
        {
            _dbContext.AdminTestimonialPath.Add(adminTestimonialPath);
            _dbContext.SaveChanges();
            return "Success";
        }
    }
}
