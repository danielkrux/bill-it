using System.Collections.Generic;

namespace Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public bool Active { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}
