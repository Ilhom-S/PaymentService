using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;


namespace project1
{
    class Program
    {
        private static PaymentManagerTest _paymentManager = new PaymentManagerTest();
        private static ServiceRequestHelper _serviceRequestHelper;
        static void Main(string[] args)
        {

            Provider provider = new Provider();
            provider.AgentId = Properties.Settings.Default.AgentId;
            provider.AgentPassword = Properties.Settings.Default.AgentPassword;
            _serviceRequestHelper = new ServiceRequestHelper(provider);
            var taskPaymentChecker = Task.Run(() => CheckPayments());
            var taskPaymentSender = Task.Run(() => SendReadyPayments());
            _serviceRequestHelper.ServerUrl = "https://hgg.kz:8802";
            Console.WriteLine("Cheking balance: ");
            _serviceRequestHelper.CheckBalance();
            // Add 1 payment
            _paymentManager.AddPayment();
            // Add 2 payment;
            _paymentManager.AddPayment();
            // var payment1 = _paymentManager.GetPayment();
            //  var payment2 = _paymentManager.GetPayment();
            //  var task1 = Task.Run(() => _serviceRequestHelper.MakePayment(payment1));
            // var task2 = Task.Run(() => _serviceRequestHelper.MakePayment(payment2));
            // task1.Wait();
            // payment1.TransactionStatus = 1;
            Console.Read();
        }
        static void SendReadyPayments()
        {
            while (true)
            {
                foreach (var payment in _paymentManager.GetPaymentToSend())
                {
                    //must take transaction status from list
                    

                    _serviceRequestHelper.SendPayment(payment);
                }
                System.Threading.Thread.Sleep(1000);
           }
        }
        static void CheckPayments()
        {
            while (true)
            {
                foreach (var payment in _paymentManager.GetPaymentsToCheck())
                {
                    Console.WriteLine("Checking payment: {0}", payment);
                    _serviceRequestHelper.CheckTransaction(payment);
                }
                System.Threading.Thread.Sleep(1000)
;
            }
        }
    }
}
