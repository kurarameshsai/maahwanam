﻿using MaaAahwanam.Models;
using MaaAahwanam.Service;
using MaaAahwanam.Utility;
using MaaAahwanam.Web.Custom;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IronPdf;
using System.Text.RegularExpressions;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.tool.xml;

namespace MaaAahwanam.Web.Controllers
{
    public class vinvoiceController : Controller
    {
        decimal rcvnmnt1, rcvnmnt2;
        newmanageuser newmanageuse = new newmanageuser();
        VendorMasterService vendorMasterService = new VendorMasterService();
        OrderdetailsServices orderdetailservices = new OrderdetailsServices();
        ReceivePaymentService rcvpaymentservice = new ReceivePaymentService();
        VendorDashBoardService mnguserservice = new VendorDashBoardService();
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        decimal amount;
        decimal tsprice;
        decimal balndue;
        double gtotal;
        double Gstplustotal;
        double GstplustotalAmount; string Discount;

        // GET: vinvoice
        public ActionResult Index(string oid)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                string id = user.UserId.ToString();
                ViewBag.userid = id;
                string email = newmanageuse.Getusername(long.Parse(id));
                Vendormaster Vendormaster = vendorMasterService.GetVendorByEmail(email);
                ViewBag.Vendor = vendorMasterService.GetVendor(Convert.ToInt64(Vendormaster.Id));
                if (oid != null && oid != "")
                {
                    var orderdetails1 = newmanageuse.allOrderList().Where(m => m.orderid == long.Parse(oid)).ToList();
                    ViewBag.orderid = orderdetails1.FirstOrDefault().orderid;
                    ViewBag.username = orderdetails1.FirstOrDefault().fname + " " + orderdetails1.FirstOrDefault().lname;
                    ViewBag.vendorname = orderdetails1.FirstOrDefault().BusinessName;
                    ViewBag.vendoraddress = orderdetails1.FirstOrDefault().Address + "," + orderdetails1.FirstOrDefault().Landmark + "," + orderdetails1.FirstOrDefault().City;
                    ViewBag.vendorcontact = orderdetails1.FirstOrDefault().ContactNumber;
                    ViewBag.bookeddate = Convert.ToDateTime(orderdetails1.FirstOrDefault().bookdate).ToString("MMM d,yyyy");
                    ViewBag.orderdate = Convert.ToDateTime(orderdetails1.FirstOrDefault().orderdate).ToString("MMM d,yyyy");
                    ViewBag.Servicetype = orderdetails1.FirstOrDefault().servicetype;
                    ViewBag.serviceprice = orderdetails1.FirstOrDefault().perunitprice * orderdetails1.FirstOrDefault().guestno;
                    ViewBag.orderdetailid = orderdetails1.FirstOrDefault().orderdetailedid;
                    ViewBag.orderdetails = orderdetails1;
                    ViewBag.totalprice = orderdetails1.FirstOrDefault().totalprice;
                    var odid1 = orderdetails1.FirstOrDefault().orderdetailedid.ToString();
                    var payments = rcvpaymentservice.getPayments(oid).ToList();
                    List<string> discount = new List<string>();
                    string odid = string.Empty;
                    foreach (var item in orderdetails1)
                    {
                        var orderdetailid = item.orderdetailedid.ToString();
                        odid = odid + item.orderdetailedid + ",";
                        ViewBag.orderdetailid5 = odid;
                        var paymentsbyodid = rcvpaymentservice.getPaymentsbyodid(orderdetailid).ToList();
                        if (paymentsbyodid.Count != 0)
                        {
                            var disc = paymentsbyodid.FirstOrDefault().Discount;
                            var disctype = paymentsbyodid.FirstOrDefault().DiscountType;
                            discount.Add(disctype + '!' + disc);
                        }
                        else
                        {    discount.Add(null);}
                        var price = item.totalpric1;
                        tsprice = Convert.ToInt64(tsprice) + Convert.ToInt64(price);
                        ViewBag.total = tsprice;
                        var bdue = item.Due;
                        balndue = Convert.ToInt64(balndue) + Convert.ToInt64(bdue);
                        ViewBag.balance = balndue;
                        if (price == bdue || price != 0 && bdue != 0)
                        {
                            var gsttotl = Convert.ToDouble(price);
                            Gstplustotal = gsttotl + (gsttotl * 0.18);
                            GstplustotalAmount = Convert.ToDouble(GstplustotalAmount) + Convert.ToDouble(Gstplustotal);
                            ViewBag.gstplustotalAmount = GstplustotalAmount;
                        }
                        ViewBag.discount = discount;
                    }
                    ViewBag.payment = payments;
                }
                return View();
            }
            return Content("<script>alert('Session Expired!!! Please Login'); location.href='/home'</script>");
        }
        [HttpPost]
        public ActionResult savepayment(Payment payments, string Received_Amount, string OrderDetailId,string finaldiscount)
        {
            var orderdetails1 = newmanageuse.allOrderList().Where(m => m.orderid == long.Parse(payments.OrderId)).ToList();
            payments.User_Type = "VendorUser";
            payments.UpdatedDate = payments.Payment_Date = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            decimal amnt, amnt1;
            decimal rcvnmnt = decimal.Parse(Received_Amount);
            Order orders = new Order();
            OrderDetail orderdetils = new OrderDetail();
            var odis = OrderDetailId.Trim(',').Split(',');
            for (int i = 0; i < odis.Length; i++)
            {
                payments.OrderDetailId = Regex.Replace(odis[i], @"\s", "");
                //var orderdetailid= Regex.Replace(odis[i], @"\s", "");
                var orderdetl = orderdetails1.Where(o => o.orderdetailedid == long.Parse(payments.OrderDetailId)).FirstOrDefault();
                decimal dueamount = Convert.ToDecimal(orderdetl.Due);
                var openamnt = Convert.ToDecimal(orderdetl.totalpric1);
                if (dueamount == openamnt && openamnt != 0 && dueamount != 0)
                {      
                    var fndisc = finaldiscount.Split('~');
                    for (int j = 0; j < fndisc.Length; j++)
                    {
                        var details = fndisc[j].Split('!');
                        if (details[0].Trim(' ') == payments.OrderDetailId)
                        {
                            payments.DiscountType = details[1];
                            payments.Discount = details[2];
                        }
                    }                   
                        var disctype = payments.DiscountType;
                    var disc = int.Parse(payments.Discount);
                            if (disc != 0 )
                            {
                                decimal discount = 0;
                                if (disctype == "Percentage %")
                                    discount = Convert.ToDecimal(dueamount - (dueamount * disc / 100));
                                else if (disctype == "Flat Rate ₹")
                                { discount = Convert.ToDecimal(dueamount - disc); }

                                dueamount = discount + (discount * 18 / 100);
                            }
                            else
                                dueamount = dueamount + (dueamount * 18 / 100);
                            if (i == 0) { amnt = rcvnmnt; amnt1 = rcvnmnt; }
                            else { if (rcvnmnt2 == 0) { amnt = amnt1 = rcvnmnt1; } else { amnt = amnt1 = rcvnmnt2; } }
                            if (rcvnmnt2 < rcvnmnt)
                            {
                                if (amnt > 0)
                                {
                         decimal multiplercvamnt = amnt; 
                                    amnt = amnt - dueamount;
                                    rcvnmnt1 = amnt;
                                    payments.Opening_Balance = dueamount.ToString().Replace(".00", "");
                                    if (amnt == 0)
                                    {
                                        payments.Received_Amount = dueamount.ToString().Replace(".00", "");
                                        payments.Current_Balance = "0";
                                    }
                                    else
                                    {
                                        payments.Received_Amount = dueamount.ToString().Replace(".00", "");
                                        if (amnt < 0) { payments.Current_Balance = (amnt * -1).ToString().Replace(".00", ""); payments.Received_Amount = multiplercvamnt.ToString().Replace(".00", ""); }
                                        else { payments.Current_Balance = "0"; }
                                    }
                                    if (payments.Current_Balance == "0")
                                    {
                                        orders.Status = orderdetils.Status = "Payment completed";
                                        var status = newmanageuse.updateOrderstatus(orders, orderdetils, Convert.ToInt64(payments.OrderId));
                                        payments.Status = "Payment completed";
                                    }
                                    else
                                    {
                                        orders.Status = orderdetils.Status = "Payment pending";
                                        var status = newmanageuse.updateOrderstatus(orders, orderdetils, Convert.ToInt64(payments.OrderId));
                                        payments.Status = "Payment pending";
                                    }                                  
                                }
                            }
                        }
              
                else {
                if (dueamount != 0)
                {
                    if (i == 0) { amnt = rcvnmnt; amnt1 = rcvnmnt; }
                    else { if (rcvnmnt2 == 0) { amnt = amnt1 = rcvnmnt1; } else { amnt = amnt1 = rcvnmnt2; } }
                    if (rcvnmnt2 < rcvnmnt)
                    {
                        if (amnt > 0)
                        {
                            amnt = amnt - dueamount;
                            rcvnmnt1 = amnt;
                            payments.Opening_Balance = dueamount.ToString().Replace(".00", "");
                            if (amnt == 0)
                            {
                                payments.Received_Amount = dueamount.ToString().Replace(".00", "");
                                payments.Current_Balance = "0";
                            }
                            else
                            {
                                payments.Received_Amount = dueamount.ToString().Replace(".00", "");
                                if (amnt < 0) { payments.Current_Balance = (amnt * -1).ToString().Replace(".00", ""); payments.Received_Amount = rcvnmnt.ToString().Replace(".00", ""); }
                                else { payments.Current_Balance = "0"; }
                            }
                            if (payments.Current_Balance == "0")
                            {
                                orders.Status = orderdetils.Status = "Payment completed";
                                var status = newmanageuse.updateOrderstatus(orders, orderdetils, Convert.ToInt64(payments.OrderId));
                                payments.Status = "Payment completed";
                            }
                            else
                            {
                                orders.Status = orderdetils.Status = "Payment pending";
                                var status = newmanageuse.updateOrderstatus(orders, orderdetils, Convert.ToInt64(payments.OrderId));
                                payments.Status = "Payment pending";
                            }
                        }
                    }
                }
              }
                payments = rcvpaymentservice.SavePayments(payments);
            }
            return Json("Payment Successfull", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Email(string oid)
        {
            HomeController home = new HomeController();

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                string vid = user.UserId.ToString();
                ViewBag.userid = vid;
                string email = newmanageuse.Getusername(long.Parse(vid));
                ViewBag.vemail = email;
                Vendormaster Vendormaster = vendorMasterService.GetVendorByEmail(email);
                List<Payment> payment = rcvpaymentservice.getPayments(oid);
                var orderdetails1 = newmanageuse.allOrderList().Where(m => m.orderid == long.Parse(oid)).ToList();
                string txtto = orderdetails1.FirstOrDefault().username;
                string name = orderdetails1.FirstOrDefault().fname + " " + orderdetails1.FirstOrDefault().lname;
                StringBuilder cds = new StringBuilder();
                cds.Append("<table style='border:1px black solid;'><tbody>");
                cds.Append("<tr><td>Order Id</td><td>Order Date</td><td> Event Type </td><td>Guest Count</td><td>Perunit Price</td><td>Total Price</td></tr>");
                cds.Append("<tr><td style = 'width: 75px;border: 1px black solid;'> " + orderdetails1.FirstOrDefault().orderid + "</td><td style = 'width: 75px;border: 1px black solid;' > " + orderdetails1.FirstOrDefault().bookdate + " </td><td style = 'width: 75px;border: 1px black solid;'> " + orderdetails1.FirstOrDefault().eventtype + " </td><td style = 'width: 50px;border: 1px black solid;'> " + orderdetails1.FirstOrDefault().guestno + " </td> <td style = 'width: 50px;border: 1px black solid;'> " + orderdetails1.FirstOrDefault().perunitprice + " </td><td style = 'width: 50px;border: 1px black solid;'> " + orderdetails1.FirstOrDefault().totalprice + " </td></tr>");  //<td style = 'width: 50px;border: 2px black solid;'> " + item.eventstartdate + " </td><td> date </td>
                cds.Append("</tbody></table>");
                if (payment.Count != 0)
                {
                    cds.Append("<table style='border:1px black solid;'><tbody>");
                    cds.Append("<tr><td> Payment Id</td><td> Payment By</td><td> Payment Type </td><td> Payment Date </td><td> Received Amount </td></tr>");
                    foreach (var item in payment)
                    {
                        cds.Append("<tr><td style = 'width: 75px;border: 1px black solid;'> " + item.Payment_Id + "</td><td style = 'width: 75px;border: 1px black solid;'> " + item.PaymentBy + "</td><td style = 'width: 75px;border: 1px black solid;' > " + item.Payment_Type + " </td><td style = 'width: 75px;border: 1px black solid;'> " + item.Payment_Date + " </td><td style = 'width: 50px;border: 1px black solid;'> " + item.Received_Amount + " </td></tr>");  //<td style = 'width: 50px;border: 2px black solid;'> " + item.eventstartdate + " </td><td> date </td>
                    }
                    cds.Append("</tbody></table>");
                }
                string url = Request.Url.Scheme + "://" + Request.Url.Authority;
                //string url = cds.ToString();
                FileInfo File = new FileInfo(Server.MapPath("/mailtemplate/order.html"));
                string readFile = File.OpenText().ReadToEnd();
                readFile = readFile.Replace("[ActivationLink]", url);
                readFile = readFile.Replace("[name]", name);
                readFile = readFile.Replace("[table]", cds.ToString());
                readFile = readFile.Replace("[orderid]", oid);
                string txtmessage = readFile;//readFile + body;
                string subj = "Thanks for your order";
                EmailSendingUtility emailSendingUtility = new EmailSendingUtility();
                emailSendingUtility.Email_maaaahwanam(txtto, txtmessage, subj, null);
                string targetmails = "lakshmi.p@xsilica.com,seema.g@xsilica.com,rameshsai@xsilica.com";
                emailSendingUtility.Email_maaaahwanam(targetmails, txtmessage, subj, null);
            }

            return Json("success", JsonRequestBehavior.AllowGet);
        }

        //public ActionResult pdfdownload(string pdfhtml)
        //{
        //    // ----ironpdf----//
        //    // HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
        //    //var PDF = HtmlToPdf.RenderHtmlAsPdf(pdfhtml);
        //    // var OutputPath = "HtmlToPDF.pdf";
        //    // PDF.SaveAs(OutputPath);
        //    // // This neat trick opens our PDF file so we can see the result in our default PDF viewer
        //    // System.Diagnostics.Process.Start(OutputPath);
        //    // return Json("success", JsonRequestBehavior.AllowGet);
        //    //}

        //}
        public ActionResult pdfpreview(string oid)
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var user = (CustomPrincipal)System.Web.HttpContext.Current.User;
                string id = user.UserId.ToString();
                ViewBag.userid = id;
                string email = newmanageuse.Getusername(long.Parse(id));
                Vendormaster Vendormaster = vendorMasterService.GetVendorByEmail(email);
                ViewBag.Vendor = vendorMasterService.GetVendor(Convert.ToInt64(Vendormaster.Id));
                if (oid != null && oid != "")
                {

                    var orderdetails1 = newmanageuse.allOrderList().Where(m => m.orderid == long.Parse(oid)).ToList();
                    ViewBag.orderid = orderdetails1.FirstOrDefault().orderid;
                    ViewBag.username = orderdetails1.FirstOrDefault().fname + " " + orderdetails1.FirstOrDefault().lname;
                    ViewBag.vendorname = orderdetails1.FirstOrDefault().BusinessName;
                    ViewBag.vendoraddress = orderdetails1.FirstOrDefault().Address + "," + orderdetails1.FirstOrDefault().Landmark + "," + orderdetails1.FirstOrDefault().City;
                    ViewBag.vendorcontact = orderdetails1.FirstOrDefault().ContactNumber;
                    ViewBag.bookeddate = Convert.ToDateTime(orderdetails1.FirstOrDefault().bookdate).ToString("MMM d,yyyy");
                    ViewBag.orderdate = Convert.ToDateTime(orderdetails1.FirstOrDefault().orderdate).ToString("MMM d,yyyy");
                    ViewBag.Servicetype = orderdetails1.FirstOrDefault().servicetype;
                    ViewBag.serviceprice = orderdetails1.FirstOrDefault().perunitprice * orderdetails1.FirstOrDefault().guestno;
                    ViewBag.orderdetailid = orderdetails1.FirstOrDefault().orderdetailedid;
                    ViewBag.orderdetails = orderdetails1;
                    ViewBag.totalprice = orderdetails1.FirstOrDefault().totalprice;
                    //   ViewBag.orderdetailid = orderdetails1.FirstOrDefault().orderdetailedid;
                    var odid1 = orderdetails1.FirstOrDefault().orderdetailedid.ToString();
                    var payments = rcvpaymentservice.getPayments(oid).ToList();
                    //ViewBag.pmntbycustomer = payments.FirstOrDefault().PaymentBy.Replace(' ', '_');
                    List<string> discount = new List<string>();

                    string odid = string.Empty;
                    foreach (var item in orderdetails1)
                    {
                        var orderdetailid = item.orderdetailedid.ToString();
                        odid = odid + item.orderdetailedid + ",";
                        ViewBag.orderdetailid5 = odid;
                        var paymentsbyodid = rcvpaymentservice.getPaymentsbyodid(orderdetailid).ToList();
                        //ViewBag.paymentby = paymentsbyodid.FirstOrDefault().PaymentBy.Replace(' ', '_');
                        if (paymentsbyodid.Count != 0)
                        {
                            var disc = paymentsbyodid.FirstOrDefault().Discount;
                            //ViewBag.discount = Convert.ToDouble(disc);
                            var disctype = paymentsbyodid.FirstOrDefault().DiscountType;
                            //ViewBag.discounttype = disctype;
                            discount.Add(disctype + '!' + disc);
                        }
                        else
                        {
                            discount.Add(null);
                        }
                        var price = item.totalpric1;
                        tsprice = Convert.ToInt64(tsprice) + Convert.ToInt64(price);
                        ViewBag.total = tsprice;
                        var bdue = item.Due;
                        balndue = Convert.ToInt64(balndue) + Convert.ToInt64(bdue);
                        ViewBag.balance = balndue;
                        if (price == bdue || price != 0 && bdue != 0)
                        {
                            var gsttotl = Convert.ToDouble(price);
                            Gstplustotal = gsttotl + (gsttotl * 0.18);
                            GstplustotalAmount = Convert.ToDouble(GstplustotalAmount) + Convert.ToDouble(Gstplustotal);
                            ViewBag.gstplustotalAmount = GstplustotalAmount;
                        }
                        ViewBag.discount = discount;
                    }
                    ViewBag.payment = payments;
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult pdfdownload(string GridHtml)
        {
           
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }

        [HttpPost]
        public JsonResult GetoderdetailsbyOrderdetailId(string odid)
        {
            var data = orderdetailservices.GetOrderDetailsByOrderdetailid(Convert.ToInt32(odid));

            return Json(data);
        }
    }
}