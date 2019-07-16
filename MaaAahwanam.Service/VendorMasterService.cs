﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaaAahwanam.Repository.db;
using MaaAahwanam.Models;

namespace MaaAahwanam.Service
{
    public class VendorMasterService
    {
        VendormasterRepository vendormasterRepository = new VendormasterRepository();
        public Vendormaster GetVendor(long id)
        {
            return vendormasterRepository.GetVendor(id);
        }
        public Policy Getpolicy(string vid,string vsid)
        {
            return vendormasterRepository.Getpolicy(vid,vsid);
        }
        public int checkemail(string emailid)
        {
            return vendormasterRepository.checkemail(emailid);
        }

        public Vendormaster GetVendorServiceType(long id)
        {
            return vendormasterRepository.GetVendorServiceType(id);
        }

        public Vendormaster UpdateVendorMaster(Vendormaster vendorMaster, long id)
        {
            return vendormasterRepository.UpdateVendorMaster(vendorMaster, id);
        }

        public Policy insertpolicy(Policy policy, string vid,string vsid)
        {
            return vendormasterRepository.insertpolicy(policy, vid,vsid);
        }
        public Policy updatepolicy(Policy policy, string vid, string vsid)
        {
            return vendormasterRepository.updatepolicy(policy, vid, vsid);
        }
        public Vendormaster GetVendorByEmail(string emailid)
        {
            return vendormasterRepository.GetVendorByEmail(emailid);
        }
        public List<dynamic> GetVendorLocations()
        {
            return vendormasterRepository.VendormasterList().Select(m => m.Landmark).ToList<dynamic>();
        }
        public List<dynamic> GetVendorname()
        {
            return vendormasterRepository.VendormasterList().Select(m => m.BusinessName).ToList<dynamic>();
        }
        public List<dynamic> GetVendorword()
        {
            var l1 = vendormasterRepository.VendormasterList().Select(i => i.BusinessName + "," + i.Address + "," + i.ServicType);
            return l1.ToList<dynamic>();
        }
        public List<dynamic> GetVendorCities()
        {
            return vendormasterRepository.VendormasterList().Select(m => m.City).ToList<dynamic>();
        }

        public List<string> AvailableServices()
        {
            return vendormasterRepository.VendormasterList().Select(m => m.ServicType).Distinct().ToList();
        }

        public Vendormaster UpdateVendorStorefront(Vendormaster vendorMaster, long id)
        {
            return vendormasterRepository.UpdateVendorStorefront(vendorMaster, id);
        }

        public List<Vendormaster> SearchVendors()
        {
            return vendormasterRepository.VendormasterList();
        }

        public Vendormaster UpdateVendorDetails(Vendormaster vendorMaster, long id)
        {
            return vendormasterRepository.UpdateVendorDetails(vendorMaster, id);
        }

    }
}
