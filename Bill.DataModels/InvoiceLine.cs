using System;

namespace Bill.DataModels
{
    public class InvoiceLine
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Amount { get; set; }
        public double VAT { get; set; }
        public int InvoiceID { get; set; }

        public Invoice Invoice { get; set; }

        public int DiscountPercent => Convert.ToInt32(Discount * 100);

        public double TotalCostAfterDiscount
        {
            get { return CalculateInvoiceLineTotal(); }
            set { }
        }

        public double CalculateInvoiceLineTotal()
        {
            var discount = (Price * Amount) / 100 * DiscountPercent;
            var total = (Price * Amount) - discount;
            return Math.Round(total, 2);
        }
    }
}
