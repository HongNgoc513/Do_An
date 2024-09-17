namespace DoAnWeb.ThanhToan
{
    public class PaymentInformationModel
    {
        public string OrderType { get; set; } = "VeXe";
        public static double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
    }
}

