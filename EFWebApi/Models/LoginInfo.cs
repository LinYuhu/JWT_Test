using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace EFWebApi.Models
{
    public class LoginInfo
    {
        public string Account { get; set; } = "";
        public string Password { get; set; } = "";

        #region 密碼編碼
        /// <summary>
        /// 密碼編碼
        /// </summary>
        /// <returns></returns>
       
        public string GetPAWDHash()
        {
            string hashedPassword =
                      FormsAuthentication.HashPasswordForStoringInConfigFile(this.Password, "SHA1");
            return hashedPassword;
        }
        #endregion
    }
}