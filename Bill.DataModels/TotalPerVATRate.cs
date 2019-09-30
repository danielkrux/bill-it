namespace Bill.DataModels
{
    public class TotalPerVATRate
    {
        public decimal VATRate { get; set; }
        public decimal VATPrice { get; set; }
        public decimal TotalBeforeVAT { get; set; }
        public decimal TotalAfterVAT { get; set; }
    }
}
