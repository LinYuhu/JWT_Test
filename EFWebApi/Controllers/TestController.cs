using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EFWebApi.Filters;
using EFWebApi.Models;

namespace EFWebApi.Controllers
{
    [RoutePrefix("Test")]
    [JwtAuth]
    public class TestController : ApiController
    {        
        [Route("GetConsignorName/{id}")]
        [HttpGet]
        public string Get(int id)
        {
            Consignor c = new Consignor();
            return c.GetConsignorName(id.ToString());
        }
    }
}