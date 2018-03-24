using RestSharp;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PlanetPayment
{
    public class PlanetPaymentService
    {
        private PlanetPaymentProxy proxy;

        public PlanetPaymentService()
        {
            proxy = new PlanetPaymentProxy();
        }
        
        public PlanetPaymentServiceResponse Registration(string cardNumber, string cardHolder, string brand, string expiryMonth, string expiryYear, string cvv)
        {
            IRestResponse<PlanetPaymentResponseDTO> responseData = proxy.Registration(cardNumber, cardHolder, brand, expiryMonth, expiryYear, cvv);

            PlanetPaymentServiceResponse result = ProcessResponse(responseData);

            return result;

        }

        public PlanetPaymentServiceResponse Charge(string registrationToken, string currency, string amount)
        {
            IRestResponse<PlanetPaymentResponseDTO> responseData = proxy.Charge(registrationToken, currency, amount);

            PlanetPaymentServiceResponse result = ProcessResponse(responseData);
            
            return result;
        }

        public PlanetPaymentServiceResponse Reverse(string referencedPaymentId)
        {
            IRestResponse<PlanetPaymentResponseDTO> responseData = proxy.Reverse(referencedPaymentId);

            PlanetPaymentServiceResponse result = ProcessResponse(responseData);

            return result;
        }

        public PlanetPaymentServiceResponse Refund(string referencedPaymentId, string currency, string amount)
        {
            IRestResponse<PlanetPaymentResponseDTO> responseData = proxy.Refund(referencedPaymentId, currency, amount);

            PlanetPaymentServiceResponse result = ProcessResponse(responseData);

            return result;
        }


        private PlanetPaymentServiceResponse ProcessResponse(IRestResponse<PlanetPaymentResponseDTO> responseData)
        {
            PlanetPaymentServiceResponse result = new PlanetPaymentServiceResponse();

            result.Success = IsTransactionSuccess(responseData.Data.result.code);
            result.PaymentId = responseData.Data.id;
            result.Message = responseData.Data.result.description;

            return result;
        }

        private IRestResponse<T> ExecutePOST<T>(string url, string resource, Dictionary<string, string> parameters) where T : new()
        {
            var client = new RestClient(url);
            var request = new RestRequest(resource, Method.POST);

            foreach (var item in parameters)
            {
                request.AddParameter(item.Key, item.Value);
            }

            var response = client.Execute<T>(request);

            return response;
        }

        public bool IsTransactionSuccess(string code)
        {
            Regex rgxTransactionSuccess = new Regex(@"^(000\.000\.|000\.100\.1|000\.[36])");
            return rgxTransactionSuccess.IsMatch(code);
        }

    }
   


}