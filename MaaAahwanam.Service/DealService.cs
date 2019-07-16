﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaaAahwanam.Repository.db;
using MaaAahwanam.Models;
using MaaAahwanam.Repository;

namespace MaaAahwanam.Service
{
    public class DealService
    {
        DealRepository dealRepository = new DealRepository();

        public List<Deal_Result> AllDealsService(string dropstatus)
        {
            return dealRepository.AllDeals(dropstatus);
        }

        public Deal GetDealService(int id)
        {
            return dealRepository.GetDeal(id);
        }

        public Deal AddDealService(Deal deal)
        {
            return dealRepository.AddDeal(deal);
        }

        public Deal UpdateDealService(Deal deal,int id)
        {
            return dealRepository.UpdateDeal(deal,id);
        }
    }
}
