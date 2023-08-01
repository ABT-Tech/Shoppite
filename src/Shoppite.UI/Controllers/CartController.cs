namespace Shoppite.UI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Shoppite.Application.Models;
    using Shoppite.UI.Extensions;
    using Shoppite.UI.Helpers;
    using Shoppite.UI.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CartController" />.
    /// </summary>
    [Authorize]
    public class CartController : Controller
    {
        /// <summary>
        /// Defines the _cartPageService.
        /// </summary>
        private readonly ICartPageServices _cartPageService;

        /// <summary>
        /// Defines the _commonHelper.
        /// </summary>
        private readonly ICommonHelper _commonHelper;

        /// <summary>
        /// Defines the _commonHelper.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        /// <param name="cartPageServices">The cartPageServices<see cref="ICartPageServices"/>.</param>
        /// <param name="commonHelper">The commonHelper<see cref="ICommonHelper"/>.</param>
        public CartController(ICartPageServices cartPageServices, ICommonHelper commonHelper, IConfiguration configuration)
        {
            _cartPageService = cartPageServices ?? throw new ArgumentNullException(nameof(cartPageServices));
            _commonHelper = commonHelper;
            _configuration = configuration;
        }

        /// <summary>
        /// The Cart.
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> Cart()
        {
            int orgid = _commonHelper.GetOrgID(HttpContext);
            var cartlist = await _cartPageService.OrderBasic(orgid);
            foreach (var swap in cartlist.F_Getproduct_CartDetails_By_Orgids)
            {
                if (swap.Qty > swap.BasicQty)
                {
                    swap.Qty = swap.BasicQty;
                }
            }

            return View(cartlist);
        }

        /// <summary>
        /// The DeleteProduct.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        [HttpGet]
        public ActionResult DeleteProduct(int id)
        {
            _cartPageService.DeleteAysnc(id);
            return RedirectToAction("Cart");
        }

        /// <summary>
        /// The AddToCheck.
        /// </summary>
        /// <param name="checkOut">The checkOut<see cref="CheckOutModel"/>.</param>
        /// <returns>The <see cref="Task{ActionResult}"/>.</returns>
        [HttpPost]
        public async Task<ActionResult> AddToCheck([FromBody] CheckOutModel checkOut)
        {
            await _cartPageService.UpdateOrderQty(checkOut);

            return Json(checkOut);
        }

        /// <summary>
        /// The CheckOut.
        /// </summary>
        /// <param name="orderid">The orderid<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{ActionResult}"/>.</returns>
        public async Task<ActionResult> CheckOut(Guid orderid)
        {
            var order = await _cartPageService.CheckOrder(orderid);
            //  var orderId = Request.Query
            return View(order);
        }

        /// <summary>
        /// The OrderSuccess.
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> OrderSuccess()
        {
            // await _cartPageService.UpdateOrder(orderid);Guid orderid
            return View();
        }

        /// <summary>
        /// The SaveAddress.
        /// </summary>
        /// <param name="Model">The Model<see cref="CartModel"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPost]
        public async Task<IActionResult> SaveAddress(CartModel Model)
        {
            int orgid = _commonHelper.GetOrgID(HttpContext);
            Model.OrderShippingModel.OrgId = orgid;
            await _cartPageService.SaveAddress(Model);

            await _cartPageService.UpdateOrder((Guid)Model.OrderBasicModel.OrderGuid);

            return RedirectToAction("OrderSuccess");
        }

        [HttpPost]
        public async Task<IActionResult> MakePaymentRequest(CartModel Model)
        {
            if (Model.IsPaytm)
            {
                var order = await _cartPageService.CheckOrder((Guid)Model.OrderBasicModel.OrderGuid);
                var merchantDetails = await _commonHelper.
                using (HttpClient client = new HttpClient())
                {
                    MerchantParams merchantParams = new MerchantParams();
                    String merchantId = "M00006063";
                    string encryptedParams = EncryptPaymentRequest();
                    ProcessPaymentRequest(encryptedParams, merchantId);
                }
                int orgid = _commonHelper.GetOrgID(HttpContext);
                Model.OrderShippingModel.OrgId = orgid;
                await _cartPageService.SaveAddress(Model);
                await _cartPageService.UpdateOrder((Guid)Model.OrderBasicModel.OrderGuid);
                return View();
            }
            else
            {
                int orgid = _commonHelper.GetOrgID(HttpContext);
                Model.OrderShippingModel.OrgId = orgid;
                await _cartPageService.SaveAddress(Model);
                await _cartPageService.UpdateOrder((Guid)Model.OrderBasicModel.OrderGuid);
                return RedirectToAction("OrderSuccess");
            }
            
        }

        public string EncryptPaymentRequest()
        {
            //add the Transaction Posting URL	
            /* 
                Merchant has to enter the Merchant Id shared by 1Pay in the below variable 'merchantId' and pass in the Request along with encrypted Request Data.
                Merchant has to enter the API Key shared by 1Pay in the below variable 'merchantEncryptionKey' to Encrypt the Transaction Request.
                Algorithm used for encryption is AES.
                Merchant Encryption Key will be different for TEST and PRODUCTION environment.
            */
            String merchantId = "M00006063";
            String merchantEncryptionKey = "Jt5cO5cf2jg8bX4Bc9yw0Nr8Ng5zm5xz"; //16 Charachter String
            String encryptedText = string.Empty;

            try
            {

                string original = "";  //"Here is some data to encrypt!";

                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
                AesManaged tdes = new AesManaged();
                tdes.Key = UTF8.GetBytes(merchantEncryptionKey);
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform crypt = tdes.CreateEncryptor();
                byte[] plain = Encoding.UTF8.GetBytes("{\"merchantId\":\"M00006063\",\"ApiKey\":\"Jt5cO5cf2jg8bX4Bc9yw0Nr8Ng5zm5xz\",\"txnId\":\"1234567890\",\"amount\":\"10.00\",\"DateTime\":\"\\/Date(1547311525000)\\/\",\"CustMail\":\"test@test.com\",\"CustMobile\":\"9876543210\",\"UDF1\":\"NA\",\"UDF2\":\"NA\",\"ReturnUrl\":\"www.merchantUrl.com/merchantResponseProcessor.jsp\",\"IsMultiSettlement\":\"0\",\"ProductId\":\"DEFAULT\",\"ChannelId\":\"0\",\"TxnType\":\"DIRECT\",\"UDF3\":\"NA\",\"UDF4\":\"NA\",\"UDF5\":\"NA\",\"InstrumentId\":\"NA\",\"CardDetails\":\"NA\",\"CardType\":\"NA\"}");
                //byte[] plain = Encoding.UTF8.GetBytes("{\"merchantId\":\"M0002\",\"apiKey\":\"jpuT6032\",\"txnId\":\"1234567892\",\"amount\":\"10.00\",\"dateTime\":\"2019-01-19 20:15:25\",\"custMail\":\"test@test.com\",\"custMobile\":\"9876543210\",\"udf1\":\"NA\",\"udf2\":\"NA\",\"returnURL\":\"http://139.59.1.254:8080/payone/merchantResponse.jsp\",\"isMultiSettlement\":\"0\",\"productId\":\"DEFAULT\",\"channelId\":\"0\",\"txnType\":\"DIRECT\",\"udf3\":\"NA\",\"udf4\":\"NA\",\"udf5\":\"NA\",\"instrumentId\":\"NA\",\"cardDetails\":\"NA\",\"cardType\":\"NA\"}");
                //byte[] plain = Encoding.UTF8.GetBytes(merchantParamsJson);
                byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
                encryptedText = Convert.ToBase64String(cipher);
                System.Console.WriteLine(encryptedText);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            return encryptedText;
        }
        public string ProcessPaymentRequest(string merchantParamsJson, string merchantId)
        {
            String tranStatus = null;
            String message = null;
            String errorCode = null;
            //JavaScriptSerializer ser = new JavaScriptSerializer();
            //add the Transaction Posting URL	
            String reqUrl = "https://pa-preprod.in/payment/payprocessorv2";
            string responseString = string.Empty;


            //======================================= Server Side Form Post ===========================================================

            //string formId = "onePayForm";

            //StringBuilder htmlForm = new StringBuilder();
            //htmlForm.AppendLine("<html>");
            //htmlForm.AppendLine(String.Format("<body onload='document.forms[\"{0}\"].submit()'>", formId));
            //htmlForm.AppendLine(String.Format("<form id='{0}' method='POST' action='{1}'>", formId, Url));
            //htmlForm.AppendLine(String.Format("<input type='hidden' name='reqData' id='reqData' value='{0}' />", merchantParamsJson));
            //htmlForm.AppendLine(String.Format("<input type='hidden' name='merchantId' id='merchantId' value='{0}' />", merchantId));
            //htmlForm.AppendLine("</form>");
            //htmlForm.AppendLine("</body>");
            //htmlForm.AppendLine("</html>");

            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.Write(htmlForm.ToString());
            //HttpContext.Current.Response.End();

            //=========================================================================================================================

            //var request = (HttpWebRequest)WebRequest.Create(reqUrl);

            try
            {
                var postData = string.Format("reqData={0}&merchantId={1}", merchantParamsJson.Trim(), merchantId);
                var data = Encoding.UTF8.GetBytes(postData);

                HttpResponseMessage response;

                using (var handler = new HttpClientHandler())
                using (var client = new HttpClient(handler))
                {
                    using (var content = new StringContent(postData, Encoding.UTF8, "application/json"))
                    {
                        client.Timeout = TimeSpan.FromMinutes(5);
                        response = client.PostAsync(new Uri(reqUrl), content).Result;
                    }
                }

                if (response.IsSuccessStatusCode)
                {
                    var responseResult = response.Content.ReadAsStringAsync();
                    responseResult.Wait();
                    responseString = responseResult.Result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            Console.WriteLine("verification started");

            if (!string.IsNullOrEmpty(responseString))
            {


                // Here please read response and deserialize that response into 'PayproceesResponseClass' class (create class same as 'VerificationResponse' in VerificationUtil.cs file)			
                var payresponse =  JsonConvert.DeserializeObject<PayproceesResponseClass>(responseString);

                String txn_id = payresponse.txn_id; // Put Here Merchant transaction id from the response;			

                VerificationUtil objVerificationUtil = new VerificationUtil();


                String verParams = "merchantId=" + merchantId + "&txnId=" + txn_id;
                String verUrl = "https://hdfcprodsigning.in/onepayVAS/getTxnDetails";

                var verResp = objVerificationUtil.VerificationCall(verUrl, verParams);
                if (verResp.resp_code == "00000" && verResp.trans_status == "ok")
                {
                    tranStatus = "Ok";
                    message = "Transaction Successful.";
                    errorCode = "00000";
                }
                else
                {
                    tranStatus = "F";
                    errorCode = "FFFFF";
                    message = "Transaction Failed.";
                }

                Console.WriteLine("::: tranStatus : " + tranStatus);
                Console.WriteLine("::: errorCode : " + errorCode);
                Console.WriteLine("::: message : " + message);



            }

            return responseString;
        }

    }
}
