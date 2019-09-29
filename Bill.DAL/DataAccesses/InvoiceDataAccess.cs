using Bill.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace Bill.DAL
{
    public class InvoiceDataAccess
    {
        //Get a list of invoices with the Client and Company to show email of client and name of company on the Index page
        public static async Task<List<Invoice>> GetInvoices()
        {
            using (BillContext db = new BillContext())
            {
                return await db.Invoices.Include(i => i.Client).Include(i => i.Company).ToListAsync();
            }
        }

        //Get an invoice with the Client and Company and include the detail lines of the invoice
        public static async Task<Invoice> GetInvoice(int id)
        {
            using (BillContext db = new BillContext())
            {
                return await db.Invoices
                    .Include(i => i.Client)
                    .Include(i => i.Company)
                    .Include(i => i.InvoiceLines)
                    .SingleOrDefaultAsync(i => i.ID == id);
            }
        }
    }
}
