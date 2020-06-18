using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using EFWebApi;

namespace EFWebApi.Models
{
    public class Consignor
    {
        public Guid ConsignorId { get; set; }
        public string ConsignorNo { get; set; }
        public string ConsignorName { get; set; }

        public string GetConsignorName(string consignorNo)
        {
            return "WebApi測試";
        }
    }
}