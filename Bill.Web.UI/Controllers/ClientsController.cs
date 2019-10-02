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
using Bill.DAL.DataAccesses;

namespace Presentation.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ClientDataAccess clientDA = new ClientDataAccess();
        private readonly ClientLogic clientLogic = new ClientLogic();

        public async Task<ActionResult> Index()
        {
            List<Client> clients = await clientDA.GetClientsWithInvoices();
            List<Client> activeClients = await clientLogic.GetActiveClients(clients);
            return View(activeClients);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await clientDA.GetClient(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Firstname,Lastname,Email,Street,PostalCode,City,Active")] Client client)
        {
            if (ModelState.IsValid)
            {
                await clientDA.CreateClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = await clientDA.GetClient(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Firstname,Lastname,Email,Street,PostalCode,City,Active")] Client client)
        {
            if (ModelState.IsValid)
            {
                await clientDA.EditClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await clientDA.GetClient(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            clientDA.SoftDeleteClient(id);
            return RedirectToAction("Index");
        }
    }
}
