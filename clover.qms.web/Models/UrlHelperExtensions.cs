﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace clover.qms.web.Models
{
    public static class UrlHelperExtensions
    {
        public static string ContentAbsUrl(this UrlHelper url, string relativeContentPath)
        {
            Uri contextUri = HttpContext.Current.Request.Url;
            
            var port = contextUri.Port;

           

                var baseUri = string.Format("{0}://{1}{2}", contextUri.Scheme,
                     contextUri.Host, contextUri.Port == 80 ? string.Empty : ":" + contextUri.Port);
                return string.Format("{0}{1}", baseUri, VirtualPathUtility.ToAbsolute(relativeContentPath));
           
            
          
        }
    }
}