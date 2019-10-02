using Bill.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Data.Entity;
using Bill.BLL;
using Bill.DAL.DataAccesses;

namespace Bill.DAL
{
    public class InvoiceDataAccess
    {
        private readonly InvoiceLogic invoiceLogic = new InvoiceLogic();
        private readonly BillContext db = new BillContext();

        //Get a list of invoices with the Client and Company to show email of client and name of company on the Index page
        public async Task<List<Invoice>> GetInvoices()
        {
            return await db.Invoices.Include(i => i.Client).Include(i => i.Company).ToListAsync();
        }

        //Get an invoice with the Client and Company and include the detail lines of the invoice
        public async Task<Invoice> GetInvoice(int id)
        {
            return await db.Invoices
                .Include(i => i.Client)
                .Include(i => i.Company)
                .Include(i => i.InvoiceLines)
                .SingleOrDefaultAsync(i => i.ID == id);
        }

        public async Task CreateInvoice(Invoice invoice)
        {
            db.Invoices.Add(invoice);
            await db.SaveChangesAsync();
            try
            {
                MonthYearCounter myCounter = await MonthYearCounterDataAccess.GetMonthYearCounterByDate(invoice.Date);
                await MonthYearCounterDataAccess.EditMonthYearCounter(myCounter);
                invoice.Code = invoiceLogic.CreateInvoiceCode(myCounter.CurrentCounter, invoice);
            }
            catch (Exception)
            {
                int counter = await MonthYearCounterDataAccess.CreateMonthYearCounter(invoice);
                invoice.Code = invoiceLogic.CreateInvoiceCode(counter, invoice);
            }
            db.Entry(invoice).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task EditInvoice(Invoice invoice)
        {
            db.Entry(invoice).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteInvoice(int id)
        {
            Invoice invoice = await GetInvoice(id);
            db.Invoices.Remove(invoice);
            await db.SaveChangesAsync();
        }
    }
}
