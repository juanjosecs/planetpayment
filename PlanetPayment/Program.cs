using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetPayment
{
    class Program
    {
        static void Main(string[] args)
        {
            TestRegistration();

            TestCharge();

            TestReverse();

            TestRefund();

            Console.ReadKey();

        }

        private static void TestRegistration()
        {
            PlanetPaymentService planetPayment = new PlanetPaymentService();
            var responseRegistration = planetPayment.Registration("4711100000000000", "TEST NAME", "VISA", "12", "2022", "111");
            Console.WriteLine("Test Registration");
            Console.WriteLine("Success: {0}", responseRegistration.Success);
            Console.WriteLine("Message: {0}", responseRegistration.Message);
            Console.WriteLine("Token:   {0}", responseRegistration.PaymentId);
            Console.WriteLine("");
        }


        private static void TestCharge()
        {
            PlanetPaymentService planetPayment = new PlanetPaymentService();
            var responseRegistration = planetPayment.Registration("4711100000000000", "TEST NAME", "VISA", "12", "2022", "111");
            Console.WriteLine("Test Charge");            

            if (responseRegistration.Success)
            {
                var responseCharge = planetPayment.Charge(responseRegistration.PaymentId, "USD", "1.99");
                Console.WriteLine("Success: {0}", responseCharge.Success);
                Console.WriteLine("Message: {0}", responseCharge.Message);
                Console.WriteLine("PaymentId: {0}", responseCharge.PaymentId);
            }
            else
            {
                Console.WriteLine("Token registration is not success");
            }

            Console.WriteLine("");
        }


        private static void TestReverse()
        {

            PlanetPaymentService planetPayment = new PlanetPaymentService();
            var responseRegistration = planetPayment.Registration("4711100000000000", "TEST NAME", "VISA", "12", "2022", "111");
            Console.WriteLine("Test Reverse");
            
            if (responseRegistration.Success)
            {
                var responseCharge = planetPayment.Charge(responseRegistration.PaymentId, "USD", "1.99");               

                if (responseCharge.Success)
                {
                    var responseReverse = planetPayment.Reverse(responseCharge.PaymentId);
                    Console.WriteLine("Success: {0}", responseReverse.Success);
                    Console.WriteLine("Message: {0}", responseReverse.Message);
                    Console.WriteLine("PaymentId: {0}", responseReverse.PaymentId);
                }
                else
                {
                    Console.WriteLine("Charge is not success");
                }
            }
            else
            {
                Console.WriteLine("Token registration is not success");
            }

            Console.WriteLine("");

        }



        private static void TestRefund()
        {

            PlanetPaymentService planetPayment = new PlanetPaymentService();
            var responseRegistration = planetPayment.Registration("4711100000000000", "TEST NAME", "VISA", "12", "2022", "111");
            Console.WriteLine("Test Refund");

            if (responseRegistration.Success)
            {
                var responseCharge = planetPayment.Charge(responseRegistration.PaymentId, "USD", "1.99");

                if (responseCharge.Success)
                {
                    var responseReverse = planetPayment.Refund(responseCharge.PaymentId, "USD", "0.99");
                    Console.WriteLine("Success: {0}", responseReverse.Success);
                    Console.WriteLine("Message: {0}", responseReverse.Message);
                    Console.WriteLine("PaymentId: {0}", responseReverse.PaymentId);
                }
                else
                {
                    Console.WriteLine("Charge is not success");
                }
            }
            else
            {
                Console.WriteLine("Token registration is not success");
            }

            Console.WriteLine("");

        }



    }
}
