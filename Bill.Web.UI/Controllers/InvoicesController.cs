using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bill.DAL;
using Bill.DAL.DataAccesses;
using Bill.DataModels;
using Bill.BLL;
using Bill.Web.UI.ViewModels;

namespace Bill.Web.UI.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoiceLogic invoiceLogic = new InvoiceLogic();
        private readonly InvoiceDataAccess invoiceDA = new InvoiceDataAccess(); 
        private readonly ClientDataAccess clientDA = new ClientDataAccess();
        private readonly CompanyDataAccess companyDA = new CompanyDataAccess();

        public async Task<ActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParam = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.EmailSortParam = sortOrder == "email" ? "email_desc" : "email";
            ViewBag.CompanySortParam = sortOrder == "company" ? "company_desc" : "company";
            ViewBag.FinishedSortParam = sortOrder == "finished" ? "finished_desc" : "finished";
            ViewBag.CurrentFilter = searchString;

            List<Invoice> invoices = await invoiceDA.GetInvoices();
            invoices = invoiceLogic.GetActiveInvoices(invoices);
            invoices = invoiceLogic.SortTable(sortOrder, invoices);

            if (!string.IsNullOrEmpty(searchString))
            {
                invoices = invoiceLogic.SearchInvoices(searchString, invoices);
            }
            return View(invoices);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = await invoiceDA.GetInvoice(id);
            invoice.TotalBeforeVAT = invoiceLogic.CalculateTotalBeforeVAT(invoice);
            invoice.TotalPerVATRate = invoiceLogic.CalculateTotalPerVATRate(invoice);
            invoice.TotalAfterVAT = invoiceLogic.CalculateTotalAfterVAT(invoice.TotalPerVATRate);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        public async Task<ActionResult> Create()
        {
            Invoice invoice = new Invoice();
            List<Client> clients = await clientDA.GetClients();
            List<Company> companies = await companyDA.GetCompanies();
            return View(new InvoiceClientsCompanies() { Invoice = invoice, Clients = clients, Companies = companies });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Date,Finished,Deleted,ClientID,CompanyID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                await invoiceDA.CreateInvoice(invoice);
                return RedirectToAction("Index");
            }

            return View(invoice);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = await invoiceDA.GetInvoice(id);
            List<Client> clients = await clientDA.GetClients();
            List<Company> companies = await companyDA.GetCompanies();
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(new InvoiceClientsCompanies() { Invoice = invoice, Clients = clients, Companies = companies } );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Date,ClientID,CompanyID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                await invoiceDA.EditInvoice(invoice);
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = await invoiceDA.GetInvoice(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await invoiceDA.DeleteInvoice(id);
            return RedirectToAction("Index");
        }
    }
}
