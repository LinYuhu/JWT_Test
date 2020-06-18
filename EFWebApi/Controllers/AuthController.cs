using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

using EFWebApi.Models;
using EFWebApi.Helpers;
using JWT;
using JWT.Algorithms;
using JWT.Builder;

namespace EFWebApi.Controllers
{
    [RoutePrefix("Auth")]
    public class AuthController : ApiController
    {
        HttpResponseMessage response;
        public string Account { get; set; }
        public string Password { get; set; }
        [Route("LogIn")]
        [HttpPost]
        public HttpResponseMessage Get_LogIn(LoginInfo info)
        {
            Account = info.Account;
            Password = info.Password;

            #region 檢查帳號與密碼是否正確

            // 這裡可以修改成為與後端資料庫內的使用者資料表進行比對
            string expectPassword = GetPassword();

            if (expectPassword == Password)
            {
                // 帳號與密碼比對正確，回傳帳密比對正確
                #region 產生這次通過身分驗證的存取權杖 Access Token
                string secretKey = MainHelper.SecretKey;
                #region 設定該存取權杖的有效期限
                IDateTimeProvider provider = new UtcDateTimeProvider();
                // 這個 Access Token只有一個小時有效
                var now = provider.GetNow().AddHours(1);
                var unixEpoch = UnixEpoch.Value; // 1970-01-01 00:00:00 UTC
                var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);
                #endregion

                var jwtToken = new JwtBuilder()
                      .WithAlgorithm(new HMACSHA256Algorithm())// 使用算法# 加密的方法: HMAC、SHA256、RSA 進行 Base64 編碼
                      .WithSecret(secretKey)// 使用秘鑰
                      .AddClaim("iss", Account)
                      .AddClaim("exp", secondsSinceEpoch)//過期時間
                      .AddClaim("role", new string[] { "Admin", "Guest" })
                      .Encode();
                //.Build();
                #endregion

                response = this.Request.CreateResponse<APIResult>(HttpStatusCode.OK, new APIResult()
                {
                    Success = true,
                    Message = $"帳號:{Account} / 密碼:{Password}",
                    Payload = $"{jwtToken}"
                });
            }
            else
            {
                // 帳號與密碼比對不正確，回傳帳密比對不正確
                response = this.Request.CreateResponse<APIResult>(HttpStatusCode.Unauthorized, new APIResult()
                {
                    Success = false,
                    Message = "帳號或密碼不正確",
                    Payload = $""
                });
            }
            #endregion
            return response;

        }


        /// <summary>
        /// 檢查與解析 Authorization 標頭是否存在與解析用戶端傳送過來的帳號與密碼
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetPassword()
        {
            //return "95794AAFDC616A663EC81BEE7675A639278F3AA3" //12345677
            return "7C4A8D09CA3762AF61E59520943DC26494F8941B";  //123456
        }
    }
}