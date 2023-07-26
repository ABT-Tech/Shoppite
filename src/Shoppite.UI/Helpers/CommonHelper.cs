using Microsoft.AspNetCore.Http;
using Shoppite.Application.Models;
using Shoppite.Infrastructure.Data;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Shoppite.UI.Helpers
{
    public class CommonHelper: ICommonHelper
    {
        protected readonly Shoppite_masterContext _dbContext;
        public CommonHelper(Shoppite_masterContext dbContext)
        {
            _dbContext = dbContext;
        }
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
                orgid = 19;
            else
            {
                LogError(subdomain);
                var orgObject = _dbContext.Organization.Where(x => x.OrgName == subdomain).FirstOrDefault();
                if (orgObject != null)
                    orgid = orgObject.Id;
                else
                    orgid = 0;
            }
            return orgid;

        }
        public void LogError(string msg)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", msg);
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = "C:\\inetpub\\vhosts\\shooppy.in\\httpdocs\\ErrorLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        public bool DoesPropertyExist(dynamic settings, string name)
        {
            if (settings is ExpandoObject)
                return ((IDictionary<string, object>)settings).ContainsKey(name);

            return settings.GetType().GetProperty(name) != null;
        }

    }
}
