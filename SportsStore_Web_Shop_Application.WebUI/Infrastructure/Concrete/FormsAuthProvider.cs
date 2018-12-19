using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SportsStore_Web_Shop_Application.WebUI.Infrastructure.Abstract;

namespace SportsStore_Web_Shop_Application.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username,false);
            }

            return result;
        }
    }
}