using System.Collections.Generic;

namespace Models
{
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string VATNumber { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
