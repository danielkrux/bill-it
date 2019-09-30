using System;

namespace Bill.DataModels
{
    public class InvoiceLine
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Amount { get; set; }
        public decimal VAT { get; set; }
        public int InvoiceID { get; set; }

        public Invoice Invoice { get; set; }

        public int DiscountPercent => Convert.ToInt32(Discount * 100);

        public decimal TotalCostAfterDiscount
        {
            get { return CalculateSubtotal(); }
            set { }
        }

        public decimal CalculateSubtotal()
        {
            var discount = (Price * Amount) / 100 * DiscountPercent;
            var subTotal = (Price * Amount) - discount;
            return Math.Round(subTotal, 2);
        }
    }
}
