using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Rest_3.Helper
{
    public static class Utility
    {
        public static string GetName(this System.Security.Principal.IPrincipal user)
        {
            var nameClaim = ((ClaimsIdentity) user.Identity).FindFirst("FullName");
            if (nameClaim != null) {return nameClaim.Value; }

            return "";
            
        }
    }
}