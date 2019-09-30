using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bill.DataModels
{
    public class Invoice
    {
        public int ID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Date { get; set; }
        public bool Finished { get; set; }
        public bool Deleted { get; set; }
        public int ClientID { get; set; }
        public int CompanyID { get; set; }
        public string Code { get; set; }

        public double InvoiceTotalCost { get; set; }

        public virtual Client Client { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}
