using Microsoft.AspNetCore.Http;
using Shoppite.Application.Models;
using Shoppite.Infrastructure.Data;
using Shoppite.UI.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
                orgid = 1;
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

        public Core.Entities.OrganizationAggregatorControl GetMerchantDetails(HttpContext httpContext)
        {
            var orgID = GetOrgID(httpContext);
            var merchantDetails = _dbContext.OrganizationAggregatorControls.Where(x => x.OrgId == orgID).FirstOrDefault();
            return merchantDetails;
        }
        public string DecryptAES256_V3(string cipherText, string _key, string _iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();

            byte[] key = Encoding.ASCII.GetBytes(_key);
            byte[] iv = Encoding.ASCII.GetBytes(_iv);

            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            encryptor.Key = key;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);

            // Will contain decrypted plaintext
            string plainText = String.Empty;

            try
            {
                // Convert the ciphertext string into a byte array
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                // Decrypt the input ciphertext string
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

                // Complete the decryption process
                cryptoStream.FlushFinalBlock();

                // Convert the decrypted data from a MemoryStream to a byte array
                byte[] plainBytes = memoryStream.ToArray();

                // Convert the decrypted byte array to string
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                // Close both the MemoryStream and the CryptoStream
                memoryStream.Close();
                cryptoStream.Close();
            }

            // Return the decrypted data as a string
            return plainText;
        }

        public string EncryptAES256_V3(string plainText, string _key, string _iv)
        {
            // Instantiate a new Aes object to perform string symmetric encryption
            Aes encryptor = Aes.Create();
            byte[] key = Encoding.ASCII.GetBytes(_key);
            byte[] iv = Encoding.ASCII.GetBytes(_iv);
            encryptor.Mode = CipherMode.CBC;

            // Set key and IV
            encryptor.Key = key;
            encryptor.IV = iv;

            // Instantiate a new MemoryStream object to contain the encrypted bytes
            MemoryStream memoryStream = new MemoryStream();

            // Instantiate a new encryptor from our Aes object
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();

            // Instantiate a new CryptoStream object to process the data and write it to the 
            // memory stream
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);

            // Convert the plainText string into a byte array
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);

            // Encrypt the input plaintext string
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);

            // Complete the encryption process
            cryptoStream.FlushFinalBlock();

            // Convert the encrypted data from a MemoryStream to a byte array
            byte[] cipherBytes = memoryStream.ToArray();

            // Close both the MemoryStream and the CryptoStream
            memoryStream.Close();
            cryptoStream.Close();

            // Convert the encrypted byte array to a base64 encoded string
            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);

            // Return the encrypted data as a string
            return cipherText;
        }

    }
}
