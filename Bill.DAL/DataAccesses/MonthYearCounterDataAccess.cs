using Bill.DAL;
using Bill.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill.DAL.DataAccesses
{
    class MonthYearCounterDataAccess
    {
        public static async Task<List<MonthYearCounter>> GetMonthYearCounters()
        {
            using (BillContext db = new BillContext())
            {
                return await db.MonthYearCounters.ToListAsync();
            }
        }

        public static async Task<MonthYearCounter> GetMonthYearCounterByDate(DateTime date)
        {
            using (BillContext db = new BillContext())
            {
               return await db.MonthYearCounters.Where(x => x.Month == date.Month && x.Year == date.Year).SingleOrDefaultAsync();
            }
        }

        public static async Task<int> CreateMonthYearCounter(Invoice invoice)
        {
            using (BillContext db = new BillContext())
            {
                MonthYearCounter myCounter = new MonthYearCounter
                {
                    CurrentCounter = 1,
                    Month = invoice.Date.Month,
                    Year = invoice.Date.Year
                };
                db.MonthYearCounters.Add(myCounter);
                await db.SaveChangesAsync();
                return myCounter.CurrentCounter;
            }
        }

        public static async Task EditMonthYearCounter(MonthYearCounter myCounter)
        {
            using (BillContext db = new BillContext())
            {
                myCounter.CurrentCounter++;
                db.Entry(myCounter).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
        }
    }
}
