using Bill.DataModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Bill.DAL.DataAccesses
{
    public class ClientDataAccess
    {
        private readonly BillContext db = new BillContext();

        public async Task<List<Client>> GetClients()
        {
            return await db.Clients.ToListAsync();
        }

        public async Task<List<Client>> GetClientsWithInvoices()
        {
            return await db.Clients.Include(c => c.Invoices).ToListAsync();
        }

        public async Task<Client> GetClient(int id)
        {
            return await db.Clients.FindAsync(id);
        }

        public async Task CreateClient(Client client)
        {
            db.Clients.Add(client);
            await db.SaveChangesAsync();
        }

        public async Task EditClient(Client client)
        {
            db.Entry(client).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task SoftDeleteClient(int id)
        {
            Client client = await GetClient(id);
            client.Active = false;
            await EditClient(client);
        }
    }
}
