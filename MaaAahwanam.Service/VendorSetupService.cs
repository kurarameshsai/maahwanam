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
    public class VendorSetupService
    {
        VendorSetupRepository vendorSetupRepository = new VendorSetupRepository();

        public List<AllVendorList_Result> AllVendorList(string type)
        {
           return vendorSetupRepository.AllVendors(type);
        }
    }
}
