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

        public static List<Invoice> SearchInvoices(string SearchString, List<Invoice>Invoices)
        {
            return Invoices = Invoices.Where(i => i.Client.Email.Contains(SearchString) || i.Company.Name.Contains(SearchString)).ToList();
        }

        public static string CreateInvoiceCode(int invoiceID)
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("d2") + '-' + $"{invoiceID:0000}";
        }

        public static double CalculateInvoiceTotal(Invoice invoice)
        {
            return invoice.InvoiceLines.ToList().Sum(x => x.TotalCostAfterDiscount);
        }

        public static double CalculateInvoiceLineTotal(InvoiceLine InvoiceLine)
        {
            var discount = (InvoiceLine.Price * InvoiceLine.Amount) / 100 * InvoiceLine.Discount;
            return (InvoiceLine.Price * InvoiceLine.Amount) - discount;
        }
    }
}
