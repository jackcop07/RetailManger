using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RMDataManager.Library.DataAccess;
using RMDataManager.Library.Models;
using RMDataManager.Models;

namespace RMDataManager.Controllers
{
    [Authorize]
    [RoutePrefix("api/Sale")]
    public class SaleController : ApiController
    {
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            var userId = RequestContext.Principal.Identity.GetUserId();

            data.SaveSale(sale, userId);
        }
    }
}
