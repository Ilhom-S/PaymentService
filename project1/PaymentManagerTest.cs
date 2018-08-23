using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1
{
    class PaymentManagerTest
    {
        private Random _random = new Random();
        private object _object=new Object();
        private List<Payment> Payments = new List<Payment>();

        private void CreatePayment()
        {
            lock (_object) {
            var payment = new Payment();
            payment.Amount = (decimal)_random.NextDouble();
            payment.Account = "test";
            payment.Currency = "KZT";
            payment.Service = "1";
            payment.TransactionId = _random.Next();
            payment.RequestDate = DateTime.Now;
            payment.TransactionStatus = 0;
            Payments.Add(payment);
            }
        }
        public void AddPayment()
        {
           CreatePayment();
            

        }
        public List<Payment> GetPaymentsToCheck()
        {
            lock (_object)
            {
                return Payments.Where(p => p.TransactionStatus == 1).ToList();
            }
         
        }
        
        public List<Payment> GetPaymentToSend()
        {
            lock (_object)
            {
                return Payments.Where(p => p.TransactionStatus == 0).ToList();
            }
        }
    }
}
