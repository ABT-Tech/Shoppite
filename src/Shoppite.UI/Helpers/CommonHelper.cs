using Microsoft.AspNetCore.Http;
using Shoppite.Application.Models;
using Shoppite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Helpers
{
    public class CommonHelper
    {
        protected readonly Shoppite_masterContext _dbContext;
        public string GetSubDomain(HttpContext httpContext)
        {
            var subDomain = string.Empty;

            var host = httpContext.Request.Host.Host;

            if (!string.IsNullOrWhiteSpace(host))
            {
                subDomain = host.Split('.')[0];
            }

            return subDomain.Trim().ToLower();
        }
        //public string GetSubDomain(Uri url)
        //{
        //    if (url.HostNameType == UriHostNameType.Dns)
        //    {
        //        string host = url.Host;

        //        var nodes = host.Split('.');
        //        int startNode = 0;
        //        if (nodes[0] == "www") startNode = 1;

        //        return string.Format("{0}.{1}", nodes[startNode], nodes[startNode]);
        //    }

        //    return null;
        //}

        public void MyMethod(Microsoft.AspNetCore.Http.HttpContext context)
        {
            var host = $"{context.Request.Scheme}://{context.Request.Host}";
        }

        public int GetOrgID(HttpContext httpContext)
        {
            int orgid = 1;
            var subdomain = GetSubDomain(httpContext);
            if (subdomain.Contains("localhost"))
                orgid = 1;
            else
            {
                var orgObject = _dbContext.Organization.Where(x => x.OrgName == subdomain).FirstOrDefault();
                orgid = orgObject.Id;
            }
            return orgid;

        }

        //public Guid? GetGuid(Guid? productGuid)
        //{
        //    var getGuid = _dbContext.ProductBasic.Where(m => m.ProductGuid == productGuid).FirstOrDefault();
        //    return productGuid;
        //}
    }
}
