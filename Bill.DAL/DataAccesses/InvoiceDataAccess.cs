using Bill.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Data.Entity;
using Bill.BLL;
using System.Linq;
using MoreLinq.Extensions;

namespace Bill.DAL
{
    public class InvoiceDataAccess
    {
        private readonly InvoiceLogic invoiceLogic = new InvoiceLogic();
        private readonly BillContext db = new BillContext();

        public async Task<List<Invoice>> GetInvoices()
        {
            return await db.Invoices.ToListAsync();
        }

        public async Task<List<Invoice>> GetInvoicesWithClientCompany()
        {
            return await db.Invoices.Include(i => i.Client).Include(i => i.Company).ToListAsync();
        }

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
            string InvoiceMonth = invoice.Date.Month.ToString("d2");
            int InvoiceYear = invoice.Date.Year;

            db.Invoices.Add(invoice);
            await db.SaveChangesAsync();

            try
            {
                Invoice invoiceWithLatestCode = await GetInvoiceWithLatestCode($"{InvoiceYear + InvoiceMonth}");
                invoice.Code = invoiceWithLatestCode.Code++;
            }
            catch (Exception)
            {
                invoice.Code = Convert.ToInt32($"{InvoiceYear}{InvoiceMonth}0001");
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

        #region HelperMethods
        private async Task<Invoice> GetInvoiceWithLatestCode(string dateOfCode)
        {
            List<Invoice> invoices = await GetInvoices();
            return invoices.Where(i => i.Code.ToString().Contains(dateOfCode))
                           .MaxBy(i => i.Code).FirstOrDefault();
        }
        #endregion
    }
}
