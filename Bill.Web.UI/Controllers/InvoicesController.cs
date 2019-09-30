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
using Bill.DataModels;
using Bill.BLL;

namespace Bill.Web.UI.Controllers
{
    public class InvoicesController : Controller
    {
        private BillContext db = new BillContext();
        public async Task<ActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParam = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.EmailSortParam = sortOrder == "email" ? "email_desc" : "email";
            ViewBag.CompanySortParam = sortOrder == "company" ? "company_desc" : "company";
            ViewBag.FinishedSortParam = sortOrder == "finished" ? "finished_desc" : "finished";
            ViewBag.CurrentFilter = searchString;
            List<Invoice> invoices = await InvoiceDataAccess.GetInvoices();
            invoices = InvoiceLogic.SortTable(sortOrder, invoices);
            if (!string.IsNullOrEmpty(searchString))
            {
                invoices = InvoiceLogic.SearchInvoices(searchString, invoices);
            }
            return View(invoices);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = await InvoiceDataAccess.GetInvoice(id);
            invoice.InvoiceTotalCost = InvoiceLogic.CalculateInvoiceTotal(invoice);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Date,Finished,Deleted,ClientID,CompanyID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                await InvoiceDataAccess.CreateInvoice(invoice);
                return RedirectToAction("Index");
            }

            return View(invoice);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = await db.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Date,Finished,Deleted,ClientID,CompanyID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = await db.Invoices.FindAsync(id);
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
            Invoice invoice = await db.Invoices.FindAsync(id);
            db.Invoices.Remove(invoice);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
