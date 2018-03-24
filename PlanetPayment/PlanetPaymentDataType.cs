
namespace PlanetPayment
{
    public class PlanetPaymentResponseDTO
    {
        public string id { get; set; }
        public string paymentType { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public ResultDTO result { get; set; }
        public ResultDetailsDTO resultDetails { get; set; }
        public RiskDTO risk { get; set; }
        public CardDTO card { get; set; }
        public string buildNumber { get; set; }
        public string timestamp { get; set; }
        public string ndc { get; set; }

    }

    public class ResultDTO
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class ResultDetailsDTO
    {
        public string ExtendedDescription { get; set; }
        public string ExchangeRate { get; set; }
        public string ConnectorTxID1 { get; set; }
        public string ConnectorTxID3 { get; set; }
        public string AuthorizationNumber { get; set; }
        public string ConnectorTxID2 { get; set; }
        public string AcquirerResponse { get; set; }
    }

    public class RiskDTO
    {
        public string score { get; set; }
    }

    public class CardDTO
    {
        public string bin { get; set; }
        public string last4Digits { get; set; }
        public string holder { get; set; }
        public string expiryMonth { get; set; }
        public string expiryYear { get; set; }
    }

    public class PlanetPaymentServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string PaymentId { get; set; }                
        public string RegistrationToken { get; set; }
    }
    

}
