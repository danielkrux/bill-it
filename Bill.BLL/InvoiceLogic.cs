using Bill.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bill.BLL
{
    public class InvoiceLogic
    {
        public List<Invoice> SortTable(string SortOrder, List<Invoice> Invoices) => SortOrder switch
        {
            "date_desc" => Invoices.OrderByDescending(i => i.Date).ToList(),
            "date" => Invoices.OrderBy(i => i.Date).ToList(),
            "email_desc" => Invoices.OrderByDescending(i => i.Client.Email).ToList(),
            "email" => Invoices.OrderBy(i => i.Client.Email).ToList(),
            "company_desc" => Invoices.OrderByDescending(i => i.Company.Name).ToList(),
            "company" => Invoices.OrderBy(i => i.Company.Name).ToList(),
            "finished_desc" => Invoices.OrderByDescending(i => i.Finished).ToList(),
            "finished" => Invoices.OrderBy(i => i.Finished).ToList(),
            _ => Invoices.OrderBy(i => i.Date).ToList(),
        };

        public List<Invoice> SearchInvoices(string SearchString, List<Invoice> Invoices)
        {
            return Invoices = Invoices.Where(i =>
            i.Client.Email.Contains(SearchString)
            || i.Company.Name.Contains(SearchString)
            || i.Code.Contains(SearchString))
                .ToList();
        }

        public string CreateInvoiceCode(int Counter, Invoice Invoice)
        {
            return $"{Invoice.Date.Year}" + $"{Invoice.Date.Month.ToString("00")}" + '-' + $"{Counter:0000}";
        }

        public decimal CalculateSubtotal(InvoiceLine InvoiceLine)
        {
            var discount = (InvoiceLine.Price * InvoiceLine.Amount) / 100 * InvoiceLine.Discount;
            return (InvoiceLine.Price * InvoiceLine.Amount) - discount;
        }

        public decimal CalculateTotalBeforeVAT(Invoice invoice)
        {
            return invoice.InvoiceLines.ToList().Sum(x => x.TotalCostAfterDiscount);
        }
        
        public List<TotalPerVATRate> CalculateTotalPerVATRate(Invoice Invoice)
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

        public decimal CalculateTotalAfterVAT(List<TotalPerVATRate> CalculatedTotals)
        {
            return CalculatedTotals.Sum(c => c.TotalAfterVAT);
        }
    }
}
