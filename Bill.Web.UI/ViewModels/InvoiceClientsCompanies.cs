using Bill.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bill.Web.UI.ViewModels
{
    public class InvoiceClientsCompanies
    {
        public Invoice Invoice { get; set; }
        public List<Client> Clients { get; set; }
        public List<Company> Companies { get; set; }
    }
}