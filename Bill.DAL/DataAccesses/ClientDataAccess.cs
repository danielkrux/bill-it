using Bill.DataModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Bill.DAL
{
    public class ClientDataAccess
    {
        public static async Task<List<Client>> GetClients()
        {
            using (BillContext db  = new BillContext())
            {
                return await db.Clients.ToListAsync();
            }
        }

        public static async Task<Client> GetClient(int? id)
        {
            using (BillContext db = new BillContext())
            {
                return await db.Clients.FindAsync(id);

            }
        }

        public static async void EditClient(Client client)
        {
            using (BillContext db = new BillContext())
            {
                db.Entry(client).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

        }
    }
}
