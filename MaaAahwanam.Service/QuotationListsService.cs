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
    public class QuotationListsService
    {
        QuotationListsRepository quotationListsRepository = new QuotationListsRepository();

        public int AddQuotationList(QuotationsList quotationsList)
        {
            return quotationListsRepository.AddQuotationList(quotationsList);
        }

        public List<QuotationsList> GetVendorVenue(string IP)
        {
            return quotationListsRepository.GetQuotationsList(IP);
        }

        public List<QuotationsList> GetAllQuotations()
        {
            return quotationListsRepository.GetAllQuotations();
        }

        public int UpdateQuote(QuotationsList quotationsList)
        {
            return quotationListsRepository.UpdateQuote(quotationsList);
        }

        public int AddInstallments(QuoteResponse quoteResponse)
        {
            return quotationListsRepository.AddInstallments(quoteResponse);
        }

        public List<QuoteResponse> GetAllQuoteResponses()
        {
            return quotationListsRepository.GetAllQuoteResponses();
        }
    }
}
