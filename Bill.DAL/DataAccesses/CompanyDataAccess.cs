using Bill.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bill.DAL.DataAccesses
{
    public class CompanyDataAccess
    {

        public static async Task<List<Company>> GetCompanies()
        {
            using (BillContext db = new BillContext())
            {
                return await db.Companies.ToListAsync();
            }
        }
    }
}
