using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Libary.Models
{
    public class Loan
    {
        public int ID { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        public DateTime LoanDate { get; set; }
        public LoanStatus LoanStatus { get; set; }

        public Loan()
        {

        }

        public int ReturnBook()
        {
            
            return Book.Quantity;
        }
    }
}
