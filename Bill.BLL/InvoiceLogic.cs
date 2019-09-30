using Bill.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bill.BLL
{
    public class InvoiceLogic
    {
        public static List<Invoice> SortTable(string SortOrder, List<Invoice>Invoices)
        {
            switch (SortOrder)
            {
                case "date_desc":
                    Invoices = Invoices.OrderByDescending(i => i.Date).ToList();
                    break;
                case "date":
                    Invoices = Invoices.OrderBy(i => i.Date).ToList();
                    break;
                case "email_desc":
                    Invoices = Invoices.OrderByDescending(i => i.Client.Email).ToList();
                    break;
                case "email":
                    Invoices = Invoices.OrderBy(i => i.Client.Email).ToList();
                    break;
                case "company_desc":
                    Invoices = Invoices.OrderByDescending(i => i.Company.Name).ToList();
                    break;
                case "company":
                    Invoices = Invoices.OrderBy(i => i.Company.Name).ToList();
                    break;
                case "finished_desc":
                    Invoices = Invoices.OrderByDescending(i => i.Finished).ToList();
                    break;
                case "finished":
                    Invoices = Invoices.OrderBy(i => i.Finished).ToList();
                    break;
                default:
                    Invoices = Invoices.OrderBy(i => i.Date).ToList();
                    break;
            }
            return Invoices;
        }
        public static string CreateInvoiceCode(int invoiceID)
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("d2") + '-' + $"{invoiceID:0000}";
        }

        public static double CalculateInvoiceTotal(Invoice invoice)
        {
            return invoice.InvoiceLines.ToList().Sum(x => x.TotalCost);
        }
    }
}
