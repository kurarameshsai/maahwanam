﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaaAahwanam.Repository.db;
using MaaAahwanam.Models;
using MaaAahwanam.Utility;

namespace MaaAahwanam.Service
{
   public class VendorWeddingCollectionService
    {
        RandomPassword randomPassword = new RandomPassword();
        UserLoginRepository userLoginRepository = new UserLoginRepository();
        UserDetailsRepository userDetailsRepository = new UserDetailsRepository();
        UserLogin userLogin = new UserLogin();
        UserDetail userDetail = new UserDetail();
        VendormasterRepository vendorMasterRepository = new VendormasterRepository();
        VendorWeddingCollectionsRepository vendorsWeddingCollectionsRepository = new VendorWeddingCollectionsRepository();
        public VendorsWeddingCollection AddWeddingCollection(VendorsWeddingCollection vendorsWeddingCollection, Vendormaster vendorMaster)
       {
            string updateddate = DateTime.UtcNow.ToShortDateString();
            vendorsWeddingCollection.Status = "Active";
           vendorsWeddingCollection.UpdatedDate =  Convert.ToDateTime(updateddate);
           vendorMaster.Status = "Active";
           vendorMaster.UpdatedDate = Convert.ToDateTime(updateddate);
           vendorMaster.ServicType = "WeddingCollection";
           vendorMaster = vendorMasterRepository.AddVendorMaster(vendorMaster);
           vendorsWeddingCollection.VendorMasterId = vendorMaster.Id;
           vendorsWeddingCollection = vendorsWeddingCollectionsRepository.AddWeddingCollections(vendorsWeddingCollection);
            userLogin.UserName = vendorMaster.EmailId;
            userLogin.Password = randomPassword.GenerateString();
            userLogin.UserType = "Vendor";
            userLogin.UpdatedBy = 2;
            userLogin.RegDate = Convert.ToDateTime(updateddate);
            userLogin.Status = "Active";
            userLogin.UpdatedDate = Convert.ToDateTime(updateddate);
            userLogin = userLoginRepository.AddVendorUserLogin(userLogin);
            userDetail.UserLoginId = userLogin.UserLoginId;
            userDetail.FirstName = vendorMaster.BusinessName;
            userDetail.UserPhone = vendorMaster.ContactNumber;
            userDetail.Url = vendorMaster.Url;
            userDetail.Address = vendorMaster.Address;
            userDetail.City = vendorMaster.City;
            userDetail.State = vendorMaster.State;
            userDetail.ZipCode = vendorMaster.ZipCode;
            userDetail.Status = "Active";
            userDetail.UpdatedBy = ValidUserUtility.ValidUser();
            userDetail.UpdatedDate = Convert.ToDateTime(updateddate);
            userDetail.AlternativeEmailID = vendorMaster.EmailId;
            userDetail.Landmark = vendorMaster.Landmark;
            userDetail = userDetailsRepository.AddUserDetails(userDetail);
            if (vendorMaster.Id != 0 && vendorsWeddingCollection.Id != 0 && userLogin.UserLoginId != 0 && userDetail.UserDetailId != 0)
            {
                return vendorsWeddingCollection;
            }
            else
            {
                vendorsWeddingCollection.Id = 0;
                return vendorsWeddingCollection;
            }
        }
        public VendorsWeddingCollection GetVendorWeddingCollection(long id, long vid)
        {
            return vendorsWeddingCollectionsRepository.GetVendorWeddingCollection(id,vid);
        }

        public VendorsWeddingCollection UpdateWeddingCollection(VendorsWeddingCollection vendorsWeddingCollection, Vendormaster vendorMaster, long masterid,long vid)
        {
            string updateddate = DateTime.UtcNow.ToShortDateString();
            vendorsWeddingCollection.Status = "Active";
            vendorsWeddingCollection.UpdatedDate = Convert.ToDateTime(updateddate);
            vendorMaster.Status = "Active";
            vendorMaster.UpdatedDate = Convert.ToDateTime(updateddate);
            vendorMaster.ServicType = "WeddingCollection";
            vendorMaster = vendorMasterRepository.UpdateVendorMaster(vendorMaster, masterid);
            vendorsWeddingCollection = vendorsWeddingCollectionsRepository.UpdateWeddingCollection(vendorsWeddingCollection, masterid,vid);
            return vendorsWeddingCollection;
        }

         public VendorsWeddingCollection activateWeddingCollection(VendorsWeddingCollection vendorsWeddingCollection, Vendormaster vendorMaster, long masterid,long vid)
        {
            string updateddate = DateTime.UtcNow.ToShortDateString();
            //vendorsWeddingCollection.Status = "Active";
            vendorsWeddingCollection.UpdatedDate = Convert.ToDateTime(updateddate);
            //vendorMaster.Status = "Active";
            vendorMaster.UpdatedDate = Convert.ToDateTime(updateddate);
            vendorMaster.ServicType = "WeddingCollection";
            vendorMaster = vendorMasterRepository.UpdateVendorMaster(vendorMaster, masterid);
            vendorsWeddingCollection = vendorsWeddingCollectionsRepository.UpdateWeddingCollection(vendorsWeddingCollection, masterid,vid);
            return vendorsWeddingCollection;
        }

        public VendorsWeddingCollection AddNewWeddingCollection(VendorsWeddingCollection vendorsWeddingCollection, Vendormaster vendorMaster)
        {
            string updateddate = DateTime.UtcNow.ToShortDateString();
            vendorsWeddingCollection.Status = "Active";
            vendorsWeddingCollection.UpdatedDate = Convert.ToDateTime(updateddate);
            vendorsWeddingCollection.VendorMasterId = vendorMaster.Id;
            vendorsWeddingCollection = vendorsWeddingCollectionsRepository.AddWeddingCollections(vendorsWeddingCollection);
            return vendorsWeddingCollection;
        }
    }
}
