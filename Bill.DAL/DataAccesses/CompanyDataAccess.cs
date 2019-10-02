using Bill.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Bill.DAL.DataAccesses
{
    public class CompanyDataAccess
    {
        private readonly BillContext db = new BillContext();

        public async Task<List<Company>> GetCompanies()
        {
            return await db.Companies.ToListAsync();
        }

        public async Task<Company> GetCompany(int id)
        {
            return await db.Companies.FindAsync(id);
        }

        public async Task CreateCompany(Company company)
        {
            db.Companies.Add(company);
            await db.SaveChangesAsync();
        }

        public async Task EditCompany(Company company)
        {
            db.Entry(company).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteCompany(int id)
        {
            Company company = await GetCompany(id);
            db.Companies.Remove(company);
            await db.SaveChangesAsync();
        }
    }
}
