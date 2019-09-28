namespace Bill.DataModels
{
    public class InvoiceLine
    {
        public int ID { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int Amount { get; set; }
        public double VAT { get; set; }
        public int MyProperty { get; set; }
        public int InvoiceID { get; set; }
    }
}
