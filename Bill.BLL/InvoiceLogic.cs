using Bill.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bill.BLL
{
    public class InvoiceLogic
    {
        public static List<Invoice> SortTable(string SortOrder, List<Invoice> Invoices)
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

        public static List<Invoice> SearchInvoices(string SearchString, List<Invoice> Invoices)
        {
            return Invoices = Invoices.Where(i => i.Client.Email.Contains(SearchString) || i.Company.Name.Contains(SearchString)).ToList();
        }

        public static string CreateInvoiceCode(int invoiceID)
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("d2") + '-' + $"{invoiceID:0000}";
        }

        public static decimal CalculateSubtotal(InvoiceLine InvoiceLine)
        {
            var discount = (InvoiceLine.Price * InvoiceLine.Amount) / 100 * InvoiceLine.Discount;
            return (InvoiceLine.Price * InvoiceLine.Amount) - discount;
        }

        public static decimal CalculateTotalBeforeVAT(Invoice invoice)
        {
            return invoice.InvoiceLines.ToList().Sum(x => x.TotalCostAfterDiscount);
        }

        public static List<TotalPerVATRate> CalculateTotalPerVATRate(Invoice Invoice)
        {
            var groupedInvoices = Invoice.InvoiceLines.GroupBy(x => x.VAT).ToList();
            List<TotalPerVATRate> CalculatedTotals = new List<TotalPerVATRate>();
            foreach (var group in groupedInvoices)
            {
                decimal vatRate = group.Key;
                decimal groupTotal = 0;
                foreach (var item in group)
                {
                    var discount = (item.Price * item.Amount) / 100 * item.DiscountPercent;
                    groupTotal += (item.Price * item.Amount) - discount;
                }
                TotalPerVATRate totalPerVAT = new TotalPerVATRate
                {
                    TotalBeforeVAT = Math.Round(groupTotal, 2),
                    VATRate = Math.Round(vatRate, 0),
                    VATPrice = Math.Round(groupTotal / 100 * vatRate, 2),
                    TotalAfterVAT = Math.Round(groupTotal + (groupTotal / 100 * vatRate), 2)
                };
                CalculatedTotals.Add(totalPerVAT);
                CalculateTotalAfterVAT(CalculatedTotals);
            }
            return CalculatedTotals;
        }

        public static decimal CalculateTotalAfterVAT(List<TotalPerVATRate> CalculatedTotals)
        {
            return CalculatedTotals.Sum(c => c.TotalAfterVAT);
        }
    }
}
