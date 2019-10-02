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
using Bill.DAL.DataAccesses;

namespace Presentation.Controllers
{
    public class CompaniesController : Controller
    {
        private BillContext db = new BillContext();
        private readonly CompanyDataAccess companyDA = new CompanyDataAccess();

        public async Task<ActionResult> Index()
        {
            return View(await companyDA.GetCompanies());
        }

        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await companyDA.GetCompany(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,VATNumber,Street,PostalCode,City")] Company company)
        {
            if (ModelState.IsValid)
            {
                await companyDA.CreateCompany(company);
                return RedirectToAction("Index");
            }

            return View(company);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await companyDA.GetCompany(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,VATNumber,Street,PostalCode,City")] Company company)
        {
            if (ModelState.IsValid)
            {
                companyDA.EditCompany(company);
                return RedirectToAction("Index");
            }
            return View(company);
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await companyDA.GetCompany(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            companyDA.DeleteCompany(id);
            return RedirectToAction("Index");
        }
    }
}
