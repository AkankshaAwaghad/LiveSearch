using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiveSearch.Models;

namespace LiveSearch.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveCustomer(CustomerModel model)
        {
            try
            {

                return Json(new { MSG = (new CustomerModel()).SaveCustomer(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    
    public JsonResult SearchCustomer(string Prefix)
    {

        try
        {

           return Json(new CustomerModel().SearchCustomer(Prefix), JsonRequestBehavior.AllowGet);
        }
        catch (Exception ex)
        {
            return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
        }
    }
   }
}