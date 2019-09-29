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

        public double TotalCost => Price * Amount;

        public Invoice Invoice { get; set; }    
    }
}
