using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace PaymentService
{
    /// <summary>
    /// Class for Hgg payment server
    /// </summary>
    class ServiceRequestHelper
    {
       
        public string ServerUrl= "https://hgg.kz:8802";
        private Provider _provider;

        public ServiceRequestHelper(Provider provider)
        {
            this._provider = provider;
        }
        

        /// <summary>
        /// Method for checking transaction from server
        /// </summary>
        /// <returns>returns string</returns>
        public string CheckTransaction(Payment payment)
        {
            WebClient client = new WebClient();
            client.QueryString.Clear();
            //  int AgentID = 1;
            //int TransactionID = 113;
            //string RequestDate = "2017--21";
            client.QueryString.Add("AgentID", _provider.AgentId);
            client.QueryString.Add("TransactionID", payment.TransactionId.ToString());
            client.QueryString.Add("RequestDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            client.QueryString.Add("Service", "1");
            client.QueryString.Add("Amount", payment.Amount.ToString("F2"));
            client.QueryString.Add("RequestType", "AccountCheck");
            client.QueryString.Add("AgentPassword", _provider.AgentPassword);
            client.QueryString.Add("account", payment.Account);
            client.QueryString.Add("Currency", payment.Currency);
            byte[] reply = client.UploadValues(ServerUrl, client.QueryString);
            // Decode and display the response.
            var response = Encoding.UTF8.GetString(reply);
            Console.WriteLine("Response from server:{0}", response);
            payment.TransactionStatus = 2;
            return "";


        }

        /// <summary>
        /// Method for getting balance from server 
        /// </summary>
        /// <returns>Returns string</returns>
        public void CheckBalance()
        {
            WebClient client = new WebClient();
            client.QueryString.Clear();
            client.QueryString.Add("AgentID", _provider.AgentId);
            client.QueryString.Add("RequestType", "CheckBalance");
            client.QueryString.Add("AgentPassword", _provider.AgentPassword);
            byte[] reply = client.UploadValues(ServerUrl, client.QueryString);
            var response = Encoding.UTF8.GetString(reply);
            Console.WriteLine("Response from server:{0}", response);

        }

        /// <summary>
        /// Method for making payment
        /// </summary>
        /// <returns>Returns String</returns>
        public string SendPayment(Payment payment)
        {
            //transaction status must change to 1
            WebClient client = new WebClient();
            Console.WriteLine("Preparing to make new transaction: {0}", payment.ToString());
            System.Threading.Thread.Sleep(5000);
            client.QueryString.Clear();
            //  int AgentID = 1;
            //int TransactionID = 113;
            //string RequestDate = "2017--21";
            client.QueryString.Add("AgentID", _provider.AgentId);
            client.QueryString.Add("TransactionID", payment.TransactionId.ToString());
            client.QueryString.Add("RequestDate", payment.RequestDate.ToString("yyyy-MM-dd hh:mm:ss"));
            client.QueryString.Add("Service", payment.Service);
            client.QueryString.Add("Amount", payment.Amount.ToString("F2"));
            client.QueryString.Add("RequestType", "Payment");
            client.QueryString.Add("AgentPassword", _provider.AgentPassword);
            client.QueryString.Add("account", payment.Account);
            client.QueryString.Add("Currency", payment.Currency);
            byte[] reply = client.UploadValues(ServerUrl, client.QueryString);
            // Decode and display the response.
            var response = Encoding.UTF8.GetString(reply);
            Console.WriteLine("Response from server:{0}", response);
            payment.TransactionStatus = 1;
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            var result = jsSerializer.DeserializeObject(response);
             Dictionary<string, object> obj2 = new Dictionary<string, object>();
             obj2=(Dictionary<string,object>)(result);
             object val = obj2["ResponseStatus"];
             System.Console.WriteLine(val);
            return response;
        }
    }
}