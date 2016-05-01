using System;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace InfoVideo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
       
        }
    }
  
}
