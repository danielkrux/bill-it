using Bill.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bill.BLL
{
    public class ClientLogic
    {
        public static async Task<List<Client>> GetActiveClients(List<Client> clients)
        {
            return clients.Where(c => c.Active == true).ToList();
        }

        public static bool IsActiveClient(Client client)
        {
           return _ = client.Active ? true : false;
        }
    }
}
