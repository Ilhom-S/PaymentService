using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1
{
    class Payment
    {
        /// <summary>
        /// Amount to proccess
        /// </summary>
        public decimal Amount;
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency;
        /// <summary>
        /// Service type
        /// </summary>
        public string Service;
        /// <summary>
        /// Request date of payment
        /// </summary>
        public DateTime RequestDate;
        /// <summary>
        /// Account Id
        /// </summary>
        public string Account;
        /// <summary>
        /// TransactionId
        /// </summary>
        public int TransactionId;
        /// <summary>
        /// Transaction status.
        /// 0- Initial not sent
        /// 1- Sent, created. Need to be verified
        /// 2- Transaction succesfully sent to customer.
        /// </summary>
        public int TransactionStatus;
        /// <summary>
        /// Overloaded method of toString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("Payment, TransactionId:{2},Service:{0},Amount:{1}", Service, Amount, TransactionId);
        }
    }
}
