using System;
using System.Collections.Generic;

namespace Models
{
    public class Invoice
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public bool Finished { get; set; }
        public bool Deleted { get; set; }
        public int ClientID { get; set; }
        public int CompanyID { get; set; }
        public List<InvoiceLine> InvoiceLines { get; set; }
    }
}
