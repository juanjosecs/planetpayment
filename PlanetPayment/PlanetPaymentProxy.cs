using log4net;
using RestSharp;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Configuration;

namespace PlanetPayment
{
    public class PlanetPaymentProxy
    {
        private static readonly ILog log = LogManager.GetLogger("app");

        private string userId = "";
        private string password = "";
        private string entityId = "";
        private string baseUrl = "";
        private Dictionary<string, string> parameters;

        public PlanetPaymentProxy()
        {
            userId   = ConfigurationManager.AppSettings["planetPayment.userId"];
            password = ConfigurationManager.AppSettings["planetPayment.password"];
            entityId = ConfigurationManager.AppSettings["planetPayment.entityId"];
            baseUrl  = ConfigurationManager.AppSettings["planetPayment.url"];
            parameters = new Dictionary<string, string>();
        }

        public IRestResponse<PlanetPaymentResponseDTO> Registration(string cardNumber, string cardHolder, string brand, string expiryMonth, string expiryYear, string cvv)
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("authentication.userId", userId);
            parameters.Add("authentication.password", password);
            parameters.Add("authentication.entityId", entityId);
            parameters.Add("paymentBrand", brand);
            parameters.Add("card.number", cardNumber);
            parameters.Add("card.holder", cardHolder);
            parameters.Add("card.expiryMonth", expiryMonth);
            parameters.Add("card.expiryYear", expiryYear);
            parameters.Add("card.cvv", cvv);

            string resourceUrl = "/v1/registrations";

            var result = ExecutePOST<PlanetPaymentResponseDTO>(baseUrl, resourceUrl, parameters);

            return result;
        }

        public IRestResponse<PlanetPaymentResponseDTO> Charge(string registrationToken, string currency, string amount)
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("authentication.userId", userId);
            parameters.Add("authentication.password", password);
            parameters.Add("authentication.entityId", entityId);
            parameters.Add("amount", amount);
            parameters.Add("currency", currency);
            parameters.Add("paymentType", "DB");
            parameters.Add("recurringType", "REPEATED");

            string resourceUrl = "/v1/registrations/" + registrationToken + "/payments";

            var result = ExecutePOST<PlanetPaymentResponseDTO>(baseUrl, resourceUrl, parameters);
            
            return result;

        }

        public IRestResponse<PlanetPaymentResponseDTO> Reverse(string referencedPaymentId)
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("authentication.userId", userId);
            parameters.Add("authentication.password", password);
            parameters.Add("authentication.entityId", entityId);
            parameters.Add("paymentType", "RV");

            string resourceUrl = "/v1/payments/" + referencedPaymentId;

            var result = ExecutePOST<PlanetPaymentResponseDTO>(baseUrl, resourceUrl, parameters);            

            return result;

        }

        public IRestResponse<PlanetPaymentResponseDTO> Refund(string referencedPaymentId, string currency, string amount)
        {
            parameters = new Dictionary<string, string>();
            parameters.Add("authentication.userId", userId);
            parameters.Add("authentication.password", password);
            parameters.Add("authentication.entityId", entityId);
            parameters.Add("amount", amount);
            parameters.Add("currency", currency);
            parameters.Add("paymentType", "RF");

            string resourceUrl = "/v1/payments/" + referencedPaymentId;

            var result = ExecutePOST<PlanetPaymentResponseDTO>(baseUrl, resourceUrl, parameters);
            
            return result;
        }

        private IRestResponse<T> ExecutePOST<T>(string url, string resource, Dictionary<string, string> parameters) where T : new()
        {
            var client = new RestClient(url);
            var request = new RestRequest(resource, Method.POST);

            log.Debug("request");
            log.Debug(url + resource);

            foreach (var item in parameters)
            {
                request.AddParameter(item.Key, item.Value);
                log.Debug(item.Key + ": " + item.Value);
            }

            var response = client.Execute<T>(request);

            log.Debug("response");
            log.Debug(response.Content);

            return response;

        }

    }



}